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

using WellTool.Cron.Pattern.Matcher;

namespace WellTool.Cron.Pattern.Parser
{
    /// <summary>
    /// Cron表达式各部分的解析器
    /// </summary>
    public class PartParser
    {
        /// <summary>
        /// 解析cron表达式的一个部分
        /// </summary>
        /// <param name="partStr">部分字符串</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>匹配器</returns>
        public static PartMatcher Parse(string partStr, int min, int max)
        {
            if (string.IsNullOrWhiteSpace(partStr))
            {
                throw new CronException("Empty cron part");
            }

            // 处理通配符 *
            if (partStr == "*")
            {
                return AlwaysTrueMatcher.Instance;
            }

            // 处理 ? 字符（用于日和星期字段，表示不指定值）
            if (partStr == "?")
            {
                return AlwaysTrueMatcher.Instance;
            }

            // 创建布尔数组匹配器
            var matcher = new BoolArrayMatcher(max - min + 1);

            // 处理逗号分隔的多个值
            string[] parts = partStr.Split(',');
            foreach (string part in parts)
            {
                ParsePart(part, min, max, matcher);
            }

            return matcher;
        }

        /// <summary>
        /// 解析单个部分
        /// </summary>
        /// <param name="part">部分字符串</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="matcher">匹配器</param>
        private static void ParsePart(string part, int min, int max, BoolArrayMatcher matcher)
        {
            // 处理范围表达式，如 1-5
            if (part.Contains('-'))
            {
                string[] rangeParts = part.Split('-');
                if (rangeParts.Length != 2)
                {
                    throw new CronException("Invalid range format: {0}", part);
                }

                int start = ParseValue(rangeParts[0], min, max);
                int end = ParseValue(rangeParts[1], min, max);

                if (start > end)
                {
                    throw new CronException("Invalid range: {0} > {1}", start, end);
                }

                // 处理步长，如 1-5/2
                if (rangeParts[1].Contains('/'))
                {
                    string[] stepParts = rangeParts[1].Split('/');
                    end = ParseValue(stepParts[0], min, max);
                    int step = ParseInt(stepParts[1], 1, int.MaxValue);

                    for (int i = start; i <= end; i += step)
                    {
                        matcher.SetMatch(i - min);
                    }
                }
                else
                {
                    for (int i = start; i <= end; i++)
                    {
                        matcher.SetMatch(i - min);
                    }
                }
            }
            // 处理步长表达式，如 */5
            else if (part.Contains('/'))
            {
                string[] stepParts = part.Split('/');
                if (stepParts.Length != 2)
                {
                    throw new CronException("Invalid step format: {0}", part);
                }

                int step = ParseInt(stepParts[1], 1, int.MaxValue);

                for (int i = min; i <= max; i += step)
                {
                    matcher.SetMatch(i - min);
                }
            }
            // 处理单个值
            else
            {
                int value = ParseValue(part, min, max);
                matcher.SetMatch(value - min);
            }
        }

        /// <summary>
        /// 解析值（支持数字、特殊字符和星期英文名称）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>解析的值</returns>
        private static int ParseValue(string str, int min, int max)
        {
            // 处理 L 字符（表示当月最后一天）
            if (str == "L")
            {
                return max;
            }

            // 处理星期的英文名称
            if (max == 7) // 星期字段，范围是 0-7
            {
                switch (str.ToLower())
                {
                    case "sun": return 7; // 周日可以是 0 或 7
                    case "mon": return 1;
                    case "tue": return 2;
                    case "wed": return 3;
                    case "thu": return 4;
                    case "fri": return 5;
                    case "sat": return 6;
                }
            }

            // 处理月份的英文名称
            if (min == 1 && max == 12) // 月份字段，范围是 1-12
            {
                switch (str.ToLower())
                {
                    case "jan": return 1;
                    case "feb": return 2;
                    case "mar": return 3;
                    case "apr": return 4;
                    case "may": return 5;
                    case "jun": return 6;
                    case "jul": return 7;
                    case "aug": return 8;
                    case "sep": return 9;
                    case "oct": return 10;
                    case "nov": return 11;
                    case "dec": return 12;
                }
            }

            // 解析普通整数
            return ParseInt(str, min, max);
        }

        /// <summary>
        /// 解析整数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns>解析的整数</returns>
        private static int ParseInt(string str, int min, int max)
        {
            if (!int.TryParse(str, out int value))
            {
                throw new CronException("Invalid number: {0}", str);
            }

            if (value < min || value > max)
            {
                throw new CronException("Value {0} out of range [{1}, {2}]", value, min, max);
            }

            return value;
        }
    }
}