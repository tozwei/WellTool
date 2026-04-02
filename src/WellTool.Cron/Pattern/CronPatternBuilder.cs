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

namespace WellTool.Cron.Pattern
{
    /// <summary>
    /// Cron表达式构建器
    /// </summary>
    public class CronPatternBuilder
    {
        private string second = "*";
        private string minute = "*";
        private string hour = "*";
        private string dayOfMonth = "*";
        private string month = "*";
        private string dayOfWeek = "*";
        private string year = "*";
        private bool matchSecond = false;

        /// <summary>
        /// 创建新的CronPatternBuilder实例
        /// </summary>
        /// <returns>CronPatternBuilder实例</returns>
        public static CronPatternBuilder Of()
        {
            return new CronPatternBuilder();
        }

        /// <summary>
        /// 创建新的CronPatternBuilder实例
        /// </summary>
        /// <returns>CronPatternBuilder实例</returns>
        public static CronPatternBuilder New()
        {
            return new CronPatternBuilder();
        }

        /// <summary>
        /// 设置指定部分的值
        /// </summary>
        /// <param name="part">部分</param>
        /// <param name="value">值</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder Set(Part part, string value)
        {
            switch (part)
            {
                case Part.SECOND:
                    second = value;
                    matchSecond = true;
                    break;
                case Part.MINUTE:
                    minute = value;
                    break;
                case Part.HOUR:
                    hour = value;
                    break;
                case Part.DAY_OF_MONTH:
                    dayOfMonth = value;
                    break;
                case Part.MONTH:
                    month = value;
                    break;
                case Part.DAY_OF_WEEK:
                    dayOfWeek = value;
                    break;
                case Part.YEAR:
                    year = value;
                    break;
            }
            return this;
        }

        /// <summary>
        /// 设置指定部分的范围值
        /// </summary>
        /// <param name="part">部分</param>
        /// <param name="start">起始值</param>
        /// <param name="end">结束值</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetRange(Part part, int start, int end)
        {
            return Set(part, $"{start}-{end}");
        }

        /// <summary>
        /// 设置秒字段
        /// </summary>
        /// <param name="second">秒字段</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetSecond(string second)
        {
            this.second = second;
            this.matchSecond = true;
            return this;
        }

        /// <summary>
        /// 设置分钟字段
        /// </summary>
        /// <param name="minute">分钟字段</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetMinute(string minute)
        {
            this.minute = minute;
            return this;
        }

        /// <summary>
        /// 设置小时字段
        /// </summary>
        /// <param name="hour">小时字段</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetHour(string hour)
        {
            this.hour = hour;
            return this;
        }

        /// <summary>
        /// 设置日字段
        /// </summary>
        /// <param name="dayOfMonth">日字段</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetDayOfMonth(string dayOfMonth)
        {
            this.dayOfMonth = dayOfMonth;
            return this;
        }

        /// <summary>
        /// 设置月字段
        /// </summary>
        /// <param name="month">月字段</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetMonth(string month)
        {
            this.month = month;
            return this;
        }

        /// <summary>
        /// 设置周字段
        /// </summary>
        /// <param name="dayOfWeek">周字段</param>
        /// <returns>当前构建器</returns>
        public CronPatternBuilder SetDayOfWeek(string dayOfWeek)
        {
            this.dayOfWeek = dayOfWeek;
            return this;
        }

        /// <summary>
        /// 构建Cron表达式字符串
        /// </summary>
        /// <returns>Cron表达式字符串</returns>
        public string Build()
        {
            if (matchSecond && year != "*")
            {
                return $"{second} {minute} {hour} {dayOfMonth} {month} {dayOfWeek} {year}";
            }
            else if (matchSecond)
            {
                return $"{second} {minute} {hour} {dayOfMonth} {month} {dayOfWeek}";
            }
            else
            {
                return $"{minute} {hour} {dayOfMonth} {month} {dayOfWeek}";
            }
        }
    }
}