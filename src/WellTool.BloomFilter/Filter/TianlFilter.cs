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
    /// Tianl过滤器
    /// </summary>
    public class TianlFilter : AbstractFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public TianlFilter(long maxValue) : base(maxValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNum">机器位数</param>
        public TianlFilter(long maxValue, int machineNum) : base(maxValue, machineNum)
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
            var hash = 0L;
            foreach (var b in bytes)
            {
                hash += b;
                hash += hash << 10;
                hash ^= hash >> 6;
            }
            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;
            return hash;
        }
    }
}