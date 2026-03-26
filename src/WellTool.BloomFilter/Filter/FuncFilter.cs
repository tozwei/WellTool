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
    /// 基于Hash函数方法的BloomFilter
    /// </summary>
    public class FuncFilter : AbstractFilter
    {
        private readonly Func<string, long> hashFunc;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="hashFunc">Hash函数</param>
        public FuncFilter(long maxValue, Func<string, long> hashFunc)
            : this(maxValue, DEFAULT_MACHINE_NUM, hashFunc)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNum">机器位数</param>
        /// <param name="hashFunc">Hash函数</param>
        public FuncFilter(long maxValue, int machineNum, Func<string, long> hashFunc)
            : base(maxValue, machineNum)
        {
            this.hashFunc = hashFunc;
        }

        /// <summary>
        /// 自定义Hash方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>HashCode</returns>
        public override long Hash(string str)
        {
            return hashFunc(str) % size;
        }
    }
}