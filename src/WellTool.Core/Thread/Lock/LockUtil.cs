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

using System.Threading;

namespace WellTool.Core.Threading.Lock
{
    /// <summary>
    /// 锁相关工具
    /// </summary>
    public static class LockUtil
    {
        private static readonly NoLock NoLockInstance = new NoLock();

        /// <summary>
        /// 创建 ReaderWriterLockSlim 锁
        /// </summary>
        /// <param name="useSlim">是否使用 ReaderWriterLockSlim，否则使用 ReaderWriterLock</param>
        /// <returns>ReaderWriterLockSlim 锁</returns>
        public static ReaderWriterLockSlim CreateReadWriteLock(bool useSlim = true)
        {
            return new ReaderWriterLockSlim();
        }

        /// <summary>
        /// 获取单例的无锁对象
        /// </summary>
        /// <returns>NoLock</returns>
        public static NoLock GetNoLock()
        {
            return NoLockInstance;
        }

        /// <summary>
        /// 创建分段锁（强引用），使用 Monitor
        /// </summary>
        /// <param name="segments">分段数量，必须大于 0</param>
        /// <returns>分段锁实例</returns>
        public static SegmentLock<object> CreateSegmentLock(int segments)
        {
            return SegmentLock<object>.Create(segments, () => new object());
        }

        /// <summary>
        /// 创建分段读写锁（强引用），使用 ReaderWriterLockSlim
        /// </summary>
        /// <param name="segments">分段数量，必须大于 0</param>
        /// <returns>分段读写锁实例</returns>
        public static SegmentLock<ReaderWriterLockSlim> CreateSegmentReadWriteLock(int segments)
        {
            return SegmentLock<ReaderWriterLockSlim>.Create(segments, () => new ReaderWriterLockSlim());
        }

        /// <summary>
        /// 创建分段信号量（强引用）
        /// </summary>
        /// <param name="segments">分段数量，必须大于 0</param>
        /// <param name="permits">每个信号量的许可数</param>
        /// <returns>分段信号量实例</returns>
        public static SegmentLock<Semaphore> CreateSegmentSemaphore(int segments, int permits)
        {
            return SegmentLock<Semaphore>.Create(segments, () => new Semaphore(permits, permits));
        }

        /// <summary>
        /// 根据 key 获取分段锁（强引用）
        /// </summary>
        /// <param name="segments">分段数量，必须大于 0</param>
        /// <param name="key">用于映射分段的 key</param>
        /// <returns>对应的锁对象</returns>
        public static object GetSegmentLock(int segments, object key)
        {
            return CreateSegmentLock(segments).Get(key);
        }

        /// <summary>
        /// 根据 key 获取分段读锁（强引用）
        /// </summary>
        /// <param name="segments">分段数量，必须大于 0</param>
        /// <param name="key">用于映射分段的 key</param>
        /// <returns>对应的读锁对象</returns>
        public static ReaderWriterLockSlim GetSegmentReadWriteLock(int segments, object key)
        {
            return CreateSegmentReadWriteLock(segments).Get(key);
        }

        /// <summary>
        /// 根据 key 获取分段信号量（强引用）
        /// </summary>
        /// <param name="segments">分段数量，必须大于 0</param>
        /// <param name="permits">每个信号量的许可数</param>
        /// <param name="key">用于映射分段的 key</param>
        /// <returns>对应的 Semaphore 实例</returns>
        public static Semaphore GetSegmentSemaphore(int segments, int permits, object key)
        {
            return CreateSegmentSemaphore(segments, permits).Get(key);
        }
    }
}