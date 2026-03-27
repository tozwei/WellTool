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
    /// RS过滤器
    /// </summary>
    public class RSFilter : AbstractFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public RSFilter(long maxValue) : base(maxValue)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNum">机器位数</param>
        public RSFilter(long maxValue, int machineNum) : base(maxValue, machineNum)
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
            var a = 63689L;
            var b = 378551L;

            foreach (var b1 in bytes)
            {
                hash = hash * a + b1;
                a *= b;
            }

            return hash;
        }
    }
}