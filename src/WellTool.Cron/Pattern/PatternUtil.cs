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

using System.Globalization;

namespace WellTool.Cron.Pattern
{
    /// <summary>
    /// Cron表达式工具类，内部使用
    /// </summary>
    internal static class PatternUtil
    {
        /// <summary>
        /// 获取处理后的字段列表
        /// 月份从1开始，周从0开始
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <param name="isMatchSecond">是否匹配秒，false则秒返回-1</param>
        /// <returns>字段值列表 {second, minute, hour, dayOfMonth, month, dayOfWeek, year}</returns>
        internal static int[] GetFields(DateTime dateTime, bool isMatchSecond)
        {
            int second = isMatchSecond ? dateTime.Second : -1;
            int minute = dateTime.Minute;
            int hour = dateTime.Hour;
            int dayOfMonth = dateTime.Day;
            int month = dateTime.Month; // 月份从1开始
            int dayOfWeek = ((int)dateTime.DayOfWeek + 6) % 7; // 星期从0开始，0和7都表示周日
            int year = dateTime.Year;
            return new int[] { second, minute, hour, dayOfMonth, month, dayOfWeek, year };
        }

        /// <summary>
        /// 获取给定年份的最后一天
        /// </summary>
        /// <param name="monthBase1">月份（从1开始）</param>
        /// <param name="year">年份</param>
        /// <returns>最后一天</returns>
        internal static int GetLastDay(int monthBase1, int year)
        {
            return DateTime.DaysInMonth(year, monthBase1);
        }

        /// <summary>
        /// 判断是否为闰年
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>是否为闰年</returns>
        internal static bool IsLeapYear(int year)
        {
            return DateTime.IsLeapYear(year);
        }
    }
}
