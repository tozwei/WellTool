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
    /// 缓存监听接口，用于监听缓存的操作
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public interface CacheListener<K, V>
    {
        /// <summary>
        /// 当缓存对象被添加时调用
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void OnAdd(K key, V value);

        /// <summary>
        /// 当缓存对象被更新时调用
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        void OnUpdate(K key, V oldValue, V newValue);

        /// <summary>
        /// 当缓存对象被移除时调用
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void OnRemove(K key, V value);

        /// <summary>
        /// 当缓存对象过期时调用
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        void OnExpire(K key, V value);
    }
}