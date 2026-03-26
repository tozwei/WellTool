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

namespace WellTool.Cron
{
    /// <summary>
    /// 定时任务配置
    /// </summary>
    public class CronConfig
    {
        /// <summary>
        /// 时区
        /// </summary>
        private TimeZoneInfo timeZone = TimeZoneInfo.Local;

        /// <summary>
        /// 是否支持秒匹配，默认不支持
        /// </summary>
        private bool matchSecond = false;

        /// <summary>
        /// 获取或设置时区
        /// </summary>
        public TimeZoneInfo TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }

        /// <summary>
        /// 获取或设置是否支持秒匹配
        /// </summary>
        public bool IsMatchSecond
        {
            get { return matchSecond; }
            set { matchSecond = value; }
        }

        /// <summary>
        /// 设置时区
        /// </summary>
        /// <param name="timeZone">时区</param>
        /// <returns>当前配置对象</returns>
        public CronConfig SetTimeZone(TimeZoneInfo timeZone)
        {
            this.timeZone = timeZone;
            return this;
        }

        /// <summary>
        /// 设置是否支持秒匹配
        /// </summary>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>当前配置对象</returns>
        public CronConfig SetMatchSecond(bool matchSecond)
        {
            this.matchSecond = matchSecond;
            return this;
        }
    }
}