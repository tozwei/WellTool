using System;
using System.Text;
using System.Globalization;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 字符工具类
    /// </summary>
    public static class CharUtil
    {
        /// <summary>
        /// 判断是否为字母
        /// </summary>
        public static bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        /// <summary>
        /// 判断是否为数字
        /// </summary>
        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// 判断是否为字母或数字
        /// </summary>
        public static bool IsAlphanumeric(char c)
        {
            return IsAlpha(c) || IsDigit(c);
        }

        /// <summary>
        /// 判断是否为空白字符
        /// </summary>
        public static bool IsBlank(char c)
        {
            return c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == '\u00A0';
        }

        /// <summary>
        /// 判断是否为ASCII字符
        /// </summary>
        public static bool IsAscii(char c)
        {
            return c < 128;
        }

        /// <summary>
        /// 判断是否为可见ASCII字符
        /// </summary>
        public static bool IsAsciiPrintable(char c)
        {
            return c >= 32 && c < 127;
        }

        /// <summary>
        /// 判断是否为控制字符
        /// </summary>
        public static bool IsControl(char c)
        {
            return !IsAsciiPrintable(c) && c < 256;
        }

        /// <summary>
        /// 判断是否为大写字母
        /// </summary>
        public static bool IsUpperCase(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        /// <summary>
        /// 判断是否为小写字母
        /// </summary>
        public static bool IsLowerCase(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        /// <summary>
        /// 将字符转为小写（支持Unicode）
        /// </summary>
        public static char ToLowerCase(char c)
        {
            return char.ToLowerInvariant(c);
        }

        /// <summary>
        /// 将字符转为大写（支持Unicode）
        /// </summary>
        public static char ToUpperCase(char c)
        {
            return char.ToUpperInvariant(c);
        }

        /// <summary>
        /// 将字符转为标题大小写
        /// </summary>
        public static char ToTitleCase(char c)
        {
            return char.ToUpperInvariant(c);
        }

        /// <summary>
        /// 将字符转为数字
        /// </summary>
        public static int ToInt(char c)
        {
            if (IsDigit(c))
            {
                return c - '0';
            }
            throw new ArgumentException($"Character '{c}' is not a digit");
        }

        /// <summary>
        /// 将字符转为十六进制数字
        /// </summary>
        public static int ToHexInt(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            if (c >= 'a' && c <= 'f')
            {
                return c - 'a' + 10;
            }
            if (c >= 'A' && c <= 'F')
            {
                return c - 'A' + 10;
            }
            throw new ArgumentException($"Character '{c}' is not a hex digit");
        }

        /// <summary>
        /// 判断是否包含在字符数组中
        /// </summary>
        public static bool IsIn(char c, params char[] chars)
        {
            foreach (var ch in chars)
            {
                if (c == ch) return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否包含在字符串中
        /// </summary>
        public static bool IsIn(char c, string chars)
        {
            return chars?.IndexOf(c) >= 0;
        }

        /// <summary>
        /// 获取字符类型
        /// </summary>
        public static UnicodeCategory GetUnicodeCategory(char c)
        {
            return char.GetUnicodeCategory(c);
        }

        /// <summary>
        /// 检查是否为汉字
        /// </summary>
        public static bool IsChinese(char c)
        {
            return (c >= 0x4E00 && c <= 0x9FFF) ||
                   (c >= 0x3400 && c <= 0x4DBF); // 扩展A区
        }

        /// <summary>
        /// 检查是否为日文
        /// </summary>
        public static bool IsJapanese(char c)
        {
            return (c >= 0x3040 && c <= 0x309F) ||  // 平假名
                   (c >= 0x30A0 && c <= 0x30FF);    // 片假名
        }

        /// <summary>
        /// 检查是否为韩文
        /// </summary>
        public static bool IsKorean(char c)
        {
            return c >= 0xAC00 && c <= 0xD7AF;
        }

        /// <summary>
        /// 判断是否为十六进制字符
        /// </summary>
        public static bool IsHexChar(char c)
        {
            return (c >= '0' && c <= '9') ||
                   (c >= 'a' && c <= 'f') ||
                   (c >= 'A' && c <= 'F');
        }

        /// <summary>
        /// 将十六进制字符转换为数字
        /// </summary>
        public static int Digit16(char c)
        {
            return ToHexInt(c);
        }
    }
}
