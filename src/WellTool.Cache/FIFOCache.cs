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
    /// FIFO(first in first out) 先进先出缓存
    /// <para>
    /// 元素不停的加入缓存直到缓存满为止，当缓存满时，清理过期缓存对象，清理后依旧满则删除先入的缓存（链表首部对象）<br/>
    /// 优点：简单快速 <br/>
    /// 缺点：不灵活，不能保证最常用的对象总是被保留
    /// </para>
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class FIFOCache<K, V> : ReentrantCache<K, V>
    {
        /// <summary>
        /// 元素添加顺序链表
        /// </summary>
        private readonly LinkedList<K> keyList = new LinkedList<K>();

        /// <summary>
        /// 构造，默认对象不过期
        /// </summary>
        /// <param name="capacity">容量</param>
        public FIFOCache(int capacity) : this(capacity, 0)
        {}

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">容量</param>
        /// <param name="timeout">过期时长</param>
        public FIFOCache(int capacity, long timeout) : base(capacity, timeout)
        {}

        /// <summary>
        /// 加入元素，无锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时长</param>
        protected override void PutWithoutLock(K key, V value, long timeout)
        {
            // 如果键已存在，先从链表中移除
            keyList.Remove(key);
            // 将新键加入链表尾部
            keyList.AddLast(key);
            
            base.PutWithoutLock(key, value, timeout);
        }

        /// <summary>
        /// 移除key对应的对象，不加锁
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除的对象，无返回null</returns>
        protected override CacheObj<K, V> RemoveWithoutLock(K key)
        {
            keyList.Remove(key);
            return base.RemoveWithoutLock(key);
        }

        /// <summary>
        /// 先进先出的清理策略<br>
        /// 先遍历缓存清理过期的缓存对象，如果清理后还是满的，则删除第一个缓存对象
        /// </summary>
        /// <returns>清理的缓存对象个数</returns>
        protected override int PruneCache()
        {
            int count = 0;

            // 清理过期对象
            var keysToRemove = new List<K>();
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
                keyList.Remove(key);
                var cacheObj = CacheMap[key];
                CacheMap.Remove(key);
                OnRemove(cacheObj.Key, cacheObj.Value);
            }

            // 清理结束后依旧是满的，则删除第一个被缓存的对象
            while (IsFull() && keyList.Count > 0)
            {
                var firstKey = keyList.First.Value;
                keyList.RemoveFirst();
                if (CacheMap.TryGetValue(firstKey, out var cacheObj))
                {
                    CacheMap.Remove(firstKey);
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
            keyList.Clear();
            base.Clear();
        }
    }
}
