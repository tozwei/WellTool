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

namespace WellTool.BloomFilter
{
    /// <summary>
    /// 基于 BitArray 实现的 Bloom Filter
    /// </summary>
    public class BitSetBloomFilter : BloomFilter
    {
        private readonly BitArray bitArray;
        private readonly int size;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="size">大小</param>
        public BitSetBloomFilter(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "size must be greater than 0");
            }
            this.size = size;
            bitArray = new BitArray(size);
        }

        /// <summary>
        /// 判断一个字符串是否在 Bloom Filter 中存在
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>判断结果，存在返回 true，不存在返回 false</returns>
        public bool Contains(string str)
        {
            int hash = Math.Abs(str.GetHashCode());
            int index = hash % size;
            return bitArray.Get(index);
        }

        /// <summary>
        /// 在 Bloom Filter 中增加一个字符串<br>
        /// 如果存在就返回 false. 如果不存在，先增加这个字符串，再返回 true
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否加入成功，如果存在就返回 false. 如果不存在返回 true</returns>
        public bool Add(string str)
        {
            int hash = Math.Abs(str.GetHashCode());
            int index = hash % size;

            if (bitArray.Get(index))
            {
                return false;
            }

            bitArray.Set(index, true);
            return true;
        }
    }
}