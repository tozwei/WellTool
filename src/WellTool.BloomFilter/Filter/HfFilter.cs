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

using System.Text;

namespace WellTool.BloomFilter.Filter
{
    /// <summary>
    /// Hf过滤器
    /// </summary>
    public class HfFilter : AbstractFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size">过滤器大小</param>
        public HfFilter(int size) : base(size)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size">过滤器大小</param>
        /// <param name="hashFunction">哈希函数</param>
        public HfFilter(int size, HashFunction hashFunction) : base(size, hashFunction)
        {
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>哈希值</returns>
        public override int Hash(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            return Hash(bytes);
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>哈希值</returns>
        public override int Hash(byte[] data)
        {
            var hash = 0;
            foreach (var b in data)
            {
                hash += b;
                hash += hash << 10;
                hash ^= hash >> 6;
            }
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;
            return Math.Abs(hash % Size);
        }
    }
}