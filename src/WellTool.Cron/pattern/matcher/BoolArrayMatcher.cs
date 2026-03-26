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
        private readonly bool[] matchArray;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="size">数组大小</param>
        public BoolArrayMatcher(int size)
        {
            matchArray = new bool[size];
        }

        /// <summary>
        /// 设置匹配值
        /// </summary>
        /// <param name="value">要匹配的值</param>
        public void SetMatch(int value)
        {
            if (value >= 0 && value < matchArray.Length)
            {
                matchArray[value] = true;
            }
        }

        /// <summary>
        /// 匹配指定值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>是否匹配</returns>
        public bool Match(int value)
        {
            if (value < 0 || value >= matchArray.Length)
            {
                return false;
            }
            return matchArray[value];
        }
    }
}