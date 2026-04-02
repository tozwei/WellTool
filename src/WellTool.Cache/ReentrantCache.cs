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
using System.Linq;
using System.Threading;

namespace WellTool.Cache
{
    /// <summary>
    /// 使用 ReentrantLock 保护的缓存，读写都使用悲观锁完成，主要避免某些 Map 无法使用读写锁的问题
    /// 例如使用了 LinkedHashMap 的缓存，由于 get 方法也会改变 Map 的结构，因此读写必须加互斥锁
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public abstract class ReentrantCache<K, V> : AbstractCache<K, V> where K : notnull
    {
        /// <summary>
        /// 可重入锁
        /// </summary>
        protected readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 缓存映射
        /// </summary>
        protected readonly Dictionary<K, CacheObj<K, V>> CacheMap = new Dictionary<K, CacheObj<K, V>>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">缓存容量，0表示无大小限制</param>
        /// <param name="timeout">缓存失效时长，0表示没有设置，单位毫秒</param>
        protected ReentrantCache(int capacity, long timeout)
            : base(capacity, timeout)
        {}

        /// <summary>
        /// 将对象加入到缓存，使用指定失效时长
        /// 如果缓存空间满了，<see cref="Prune()"/> 将被调用以获得空间来存放新对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="timeout">失效时长，单位毫秒</param>
        public override void Put(K key, V value, long timeout)
        {
            Lock.EnterWriteLock();
            try
            {
                PutWithoutLock(key, value, timeout);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 加入元素，无锁
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时长</param>
        protected virtual void PutWithoutLock(K key, V value, long timeout)
        {
            var cacheObj = new CacheObj<K, V>(key, value, timeout);

            // 检查是否存在相同的键
            if (CacheMap.TryGetValue(key, out var oldObj))
            {
                OnRemove(oldObj.Key, oldObj.Value);
                // 存在相同key，覆盖之
                CacheMap[key] = cacheObj;
            }
            else
            {
                // 确保缓存有空间
                while (IsFull())
                {
                    int prunedCount = PruneCache();
                    // 如果没有清理任何元素，说明无法清理更多元素，直接返回（不添加）
                    if (prunedCount == 0)
                    {
                        return;
                    }
                }
                // 添加新元素
                CacheMap[key] = cacheObj;
            }
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回null
        /// 调用此方法时，会检查上次调用时间，如果与当前时间差值大于超时时间返回null，否则返回值。
        /// 每次调用此方法会可选是否刷新最后访问时间，true表示会重新计算超时时间。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间，即重新计算超时时间。</param>
        /// <returns>键对应的对象</returns>
        public override V Get(K key, bool isUpdateLastAccess)
        {
            Lock.EnterWriteLock();
            try
            {
                var cacheObj = GetWithoutLock(key);
                if (cacheObj != null && cacheObj.IsExpired())
                {
                    RemoveWithoutLock(key);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                    return default;
                }
                
                if (cacheObj != null && isUpdateLastAccess)
                {
                    cacheObj.UpdateLastAccess();
                }
                
                return cacheObj != null ? cacheObj.Value : default;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回supplier回调产生的对象
        /// 调用此方法时，会检查上次调用时间，如果与当前时间差值大于超时时间返回supplier回调产生的对象，否则返回值。
        /// 每次调用此方法会可选是否刷新最后访问时间，true表示会重新计算超时时间。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间，即重新计算超时时间。</param>
        /// <param name="timeout">自定义超时时间</param>
        /// <param name="supplier">如果不存在回调方法，用于生产值对象</param>
        /// <returns>值对象</returns>
        public override V Get(K key, bool isUpdateLastAccess, long timeout, Func<V> supplier)
        {
            var value = Get(key, isUpdateLastAccess);

            // 对象不存在，则加锁创建
            if (value == null && supplier != null)
            {
                Lock.EnterWriteLock();
                try
                {
                    // 双重检查锁，防止在竞争锁的过程中已经有其它线程写入
                    var cacheObj = GetWithoutLock(key);
                    if (cacheObj == null)
                    {
                        value = supplier();
                        PutWithoutLock(key, value, timeout);
                    }
                }
                finally
                {
                    Lock.ExitWriteLock();
                }
            }
            return value;
        }

        /// <summary>
        /// 获得值或清除过期值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间</param>
        /// <param name="isUpdateCount">是否更新计数器</param>
        /// <returns>值或null</returns>
        private V GetOrRemoveExpired(K key, bool isUpdateLastAccess, bool isUpdateCount)
        {
            CacheObj<K, V> cacheObj;
            Lock.EnterReadLock();
            try
            {
                cacheObj = GetWithoutLock(key);
                if (cacheObj != null && cacheObj.IsExpired())
                {
                    // 过期，需要移除
                    Lock.ExitReadLock();
                    Lock.EnterWriteLock();
                    try
                    {
                        // 再次检查，防止在获取写锁的过程中对象状态发生变化
                        var updatedCacheObj = GetWithoutLock(key);
                        if (updatedCacheObj != null && updatedCacheObj.IsExpired())
                        {
                            RemoveWithoutLock(key);
                            OnRemove(updatedCacheObj.Key, updatedCacheObj.Value);
                        }
                    }
                    finally
                    {
                        Lock.ExitWriteLock();
                        Lock.EnterReadLock();
                    }
                    cacheObj = null;
                }
            }
            finally
            {
                Lock.ExitReadLock();
            }

            // 未命中
            if (cacheObj == null)
            {
                return default;
            }

            if (isUpdateLastAccess)
            {
                cacheObj.UpdateLastAccess();
            }

            return cacheObj.Value;
        }

        /// <summary>
        /// 获取键对应的 CacheObj
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>CacheObj</returns>
        protected virtual CacheObj<K, V> GetWithoutLock(K key)
        {
            if (key == null)
            {
                return null;
            }
            CacheMap.TryGetValue(key, out var cacheObj);
            return cacheObj;
        }

        /// <summary>
        /// 返回包含键和值得迭代器
        /// </summary>
        /// <returns>缓存对象迭代器</returns>
        public override IEnumerator<CacheObj<K, V>> CacheObjIterator()
        {
            List<CacheObj<K, V>> cacheObjs;
            Lock.EnterReadLock();
            try
            {
                cacheObjs = new List<CacheObj<K, V>>(CacheMap.Values);
            }
            finally
            {
                Lock.ExitReadLock();
            }
            return cacheObjs.GetEnumerator();
        }

        /// <summary>
        /// 从缓存中清理过期对象，清理策略取决于具体实现
        /// </summary>
        /// <returns>清理的缓存对象个数</returns>
        public override int Prune()
        {
            Lock.EnterWriteLock();
            try
            {
                return PruneCache();
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 清理实现
        /// 子类实现此方法时无需加锁
        /// </summary>
        /// <returns>清理数</returns>
        protected abstract int PruneCache();

        /// <summary>
        /// 从缓存中移除对象
        /// </summary>
        /// <param name="key">键</param>
        public override void Remove(K key)
        {
            CacheObj<K, V> cacheObj;
            Lock.EnterWriteLock();
            try
            {
                cacheObj = RemoveWithoutLock(key);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
            if (cacheObj != null)
            {
                OnRemove(cacheObj.Key, cacheObj.Value);
            }
        }

        /// <summary>
        /// 移除key对应的对象，不加锁
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除的对象，无返回null</returns>
        protected virtual CacheObj<K, V> RemoveWithoutLock(K key)
        {
            if (CacheMap.TryGetValue(key, out var cacheObj))
            {
                CacheMap.Remove(key);
                return cacheObj;
            }
            return null;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            Lock.EnterWriteLock();
            try
            {
                // 获取所有键的副本
                var keys = new List<K>(CacheMap.Keys);
                foreach (var key in keys)
                {
                    var cacheObj = RemoveWithoutLock(key);
                    if (cacheObj != null)
                    {
                        OnRemove(cacheObj.Key, cacheObj.Value); // 触发资源释放
                    }
                }
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 缓存的对象数量
        /// </summary>
        /// <returns>缓存的对象数量</returns>
        public override int Size()
        {
            // 检查是否已经持有写锁
            if (Lock.IsWriteLockHeld)
            {
                // 直接返回，避免锁递归
                return CacheMap.Count;
            }
            
            Lock.EnterReadLock();
            try
            {
                return CacheMap.Count;
            }
            finally
            {
                Lock.ExitReadLock();
            }
        }

        /// <summary>
        /// 是否包含key
        /// </summary>
        /// <param name="key">KEY</param>
        /// <returns>是否包含key</returns>
        public override bool ContainsKey(K key)
        {
            Lock.EnterWriteLock();
            try
            {
                var cacheObj = GetWithoutLock(key);
                if (cacheObj != null && cacheObj.IsExpired())
                {
                    RemoveWithoutLock(key);
                    OnRemove(cacheObj.Key, cacheObj.Value);
                    return false;
                }
                return cacheObj != null;
            }
            finally
            {
                Lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>一个可用于循环访问集合的枚举器</returns>
        public override IEnumerator<V> GetEnumerator()
        {
            List<V> values;
            Lock.EnterReadLock();
            try
            {
                values = CacheMap.Values.Select(co => co.Value).ToList();
            }
            finally
            {
                Lock.ExitReadLock();
            }
            return values.GetEnumerator();
        }

        /// <summary>
        /// 对象移除回调。默认无动作
        /// 子类可重写此方法用于监听移除事件，如果重写，listener将无效
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cachedObject">被缓存的对象</param>
        protected virtual void OnRemove(K key, V cachedObject)
        {
            if (listener != null)
            {
                listener.OnRemove(key, cachedObject);
            }
        }
    }
}
