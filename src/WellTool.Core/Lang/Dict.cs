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

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 字典工具类
    /// </summary>
    public class Dict : Dictionary<string, object>
    {
        /// <summary>
        /// 创建一个新的Dict实例
        /// </summary>
        /// <returns>新的Dict实例</returns>
        public static Dict Create()
        {
            return new Dict();
        }

        /// <summary>
        /// 设置键值对
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <returns>当前Dict实例</returns>
        public Dict Set(string key, object value)
        {
            this[key] = value;
            return this;
        }

        /// <summary>
        /// 获取指定键的值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public T Get<T>(string key)
        {
            if (TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return default;
        }

        /// <summary>
        /// 获取指定键的值，如果不存在则返回默认值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>值</returns>
        public T Get<T>(string key, T defaultValue)
        {
            if (TryGetValue(key, out object value))
            {
                return (T)value;
            }
            return defaultValue;
        }

        /// <summary>
        /// 检查字典是否包含指定的键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>如果包含则返回 true，否则返回 false</returns>
        public bool ContainsKey(string key)
        {
            return base.ContainsKey(key);
        }

        /// <summary>
        /// 移除指定的键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>如果移除成功则返回 true，否则返回 false</returns>
        public new bool Remove(string key)
        {
            return base.Remove(key);
        }
    }
}
