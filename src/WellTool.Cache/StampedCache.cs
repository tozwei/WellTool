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
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace WellTool.Cache
{
    /// <summary>
    /// 使用乐观读锁（StampedLock模式）保护的缓存，适用于读多写少的场景。
    /// 在.NET中，我们使用ReaderWriterLockSlim的TryEnterReadLock来模拟乐观读。
    /// 
    /// 注意：此类在Hutool中已被标记为deprecated，
    /// 因为使用StampedLock可能导致数据不一致甚至锁循环调用的问题。
    /// 在WellTool中保留此实现以供参考，但在生产环境中请谨慎使用。
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    [Obsolete("此类已被标记为deprecated，使用ReentrantCache可能更安全")]
    public abstract class StampedCache<K, V> : AbstractCache<K, V> where K : notnull
    {
        /// <summary>
        /// 缓存映射
        /// </summary>
        protected readonly Dictionary<K, CacheObj<K, V>> CacheMap = new Dictionary<K, CacheObj<K, V>>();

        /// <summary>
        /// 可重入读写锁
        /// </summary>
        protected readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">缓存容量，0表示无大小限制</param>
        /// <param name="timeout">缓存失效时长，0表示没有设置，单位毫秒</param>
        protected StampedCache(int capacity, long timeout)
            : base(capacity, timeout)
        { }

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
                CacheMap[key] = cacheObj;
            }
            else
            {
                // 确保缓存有空间
                while (IsFull())
                {
                    int prunedCount = PruneCache();
                    if (prunedCount == 0)
                    {
                        return;
                    }
                }
                CacheMap[key] = cacheObj;
            }
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回null
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间</param>
        /// <returns>键对应的对象</returns>
        public override V Get(K key, bool isUpdateLastAccess)
        {
            return Get(key, isUpdateLastAccess, true);
        }

        /// <summary>
        /// 获取值，使用乐观读锁模式
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间</param>
        /// <param name="isUpdateCount">是否更新命中数</param>
        /// <returns>值或null</returns>
        private V Get(K key, bool isUpdateLastAccess, bool isUpdateCount)
        {
            CacheObj<K, V> cacheObj = null;
            bool readSuccess = false;

            // 尝试乐观读
            try
            {
                if (Lock.TryEnterReadLock(0))
                {
                    try
                    {
                        cacheObj = GetWithoutLock(key);
                        readSuccess = true;
                    }
                    finally
                    {
                        Lock.ExitReadLock();
                    }
                }
            }
            catch
            {
                // ignore
            }

            if (!readSuccess)
            {
                // 转为悲观读
                Lock.EnterReadLock();
                try
                {
                    cacheObj = GetWithoutLock(key);
                }
                finally
                {
                    Lock.ExitReadLock();
                }
            }

            // 未命中
            if (cacheObj == null)
            {
                return default;
            }
            else if (!cacheObj.IsExpired())
            {
                if (isUpdateLastAccess)
                {
                    cacheObj.UpdateLastAccess();
                }
                return cacheObj.Value;
            }

            // 悲观锁，二次检查
            return GetOrRemoveExpired(key, isUpdateCount);
        }

        /// <summary>
        /// 同步获取值，如果过期则移除之
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateCount">是否更新命中数</param>
        /// <returns>有效值或null</returns>
        private V GetOrRemoveExpired(K key, bool isUpdateCount)
        {
            CacheObj<K, V> cacheObj;
            Lock.EnterWriteLock();
            try
            {
                cacheObj = GetWithoutLock(key);
                if (cacheObj == null)
                {
                    return default;
                }
                if (!cacheObj.IsExpired())
                {
                    return cacheObj.Value;
                }

                // 无效移除
                RemoveWithoutLock(key);
            }
            finally
            {
                Lock.ExitWriteLock();
            }
            if (cacheObj != null)
            {
                OnRemove(cacheObj.Key, cacheObj.Value);
            }
            return default;
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
            List<CacheObj<K, V>> copiedList;
            Lock.EnterReadLock();
            try
            {
                copiedList = new List<CacheObj<K, V>>(CacheMap.Values);
            }
            finally
            {
                Lock.ExitReadLock();
            }
            return new CacheObjIterator<K, V>(copiedList.GetEnumerator());
        }

        /// <summary>
        /// 从缓存中清理过期对象
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
        /// </summary>
        /// <returns>清理数</returns>
        protected abstract int PruneCache();

        /// <summary>
        /// 从缓存中移除对象
        /// </summary>
        /// <param name="key">键</param>
        public override void Remove(K key)
        {
            Lock.EnterWriteLock();
            try
            {
                var co = RemoveWithoutLock(key);
                if (co != null)
                {
                    OnRemove(co.Key, co.Value);
                }
            }
            finally
            {
                Lock.ExitWriteLock();
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
                CacheMap.Clear();
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
            return Get(key) != null;
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public override IEnumerator<V> GetEnumerator()
        {
            List<V> values;
            Lock.EnterReadLock();
            try
            {
                values = new List<V>();
                foreach (var co in CacheMap.Values)
                {
                    if (!co.IsExpired())
                    {
                        values.Add(co.Value);
                    }
                }
            }
            finally
            {
                Lock.ExitReadLock();
            }
            return values.GetEnumerator();
        }

        /// <summary>
        /// 对象移除回调
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cachedObject">被缓存的对象</param>
        protected void OnRemove(K key, V cachedObject)
        {
            if (listener != null)
            {
                listener.OnRemove(key, cachedObject);
            }
        }
    }
}
