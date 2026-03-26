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
    /// 总是匹配的匹配器（用于处理 * 表达式）
    /// </summary>
    public class AlwaysTrueMatcher : PartMatcher
    {
        /// <summary>
        /// 单例实例
        /// </summary>
        public static readonly AlwaysTrueMatcher Instance = new AlwaysTrueMatcher();

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private AlwaysTrueMatcher() { }

        /// <summary>
        /// 匹配指定值，总是返回true
        /// </summary>
        /// <param name="value">值</param>
        /// <returns>是否匹配</returns>
        public bool Match(int value)
        {
            return true;
        }
    }
}