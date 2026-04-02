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
using System.Collections;
using System.Collections.Generic;

namespace WellTool.Cache
{
    /// <summary>
    /// 缓存对象迭代器，用于遍历缓存中未过期的CacheObj
    /// </summary>
    /// <typeparam name="K">键类型</typeparam>
    /// <typeparam name="V">值类型</typeparam>
    public class CacheObjIterator<K, V> : IEnumerator<CacheObj<K, V>>
    {
        private readonly IEnumerator<CacheObj<K, V>> _iterator;
        private CacheObj<K, V> _nextValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterator">原始迭代器</param>
        public CacheObjIterator(IEnumerator<CacheObj<K, V>> iterator)
        {
            _iterator = iterator;
            NextValue();
        }

        /// <summary>
        /// 是否有下一个值
        /// </summary>
        public bool HasNext()
        {
            return _nextValue != null;
        }

        /// <summary>
        /// 是否有下一个值
        /// </summary>
        public bool MoveNext()
        {
            return HasNext();
        }

        /// <summary>
        /// 获取当前元素
        /// </summary>
        public CacheObj<K, V> Current
        {
            get
            {
                if (_nextValue == null)
                {
                    throw new InvalidOperationException();
                }
                var cachedObject = _nextValue;
                NextValue();
                return cachedObject;
            }
        }

        /// <summary>
        /// 获取当前元素
        /// </summary>
        object IEnumerator.Current => Current;

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            // 无需释放，迭代器是值类型的副本
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _iterator.Reset();
            NextValue();
        }

        /// <summary>
        /// 获取下一个未过期的值
        /// </summary>
        private void NextValue()
        {
            while (_iterator.MoveNext())
            {
                _nextValue = _iterator.Current;
                if (_nextValue != null && !_nextValue.IsExpired())
                {
                    return;
                }
            }
            _nextValue = null;
        }
    }
}
