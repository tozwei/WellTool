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

namespace WellTool.Cron.Pattern.Matcher
{
    /// <summary>
    /// 基于布尔数组的匹配器
    /// </summary>
    public class BoolArrayMatcher : PartMatcher
    {
        /// <summary>
        /// 匹配值的布尔数组，索引表示值，值表示是否匹配
        /// </summary>
        protected readonly bool[] bValues;

        /// <summary>
        /// 最小匹配值
        /// </summary>
        protected int minValue = int.MaxValue;

        /// <summary>
        /// 最大匹配值
        /// </summary>
        protected int maxValue = int.MinValue;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size">数组大小</param>
        public BoolArrayMatcher(int size)
        {
            bValues = new bool[size];
        }

        /// <summary>
        /// 设置匹配值
        /// </summary>
        /// <param name="value">要匹配的值</param>
        public void SetMatch(int value)
        {
            if (value >= 0 && value < bValues.Length)
            {
                bValues[value] = true;
                if (value < minValue) minValue = value;
                if (value > maxValue) maxValue = value;
            }
        }

        /// <summary>
        /// 匹配指定值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>是否匹配</returns>
        public virtual bool Match(int value)
        {
            if (value < 0 || value >= bValues.Length)
            {
                return false;
            }
            return bValues[value];
        }

        /// <summary>
        /// 获取大于等于指定值的下一个匹配值
        /// </summary>
        /// <param name="value">指定值</param>
        /// <returns>下一个匹配值，如果不存在则返回 -1</returns>
        public virtual int NextAfter(int value)
        {
            for (int i = value; i < bValues.Length; i++)
            {
                if (bValues[i])
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 获取最小匹配值
        /// </summary>
        /// <returns>最小匹配值</returns>
        public int GetMinValue()
        {
            return minValue == int.MaxValue ? 0 : minValue;
        }

        /// <summary>
        /// 获取最大匹配值
        /// </summary>
        /// <returns>最大匹配值</returns>
        public int GetMaxValue()
        {
            return maxValue == int.MinValue ? bValues.Length - 1 : maxValue;
        }
    }
}