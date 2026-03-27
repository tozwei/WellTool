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
    /// FNV过滤器
    /// </summary>
    public class FNVFilter : AbstractFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public FNVFilter(long maxValue) : base(maxValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNum">机器位数</param>
        public FNVFilter(long maxValue, int machineNum) : base(maxValue, machineNum)
        {
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>哈希值</returns>
        public override long Hash(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            const long p = 16777619;
            var hash = 2166136261L;

            foreach (var b in bytes)
            {
                hash = (hash ^ b) * p;
            }

            hash += hash << 13;
            hash ^= hash >> 7;
            hash += hash << 3;
            hash ^= hash >> 17;
            hash += hash << 5;

            return hash;
        }
    }
}