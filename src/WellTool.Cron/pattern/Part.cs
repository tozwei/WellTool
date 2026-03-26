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
    /// Cron表达式的各个部分
    /// </summary>
    public enum Part
    {
        /// <summary>
        /// 秒（0-59）
        /// </summary>
        SECOND = 0,

        /// <summary>
        /// 分（0-59）
        /// </summary>
        MINUTE = 1,

        /// <summary>
        /// 时（0-23）
        /// </summary>
        HOUR = 2,

        /// <summary>
        /// 日（1-31）
        /// </summary>
        DAY_OF_MONTH = 3,

        /// <summary>
        /// 月（1-12）
        /// </summary>
        MONTH = 4,

        /// <summary>
        /// 周（0-7，0和7都表示周日）
        /// </summary>
        DAY_OF_WEEK = 5
    }

    /// <summary>
    /// 各部分的最小值和最大值
    /// </summary>
    public static class PartUtil
    {
        /// <summary>
        /// 获取各部分的最小值
        /// </summary>
        /// <param name="part">部分</param>
        /// <returns>最小值</returns>
        public static int GetMin(Part part)
        {
            switch (part)
            {
                case Part.SECOND:
                case Part.MINUTE:
                    return 0;
                case Part.HOUR:
                    return 0;
                case Part.DAY_OF_MONTH:
                    return 1;
                case Part.MONTH:
                    return 1;
                case Part.DAY_OF_WEEK:
                    return 0;
                default:
                    throw new CronException("Unknown part: {0}", part);
            }
        }

        /// <summary>
        /// 获取各部分的最大值
        /// </summary>
        /// <param name="part">部分</param>
        /// <returns>最大值</returns>
        public static int GetMax(Part part)
        {
            switch (part)
            {
                case Part.SECOND:
                case Part.MINUTE:
                    return 59;
                case Part.HOUR:
                    return 23;
                case Part.DAY_OF_MONTH:
                    return 31;
                case Part.MONTH:
                    return 12;
                case Part.DAY_OF_WEEK:
                    return 7;
                default:
                    throw new CronException("Unknown part: {0}", part);
            }
        }
    }
}