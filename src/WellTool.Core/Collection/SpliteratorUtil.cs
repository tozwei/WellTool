using System;
using System.Collections.Generic;

namespace WellTool.Core.Collection
{
    /// <summary>
    /// Spliterator 工具类
    /// </summary>
    public static class SpliteratorUtil
    {
        /// <summary>
        /// 创建一个数组的 Spliterator
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="array">数组</param>
        /// <returns>Spliterator</returns>
        public static ISpliterator<T> OfArray<T>(params T[] array)
        {
            return new ArraySpliterator<T>(array);
        }

        /// <summary>
        /// 创建一个集合的 Spliterator
        /// </summary>
        /// <typeparam name="T">元素类型</typeparam>
        /// <param name="collection">集合</param>
        /// <returns>Spliterator</returns>
        public static ISpliterator<T> OfCollection<T>(ICollection<T> collection)
        {
            return new CollectionSpliterator<T>(collection);
        }
    }

    /// <summary>
    /// Spliterator 接口
    /// </summary>
    public interface ISpliterator<T>
    {
        /// <summary>
        /// 尝试分割
        /// </summary>
        ISpliterator<T> TrySplit();

        /// <summary>
        /// 估算剩余元素数量
        /// </summary>
        long EstimateSize();

        /// <summary>
        /// 前进
        /// </summary>
        bool TryAdvance(Action<T> action);

        /// <summary>
        /// 遍历剩余元素
        /// </summary>
        void ForEachRemaining(Action<T> action);
    }

    /// <summary>
    /// 数组 Spliterator
    /// </summary>
    internal class ArraySpliterator<T> : ISpliterator<T>
    {
        private readonly T[] _array;
        private int _index;
        private readonly int _fence;

        public ArraySpliterator(T[] array)
        {
            _array = array;
            _index = 0;
            _fence = array?.Length ?? 0;
        }

        private ArraySpliterator(T[] array, int index, int fence)
        {
            _array = array;
            _index = index;
            _fence = fence;
        }

        public long EstimateSize() => _fence - _index;

        public ISpliterator<T> TrySplit()
        {
            int mid = (_fence + _index) >> 1;
            if (_index >= mid)
            {
                return null;
            }
            int start = _index;
            _index = mid;
            return new ArraySpliterator<T>(_array, start, mid);
        }

        public bool TryAdvance(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (_index < _fence)
            {
                action(_array[_index++]);
                return true;
            }
            return false;
        }

        public void ForEachRemaining(Action<T> action)
        {
            while (_index < _fence)
            {
                action(_array[_index++]);
            }
        }
    }

    /// <summary>
    /// 集合 Spliterator
    /// </summary>
    internal class CollectionSpliterator<T> : ISpliterator<T>
    {
        private readonly ICollection<T> _collection;
        private IEnumerator<T> _enumerator;

        public CollectionSpliterator(ICollection<T> collection)
        {
            _collection = collection;
            _enumerator = collection.GetEnumerator();
        }

        public long EstimateSize() => _collection.Count;

        public ISpliterator<T> TrySplit()
        {
            return null; // 不支持分割
        }

        public bool TryAdvance(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            if (_enumerator.MoveNext())
            {
                action(_enumerator.Current);
                return true;
            }
            return false;
        }

        public void ForEachRemaining(Action<T> action)
        {
            while (_enumerator.MoveNext())
            {
                action(_enumerator.Current);
            }
        }
    }
}
