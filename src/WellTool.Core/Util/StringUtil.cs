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
    public static class StringUtil
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
        /// 替换字符串中的指定内容（忽略大小写）
        /// </summary>
        /// <param name="str">要替换的字符串</param>
        /// <param name="oldValue">要替换的旧值</param>
        /// <param name="newValue">替换的新值</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceIgnoreCase(string str, string oldValue, string newValue)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return Regex.Replace(str, Regex.Escape(oldValue), newValue, RegexOptions.IgnoreCase);
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
        /// 去除字符串首部的空白字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>去除首部空白字符后的字符串</returns>
        public static string TrimStart(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.TrimStart();
        }

        /// <summary>
        /// 去除字符串尾部的空白字符
        /// </summary>
        /// <param name="str">要处理的字符串</param>
        /// <returns>去除尾部空白字符后的字符串</returns>
        public static string TrimEnd(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            return str.TrimEnd();
        }

        /// <summary>
        /// 检查字符串是否以指定的前缀开始
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns>如果字符串以指定的前缀开始，则返回 true；否则返回 false</returns>
        public static bool StartsWith(string str, string prefix)
        {
            if (IsEmpty(str) || IsEmpty(prefix))
            {
                return false;
            }

            return str.StartsWith(prefix);
        }

        /// <summary>
        /// 检查字符串是否以指定的前缀开始（忽略大小写）
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns>如果字符串以指定的前缀开始，则返回 true；否则返回 false</returns>
        public static bool StartsWithIgnoreCase(string str, string prefix)
        {
            if (IsEmpty(str) || IsEmpty(prefix))
            {
                return false;
            }

            return str.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 检查字符串是否以指定的后缀结束
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="suffix">后缀</param>
        /// <returns>如果字符串以指定的后缀结束，则返回 true；否则返回 false</returns>
        public static bool EndsWith(string str, string suffix)
        {
            if (IsEmpty(str) || IsEmpty(suffix))
            {
                return false;
            }

            return str.EndsWith(suffix);
        }

        /// <summary>
        /// 检查字符串是否以指定的后缀结束（忽略大小写）
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="suffix">后缀</param>
        /// <returns>如果字符串以指定的后缀结束，则返回 true；否则返回 false</returns>
        public static bool EndsWithIgnoreCase(string str, string suffix)
        {
            if (IsEmpty(str) || IsEmpty(suffix))
            {
                return false;
            }

            return str.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 检查字符串是否包含指定的子字符串
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="substring">子字符串</param>
        /// <returns>如果字符串包含指定的子字符串，则返回 true；否则返回 false</returns>
        public static bool Contains(string str, string substring)
        {
            if (IsEmpty(str) || IsEmpty(substring))
            {
                return false;
            }

            return str.Contains(substring);
        }

        /// <summary>
        /// 检查字符串是否包含指定的子字符串（忽略大小写）
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <param name="substring">子字符串</param>
        /// <returns>如果字符串包含指定的子字符串，则返回 true；否则返回 false</returns>
        public static bool ContainsIgnoreCase(string str, string substring)
        {
            if (IsEmpty(str) || IsEmpty(substring))
            {
                return false;
            }

            return str.IndexOf(substring, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// 获取字符串中指定子字符串的索引
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="substring">要查找的子字符串</param>
        /// <returns>子字符串在字符串中的索引，如果没有找到则返回 -1</returns>
        public static int IndexOf(string str, string substring)
        {
            if (IsEmpty(str) || IsEmpty(substring))
            {
                return -1;
            }

            return str.IndexOf(substring);
        }

        /// <summary>
        /// 获取字符串中指定子字符串的索引（忽略大小写）
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="substring">要查找的子字符串</param>
        /// <returns>子字符串在字符串中的索引，如果没有找到则返回 -1</returns>
        public static int IndexOfIgnoreCase(string str, string substring)
        {
            if (IsEmpty(str) || IsEmpty(substring))
            {
                return -1;
            }

            return str.IndexOf(substring, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取字符串中指定子字符串最后出现的索引
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="substring">要查找的子字符串</param>
        /// <returns>子字符串在字符串中最后出现的索引，如果没有找到则返回 -1</returns>
        public static int LastIndexOf(string str, string substring)
        {
            if (IsEmpty(str) || IsEmpty(substring))
            {
                return -1;
            }

            return str.LastIndexOf(substring);
        }

        /// <summary>
        /// 获取字符串中指定子字符串最后出现的索引（忽略大小写）
        /// </summary>
        /// <param name="str">要搜索的字符串</param>
        /// <param name="substring">要查找的子字符串</param>
        /// <returns>子字符串在字符串中最后出现的索引，如果没有找到则返回 -1</returns>
        public static int LastIndexOfIgnoreCase(string str, string substring)
        {
            if (IsEmpty(str) || IsEmpty(substring))
            {
                return -1;
            }

            return str.LastIndexOf(substring, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 重复字符串指定的次数
        /// </summary>
        /// <param name="str">要重复的字符串</param>
        /// <param name="count">重复的次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(string str, int count)
        {
            if (IsEmpty(str) || count <= 0)
            {
                return Empty;
            }

            return new StringBuilder(str.Length * count).Insert(0, str, count).ToString();
        }

        /// <summary>
        /// 用指定的字符填充字符串到指定的长度
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <param name="isLeftPad">是否在左侧填充</param>
        /// <returns>填充后的字符串</returns>
        public static string Pad(string str, int length, char padChar, bool isLeftPad)
        {
            if (str == null)
            {
                str = Empty;
            }

            if (str.Length >= length)
            {
                return str;
            }

            int padLength = length - str.Length;
            string pad = new string(padChar, padLength);

            return isLeftPad ? pad + str : str + pad;
        }

        /// <summary>
        /// 在字符串左侧填充指定的字符到指定的长度
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <returns>填充后的字符串</returns>
        public static string PadLeft(string str, int length, char padChar)
        {
            return Pad(str, length, padChar, true);
        }

        /// <summary>
        /// 在字符串右侧填充指定的字符到指定的长度
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <returns>填充后的字符串</returns>
        public static string PadRight(string str, int length, char padChar)
        {
            return Pad(str, length, padChar, false);
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="str">要反转的字符串</param>
        /// <returns>反转后的字符串</returns>
        public static string Reverse(string str)
        {
            if (IsEmpty(str))
            {
                return str;
            }

            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 计算字符串的长度（考虑中文字符）
        /// </summary>
        /// <param name="str">要计算长度的字符串</param>
        /// <returns>字符串的长度</returns>
        public static int Length(string str)
        {
            if (IsEmpty(str))
            {
                return 0;
            }

            return str.Length;
        }

        /// <summary>
        /// 截取字符串，超过指定长度时添加省略号
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">省略号</param>
        /// <returns>截取后的字符串</returns>
        public static string Truncate(string str, int maxLength, string suffix = "...")
        {
            if (IsEmpty(str))
            {
                return str;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Substring(0, maxLength - suffix.Length) + suffix;
        }
    }
}
