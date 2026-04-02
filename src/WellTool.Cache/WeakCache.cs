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
using System.Runtime.CompilerServices;

namespace WellTool.Cache
{
    /// <summary>
    /// 弱引用缓存<br>
    /// 对于一个给定的键，其映射的存在并不阻止垃圾回收器对该键的丢弃，这就使该键成为可终止的，被终止，然后被回收。<br>
    /// 丢弃某个键时，其条目从映射中有效地移除。<br>
    /// </summary>
    /// <typeparam name="K">键</typeparam>
    /// <typeparam name="V">值</typeparam>
    public class WeakCache<K, V> : TimedCache<K, V> where K : class
    {
        /// <summary>
        /// 弱引用字典，用于存储键值对
        /// </summary>
        private readonly ConditionalWeakTable<K, CacheObj<K, V>> weakCache = new ConditionalWeakTable<K, CacheObj<K, V>>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="timeout">超时时常，单位毫秒，-1或0表示无限制</param>
        public WeakCache(long timeout) : base(timeout)
        {}

        /// <summary>
        /// 加入元素，无锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时长</param>
        protected override void PutWithoutLock(K key, V value, long timeout)
        {
            var cacheObj = new CacheObj<K, V>(key, value, timeout);
            weakCache.AddOrUpdate(key, cacheObj);
        }

        /// <summary>
        /// 获取键对应的 CacheObj
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>CacheObj</returns>
        protected override CacheObj<K, V> GetWithoutLock(K key)
        {
            if (weakCache.TryGetValue(key, out var cacheObj))
            {
                if (!cacheObj.IsExpired())
                {
                    return cacheObj;
                }
                else
                {
                    // 过期对象移除
                    weakCache.Remove(key);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                }
            }
            return null;
        }

        /// <summary>
        /// 移除key对应的对象，不加锁
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除的对象，无返回null</returns>
        protected override CacheObj<K, V> RemoveWithoutLock(K key)
        {
            if (weakCache.TryGetValue(key, out var cacheObj))
            {
                weakCache.Remove(key);
                return cacheObj;
            }
            return null;
        }

        /// <summary>
        /// 清理过期对象
        /// </summary>
        /// <returns>清理数</returns>
        protected override int PruneCache()
        {
            // 由于使用的是 ConditionalWeakTable，过期的对象会在键被垃圾回收时自动移除
            // 这里我们只需要清理明确过期的对象
            int count = 0;
            var keysToRemove = new List<K>();

            // 遍历所有键值对，清理过期对象
            foreach (var entry in weakCache)
            {
                var cacheObj = entry.Value;
                if (cacheObj.IsExpired())
                {
                    keysToRemove.Add(entry.Key);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                    count++;
                }
            }

            // 移除过期对象
            foreach (var key in keysToRemove)
            {
                weakCache.Remove(key);
            }

            return count;
        }

        /// <summary>
        /// 缓存的对象数量
        /// </summary>
        /// <returns>缓存的对象数量</returns>
        public override int Size()
        {
            // 由于 ConditionalWeakTable 没有提供 Count 属性，我们需要遍历计数
            int count = 0;
            foreach (var _ in weakCache)
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            // 由于 ConditionalWeakTable 没有提供 Clear 方法，我们需要遍历移除所有键
            var keysToRemove = new List<K>();
            foreach (var entry in weakCache)
            {
                keysToRemove.Add(entry.Key);
            }

            foreach (var key in keysToRemove)
            {
                weakCache.Remove(key);
            }

            base.Clear();
        }

        /// <summary>
        /// 缓存是否为空
        /// </summary>
        /// <returns>缓存是否为空</returns>
        public override bool IsEmpty()
        {
            return Size() == 0;
        }

        /// <summary>
        /// 返回包含键和值得迭代器
        /// </summary>
        /// <returns>缓存对象迭代器</returns>
        public override System.Collections.Generic.IEnumerator<CacheObj<K, V>> CacheObjIterator()
        {
            var cacheObjs = new List<CacheObj<K, V>>();
            foreach (var entry in weakCache)
            {
                var cacheObj = entry.Value;
                if (!cacheObj.IsExpired())
                {
                    cacheObjs.Add(cacheObj);
                }
            }
            return cacheObjs.GetEnumerator();
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>一个可用于循环访问集合的枚举器</returns>
        public override System.Collections.Generic.IEnumerator<V> GetEnumerator()
        {
            var values = new List<V>();
            foreach (var entry in weakCache)
            {
                var cacheObj = entry.Value;
                if (!cacheObj.IsExpired())
                {
                    values.Add(cacheObj.Value);
                }
            }
            return values.GetEnumerator();
        }
    }
}
