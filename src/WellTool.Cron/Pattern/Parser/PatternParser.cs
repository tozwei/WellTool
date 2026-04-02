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
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Cron.Pattern.Parser
{
    /// <summary>
    /// 定时任务表达式解析器，用于将表达式字符串解析为PatternMatcher的列表
    /// </summary>
    public class PatternParser
    {
        private static readonly PartParser SECOND_VALUE_PARSER = PartParser.Of(Pattern.Part.SECOND);
        private static readonly PartParser MINUTE_VALUE_PARSER = PartParser.Of(Pattern.Part.MINUTE);
        private static readonly PartParser HOUR_VALUE_PARSER = PartParser.Of(Pattern.Part.HOUR);
        private static readonly PartParser DAY_OF_MONTH_VALUE_PARSER = PartParser.Of(Pattern.Part.DAY_OF_MONTH);
        private static readonly PartParser MONTH_VALUE_PARSER = PartParser.Of(Pattern.Part.MONTH);
        private static readonly PartParser DAY_OF_WEEK_VALUE_PARSER = PartParser.Of(Pattern.Part.DAY_OF_WEEK);
        private static readonly PartParser YEAR_VALUE_PARSER = PartParser.Of(Pattern.Part.YEAR);

        /// <summary>
        /// 解析表达式到匹配列表中
        /// </summary>
        /// <param name="cronPattern">复合表达式</param>
        /// <returns>PatternMatcher列表</returns>
        public static List<Matcher.PatternMatcher> Parse(string cronPattern)
        {
            return ParseGroupPattern(cronPattern);
        }

        /// <summary>
        /// 解析复合任务表达式，格式为：cronA | cronB | ...
        /// </summary>
        /// <param name="groupPattern">复合表达式</param>
        /// <returns>PatternMatcher列表</returns>
        private static List<Matcher.PatternMatcher> ParseGroupPattern(string groupPattern)
        {
            var patternList = groupPattern.Split('|');
            var patternMatchers = new List<Matcher.PatternMatcher>(patternList.Length);
            foreach (var pattern in patternList)
            {
                patternMatchers.Add(ParseSingle(pattern.Trim()));
            }
            return patternMatchers;
        }

        /// <summary>
        /// 解析单一定时任务表达式
        /// </summary>
        /// <param name="pattern">表达式</param>
        /// <returns>PatternMatcher</returns>
        private static Matcher.PatternMatcher ParseSingle(string pattern)
        {
            var parts = pattern.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 5 || parts.Length > 7)
            {
                throw new CronException(string.Format("Pattern [{0}] is invalid, it must be 5-7 parts!", pattern));
            }

            // 偏移量用于兼容Quartz表达式，当表达式有6或7项时，第一项为秒
            int offset = 0;
            if (parts.Length == 6 || parts.Length == 7)
            {
                offset = 1;
            }

            // 秒，如果不支持秒的表达式，则第一位赋值0，表示整分匹配
            string secondPart = offset == 1 ? parts[0] : "0";

            // 年
            Matcher.PartMatcher yearMatcher;
            if (parts.Length == 7)
            {
                yearMatcher = YEAR_VALUE_PARSER.Parse(parts[6]);
            }
            else
            {
                yearMatcher = Matcher.AlwaysTrueMatcher.Instance;
            }

            return new Matcher.PatternMatcher(
                SECOND_VALUE_PARSER.Parse(secondPart),
                MINUTE_VALUE_PARSER.Parse(parts[offset]),
                HOUR_VALUE_PARSER.Parse(parts[1 + offset]),
                DAY_OF_MONTH_VALUE_PARSER.Parse(parts[2 + offset]),
                MONTH_VALUE_PARSER.Parse(parts[3 + offset]),
                DAY_OF_WEEK_VALUE_PARSER.Parse(parts[4 + offset]),
                yearMatcher
            );
        }
    }
}
