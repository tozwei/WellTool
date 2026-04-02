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

namespace WellTool.Cache
{
    /// <summary>
    /// 缓存工具类，用于创建不同类型的缓存实例
    /// </summary>
    public class CacheUtil
    {
        /// <summary>
        /// 创建 FIFO 缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">容量</param>
        /// <returns>FIFO 缓存</returns>
        public static FIFOCache<K, V> NewFIFOCache<K, V>(int capacity)
        {
            return new FIFOCache<K, V>(capacity);
        }

        /// <summary>
        /// 创建 FIFO 缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">容量</param>
        /// <param name="timeout">过期时长</param>
        /// <returns>FIFO 缓存</returns>
        public static FIFOCache<K, V> NewFIFOCache<K, V>(int capacity, long timeout)
        {
            return new FIFOCache<K, V>(capacity, timeout);
        }

        /// <summary>
        /// 创建 LRU 缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">容量</param>
        /// <returns>LRU 缓存</returns>
        public static LRUCache<K, V> NewLRUCache<K, V>(int capacity)
        {
            return new LRUCache<K, V>(capacity);
        }

        /// <summary>
        /// 创建 LRU 缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">容量</param>
        /// <param name="timeout">过期时长</param>
        /// <returns>LRU 缓存</returns>
        public static LRUCache<K, V> NewLRUCache<K, V>(int capacity, long timeout)
        {
            return new LRUCache<K, V>(capacity, timeout);
        }

        /// <summary>
        /// 创建 LFU 缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">容量</param>
        /// <returns>LFU 缓存</returns>
        public static LFUCache<K, V> NewLFUCache<K, V>(int capacity)
        {
            return new LFUCache<K, V>(capacity);
        }

        /// <summary>
        /// 创建 LFU 缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="capacity">容量</param>
        /// <param name="timeout">过期时长</param>
        /// <returns>LFU 缓存</returns>
        public static LFUCache<K, V> NewLFUCache<K, V>(int capacity, long timeout)
        {
            return new LFUCache<K, V>(capacity, timeout);
        }

        /// <summary>
        /// 创建定时缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="timeout">过期时长</param>
        /// <returns>定时缓存</returns>
        public static TimedCache<K, V> NewTimedCache<K, V>(long timeout)
        {
            return new TimedCache<K, V>(timeout);
        }

        /// <summary>
        /// 创建弱引用缓存
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <param name="timeout">过期时长</param>
        /// <returns>弱引用缓存</returns>
        public static WeakCache<K, V> NewWeakCache<K, V>(long timeout) where K : class
        {
            return new WeakCache<K, V>(timeout);
        }

        /// <summary>
        /// 创建无缓存实现
        /// </summary>
        /// <typeparam name="K">键类型</typeparam>
        /// <typeparam name="V">值类型</typeparam>
        /// <returns>无缓存实现</returns>
        public static NoCache<K, V> NewNoCache<K, V>()
        {
            return new NoCache<K, V>();
        }
    }
}
