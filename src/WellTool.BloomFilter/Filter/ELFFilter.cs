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

namespace WellTool.BloomFilter.Filter
{
    /// <summary>
    /// ELF哈希算法过滤器
    /// </summary>
    public class ELFFilter : FuncFilter
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public ELFFilter(long maxValue)
            : this(maxValue, DEFAULT_MACHINE_NUM)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNumber">机器位数</param>
        public ELFFilter(long maxValue, int machineNumber)
            : base(maxValue, machineNumber, ElfHash)
        {
        }

        /// <summary>
        /// ELF哈希算法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>哈希值</returns>
        private static long ElfHash(string str)
        {
            long hash = 0;
            long x = 0;

            foreach (char c in str)
            {
                hash = (hash << 4) + c;
                if ((x = hash & 0xF0000000L) != 0)
                {
                    hash ^= x >> 24;
                    hash &= ~x;
                }
            }

            return hash;
        }
    }
}