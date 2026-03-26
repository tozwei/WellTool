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
using WellTool.BloomFilter.BitMap;

namespace WellTool.BloomFilter.Filter
{
    /// <summary>
    /// 抽象Bloom过滤器
    /// </summary>
    public abstract class AbstractFilter : BloomFilter
    {
        /// <summary>
        /// 默认机器位数
        /// </summary>
        protected static int DEFAULT_MACHINE_NUM = WellTool.BloomFilter.BitMap.BitMap.MACHINE32;

        private WellTool.BloomFilter.BitMap.BitMap bitMap;

        /// <summary>
        /// 大小
        /// </summary>
        protected long size;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNum">机器位数</param>
        public AbstractFilter(long maxValue, int machineNum)
        {
            Init(maxValue, machineNum);
        }

        /// <summary>
        /// 构造32位
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public AbstractFilter(long maxValue)
            : this(maxValue, DEFAULT_MACHINE_NUM)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="maxValue">最大值</param>
        /// <param name="machineNum">机器位数</param>
        public void Init(long maxValue, int machineNum)
        {
            if (maxValue < 1 || maxValue > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue), "maxValue must be between 1 and int.MaxValue");
            }

            size = maxValue;
            int capacity = (int)((size + machineNum - 1) / machineNum);

            switch (machineNum)
            {
                case WellTool.BloomFilter.BitMap.BitMap.MACHINE32:
                    bitMap = new IntMap(capacity);
                    break;
                case WellTool.BloomFilter.BitMap.BitMap.MACHINE64:
                    bitMap = new LongMap(capacity);
                    break;
                default:
                    throw new ArgumentException("Error Machine number!");
            }
        }

        /// <summary>
        /// 判断一个字符串是否在 Bloom Filter 中存在
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>判断结果，存在返回 true，不存在返回 false</returns>
        public bool Contains(string str)
        {
            return bitMap.Contains(Math.Abs(Hash(str)));
        }

        /// <summary>
        /// 在 Bloom Filter 中增加一个字符串<br>
        /// 如果存在就返回 false. 如果不存在，先增加这个字符串，再返回 true
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否加入成功，如果存在就返回 false. 如果不存在返回 true</returns>
        public bool Add(string str)
        {
            long hash = Math.Abs(Hash(str));
            if (bitMap.Contains(hash))
            {
                return false;
            }

            bitMap.Add(hash);
            return true;
        }

        /// <summary>
        /// 自定义Hash方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>HashCode</returns>
        public abstract long Hash(string str);
    }
}