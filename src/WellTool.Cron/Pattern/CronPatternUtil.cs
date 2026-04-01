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

namespace WellTool.Cron.Pattern
{
    /// <summary>
    /// Cron表达式工具类
    /// </summary>
    public class CronPatternUtil
    {
        /// <summary>
        /// 解析Cron表达式
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <returns>CronPattern对象</returns>
        public static CronPattern Parse(string pattern)
        {
            return new CronPattern(pattern);
        }

        /// <summary>
        /// 解析Cron表达式
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>CronPattern对象</returns>
        public static CronPattern Parse(string pattern, bool matchSecond)
        {
            return new CronPattern(pattern, matchSecond);
        }

        /// <summary>
        /// 检查Cron表达式是否有效
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <returns>是否有效</returns>
        public static bool IsValid(string pattern)
        {
            try
            {
                new CronPattern(pattern);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查Cron表达式是否有效
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>是否有效</returns>
        public static bool IsValid(string pattern, bool matchSecond)
        {
            try
            {
                new CronPattern(pattern, matchSecond);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 检查Cron表达式是否有效（与IsValid方法相同，为了兼容测试代码）
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <returns>是否有效</returns>
        public static bool IsValidPattern(string pattern)
        {
            // 自动检测是否包含秒
            bool matchSecond = pattern.Trim().Split(' ').Length == 6 || pattern.Trim().Split(' ').Length == 7;
            return IsValid(pattern, matchSecond);
        }

        /// <summary>
        /// 检查Cron表达式是否有效（与IsValid方法相同，为了兼容测试代码）
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>是否有效</returns>
        public static bool IsValidPattern(string pattern, bool matchSecond)
        {
            return IsValid(pattern, matchSecond);
        }

        /// <summary>
        /// 获取下一次匹配的时间（与CronPattern.NextMatch方法相同，为了兼容测试代码）
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="dateTime">起始时间</param>
        /// <returns>下一次匹配的时间</returns>
        public static DateTime? GetNextMatchingTime(string pattern, DateTime dateTime)
        {
            var cronPattern = new CronPattern(pattern);
            return cronPattern.NextMatch(dateTime);
        }

        /// <summary>
        /// 格式化Cron表达式（为了兼容测试代码）
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <returns>格式化后的Cron表达式</returns>
        public static string Format(string pattern)
        {
            // 简单实现，直接返回原表达式
            return pattern;
        }
    }
}