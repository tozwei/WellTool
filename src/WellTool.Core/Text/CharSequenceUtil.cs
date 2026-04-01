using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WellTool.Core.Text
{
    /// <summary>
    /// 字符序列工具类
    /// </summary>
    public class CharSequenceUtil
    {
        /// <summary>
        /// 检查字符序列是否为空
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty(string cs)
        {
            return cs == null || cs.Length == 0;
        }

        /// <summary>
        /// 检查字符序列是否不为空
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>是否不为空</returns>
        public static bool IsNotEmpty(string cs)
        {
            return !IsEmpty(cs);
        }

        /// <summary>
        /// 检查字符序列是否为空白
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>是否为空白</returns>
        public static bool IsBlank(string cs)
        {
            if (IsEmpty(cs))
            {
                return true;
            }
            for (int i = 0; i < cs.Length; i++)
            {
                if (!char.IsWhiteSpace(cs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查字符序列是否不为空白
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>是否不为空白</returns>
        public static bool IsNotBlank(string cs)
        {
            return !IsBlank(cs);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="start">开始位置</param>
        /// <returns>截取后的字符串</returns>
        public static string Sub(string str, int start)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            if (start < 0)
            {
                start = str.Length + start;
            }
            if (start < 0)
            {
                start = 0;
            }
            if (start >= str.Length)
            {
                return string.Empty;
            }
            return str.Substring(start);
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="start">开始位置</param>
        /// <param name="end">结束位置</param>
        /// <returns>截取后的字符串</returns>
        public static string Sub(string str, int start, int end)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            int length = str.Length;
            if (start < 0)
            {
                start = length + start;
            }
            if (end < 0)
            {
                end = length + end;
            }
            if (start < 0)
            {
                start = 0;
            }
            if (end > length)
            {
                end = length;
            }
            if (start > end)
            {
                return string.Empty;
            }
            return str.Substring(start, end - start);
        }

        /// <summary>
        /// 查找字符串第一次出现的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchChar">要查找的字符</param>
        /// <returns>位置，未找到返回 -1</returns>
        public static int IndexOf(string str, char searchChar)
        {
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == searchChar)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 查找字符序列第一次出现的位置
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <param name="searchStr">要查找的字符串</param>
        /// <returns>位置，未找到返回-1</returns>
        public static int IndexOf(string str, string searchStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(searchStr))
            {
                return -1;
            }
            return str.IndexOf(searchStr);
        }

        /// <summary>
        /// 查找字符串最后一次出现的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchChar">要查找的字符</param>
        /// <returns>位置，未找到返回 -1</returns>
        public static int LastIndexOf(string str, char searchChar)
        {
            if (string.IsNullOrEmpty(str))
            {
                return -1;
            }
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == searchChar)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 查找字符串最后一次出现的位置
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchStr">要查找的字符串</param>
        /// <returns>位置，未找到返回 -1</returns>
        public static int LastIndexOf(string str, string searchStr)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(searchStr))
            {
                return -1;
            }
            return str.LastIndexOf(searchStr);
        }

        /// <summary>
        /// 检查字符串是否包含指定字符
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchChar">要查找的字符</param>
        /// <returns>是否包含</returns>
        public static bool Contains(string str, char searchChar)
        {
            return IndexOf(str, searchChar) != -1;
        }

        /// <summary>
        /// 检查字符串是否包含指定字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchStr">要查找的字符串</param>
        /// <returns>是否包含</returns>
        public static bool Contains(string str, string searchStr)
        {
            return IndexOf(str, searchStr) != -1;
        }

        /// <summary>
        /// 检查字符串是否以指定字符串开头
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns>是否以指定字符串开头</returns>
        public static bool StartsWith(string str, string prefix)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(prefix))
            {
                return false;
            }
            if (prefix.Length > str.Length)
            {
                return false;
            }
            for (int i = 0; i < prefix.Length; i++)
            {
                if (str[i] != prefix[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查字符串是否以指定字符串结尾
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="suffix">后缀</param>
        /// <returns>是否以指定字符串结尾</returns>
        public static bool EndsWith(string str, string suffix)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(suffix))
            {
                return false;
            }
            if (suffix.Length > str.Length)
            {
                return false;
            }
            int start = str.Length - suffix.Length;
            for (int i = 0; i < suffix.Length; i++)
            {
                if (str[start + i] != suffix[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较两个字符串是否相等
        /// </summary>
        /// <param name="str1">第一个字符串</param>
        /// <param name="str2">第二个字符串</param>
        /// <returns>是否相等</returns>
        public static bool Equals(string str1, string str2)
        {
            if (ReferenceEquals(str1, str2))
            {
                return true;
            }
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return false;
            }
            if (str1.Length != str2.Length)
            {
                return false;
            }
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>字符串</returns>
        public static string ToString(string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// 转换为小写
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>小写字符串</returns>
        public static string ToLower(string str)
        {
            return str?.ToLower() ?? string.Empty;
        }

        /// <summary>
        /// 转换为大写
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>大写字符串</returns>
        public static string ToUpper(string str)
        {
            return str?.ToUpper() ?? string.Empty;
        }

        /// <summary>
        /// 去除首尾空白
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>去除空白后的字符串</returns>
        public static string Trim(string str)
        {
            return str?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// 去除首部空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>去除首部空白后的字符串</returns>
        public static string TrimStart(string str)
        {
            return str?.TrimStart() ?? string.Empty;
        }

        /// <summary>
        /// 去除尾部空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>去除尾部空白后的字符串</returns>
        public static string TrimEnd(string str)
        {
            return str?.TrimEnd() ?? string.Empty;
        }

        /// <summary>
        /// 清理所有空白字符
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <returns>清理空白后的字符串</returns>
        public static string CleanBlank(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (!char.IsWhiteSpace(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>首字母大写的字符串</returns>
        public static string UpperFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (char.IsUpper(str[0]))
            {
                return str;
            }
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>首字母小写的字符串</returns>
        public static string LowerFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            if (char.IsLower(str[0]))
            {
                return str;
            }
            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>反转后的字符串</returns>
        public static string Reverse(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 重复字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="count">重复次数</param>
        /// <returns>重复后的字符串</returns>
        public static string Repeat(string str, int count)
        {
            if (string.IsNullOrEmpty(str) || count <= 0)
            {
                return string.Empty;
            }
            return string.Concat(Enumerable.Repeat(str, count));
        }

        /// <summary>
        /// 如果末尾没有指定字符串则追加
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="suffix">后缀</param>
        /// <returns>处理后的字符串</returns>
        public static string AppendIfMissing(string str, string suffix)
        {
            if (string.IsNullOrEmpty(str))
            {
                return suffix;
            }
            if (!str.EndsWith(suffix))
            {
                return str + suffix;
            }
            return str;
        }

        /// <summary>
        /// 如果开头没有指定字符串则添加
        /// </summary>
        /// <param name="cs">字符序列</param>
        /// <param name="prefix">前缀</param>
        /// <returns>处理后的字符串</returns>
        public static string PrependIfMissing(string cs, string prefix)
        {
            if (cs == null)
            {
                return prefix;
            }
            string str = cs.ToString();
            if (!str.StartsWith(prefix))
            {
                return prefix + str;
            }
            return str;
        }

        /// <summary>
        /// 移除前缀
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="prefix">前缀</param>
        /// <returns>移除前缀后的字符串</returns>
        public static string RemovePrefix(string str, string prefix)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(prefix))
            {
                return str;
            }
            if (str.StartsWith(prefix))
            {
                return str.Substring(prefix.Length);
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
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(suffix))
            {
                return str;
            }
            if (str.EndsWith(suffix))
            {
                return str.Substring(0, str.Length - suffix.Length);
            }
            return str;
        }

        /// <summary>
        /// 检查是否包含任意一个指定字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchStrs">要查找的字符串数组</param>
        /// <returns>是否包含任意一个</returns>
        public static bool ContainsAny(string str, params string[] searchStrs)
        {
            if (string.IsNullOrEmpty(str) || searchStrs == null || searchStrs.Length == 0)
            {
                return false;
            }
            foreach (string searchStr in searchStrs)
            {
                if (str.Contains(searchStr))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查是否包含所有指定字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="searchStrs">要查找的字符串数组</param>
        /// <returns>是否包含所有</returns>
        public static bool ContainsAll(string str, params string[] searchStrs)
        {
            if (string.IsNullOrEmpty(str) || searchStrs == null || searchStrs.Length == 0)
            {
                return false;
            }
            foreach (string searchStr in searchStrs)
            {
                if (!str.Contains(searchStr))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 比较两个字符串是否相等（忽略大小写）
        /// </summary>
        /// <param name="str1">第一个字符串</param>
        /// <param name="str2">第二个字符串</param>
        /// <returns>是否相等</returns>
        public static bool EqualsIgnoreCase(string str1, string str2)
        {
            if (ReferenceEquals(str1, str2))
            {
                return true;
            }
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return false;
            }
            return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 获取长度
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>长度</returns>
        public static int Length(string str)
        {
            return str?.Length ?? 0;
        }
    }
}