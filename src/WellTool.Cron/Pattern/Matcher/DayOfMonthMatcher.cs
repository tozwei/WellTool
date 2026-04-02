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
    /// 每月第几天匹配器
    /// 考虑每月的天数不同，且存在闰年情况，日匹配单独使用
    /// </summary>
    public class DayOfMonthMatcher : BoolArrayMatcher
    {
        /// <summary>
        /// 最后一天标记
        /// </summary>
        private static readonly int LAST_DAY = 32;

        /// <summary>
        /// 最小值（考虑闰年）
        /// </summary>
        private int minValueWithLeap = int.MaxValue;

        /// <summary>
        /// 最大值（考虑闰年）
        /// </summary>
        private int maxValueWithLeap = int.MinValue;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="intValueList">匹配的日值列表</param>
        public DayOfMonthMatcher(List<int> intValueList) : base(32)
        {
            foreach (var value in intValueList)
            {
                SetMatch(value);
            }
        }

        /// <summary>
        /// 设置匹配值
        /// </summary>
        /// <param name="value">要匹配的值</param>
        public new void SetMatch(int value)
        {
            base.SetMatch(value);
        }

        /// <summary>
        /// 给定的日期是否匹配当前匹配器
        /// </summary>
        /// <param name="dayValue">被检查的值，此处为日</param>
        /// <param name="month">实际的月份，从1开始</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>是否匹配</returns>
        public bool Match(int dayValue, int month, bool isLeapYear)
        {
            return (base.Match(dayValue) // 在约定日范围内的某一天
                // 匹配器中用户定义了最后一天（32表示最后一天）
                || MatchLastDay(dayValue, month, isLeapYear));
        }

        /// <summary>
        /// 获取指定日之后的匹配值，也可以是其本身
        /// 如果表达式中存在最后一天（如使用"L"），则：
        /// - 4月、6月、9月、11月最多匹配到30日
        /// - 4月闰年匹配到29日，非闰年28日
        /// </summary>
        /// <param name="dayValue">指定的天值</param>
        /// <param name="month">月份，从1开始</param>
        /// <param name="isLeapYear">是否为闰年</param>
        /// <returns>匹配到的值或之后的值</returns>
        public int NextAfter(int dayValue, int month, bool isLeapYear)
        {
            int maxVal = GetMaxValue(month, isLeapYear);
            int minVal = GetMinValue(month, isLeapYear);

            if (dayValue > minVal)
            {
                while (dayValue <= maxVal)
                {
                    // 匹配到有效值
                    if (bValues[dayValue] ||
                        // 如果最大值不在有效值中，这个最大值表示最后一天，则在包含了最后一天的情况下返回最后一天
                        (dayValue == maxVal && Match(LAST_DAY)))
                    {
                        return dayValue;
                    }
                    dayValue++;
                }
            }

            // 两种情况返回最小值
            // 一是给定值小于最小值，那下一个匹配值就是最小值
            // 二是给定值大于最大值，那下一个匹配值也是下一轮的最小值
            return minVal;
        }

        /// <summary>
        /// 重写 NextAfter 方法
        /// </summary>
        public override int NextAfter(int value)
        {
            return NextAfter(value, 1, false);
        }

        /// <summary>
        /// 是否包含最后一天
        /// </summary>
        /// <returns>包含最后一天</returns>
        public bool IsLast()
        {
            return Match(LAST_DAY);
        }

        /// <summary>
        /// 检查value是这个月的最后一天
        /// </summary>
        /// <param name="value">被检查的值</param>
        /// <param name="month">月份，从1开始</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>是否是这个月的最后一天</returns>
        public bool IsLastDay(int value, int month, bool isLeapYear)
        {
            return MatchLastDay(value, month, isLeapYear);
        }

        /// <summary>
        /// 获取表达式定义中指定月的最小日的值
        /// </summary>
        /// <param name="month">月，从1开始</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>匹配的最小值</returns>
        public int GetMinValue(int month, bool isLeapYear)
        {
            if (LAST_DAY == base.GetMinValue())
            {
                // 用户指定了 L 等表示最后一天
                return GetLastDay(month, isLeapYear);
            }
            return base.GetMinValue();
        }

        /// <summary>
        /// 获取表达式定义中指定月的最大日的值
        /// 首先获取表达式定义的最大值，如果这个值大于本月最后一天，则返回最后一天，否则返回用户定义的最大值
        /// 注意最后一天可能不是表达式中定义的有效值
        /// </summary>
        /// <param name="month">月，从1开始</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>匹配的最大值</returns>
        public int GetMaxValue(int month, bool isLeapYear)
        {
            return Math.Min(base.GetMaxValue(), GetLastDay(month, isLeapYear));
        }

        /// <summary>
        /// 是否匹配本月最后一天，规则如下：
        /// 1、闰年2月匹配是否为29
        /// 2、其它月份是否匹配最后一天的日期（可能为30或者31）
        /// 3、表达式包含最后一天（使用31表示）
        /// </summary>
        /// <param name="dayValue">被检查的值</param>
        /// <param name="month">月，从1开始</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>是否为本月最后一天</returns>
        private bool MatchLastDay(int dayValue, int month, bool isLeapYear)
        {
            return dayValue > 27
                // 表达式中定义包含了最后一天
                && Match(LAST_DAY)
                // 用户指定的日正好是最后一天
                && dayValue == GetLastDay(month, isLeapYear);
        }

        /// <summary>
        /// 获取最后一天
        /// </summary>
        /// <param name="month">月，从1开始</param>
        /// <param name="isLeapYear">是否闰年</param>
        /// <returns>最后一天</returns>
        private static int GetLastDay(int month, bool isLeapYear)
        {
            return DateTime.DaysInMonth(2000, month); // 使用2000年是固定的闰年参考年
        }
    }
}
