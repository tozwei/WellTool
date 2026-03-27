using System;
using System.Text;
using System.Text.RegularExpressions;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class StringUtil
    {
        /// <summary>
        /// 空字符串
        /// </summary>
        public const string EMPTY = "";

        /// <summary>
        /// 空格
        /// </summary>
        public const string SPACE = " ";

        /// <summary>
        /// 制表符
        /// </summary>
        public const string TAB = "\t";

        /// <summary>
        /// 换行符
        /// </summary>
        public const string NEWLINE = "\n";

        /// <summary>
        /// 回车符
        /// </summary>
        public const string CARRIAGE_RETURN = "\r";

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 检查字符串是否不为空
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否不为空</returns>
        public static bool IsNotEmpty(string str)
        {
            return !IsEmpty(str);
        }

        /// <summary>
        /// 检查字符串是否为空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否为空白</returns>
        public static bool IsBlank(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 检查字符串是否不为空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>是否不为空白</returns>
        public static bool IsNotBlank(string str)
        {
            return !IsBlank(str);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startIndex">开始索引</param>
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
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Substring(string str, int startIndex, int length)
        {
            if (IsEmpty(str))
            {
                return str;
            }
            if (startIndex >= str.Length)
            {
                return EMPTY;
            }
            if (startIndex + length > str.Length)
            {
                length = str.Length - startIndex;
            }
            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="values">值数组</param>
        /// <returns>连接后的字符串</returns>
        public static string Join(string separator, params object[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="values">值数组</param>
        /// <returns>连接后的字符串</returns>
        public static string Join(string separator, params string[] values)
        {
            return string.Join(separator, values);
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(string str, int count)
        {
            if (IsEmpty(str) || count <= 0)
            {
                return EMPTY;
            }
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <param name="isLeftPad">是否左填充</param>
        /// <returns>填充后的字符串</returns>
        public static string Pad(string str, int length, char padChar, bool isLeftPad = true)
        {
            str = str ?? EMPTY;
            if (str.Length >= length)
            {
                return str;
            }
            var padLength = length - str.Length;
            var padStr = Repeat(padChar.ToString(), padLength);
            return isLeftPad ? padStr + str : str + padStr;
        }

        /// <summary>
        /// 左填充字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <returns>填充后的字符串</returns>
        public static string PadLeft(string str, int length, char padChar = ' ')
        {
            return Pad(str, length, padChar, true);
        }

        /// <summary>
        /// 右填充字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="length">目标长度</param>
        /// <param name="padChar">填充字符</param>
        /// <returns>填充后的字符串</returns>
        public static string PadRight(string str, int length, char padChar = ' ')
        {
            return Pad(str, length, padChar, false);
        }

        /// <summary>
        /// 去除字符串首尾空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>去除空白后的字符串</returns>
        public static string Trim(string str)
        {
            return str?.Trim() ?? EMPTY;
        }

        /// <summary>
        /// 去除字符串首部空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>去除首部空白后的字符串</returns>
        public static string TrimStart(string str)
        {
            return str?.TrimStart() ?? EMPTY;
        }

        /// <summary>
        /// 去除字符串尾部空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>去除尾部空白后的字符串</returns>
        public static string TrimEnd(string str)
        {
            return str?.TrimEnd() ?? EMPTY;
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="oldValue">旧值</param>
        /// <param name="newValue">新值</param>
        /// <returns>替换后的字符串</returns>
        public static string Replace(string str, string oldValue, string newValue)
        {
            return str?.Replace(oldValue, newValue) ?? EMPTY;
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的数组</returns>
        public static string[] Split(string str, params char[] separator)
        {
            return str?.Split(separator, StringSplitOptions.None) ?? new string[0];
        }

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="separator">分隔符</param>
        /// <returns>分割后的数组</returns>
        public static string[] Split(string str, string separator)
        {
            return str?.Split(new[] { separator }, StringSplitOptions.None) ?? new string[0];
        }

        /// <summary>
        /// 检查字符串是否包含指定内容
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="value">要检查的内容</param>
        /// <returns>是否包含</returns>
        public static bool Contains(string str, string value)
        {
            return str?.Contains(value) ?? false;
        }

        /// <summary>
        /// 检查字符串是否以指定内容开头
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="value">要检查的内容</param>
        /// <returns>是否以指定内容开头</returns>
        public static bool StartsWith(string str, string value)
        {
            return str?.StartsWith(value) ?? false;
        }

        /// <summary>
        /// 检查字符串是否以指定内容结尾
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="value">要检查的内容</param>
        /// <returns>是否以指定内容结尾</returns>
        public static bool EndsWith(string str, string value)
        {
            return str?.EndsWith(value) ?? false;
        }

        /// <summary>
        /// 查找字符串第一次出现的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="value">要查找的内容</param>
        /// <returns>第一次出现的位置，未找到返回-1</returns>
        public static int IndexOf(string str, string value)
        {
            return str?.IndexOf(value) ?? -1;
        }

        /// <summary>
        /// 查找字符串最后一次出现的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="value">要查找的内容</param>
        /// <returns>最后一次出现的位置，未找到返回-1</returns>
        public static int LastIndexOf(string str, string value)
        {
            return str?.LastIndexOf(value) ?? -1;
        }

        /// <summary>
        /// 将字符串转换为小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>小写字符串</returns>
        public static string ToLower(string str)
        {
            return str?.ToLower() ?? EMPTY;
        }

        /// <summary>
        /// 将字符串转换为大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>大写字符串</returns>
        public static string ToUpper(string str)
        {
            return str?.ToUpper() ?? EMPTY;
        }

        /// <summary>
        /// 检查字符串是否匹配正则表达式
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>是否匹配</returns>
        public static bool Matches(string str, string pattern)
        {
            return !IsEmpty(str) && Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 使用正则表达式替换字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <param name="replacement">替换内容</param>
        /// <returns>替换后的字符串</returns>
        public static string ReplaceAll(string str, string pattern, string replacement)
        {
            return str != null ? Regex.Replace(str, pattern, replacement) : EMPTY;
        }

        /// <summary>
        /// 清理空白字符
        /// </summary>
        /// <param name="str">字符串</param>
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
        /// 截取字符串（简写）
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="startIndex">开始索引</param>
        /// <param name="length">长度</param>
        /// <returns>截取后的字符串</returns>
        public static string Sub(string str, int startIndex, int length)
        {
            return Substring(str, startIndex, length);
        }

        /// <summary>
        /// 忽略大小写比较字符串
        /// </summary>
        /// <param name="str1">字符串1</param>
        /// <param name="str2">字符串2</param>
        /// <returns>是否相等</returns>
        public static bool EqualsIgnoreCase(string str1, string str2)
        {
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取字符串长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>长度</returns>
        public static int Length(string str)
        {
            return str?.Length ?? 0;
        }

        /// <summary>
        /// 移除前缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns>移除前缀后的字符串</returns>
        public static string RemovePrefix(string str, string prefix)
        {
            if (IsEmpty(str) || IsEmpty(prefix))
            {
                return str;
            }
            if (StartsWith(str, prefix))
            {
                return Substring(str, prefix.Length);
            }
            return str;
        }

        /// <summary>
        /// 移除后缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="suffix">后缀</param>
        /// <returns>移除后缀后的字符串</returns>
        public static string RemoveSuffix(string str, string suffix)
        {
            if (IsEmpty(str) || IsEmpty(suffix))
            {
                return str;
            }
            if (EndsWith(str, suffix))
            {
                return Substring(str, 0, str.Length - suffix.Length);
            }
            return str;
        }
    }
}