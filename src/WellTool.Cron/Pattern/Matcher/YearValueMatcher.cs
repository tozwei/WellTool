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

using System.Collections.Generic;

namespace WellTool.Cron.Pattern.Matcher
{
    /// <summary>
    /// 年份匹配器
    /// 考虑年数字太大，不适合boolean数组，单独使用HashSet匹配
    /// </summary>
    public class YearValueMatcher : PartMatcher
    {
        private readonly HashSet<int> valueList;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="intValueList">匹配的年份值集合</param>
        public YearValueMatcher(IEnumerable<int> intValueList)
        {
            valueList = new HashSet<int>(intValueList);
        }

        /// <summary>
        /// 匹配指定值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>是否匹配</returns>
        public bool Match(int value)
        {
            return valueList.Contains(value);
        }

        /// <summary>
        /// 获取大于等于指定值的下一个匹配值
        /// </summary>
        /// <param name="value">指定值</param>
        /// <returns>下一个匹配值，如果不存在则返回 -1</returns>
        public int NextAfter(int value)
        {
            foreach (var year in valueList)
            {
                if (year >= value)
                {
                    return year;
                }
            }

            // 年无效，此表达式整体无效
            return -1;
        }
    }
}
