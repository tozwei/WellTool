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
    /// 缓存值迭代器，用于遍历缓存中未过期的值
    /// </summary>
    /// <typeparam name="V">值类型</typeparam>
    public class CacheValuesIterator<V> : IEnumerator<V>
    {
        private readonly CacheObjIterator<object, V> _cacheObjIter;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iterator">缓存对象迭代器</param>
        public CacheValuesIterator(CacheObjIterator<object, V> iterator)
        {
            _cacheObjIter = iterator;
        }

        /// <summary>
        /// 是否有下一个值
        /// </summary>
        public bool HasNext()
        {
            return _cacheObjIter.HasNext();
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
        public V Current
        {
            get
            {
                return _cacheObjIter.Current.Value;
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
            // 无需释放
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            _cacheObjIter.Reset();
        }
    }
}
