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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StrUtil
    {
        /// <summary>
        /// 空字符串
        /// </summary>
        public static readonly string Empty = string.Empty;

        /// <summary>
        /// 空格字符串
        /// </summary>
        public static readonly string Space = " ";

        /// <summary>
        /// 检查字符串是否为 null 或空字符串
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果字符串为 null 或空字符串，则返回 true；否则返回 false</returns>
        public static bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 检查字符串是否不为 null 且不为空字符串
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果字符串不为 null 且不为空字符串，则返回 true；否则返回 false</returns>
        public static bool IsNotEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 检查字符串是否为 null、空字符串或仅包含空白字符
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果字符串为 null、空字符串或仅包含空白字符，则返回 true；否则返回 false</returns>
        public static bool IsBlank(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 检查字符串是否不为 null、不为空字符串且不仅包含空白字符
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果字符串不为 null、不为空字符串且不仅包含空白字符，则返回 true；否则返回 false</returns>
        public static bool IsNotBlank(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 截取字符串的指定部分
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="startIndex">起始索引（包含）</param>
        /// <param name="length">截取的长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string str, int startIndex, int length)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            int endIndex = startIndex + length;
            if (endIndex > str.Length)
            {
                endIndex = str.Length;
            }

            return str.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 截取字符串从指定索引到末尾
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="startIndex">起始索引（包含）</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string str, int startIndex)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.Substring(startIndex);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的字符串数组</returns>
        public static string[] Split(string str, params char[] separator)
        {
            if (IsEmpty(str))
            {
                return new string[0];
            }

            return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的字符串数组</returns>
        public static string[] Split(string str, string separator)
        {
            if (IsEmpty(str))
            {
                return new string[0];
            }

            return str.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="limit">分割限制</param>
        /// <param name="trim">是否修剪每个分割后的字符串</param>
        /// <param name="ignoreEmpty">是否忽略空字符串</param>
        /// <returns>分割后的字符串列表</returns>
        public static List<string> Split(string str, char separator, int limit, bool trim, bool ignoreEmpty)
        {
            if (IsEmpty(str))
            {
                return new List<string>();
            }

            var result = new List<string>();
            var parts = str.Split(separator);
            
            for (int i = 0; i < parts.Length; i++)
            {
                if (result.Count >= limit - 1 && limit > 0)
                {
                    // 剩余部分作为一个整体
                    var remaining = string.Join(separator.ToString(), parts.Skip(i));
                    if (trim) remaining = remaining.Trim();
                    if (!ignoreEmpty || !IsEmpty(remaining))
                    {
                        result.Add(remaining);
                    }
                    break;
                }

                var part = parts[i];
                if (trim) part = part.Trim();
                if (!ignoreEmpty || !IsEmpty(part))
                {
                    result.Add(part);
                }
            }

            return result;
        }

        /// <summary>
        /// 分割字符串为long数组
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的long数组</returns>
        public static long[] SplitToLong(string str, char separator)
        {
            if (IsEmpty(str))
            {
                return new long[0];
            }

            return Split(str, separator)
                .Select(long.Parse)
                .ToArray();
        }

        /// <summary>
        /// 分割字符串为long数组
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的long数组</returns>
        public static long[] SplitToLong(string str, string separator)
        {
            if (IsEmpty(str))
            {
                return new long[0];
            }

            return Split(str, separator)
                .Select(long.Parse)
                .ToArray();
        }

        /// <summary>
        /// 分割字符串为int数组
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的int数组</returns>
        public static int[] SplitToInt(string str, char separator)
        {
            if (IsEmpty(str))
            {
                return new int[0];
            }

            return Split(str, separator)
                .Select(int.Parse)
                .ToArray();
        }

        /// <summary>
        /// 分割字符串为int数组
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的int数组</returns>
        public static int[] SplitToInt(string str, string separator)
        {
            if (IsEmpty(str))
            {
                return new int[0];
            }

            return Split(str, separator)
                .Select(int.Parse)
                .ToArray();
        }

        /// <summary>
        /// 连接字符串数组
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="values">要连接的字符串数组</param>
        /// <returns>连接后的字符串</returns>
        public static string Join(string separator, params string[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 连接集合中的字符串
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="values">要连接的字符串集合</param>
        /// <returns>连接后的字符串</returns>
        public static string Join(string separator, IEnumerable<string> values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 替换字符串中的指定内容
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <param name="oldValue">要替换的旧值</param>
        /// <param name="newValue">替换的新值</param>
        /// <returns>替换后的字符串</returns>
        public static string Replace(string str, string oldValue, string newValue)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.Replace(oldValue, newValue);
        }

        /// <summary>
        /// 按代码点替换字符串
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <param name="replacement">替换字符</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceByCodePoint(string str, int startIndex, int endIndex, char replacement)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            var chars = str.ToCharArray();
            for (int i = startIndex; i < endIndex && i < chars.Length; i++)
            {
                chars[i] = replacement;
            }
            return new string(chars);
        }

        /// <summary>
        /// 转换字符串为小写
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的小写字符串</returns>
        public static string ToLower(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.ToLower();
        }

        /// <summary>
        /// 转换字符串为大写
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的大写字符串</returns>
        public static string ToUpper(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.ToUpper();
        }

        /// <summary>
        /// 去除字符串首尾的空白字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>去除首尾空白字符后的字符串</returns>
        public static string Trim(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.Trim();
        }

        /// <summary>
        /// 清理字符串中的空白字符
        /// </summary>
        /// <param name="str">要清理的字符串</param>
        /// <returns>清理后的字符串</returns>
        public static string CleanBlank(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return Regex.Replace(str, @"\s+", "");
        }

        /// <summary>
        /// 截取字符串，按指定长度分割
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">每个截取部分的长度</param>
        /// <returns>截取后的字符串数组</returns>
        public static string[] Cut(string str, int length)
        {
            if (IsEmpty(str))
            {
                return new string[0];
            }

            var result = new List<string>();
            for (int i = 0; i < str.Length; i += length)
            {
                if (i + length <= str.Length)
                {
                    result.Add(str.Substring(i, length));
                }
                else
                {
                    result.Add(str.Substring(i));
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="map">参数映射</param>
        /// <returns>格式化后的字符串</returns>
        public static string Format(string template, Dictionary<string, object> map)
        {
            if (IsEmpty(template) || map == null)
            {
                return template;
            }

            var result = template;
            foreach (var entry in map)
            {
                var placeholder = "{" + entry.Key + "}";
                if (entry.Value != null)
                {
                    result = result.Replace(placeholder, entry.Value.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// 去除字符串首尾的指定字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="prefix">前缀字符</param>
        /// <param name="suffix">后缀字符</param>
        /// <returns>处理后的字符串</returns>
        public static string Strip(string str, string prefix, string suffix)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            if (!IsEmpty(prefix) && str.StartsWith(prefix))
            {
                str = str.Substring(prefix.Length);
            }

            if (!IsEmpty(suffix) && str.EndsWith(suffix))
            {
                str = str.Substring(0, str.Length - suffix.Length);
            }

            return str;
        }

        /// <summary>
        /// 去除字符串首尾的指定字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <param name="chars">要去除的字符</param>
        /// <returns>处理后的字符串</returns>
        public static string Strip(string str, string chars)
        {
            return Strip(str, chars, chars);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string UpperFirst(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="sb">要处理的StringBuilder</param>
        /// <returns>处理后的字符串</returns>
        public static string UpperFirst(StringBuilder sb)
        {
            if (sb == null || sb.Length == 0)
            {
                return string.Empty;
            }

            return UpperFirst(sb.ToString());
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string LowerFirst(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="sb">要处理的StringBuilder</param>
        /// <returns>处理后的字符串</returns>
        public static string LowerFirst(StringBuilder sb)
        {
            if (sb == null || sb.Length == 0)
            {
                return string.Empty;
            }

            return LowerFirst(sb.ToString());
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>截取后的字符串</returns>
        public static string Sub(string str, int startIndex, int endIndex)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            if (startIndex < 0)
            {
                startIndex = str.Length + startIndex;
            }

            if (endIndex < 0)
            {
                endIndex = str.Length + endIndex;
            }

            if (startIndex >= endIndex)
            {
                return string.Empty;
            }

            if (startIndex < 0)
            {
                startIndex = 0;
            }

            if (endIndex > str.Length)
            {
                endIndex = str.Length;
            }

            return str.Substring(startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 截取字符串从指定索引到末尾
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="startIndex">起始索引</param>
        /// <returns>截取后的字符串</returns>
        public static string Sub(string str, int startIndex)
        {
            return Sub(str, startIndex, str.Length);
        }

        /// <summary>
        /// 截取字符串中指定字符之前的部分
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="fromEnd">是否从末尾开始查找</param>
        /// <returns>截取后的字符串</returns>
        public static string SubBefore(string str, string separator, bool fromEnd)
        {
            if (IsEmpty(str) || IsEmpty(separator))
            {
                return str;
            }

            int index = fromEnd ? str.LastIndexOf(separator) : str.IndexOf(separator);
            if (index == -1)
            {
                return str;
            }

            return str.Substring(0, index);
        }

        /// <summary>
        /// 截取字符串中指定字符之前的部分
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="fromEnd">是否从末尾开始查找</param>
        /// <returns>截取后的字符串</returns>
        public static string SubBefore(string str, char separator, bool fromEnd)
        {
            return SubBefore(str, separator.ToString(), fromEnd);
        }

        /// <summary>
        /// 截取字符串中指定字符之后的部分
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="fromEnd">是否从末尾开始查找</param>
        /// <returns>截取后的字符串</returns>
        public static string SubAfter(string str, string separator, bool fromEnd)
        {
            if (IsEmpty(str) || IsEmpty(separator))
            {
                return string.Empty;
            }

            int index = fromEnd ? str.LastIndexOf(separator) : str.IndexOf(separator);
            if (index == -1)
            {
                return string.Empty;
            }

            return str.Substring(index + separator.Length);
        }

        /// <summary>
        /// 截取字符串中指定字符之后的部分
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="separator">分隔符</param>
        /// <param name="fromEnd">是否从末尾开始查找</param>
        /// <returns>截取后的字符串</returns>
        public static string SubAfter(string str, char separator, bool fromEnd)
        {
            return SubAfter(str, separator.ToString(), fromEnd);
        }

        /// <summary>
        /// 重复并连接字符串
        /// </summary>
        /// <param name="str">要重复的字符串</param>
        /// <param name="count">重复次数</param>
        /// <param name="separator">连接符</param>
        /// <returns>重复并连接后的字符串</returns>
        public static string RepeatAndJoin(string str, int count, string separator)
        {
            if (count <= 0 || IsEmpty(str))
            {
                return string.Empty;
            }

            var parts = new string[count];
            for (int i = 0; i < count; i++)
            {
                parts[i] = str;
            }

            return string.Join(separator, parts);
        }

        /// <summary>
        /// 限制字符串长度
        /// </summary>
        /// <param name="str">要限制的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>限制后的字符串</returns>
        public static string MaxLength(string str, int maxLength)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Substring(0, maxLength - 3) + "...";
        }

        /// <summary>
        /// 检查字符串是否包含指定的字符
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="chars">要检查的字符</param>
        /// <returns>如果包含则返回true，否则返回false</returns>
        public static bool ContainsAny(string str, params char[] chars)
        {
            if (IsEmpty(str) || chars == null || chars.Length == 0)
            {
                return false;
            }

            foreach (var c in chars)
            {
                if (str.Contains(c))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 检查字符串是否包含指定的子字符串
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="substrings">要检查的子字符串</param>
        /// <returns>如果包含则返回true，否则返回false</returns>
        public static bool ContainsAny(string str, params string[] substrings)
        {
            if (IsEmpty(str) || substrings == null || substrings.Length == 0)
            {
                return false;
            }

            foreach (var substring in substrings)
            {
                if (str.Contains(substring))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 居中字符串
        /// </summary>
        /// <param name="str">要居中的字符串</param>
        /// <param name="length">目标长度</param>
        /// <returns>居中后的字符串</returns>
        public static string Center(string str, int length)
        {
            if (str == null)
            {
                return null;
            }

            if (length <= 0)
            {
                return str;
            }

            int padding = length - str.Length;
            if (padding <= 0)
            {
                return str;
            }

            int leftPadding = padding / 2;
            int rightPadding = padding - leftPadding;

            return new string(' ', leftPadding) + str + new string(' ', rightPadding);
        }

        /// <summary>
        /// 左填充字符串
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <returns>填充后的字符串</returns>
        public static string PadPre(string str, int length, char padChar)
        {
            if (str == null)
            {
                return null;
            }

            if (length <= 0)
            {
                return str;
            }

            // 如果字符串长度超过指定长度，截断它
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }

            int padding = length - str.Length;
            return new string(padChar, padding) + str;
        }

        /// <summary>
        /// 左填充字符串
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padStr">填充字符串</param>
        /// <returns>填充后的字符串</returns>
        public static string PadPre(string str, int length, string padStr)
        {
            if (str == null)
            {
                return null;
            }

            if (length <= 0 || IsEmpty(padStr))
            {
                return str;
            }

            // 如果字符串长度超过指定长度，截断它
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }

            int padding = length - str.Length;
            int padStrLength = padStr.Length;
            int padCount = padding / padStrLength;
            int remainder = padding % padStrLength;

            string paddingStr = string.Concat(Enumerable.Repeat(padStr, padCount)) + padStr.Substring(0, remainder);
            return paddingStr + str;
        }

        /// <summary>
        /// 右填充字符串
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <returns>填充后的字符串</returns>
        public static string PadAfter(string str, int length, char padChar)
        {
            if (str == null)
            {
                return null;
            }

            if (length <= 0)
            {
                return str;
            }

            // 如果字符串长度超过指定长度，截断它
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }

            int padding = length - str.Length;
            return str + new string(padChar, padding);
        }

        /// <summary>
        /// 右填充字符串
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padStr">填充字符串</param>
        /// <returns>填充后的字符串</returns>
        public static string PadAfter(string str, int length, string padStr)
        {
            if (str == null)
            {
                return null;
            }

            if (length <= 0 || IsEmpty(padStr))
            {
                return str;
            }

            // 如果字符串长度超过指定长度，截断它
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }

            int padding = length - str.Length;
            int padStrLength = padStr.Length;
            int padCount = padding / padStrLength;
            int remainder = padding % padStrLength;

            string paddingStr = string.Concat(Enumerable.Repeat(padStr, padCount)) + padStr.Substring(0, remainder);
            return str + paddingStr;
        }

        /// <summary>
        /// 索引格式化字符串
        /// </summary>
        /// <param name="template">模板字符串</param>
        /// <param name="args">参数</param>
        /// <returns>格式化后的字符串</returns>
        public static string IndexedFormat(string template, params object[] args)
        {
            if (IsEmpty(template) || args == null || args.Length == 0)
            {
                return template;
            }

            var result = template;
            for (int i = 0; i < args.Length; i++)
            {
                var placeholder = "{" + i + "}";
                if (args[i] != null)
                {
                    string value = args[i].ToString();
                    // 对数字进行格式化
                    if (args[i] is int || args[i] is long || args[i] is double || args[i] is decimal)
                    {
                        if (double.TryParse(value, out double num))
                        {
                            value = num.ToString("N0");
                        }
                    }
                    result = result.Replace(placeholder, value);
                }
            }
            return result;
        }

        /// <summary>
        /// 检查字符串是否为数字
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是数字则返回true，否则返回false</returns>
        public static bool IsNumeric(string str)
        {
            if (IsEmpty(str))
            {
                return false;
            }

            return double.TryParse(str, out _);
        }
    }
}
