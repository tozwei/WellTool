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
    /// 缓存对象，包装缓存的键值对
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class CacheObj<K, V>
    {
        /// <summary>
        /// 键
        /// </summary>
        public K Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public V Value { get; set; }

        /// <summary>
        /// 最后访问时间
        /// </summary>
        public long LastAccess { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public long Expire { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">超时时间，单位毫秒</param>
        public CacheObj(K key, V value, long timeout)
        {
            Key = key;
            Value = value;
            LastAccess = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            Expire = timeout > 0 ? LastAccess + timeout : 0; // 当timeout <= 0时，永不过期
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public CacheObj(K key, V value)
            : this(key, value, 0)
        {}

        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns>是否过期</returns>
        public bool IsExpired()
        {
            return Expire > 0 && Expire < DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 更新最后访问时间
        /// </summary>
        public void UpdateLastAccess()
        {
            LastAccess = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// 更新过期时间
        /// </summary>
        /// <param name="timeout">超时时间，单位毫秒</param>
        public void UpdateExpire(long timeout)
        {
            Expire = DateTimeOffset.Now.ToUnixTimeMilliseconds() + timeout;
        }
    }
}