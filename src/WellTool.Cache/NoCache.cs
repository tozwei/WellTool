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
    /// 无缓存实现，用于快速关闭缓存
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class NoCache<K, V> : Cache<K, V>
    {
        /// <summary>
        /// 返回缓存容量，0表示无大小限制
        /// </summary>
        /// <returns>返回缓存容量，0表示无大小限制</returns>
        public int Capacity()
        {
            return 0;
        }

        /// <summary>
        /// 缓存失效时长，0表示没有设置，单位毫秒
        /// </summary>
        /// <returns>缓存失效时长，0表示没有设置，单位毫秒</returns>
        public long Timeout()
        {
            return 0;
        }

        /// <summary>
        /// 将对象加入到缓存，使用默认失效时长
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">缓存的对象</param>
        public void Put(K key, V value)
        {
            // 跳过
        }

        /// <summary>
        /// 将对象加入到缓存，使用指定失效时长
        /// 如果缓存空间满了，<see cref="Prune()"/> 将被调用以获得空间来存放新对象
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">缓存的对象</param>
        /// <param name="timeout">失效时长，单位毫秒</param>
        public void Put(K key, V value, long timeout)
        {
            // 跳过
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回null
        /// 调用此方法时，会检查上次调用时间，如果与当前时间差值大于超时时间返回null，否则返回值。
        /// 每次调用此方法会刷新最后访问时间，也就是说会重新计算超时时间。
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>键对应的对象</returns>
        public V Get(K key)
        {
            return default;
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回supplier回调产生的对象
        /// 调用此方法时，会检查上次调用时间，如果与当前时间差值大于超时时间返回返回supplier回调产生的对象，否则返回值。
        /// 每次调用此方法会刷新最后访问时间，也就是说会重新计算超时时间。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="supplier">如果不存在回调方法，用于生产值对象</param>
        /// <returns>值对象</returns>
        public V Get(K key, Func<V> supplier)
        {
            return Get(key, true, supplier);
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回supplier回调产生的对象
        /// 调用此方法时，会检查上次调用时间，如果与当前时间差值大于超时时间返回supplier回调产生的对象，否则返回值。
        /// 每次调用此方法会可选是否刷新最后访问时间，true表示会重新计算超时时间。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间，即重新计算超时时间。</param>
        /// <param name="supplier">如果不存在回调方法，用于生产值对象</param>
        /// <returns>值对象</returns>
        public V Get(K key, bool isUpdateLastAccess, Func<V> supplier)
        {
            return Get(key, isUpdateLastAccess, 0, supplier);
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
        public V Get(K key, bool isUpdateLastAccess, long timeout, Func<V> supplier)
        {
            return supplier != null ? supplier() : default;
        }

        /// <summary>
        /// 从缓存中获得对象，当对象不在缓存中或已经过期返回null
        /// 调用此方法时，会检查上次调用时间，如果与当前时间差值大于超时时间返回null，否则返回值。
        /// 每次调用此方法会可选是否刷新最后访问时间，true表示会重新计算超时时间。
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="isUpdateLastAccess">是否更新最后访问时间，即重新计算超时时间。</param>
        /// <returns>键对应的对象</returns>
        public V Get(K key, bool isUpdateLastAccess)
        {
            return default;
        }

        /// <summary>
        /// 返回包含键和值得迭代器
        /// </summary>
        /// <returns>缓存对象迭代器</returns>
        public IEnumerator<CacheObj<K, V>> CacheObjIterator()
        {
            yield break;
        }

        /// <summary>
        /// 从缓存中清理过期对象，清理策略取决于具体实现
        /// </summary>
        /// <returns>清理的缓存对象个数</returns>
        public int Prune()
        {
            return 0;
        }

        /// <summary>
        /// 缓存是否已满，仅用于有空间限制的缓存对象
        /// </summary>
        /// <returns>缓存是否已满，仅用于有空间限制的缓存对象</returns>
        public bool IsFull()
        {
            return false;
        }

        /// <summary>
        /// 从缓存中移除对象
        /// </summary>
        /// <param name="key">键</param>
        public void Remove(K key)
        {
            // 跳过
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            // 跳过
        }

        /// <summary>
        /// 缓存的对象数量
        /// </summary>
        /// <returns>缓存的对象数量</returns>
        public int Size()
        {
            return 0;
        }

        /// <summary>
        /// 缓存是否为空
        /// </summary>
        /// <returns>缓存是否为空</returns>
        public bool IsEmpty()
        {
            return true;
        }

        /// <summary>
        /// 是否包含key
        /// </summary>
        /// <param name="key">KEY</param>
        /// <returns>是否包含key</returns>
        public bool ContainsKey(K key)
        {
            return false;
        }

        /// <summary>
        /// 设置监听
        /// </summary>
        /// <param name="listener">监听</param>
        /// <returns>this</returns>
        public Cache<K, V> SetListener(CacheListener<K, V> listener)
        {
            return this;
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>一个可用于循环访问集合的枚举器</returns>
        public IEnumerator<V> GetEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>一个可用于循环访问集合的枚举器</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
