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

namespace WellTool.Cron.Pattern.Matcher
{
    /// <summary>
    /// 单一表达式的匹配器，匹配器由7个PartMatcher组成，分别是：
    /// SECOND, MINUTE, HOUR, DAY_OF_MONTH, MONTH, DAY_OF_WEEK, YEAR
    /// </summary>
    public class PatternMatcher
    {
        private readonly PartMatcher[] matchers;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="secondMatcher">秒匹配器</param>
        /// <param name="minuteMatcher">分匹配器</param>
        /// <param name="hourMatcher">时匹配器</param>
        /// <param name="dayOfMonthMatcher">日匹配器</param>
        /// <param name="monthMatcher">月匹配器</param>
        /// <param name="dayOfWeekMatcher">周匹配器</param>
        /// <param name="yearMatcher">年匹配器</param>
        public PatternMatcher(PartMatcher secondMatcher,
                              PartMatcher minuteMatcher,
                              PartMatcher hourMatcher,
                              PartMatcher dayOfMonthMatcher,
                              PartMatcher monthMatcher,
                              PartMatcher dayOfWeekMatcher,
                              PartMatcher yearMatcher)
        {
            matchers = new PartMatcher[]
            {
                secondMatcher,
                minuteMatcher,
                hourMatcher,
                dayOfMonthMatcher,
                monthMatcher,
                dayOfWeekMatcher,
                yearMatcher
            };
        }

        /// <summary>
        /// 根据表达式位置，获取对应的PartMatcher
        /// </summary>
        /// <param name="part">表达式位置</param>
        /// <returns>PartMatcher</returns>
        public PartMatcher Get(Pattern.Part part)
        {
            return matchers[(int)part];
        }

        /// <summary>
        /// 给定时间是否匹配定时任务表达式
        /// </summary>
        /// <param name="fields">时间字段值，{second, minute, hour, dayOfMonth, month, dayOfWeek, year}</param>
        /// <returns>如果匹配返回true，否则返回false</returns>
        public bool Match(int[] fields)
        {
            return Match(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6]);
        }

        /// <summary>
        /// 给定周的值是否匹配定时任务表达式对应部分
        /// </summary>
        /// <param name="dayOfWeekValue">dayOfMonth值，星期从0开始，0和7都表示周日</param>
        /// <returns>如果匹配返回true，否则返回false</returns>
        public bool MatchWeek(int dayOfWeekValue)
        {
            return matchers[5].Match(dayOfWeekValue);
        }

        /// <summary>
        /// 给定时间是否匹配定时任务表达式
        /// </summary>
        /// <param name="second">秒数，-1表示不匹配此项</param>
        /// <param name="minute">分钟</param>
        /// <param name="hour">小时</param>
        /// <param name="dayOfMonth">天</param>
        /// <param name="month">月，从1开始</param>
        /// <param name="dayOfWeek">周，从0开始，0和7都表示周日</param>
        /// <param name="year">年</param>
        /// <returns>如果匹配返回true，否则返回false</returns>
        private bool Match(int second, int minute, int hour, int dayOfMonth, int month, int dayOfWeek, int year)
        {
            return ((second < 0) || matchers[0].Match(second)) // 匹配秒（非秒匹配模式下始终返回true）
                && matchers[1].Match(minute) // 匹配分
                && matchers[2].Match(hour) // 匹配时
                && MatchDayOfMonth(matchers[3], dayOfMonth, month, PatternUtil.IsLeapYear(year)) // 匹配日
                && matchers[4].Match(month) // 匹配月
                && matchers[5].Match(dayOfWeek) // 匹配周
                && matchers[6].Match(year); // 匹配年
        }

        /// <summary>
        /// 是否匹配日（指定月份的第几天）
        /// </summary>
        /// <param name="matcher">PartMatcher</param>
        /// <param name="dayOfMonth">日</param>
        /// <param name="month">月</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>是否匹配</returns>
        private static bool MatchDayOfMonth(PartMatcher matcher, int dayOfMonth, int month, bool isLeapYear)
        {
            if (matcher is DayOfMonthMatcher dayMatcher)
            {
                return dayMatcher.Match(dayOfMonth, month, isLeapYear);
            }
            return matcher.Match(dayOfMonth);
        }

        /// <summary>
        /// 获取下一个匹配日期时间
        /// 获取方法是，先从年开始查找对应部分的下一个值
        /// </summary>
        /// <param name="values">时间字段值，{second, minute, hour, dayOfMonth, month, dayOfWeek, year}</param>
        /// <param name="zone">时区</param>
        /// <returns>DateTime，毫秒数为0</returns>
        public DateTime? NextMatchAfter(int[] values, TimeZoneInfo zone)
        {
            var newValues = NextMatchValuesAfter(values);
            if (newValues == null)
            {
                return null;
            }

            try
            {
                // 周无需设置
                var year = newValues[6];
                var month = newValues[4];
                var day = newValues[3];
                var hour = newValues[2];
                var minute = newValues[1];
                var second = newValues[0] >= 0 ? newValues[0] : 0;

                return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Local);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取下一个匹配日期时间
        /// </summary>
        /// <param name="values">时间字段值</param>
        /// <returns>新值数组，如果无效返回null</returns>
        private int[] NextMatchValuesAfter(int[] values)
        {
            int i = 6; // YEAR ordinal
            int nextValue = 0;
            while (i >= 0)
            {
                if (i == 5) // DAY_OF_WEEK
                {
                    i--;
                    continue;
                }

                nextValue = GetNextMatch(values, i, 0);

                if (nextValue > values[i])
                {
                    values[i] = nextValue;
                    i--;
                    break;
                }
                else if (nextValue < values[i])
                {
                    i++;
                    nextValue = -1;
                    break;
                }

                i--;
            }

            // 值产生回退，向上查找变更值
            if (nextValue == -1)
            {
                while (i <= 6)
                {
                    if (i == 5)
                    {
                        i++;
                        continue;
                    }

                    nextValue = GetNextMatch(values, i, 1);

                    if (nextValue > values[i])
                    {
                        values[i] = nextValue;
                        i--;
                        break;
                    }
                    i++;
                }
            }

            if (i < 0)
            {
                return null;
            }

            SetToMin(values, i);
            return values;
        }

        /// <summary>
        /// 获取指定部分的下一个匹配值
        /// </summary>
        private int GetNextMatch(int[] newValues, int partOrdinal, int plusValue)
        {
            if (partOrdinal == 3 && matchers[partOrdinal] is DayOfMonthMatcher dayMatcher) // DAY_OF_MONTH
            {
                bool isLeapYear = PatternUtil.IsLeapYear(newValues[6]);
                int month = newValues[4];
                return dayMatcher.NextAfter(newValues[partOrdinal] + plusValue, month, isLeapYear);
            }

            return matchers[partOrdinal].NextAfter(newValues[partOrdinal] + plusValue);
        }

        /// <summary>
        /// 设置从SECOND到指定部分，全部设置为最小值
        /// </summary>
        private void SetToMin(int[] values, int toPart)
        {
            for (int i = toPart; i >= 0; i--)
            {
                var part = (Pattern.Part)i;
                if (part == Pattern.Part.DAY_OF_MONTH)
                {
                    bool isLeapYear = PatternUtil.IsLeapYear(values[6]);
                    int month = values[4];
                    var partMatcher = Get(part);
                    if (partMatcher is DayOfMonthMatcher dayMatcher)
                    {
                        values[i] = dayMatcher.GetMinValue(month, isLeapYear);
                        continue;
                    }
                }

                values[i] = PartUtil.GetMin(part);
            }
        }
    }
}
