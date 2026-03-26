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

using WellTool.BloomFilter.Filter;

namespace WellTool.BloomFilter
{
    /// <summary>
    /// BloomFilter 实现 <br>
    /// 1.构建hash算法 <br>
    /// 2.散列hash映射到数组的bit位置 <br>
    /// 3.验证<br>
    /// 此实现方式可以指定Hash算法
    /// </summary>
    public class BitMapBloomFilter : BloomFilter
    {
        private readonly BloomFilter[] filters;

        /// <summary>
        /// 构造，使用默认的5个过滤器
        /// </summary>
        /// <param name="m">M值决定BitMap的大小</param>
        public BitMapBloomFilter(int m)
        {
            long mNum = m / 5;
            long size = mNum * 1024 * 1024 * 8;

            filters = new BloomFilter[]
            {
                new DefaultFilter(size),
                new ELFFilter(size),
                new JSFilter(size),
                new PJWFilter(size),
                new SDBMFilter(size)
            };
        }

        /// <summary>
        /// 使用自定的多个过滤器建立BloomFilter
        /// </summary>
        /// <param name="m">M值决定BitMap的大小</param>
        /// <param name="filters">Bloom过滤器列表</param>
        public BitMapBloomFilter(int m, params BloomFilter[] filters)
            : this(m)
        {
            this.filters = filters;
        }

        /// <summary>
        /// 判断一个字符串是否在 Bloom Filter 中存在
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>判断结果，存在返回 true，不存在返回 false</returns>
        public bool Contains(string str)
        {
            foreach (var filter in filters)
            {
                if (!filter.Contains(str))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 在 Bloom Filter 中增加一个字符串<br>
        /// 如果存在就返回 false. 如果不存在，先增加这个字符串，再返回 true
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否加入成功，如果存在就返回 false. 如果不存在返回 true</returns>
        public bool Add(string str)
        {
            bool flag = false;
            foreach (var filter in filters)
            {
                flag |= filter.Add(str);
            }
            return flag;
        }
    }
}