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
using WellTool.Cron.Pattern.Matcher;
using WellTool.Cron.Pattern.Parser;

namespace WellTool.Cron.Pattern
{
    /// <summary>
    /// Cron表达式
    /// </summary>
    public class CronPattern
    {
        /// <summary>
        /// 匹配器数组
        /// </summary>
        private readonly PartMatcher[] matchers;

        /// <summary>
        /// 是否支持秒匹配
        /// </summary>
        private readonly bool matchSecond;

        /// <summary>
        /// 原始表达式
        /// </summary>
        private readonly string pattern;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pattern">cron表达式</param>
        public CronPattern(string pattern) : this(pattern, pattern.Trim().Split(' ').Length == 6)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pattern">cron表达式</param>
        /// <param name="matchSecond">是否支持秒匹配</param>
        public CronPattern(string pattern, bool matchSecond)
        {
            this.pattern = pattern;
            this.matchSecond = matchSecond;
            this.matchers = Parse(pattern, matchSecond);
        }

        /// <summary>
        /// 解析cron表达式
        /// </summary>
        /// <param name="pattern">cron表达式</param>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>匹配器数组</returns>
        private PartMatcher[] Parse(string pattern, bool matchSecond)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                throw new CronException("Empty cron pattern");
            }

            string[] parts = pattern.Trim().Split(' ');
            int expectedParts = matchSecond ? 6 : 5;

            // 允许 7 个部分的 Cron 表达式（某些扩展语法）
            if (parts.Length != expectedParts && parts.Length != 7)
            {
                throw new CronException("Invalid cron pattern: {0}, expected {1} or 7 parts, got {2}", pattern, expectedParts, parts.Length);
            }

            PartMatcher[] matchers = new PartMatcher[6];

            if (parts.Length == 7)
            {
                // 支持 7 个部分的表达式：秒 分 时 日 月 周 年（年部分忽略）
                matchers[(int)Part.SECOND] = PartParser.Parse(parts[0], PartUtil.GetMin(Part.SECOND), PartUtil.GetMax(Part.SECOND));
                matchers[(int)Part.MINUTE] = PartParser.Parse(parts[1], PartUtil.GetMin(Part.MINUTE), PartUtil.GetMax(Part.MINUTE));
                matchers[(int)Part.HOUR] = PartParser.Parse(parts[2], PartUtil.GetMin(Part.HOUR), PartUtil.GetMax(Part.HOUR));
                matchers[(int)Part.DAY_OF_MONTH] = PartParser.Parse(parts[3], PartUtil.GetMin(Part.DAY_OF_MONTH), PartUtil.GetMax(Part.DAY_OF_MONTH));
                matchers[(int)Part.MONTH] = PartParser.Parse(parts[4], PartUtil.GetMin(Part.MONTH), PartUtil.GetMax(Part.MONTH));
                matchers[(int)Part.DAY_OF_WEEK] = PartParser.Parse(parts[5], PartUtil.GetMin(Part.DAY_OF_WEEK), PartUtil.GetMax(Part.DAY_OF_WEEK));
            }
            else if (matchSecond)
            {
                // 秒 分 时 日 月 周
                matchers[(int)Part.SECOND] = PartParser.Parse(parts[0], PartUtil.GetMin(Part.SECOND), PartUtil.GetMax(Part.SECOND));
                matchers[(int)Part.MINUTE] = PartParser.Parse(parts[1], PartUtil.GetMin(Part.MINUTE), PartUtil.GetMax(Part.MINUTE));
                matchers[(int)Part.HOUR] = PartParser.Parse(parts[2], PartUtil.GetMin(Part.HOUR), PartUtil.GetMax(Part.HOUR));
                matchers[(int)Part.DAY_OF_MONTH] = PartParser.Parse(parts[3], PartUtil.GetMin(Part.DAY_OF_MONTH), PartUtil.GetMax(Part.DAY_OF_MONTH));
                matchers[(int)Part.MONTH] = PartParser.Parse(parts[4], PartUtil.GetMin(Part.MONTH), PartUtil.GetMax(Part.MONTH));
                matchers[(int)Part.DAY_OF_WEEK] = PartParser.Parse(parts[5], PartUtil.GetMin(Part.DAY_OF_WEEK), PartUtil.GetMax(Part.DAY_OF_WEEK));
            }
            else
            {
                // 分 时 日 月 周
                matchers[(int)Part.SECOND] = AlwaysTrueMatcher.Instance; // 秒部分始终匹配
                matchers[(int)Part.MINUTE] = PartParser.Parse(parts[0], PartUtil.GetMin(Part.MINUTE), PartUtil.GetMax(Part.MINUTE));
                matchers[(int)Part.HOUR] = PartParser.Parse(parts[1], PartUtil.GetMin(Part.HOUR), PartUtil.GetMax(Part.HOUR));
                matchers[(int)Part.DAY_OF_MONTH] = PartParser.Parse(parts[2], PartUtil.GetMin(Part.DAY_OF_MONTH), PartUtil.GetMax(Part.DAY_OF_MONTH));
                matchers[(int)Part.MONTH] = PartParser.Parse(parts[3], PartUtil.GetMin(Part.MONTH), PartUtil.GetMax(Part.MONTH));
                matchers[(int)Part.DAY_OF_WEEK] = PartParser.Parse(parts[4], PartUtil.GetMin(Part.DAY_OF_WEEK), PartUtil.GetMax(Part.DAY_OF_WEEK));
            }

            return matchers;
        }

        /// <summary>
        /// 匹配指定时间
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <returns>是否匹配</returns>
        public bool Match(DateTime dateTime)
        {
            // 周的处理：0和7都表示周日
            int dayOfWeek = (int)dateTime.DayOfWeek;
            // 直接使用 0-6 的范围，与 .NET 的 DayOfWeek 枚举保持一致

            // 检查 DAY_OF_MONTH 是否为 AlwaysTrueMatcher (即 ? 字符)
            bool dayOfMonthIsAlwaysTrue = matchers[(int)Part.DAY_OF_MONTH] is AlwaysTrueMatcher;

            // 检查 DAY_OF_WEEK 是否为 AlwaysTrueMatcher (即 ? 字符)
            bool dayOfWeekIsAlwaysTrue = matchers[(int)Part.DAY_OF_WEEK] is AlwaysTrueMatcher;

            // 处理 DAY_OF_MONTH 部分，考虑 L 字符的特殊含义
            bool dayOfMonthMatch = false;
            if (matchers[(int)Part.DAY_OF_MONTH] is BoolArrayMatcher dayOfMonthMatcher)
            {
                // 检查是否匹配当天的日期
                if (dayOfMonthMatcher.Match(dateTime.Day - 1))
                {
                    dayOfMonthMatch = true;
                }
                // 检查是否匹配当月的最后一天
                else
                {
                    int daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                    if (dateTime.Day == daysInMonth && dayOfMonthMatcher.Match(30)) // 30 是 PartUtil.GetMax(Part.DAY_OF_MONTH) - 1
                    {
                        dayOfMonthMatch = true;
                    }
                }
            }
            else
            {
                dayOfMonthMatch = matchers[(int)Part.DAY_OF_MONTH].Match(dateTime.Day - 1);
            }

            // DAY_OF_WEEK 匹配
            bool dayOfWeekMatch = matchers[(int)Part.DAY_OF_WEEK].Match(dayOfWeek);

            // 如果日和周都没有被忽略，则两者都需要匹配
            // 如果其中一个被忽略（?），则只检查另一个
            // 如果两者都被指定，则两者都需要匹配（Quartz标准）
            bool dayMatch;
            if (dayOfMonthIsAlwaysTrue && dayOfWeekIsAlwaysTrue)
            {
                // 两者都被忽略
                dayMatch = true;
            }
            else if (dayOfMonthIsAlwaysTrue)
            {
                // 日被忽略，只检查周
                dayMatch = dayOfWeekMatch;
            }
            else if (dayOfWeekIsAlwaysTrue)
            {
                // 周被忽略，只检查日
                dayMatch = dayOfMonthMatch;
            }
            else
            {
                // 两者都被指定，两者都需要匹配
                dayMatch = dayOfMonthMatch && dayOfWeekMatch;
            }

            // 对于没有指定秒的表达式（5字段），秒必须为0才算匹配
            bool secondMatch;
            if (!matchSecond)
            {
                // 5字段表达式，秒隐式为0
                secondMatch = dateTime.Second == 0;
            }
            else
            {
                // 6/7字段表达式，使用实际的秒匹配器
                secondMatch = matchers[(int)Part.SECOND].Match(dateTime.Second);
            }

            return secondMatch &&
                   matchers[(int)Part.MINUTE].Match(dateTime.Minute) &&
                   matchers[(int)Part.HOUR].Match(dateTime.Hour) &&
                   dayMatch &&
                   matchers[(int)Part.MONTH].Match(dateTime.Month - 1);
        }

        /// <summary>
        /// 获取下一次匹配的时间
        /// </summary>
        /// <param name="dateTime">起始时间</param>
        /// <returns>下一次匹配的时间</returns>
        public DateTime? NextMatch(DateTime dateTime)
        {
            // 从下一秒开始查找
            DateTime nextTime = dateTime.AddSeconds(1);
            
            // 最多查找31天，避免无限循环
            DateTime maxTime = nextTime.AddDays(31);
            while (nextTime < maxTime)
            {
                if (Match(nextTime))
                {
                    return nextTime;
                }
                nextTime = nextTime.AddSeconds(1);
            }

            return null;
        }

        /// <summary>
        /// 获取下一次匹配的时间（与NextMatch方法相同，为了兼容测试代码）
        /// </summary>
        /// <param name="dateTime">起始时间</param>
        /// <returns>下一次匹配的时间</returns>
        public DateTime? NextMatchingAfter(DateTime dateTime)
        {
            return NextMatch(dateTime);
        }

        /// <summary>
        /// 返回Cron表达式的字符串表示
        /// </summary>
        /// <returns>Cron表达式</returns>
        public override string ToString()
        {
            return pattern;
        }
    }
}