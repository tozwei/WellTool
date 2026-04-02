// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Thread.Lock
{
    /// <summary>
    /// 分段锁工具类，支持对象锁、信号量的分段实现。
    /// <p>
    /// 通过将锁分成多个段（segments），不同的操作可以并发使用不同的段，避免所有线程竞争同一把锁。
    /// 相等的 key 保证映射到同一段锁（如 key1.Equals(key2) 时，Get(key1) 和 Get(key2) 返回相同对象）。
    /// 但不同 key 可能因哈希冲突映射到同一段，段数越少冲突概率越高。
    /// <p>
    /// 支持两种实现：
    /// <ul>
    ///     <li>强引用：创建时初始化所有段，内存占用稳定。</li>
    ///     <li>弱引用：懒加载，首次使用时创建段，未使用时可被垃圾回收，适合大量段但使用较少的场景。</li>
    /// </ul>
    /// </summary>
    /// <typeparam name="L">锁类型</typeparam>
    public abstract class SegmentLock<L>
    {
        /// <summary>
        /// 当段数大于此阈值时，使用 ConcurrentDictionary 替代大数组以节省内存（适用于懒加载场景）
        /// </summary>
        private const int LargeLazyCutoff = 1024;

        /// <summary>
        /// 根据 key 获取对应的锁段，保证相同 key 返回相同对象。
        /// </summary>
        /// <param name="key">非空 key</param>
        /// <returns>对应的锁段</returns>
        public abstract L Get(object key);

        /// <summary>
        /// 根据索引获取锁段，索引范围为 [0, Size())。
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>指定索引的锁段</returns>
        public abstract L GetAt(int index);

        /// <summary>
        /// 计算 key 对应的段索引。
        /// </summary>
        /// <param name="key">非空 key</param>
        /// <returns>段索引</returns>
        protected abstract int IndexFor(object key);

        /// <summary>
        /// 获取总段数。
        /// </summary>
        /// <returns>段数</returns>
        public abstract int Size();

        /// <summary>
        /// 批量获取多个 key 对应的锁段列表，按索引升序排列，避免死锁。
        /// </summary>
        /// <param name="keys">非空 key 集合</param>
        /// <returns>锁段列表（可能有重复）</returns>
        public IEnumerable<L> BulkGet(IEnumerable<object> keys)
        {
            var keyList = keys.ToList();
            if (keyList.Count == 0)
            {
                return Enumerable.Empty<L>();
            }

            var stripes = new int[keyList.Count];
            for (int i = 0; i < keyList.Count; i++)
            {
                stripes[i] = IndexFor(keyList[i]);
            }

            Array.Sort(stripes);
            var result = new List<L>(keyList.Count);
            int previousStripe = stripes[0];
            result.Add(GetAt(previousStripe));

            for (int i = 1; i < keyList.Count; i++)
            {
                int currentStripe = stripes[i];
                if (currentStripe == previousStripe)
                {
                    result.Add(result[i - 1]);
                }
                else
                {
                    result.Add(GetAt(currentStripe));
                    previousStripe = currentStripe;
                }
            }

            return new ReadOnlyCollection<L>(result);
        }

        /// <summary>
        /// 创建强引用的分段锁，所有段在创建时初始化。
        /// </summary>
        /// <param name="stripes">段数</param>
        /// <param name="supplier">锁提供者</param>
        /// <returns>分段锁实例</returns>
        public static SegmentLock<L> Create<L>(int stripes, Func<L> supplier)
        {
            return new CompactSegmentLock<L>(stripes, supplier);
        }

        /// <summary>
        /// 抽象基类，确保段数为 2 的幂。
        /// </summary>
        private abstract class PowerOfTwoSegmentLock<L> : SegmentLock<L>
        {
            protected readonly int Mask;

            protected PowerOfTwoSegmentLock(int stripes)
            {
                if (stripes <= 0)
                {
                    throw new ArgumentException("Segment count must be positive");
                }

                Mask = stripes > int.MaxValue / 2 ? ~0 : CeilToPowerOfTwo(stripes) - 1;
            }

            protected override int IndexFor(object key)
            {
                int hash = Smear(key.GetHashCode());
                return hash & Mask;
            }

            public override L Get(object key)
            {
                return GetAt(IndexFor(key));
            }

            private static int CeilToPowerOfTwo(int x)
            {
                return 1 << (32 - NumberOfLeadingZeros(x - 1));
            }

            private static int NumberOfLeadingZeros(int x)
            {
                if (x == 0)
                    return 32;

                int n = 1;
                if (x >> 16 == 0) { n += 16; x <<= 16; }
                if (x >> 24 == 0) { n += 8; x <<= 8; }
                if (x >> 28 == 0) { n += 4; x <<= 4; }
                if (x >> 30 == 0) { n += 2; x <<= 2; }
                n -= x >> 31;
                return n;
            }

            private static int Smear(int hashCode)
            {
                hashCode ^= (hashCode >> 20) ^ (hashCode >> 12);
                return hashCode ^ (hashCode >> 7) ^ (hashCode >> 4);
            }
        }

        /// <summary>
        /// 强引用实现，使用固定数组存储段。
        /// </summary>
        private class CompactSegmentLock<L> : PowerOfTwoSegmentLock<L>
        {
            private readonly object[] _array;

            public CompactSegmentLock(int stripes, Func<L> supplier) : base(stripes)
            {
                if (stripes > int.MaxValue / 2)
                {
                    throw new ArgumentException("Segment count must be <= 2^30");
                }

                _array = new object[Mask + 1];
                for (int i = 0; i < _array.Length; i++)
                {
                    _array[i] = supplier();
                }
            }

            public override L GetAt(int index)
            {
                if (index < 0 || index >= _array.Length)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of bounds for size {_array.Length}");
                }

                return (L)_array[index];
            }

            public override int Size()
            {
                return _array.Length;
            }
        }

        /// <summary>
        /// 小规模弱引用实现，使用 ConcurrentDictionary 存储段。
        /// </summary>
        private class SmallLazySegmentLock<L> : PowerOfTwoSegmentLock<L>
        {
            private readonly ConcurrentDictionary<int, L> _locks;
            private readonly Func<L> _supplier;
            private readonly int _size;

            public SmallLazySegmentLock(int stripes, Func<L> supplier) : base(stripes)
            {
                _size = (Mask == ~0) ? int.MaxValue : Mask + 1;
                _locks = new ConcurrentDictionary<int, L>();
                _supplier = supplier;
            }

            public override L GetAt(int index)
            {
                if (_size != int.MaxValue)
                {
                    if (index < 0 || index >= _size)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), "Index out of bounds");
                    }
                }

                return _locks.GetOrAdd(index, _ => _supplier());
            }

            public override int Size()
            {
                return _size;
            }
        }

        /// <summary>
        /// 大规模弱引用实现，使用 ConcurrentDictionary 存储段。
        /// </summary>
        private class LargeLazySegmentLock<L> : PowerOfTwoSegmentLock<L>
        {
            private readonly ConcurrentDictionary<int, L> _locks;
            private readonly Func<L> _supplier;
            private readonly int _size;

            public LargeLazySegmentLock(int stripes, Func<L> supplier) : base(stripes)
            {
                _size = (Mask == ~0) ? int.MaxValue : Mask + 1;
                _locks = new ConcurrentDictionary<int, L>();
                _supplier = supplier;
            }

            public override L GetAt(int index)
            {
                if (_size != int.MaxValue)
                {
                    if (index < 0 || index >= _size)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), "Index out of bounds");
                    }
                }

                return _locks.GetOrAdd(index, _ => _supplier());
            }

            public override int Size()
            {
                return _size;
            }
        }
    }

    /// <summary>
    /// 分段锁工具类
    /// </summary>
    public static class SegmentLock
    {
        /// <summary>
        /// 创建强引用的分段锁，使用 object 作为锁对象。
        /// </summary>
        /// <param name="stripes">段数</param>
        /// <returns>分段锁实例</returns>
        public static SegmentLock<object> Create(int stripes)
        {
            return SegmentLock<object>.Create(stripes, () => new object());
        }

        /// <summary>
        /// 创建强引用的分段锁，使用指定的锁提供者。
        /// </summary>
        /// <typeparam name="L">锁类型</typeparam>
        /// <param name="stripes">段数</param>
        /// <param name="supplier">锁提供者</param>
        /// <returns>分段锁实例</returns>
        public static SegmentLock<L> Create<L>(int stripes, Func<L> supplier)
        {
            return SegmentLock<L>.Create(stripes, supplier);
        }
    }
}