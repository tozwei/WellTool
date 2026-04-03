using System;
using System.Text;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 进制转换工具类
    /// </summary>
    public static class RadixUtil
    {
        private const string Digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const int MaxRadix = Digits.Length;
        private const int MinRadix = 2;

        /// <summary>
        /// 将数字转换为指定进制的字符串
        /// </summary>
        public static string ToString(long value, int radix = 62)
        {
            if (radix < MinRadix || radix > MaxRadix)
            {
                throw new ArgumentException($"Radix must be between {MinRadix} and {MaxRadix}");
            }

            if (value == 0)
            {
                return "0";
            }

            bool negative = value < 0;
            if (negative)
            {
                value = -value;
            }

            var sb = new StringBuilder();
            while (value > 0)
            {
                int index = (int)(value % radix);
                sb.Insert(0, Digits[index]);
                value /= radix;
            }

            if (negative)
            {
                sb.Insert(0, '-');
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将指定进制的字符串转换为数字
        /// </summary>
        public static long ToLong(string value, int radix = 62)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty");
            }

            if (radix < MinRadix || radix > MaxRadix)
            {
                throw new ArgumentException($"Radix must be between {MinRadix} and {MaxRadix}");
            }

            bool negative = value[0] == '-';
            if (negative)
            {
                value = value.Substring(1);
            }

            long result = 0;
            foreach (char c in value)
            {
                int digit = Digits.IndexOf(c);
                if (digit < 0 || digit >= radix)
                {
                    throw new ArgumentException($"Invalid character '{c}' for radix {radix}");
                }
                result = result * radix + digit;
            }

            return negative ? -result : result;
        }

        /// <summary>
        /// 十进制转二进制
        /// </summary>
        public static string ToBinary(long value)
        {
            return Convert.ToString(value, 2);
        }

        /// <summary>
        /// 十进制转八进制
        /// </summary>
        public static string ToOctal(long value)
        {
            return Convert.ToString(value, 8);
        }

        /// <summary>
        /// 十进制转十六进制
        /// </summary>
        public static string ToHex(long value)
        {
            return Convert.ToString(value, 16).ToUpper();
        }

        /// <summary>
        /// 二进制转十进制
        /// </summary>
        public static long BinaryToDecimal(string value)
        {
            return Convert.ToInt64(value, 2);
        }

        /// <summary>
        /// 八进制转十进制
        /// </summary>
        public static long OctalToDecimal(string value)
        {
            return Convert.ToInt64(value, 8);
        }

        /// <summary>
        /// 十六进制转十进制
        /// </summary>
        public static long HexToDecimal(string value)
        {
            return Convert.ToInt64(value, 16);
        }

        /// <summary>
        /// 任意进制转换
        /// </summary>
        public static string Convert(string value, int fromRadix, int toRadix)
        {
            if (fromRadix < MinRadix || fromRadix > MaxRadix)
            {
                throw new ArgumentException($"Source radix must be between {MinRadix} and {MaxRadix}");
            }

            if (toRadix < MinRadix || toRadix > MaxRadix)
            {
                throw new ArgumentException($"Target radix must be between {MinRadix} and {MaxRadix}");
            }

            long decimalValue = ToLong(value, fromRadix);
            return ToString(decimalValue, toRadix);
        }

        /// <summary>
        /// 转换为Base62
        /// </summary>
        public static string ToBase62(long value)
        {
            return ToString(value, 62);
        }

        /// <summary>
        /// 从Base62转换
        /// </summary>
        public static long FromBase62(string value)
        {
            return ToLong(value, 62);
        }
    }
}
