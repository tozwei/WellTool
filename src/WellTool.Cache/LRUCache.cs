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

namespace WellTool.Cache
{
    /// <summary>
    /// LRU (least recently used)最近最久未使用缓存<br>
    /// 根据使用时间来判定对象是否被持续缓存<br>
    /// 当对象被访问时放入缓存，当缓存满了，最久未被使用的对象将被移除。<br>
    /// 此缓存基于 LinkedHashMap，因此当被缓存的对象每被访问一次，这个对象的key就到链表头部。<br>
    /// 这个算法简单并且非常快，他比FIFO有一个显著优势是经常使用的对象不太可能被移除缓存。<br>
    /// 缺点是当缓存满时，不能被很快的访问。
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class LRUCache<K, V> : ReentrantCache<K, V>
    {
        /// <summary>
        /// 访问顺序链表，用于记录访问顺序
        /// </summary>
        private readonly LinkedList<K> accessOrderList = new LinkedList<K>();

        /// <summary>
        /// 构造<br>
        /// 默认无超时
        /// </summary>
        /// <param name="capacity">容量</param>
        public LRUCache(int capacity) : this(capacity, 0)
        {}

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">容量</param>
        /// <param name="timeout">默认超时时间，单位：毫秒</param>
        public LRUCache(int capacity, long timeout) : base(capacity, timeout)
        {}

        /// <summary>
        /// 加入元素，无锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时长</param>
        protected override void PutWithoutLock(K key, V value, long timeout)
        {
            // 如果键已存在，先移除旧的访问记录
            accessOrderList.Remove(key);
            // 将新键添加到链表头部
            accessOrderList.AddFirst(key);
            
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
                // 将访问的键移到链表头部
                accessOrderList.Remove(key);
                accessOrderList.AddFirst(key);
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
            accessOrderList.Remove(key);
            return base.RemoveWithoutLock(key);
        }

        /// <summary>
        /// 只清理超时对象，LRU的实现会交给访问顺序链表
        /// </summary>
        /// <returns>清理的缓存对象个数</returns>
        protected override int PruneCache()
        {
            int count = 0;
            var keysToRemove = new List<K>();

            // 清理过期对象
            foreach (var entry in CacheMap)
            {
                var cacheObj = entry.Value;
                if (cacheObj.IsExpired())
                {
                    keysToRemove.Add(entry.Key);
                    count++;
                }
            }

            // 移除过期对象
            foreach (var key in keysToRemove)
            {
                accessOrderList.Remove(key);
                var cacheObj = CacheMap[key];
                CacheMap.Remove(key);
                OnRemove(cacheObj.Key, cacheObj.Value);
            }

            // 如果缓存仍然满，移除最久未使用的对象
            while (IsFull() && accessOrderList.Count > 0)
            {
                var leastUsedKey = accessOrderList.Last.Value;
                accessOrderList.RemoveLast();
                if (CacheMap.TryGetValue(leastUsedKey, out var cacheObj))
                {
                    CacheMap.Remove(leastUsedKey);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            accessOrderList.Clear();
            base.Clear();
        }
    }
}
