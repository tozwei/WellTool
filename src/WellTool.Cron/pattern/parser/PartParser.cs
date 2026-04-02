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

using System.Collections.Generic;
using WellTool.Cron.Pattern.Matcher;

namespace WellTool.Cron.Pattern.Parser
{
    /// <summary>
    /// Cron表达式各部分的解析器
    /// </summary>
    public class PartParser
    {
        private readonly Part part;
        private readonly int min;
        private readonly int max;

        /// <summary>
        /// 创建指定部分的解析器
        /// </summary>
        /// <param name="part">部分</param>
        /// <returns>解析器</returns>
        public static PartParser Of(Pattern.Part part)
        {
            switch (part)
            {
                case Pattern.Part.SECOND:
                    return new PartParser(part, 0, 59);
                case Pattern.Part.MINUTE:
                    return new PartParser(part, 0, 59);
                case Pattern.Part.HOUR:
                    return new PartParser(part, 0, 23);
                case Pattern.Part.DAY_OF_MONTH:
                    return new PartParser(part, 1, 31);
                case Pattern.Part.MONTH:
                    return new PartParser(part, 1, 12);
                case Pattern.Part.DAY_OF_WEEK:
                    return new PartParser(part, 0, 7);
                case Pattern.Part.YEAR:
                    return new PartParser(part, 1970, 2099);
                default:
                    throw new CronException("Unknown part: {0}", part);
            }
        }

        /// <summary>
        /// 解析表达式（静态方法，兼容旧代码）
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

            // 处理 ? 字符
            if (partStr == "?")
            {
                return AlwaysTrueMatcher.Instance;
            }

            // 创建布尔数组匹配器
            var matcher = new BoolArrayMatcher(max - min + 1);

            // 处理逗号分隔的多个值
            string[] parts = partStr.Split(',');
            foreach (string p in parts)
            {
                ParsePart(p, min, max, matcher);
            }

            return matcher;
        }

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private PartParser(Part part, int min, int max)
        {
            this.part = part;
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// 解析表达式
        /// </summary>
        /// <param name="partStr">部分字符串</param>
        /// <returns>匹配器</returns>
        public PartMatcher Parse(string partStr)
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

            // 年份特殊处理
            if (part == Pattern.Part.YEAR)
            {
                return ParseYearValue(partStr);
            }

            // 日特殊处理
            if (part == Pattern.Part.DAY_OF_MONTH)
            {
                return ParseDayOfMonthValue(partStr);
            }

            // 创建布尔数组匹配器
            var matcher = new BoolArrayMatcher(max - min + 1);

            // 处理逗号分隔的多个值
            string[] parts = partStr.Split(',');
            foreach (string p in parts)
            {
                ParsePart(p, matcher);
            }

            return matcher;
        }

        /// <summary>
        /// 解析年份值
        /// </summary>
        private PartMatcher ParseYearValue(string partStr)
        {
            var values = new List<int>();

            string[] parts = partStr.Split(',');
            foreach (string p in parts)
            {
                ParseYearPart(p, values);
            }

            return new YearValueMatcher(values);
        }

        /// <summary>
        /// 解析年份部分
        /// </summary>
        private void ParseYearPart(string part, List<int> values)
        {
            // 处理步长表达式，如 */5
            if (part.Contains('/'))
            {
                string[] stepParts = part.Split('/');
                string rangeStr = stepParts[0];
                int step = ParseInt(stepParts[1], 1, int.MaxValue);

                int start, end;
                if (rangeStr == "*")
                {
                    start = min;
                    end = max;
                }
                else if (rangeStr.Contains('-'))
                {
                    string[] rangeParts = rangeStr.Split('-');
                    start = ParseInt(rangeParts[0], min, max);
                    end = ParseInt(rangeParts[1], min, max);
                }
                else
                {
                    start = ParseInt(rangeStr, min, max);
                    end = max;
                }

                for (int i = start; i <= end; i += step)
                {
                    values.Add(i);
                }
            }
            // 处理范围表达式，如 2020-2030
            else if (part.Contains('-'))
            {
                string[] rangeParts = part.Split('-');
                int start = ParseInt(rangeParts[0], min, max);
                int end = ParseInt(rangeParts[1], min, max);

                for (int i = start; i <= end; i++)
                {
                    values.Add(i);
                }
            }
            // 处理单个值
            else
            {
                values.Add(ParseInt(part, min, max));
            }
        }

        /// <summary>
        /// 解析日值
        /// </summary>
        private PartMatcher ParseDayOfMonthValue(string partStr)
        {
            var values = new List<int>();

            string[] parts = partStr.Split(',');
            foreach (string p in parts)
            {
                ParseDayOfMonthPart(p, values);
            }

            return new DayOfMonthMatcher(values);
        }

        /// <summary>
        /// 解析日部分
        /// </summary>
        private void ParseDayOfMonthPart(string part, List<int> values)
        {
            // 处理 L 字符（表示当月最后一天）
            if (part == "L")
            {
                values.Add(32); // 使用32表示最后一天
                return;
            }

            // 处理步长表达式，如 */5
            if (part.Contains('/'))
            {
                string[] stepParts = part.Split('/');
                string rangeStr = stepParts[0];
                int step = ParseInt(stepParts[1], 1, int.MaxValue);

                int start, end;
                if (rangeStr == "*")
                {
                    start = min;
                    end = max;
                }
                else if (rangeStr.Contains('-'))
                {
                    string[] rangeParts = rangeStr.Split('-');
                    start = ParseInt(rangeParts[0], min, max);
                    end = ParseInt(rangeParts[1], min, max);
                }
                else
                {
                    start = ParseInt(rangeStr, min, max);
                    end = max;
                }

                for (int i = start; i <= end; i += step)
                {
                    values.Add(i);
                }
            }
            // 处理范围表达式，如 1-5
            else if (part.Contains('-'))
            {
                string[] rangeParts = part.Split('-');
                int start = ParseInt(rangeParts[0], min, max);
                int end = ParseInt(rangeParts[1], min, max);

                for (int i = start; i <= end; i++)
                {
                    values.Add(i);
                }
            }
            // 处理单个值
            else
            {
                values.Add(ParseValue(part));
            }
        }

        /// <summary>
        /// 解析单个部分（静态方法，兼容旧代码）
        /// </summary>
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

                int start = ParseValueStatic(rangeParts[0], min, max);
                int end = ParseValueStatic(rangeParts[1], min, max);

                if (start > end)
                {
                    throw new CronException("Invalid range: {0} > {1}", start, end);
                }

                for (int i = start; i <= end; i++)
                {
                    matcher.SetMatch(i - min);
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

                string rangeStr = stepParts[0];
                int step = ParseInt(stepParts[1], 1, int.MaxValue);

                int start, end;
                if (rangeStr == "*")
                {
                    start = min;
                    end = max;
                }
                else if (rangeStr.Contains('-'))
                {
                    string[] rangeParts = rangeStr.Split('-');
                    start = ParseValueStatic(rangeParts[0], min, max);
                    end = ParseValueStatic(rangeParts[1], min, max);
                }
                else
                {
                    start = ParseValueStatic(rangeStr, min, max);
                    end = max;
                }

                for (int i = start; i <= end; i += step)
                {
                    matcher.SetMatch(i - min);
                }
            }
            // 处理单个值
            else
            {
                int value = ParseValueStatic(part, min, max);
                matcher.SetMatch(value - min);
            }
        }

        /// <summary>
        /// 解析单个部分
        /// </summary>
        private void ParsePart(string part, BoolArrayMatcher matcher)
        {
            // 处理范围表达式，如 1-5
            if (part.Contains('-'))
            {
                string[] rangeParts = part.Split('-');
                if (rangeParts.Length != 2)
                {
                    throw new CronException("Invalid range format: {0}", part);
                }

                int start = ParseValue(rangeParts[0]);
                int end = ParseValue(rangeParts[1]);

                if (start > end)
                {
                    throw new CronException("Invalid range: {0} > {1}", start, end);
                }

                for (int i = start; i <= end; i++)
                {
                    matcher.SetMatch(i - min);
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

                string rangeStr = stepParts[0];
                int step = ParseInt(stepParts[1], 1, int.MaxValue);

                int start, end;
                if (rangeStr == "*")
                {
                    start = min;
                    end = max;
                }
                else if (rangeStr.Contains('-'))
                {
                    string[] rangeParts = rangeStr.Split('-');
                    start = ParseValue(rangeParts[0]);
                    end = ParseValue(rangeParts[1]);
                }
                else
                {
                    start = ParseValue(rangeStr);
                    end = max;
                }

                for (int i = start; i <= end; i += step)
                {
                    matcher.SetMatch(i - min);
                }
            }
            // 处理单个值
            else
            {
                int value = ParseValue(part);
                matcher.SetMatch(value - min);
            }
        }

        /// <summary>
        /// 解析值（支持数字、特殊字符和星期英文名称）
        /// </summary>
        private int ParseValue(string str)
        {
            // 处理 L 字符（表示当月最后一天）
            if (str == "L")
            {
                return max;
            }

            // 处理星期的英文名称
            if (part == Pattern.Part.DAY_OF_WEEK)
            {
                switch (str.ToLower())
                {
                    case "sun": return 0;
                    case "mon": return 1;
                    case "tue": return 2;
                    case "wed": return 3;
                    case "thu": return 4;
                    case "fri": return 5;
                    case "sat": return 6;
                }

                if (str == "7")
                {
                    return 0;
                }
            }

            // 处理月份的英文名称
            if (part == Pattern.Part.MONTH)
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

            return ParseInt(str, min, max);
        }

        /// <summary>
        /// 解析值（静态方法，兼容旧代码）
        /// </summary>
        private static int ParseValueStatic(string str, int min, int max)
        {
            // 处理 L 字符（表示当月最后一天）
            if (str == "L")
            {
                return max;
            }

            // 处理星期的英文名称
            if (min == 0 && max == 7)
            {
                switch (str.ToLower())
                {
                    case "sun": return 0;
                    case "mon": return 1;
                    case "tue": return 2;
                    case "wed": return 3;
                    case "thu": return 4;
                    case "fri": return 5;
                    case "sat": return 6;
                }

                if (str == "7")
                {
                    return 0;
                }
            }

            // 处理月份的英文名称
            if (min == 1 && max == 12)
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

            return ParseInt(str, min, max);
        }

        /// <summary>
        /// 解析整数
        /// </summary>
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