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

using System.Collections.Generic;
using System.Threading;

namespace WellTool.Cache
{
    /// <summary>
    /// LFU(least frequently used) 最少使用率缓存<br>
    /// 根据使用次数来判定对象是否被持续缓存<br>
    /// 使用率是通过访问次数计算的。<br>
    /// 当缓存满时清理过期对象。<br>
    /// 清理后依旧满的情况下清除最少访问（访问计数最小）的对象并将其他对象的访问数减去这个最小访问数，以便新对象进入后可以公平计数。
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class LFUCache<K, V> : ReentrantCache<K, V> where K : notnull
    {
        /// <summary>
        /// 访问计数映射
        /// </summary>
        private readonly Dictionary<K, int> accessCountMap = new Dictionary<K, int>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">容量</param>
        public LFUCache(int capacity) : this(capacity, 0)
        {}

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">容量</param>
        /// <param name="timeout">过期时长</param>
        public LFUCache(int capacity, long timeout) : base(capacity, timeout)
        {}

        /// <summary>
        /// 加入元素，无锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时长</param>
        protected override void PutWithoutLock(K key, V value, long timeout)
        {
            // 如果键已存在，保持原来的访问计数
            if (!accessCountMap.ContainsKey(key))
            {
                // 新元素初始访问计数为1
                accessCountMap[key] = 1;
            }
            
            base.PutWithoutLock(key, value, timeout);
        }

        /// <summary>
        /// 获取键对应的 CacheObj
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>CacheObj</returns>
        protected override CacheObj<K, V> GetWithoutLock(K key)
        {
            var cacheObj = base.GetWithoutLock(key);
            if (cacheObj != null)
            {
                // 增加访问计数
                if (accessCountMap.TryGetValue(key, out var count))
                {
                    accessCountMap[key] = count + 1;
                }
                else
                {
                    accessCountMap[key] = 1;
                }
            }
            return cacheObj;
        }

        /// <summary>
        /// 移除key对应的对象，不加锁
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除的对象，无返回null</returns>
        protected override CacheObj<K, V> RemoveWithoutLock(K key)
        {
            accessCountMap.Remove(key);
            return base.RemoveWithoutLock(key);
        }

        /// <summary>
        /// 清理过期对象。<br>
        /// 清理后依旧满的情况下清除最少访问（访问计数最小）的对象并将其他对象的访问数减去这个最小访问数，以便新对象进入后可以公平计数。
        /// </summary>
        /// <returns>清理的缓存对象个数</returns>
        protected override int PruneCache()
        {
            int count = 0;
            var keysToRemove = new List<K>();
            int minAccessCount = int.MaxValue;

            // 清理过期对象并找出访问最少的对象
            foreach (var entry in CacheMap)
            {
                var cacheObj = entry.Value;
                if (cacheObj.IsExpired())
                {
                    keysToRemove.Add(entry.Key);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                    count++;
                    continue;
                }

                // 找出访问最少的对象
                if (accessCountMap.TryGetValue(entry.Key, out var currentCount))
                {
                    if (currentCount < minAccessCount)
                    {
                        minAccessCount = currentCount;
                    }
                }
            }

            // 移除过期对象
            foreach (var key in keysToRemove)
            {
                accessCountMap.Remove(key);
                CacheMap.Remove(key);
            }

            // 清理结束后依旧满的情况下，清除最少访问的对象
            if (IsFull() && minAccessCount < int.MaxValue)
            {
                // 找到所有访问计数等于最小访问计数的键
                var keysWithMinCount = new List<K>();
                foreach (var entry in accessCountMap)
                {
                    if (entry.Value == minAccessCount)
                    {
                        keysWithMinCount.Add(entry.Key);
                    }
                }

                // 移除这些键
                foreach (var key in keysWithMinCount)
                {
                    var cacheObj = RemoveWithoutLock(key);
                    if (cacheObj != null)
                    {
                        OnRemove(cacheObj.Key, cacheObj.Value);
                        count++;
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            accessCountMap.Clear();
            base.Clear();
        }
    }
}
