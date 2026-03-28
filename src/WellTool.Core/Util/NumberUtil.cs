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
using System.Globalization;
using System.Text.RegularExpressions;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 数字工具类
    /// </summary>
    public static class NumberUtil
    {
        /// <summary>
        /// 检查字符串是否为数字
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是数字则返回 true，否则返回 false</returns>
        public static bool IsNumber(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return double.TryParse(str, out _);
        }

        /// <summary>
        /// 检查字符串是否为整数
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是整数则返回 true，否则返回 false</returns>
        public static bool IsInteger(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return int.TryParse(str, out _);
        }

        /// <summary>
        /// 检查字符串是否为长整数
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是长整数则返回 true，否则返回 false</returns>
        public static bool IsLong(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return long.TryParse(str, out _);
        }

        /// <summary>
        /// 检查字符串是否为浮点数
        /// </summary>
        /// <param name="str">要检查的字符串</param>
        /// <returns>如果是浮点数则返回 true，否则返回 false</returns>
        public static bool IsDouble(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return double.TryParse(str, out _);
        }

        /// <summary>
        /// 将字符串转换为整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的整数</returns>
        public static int ToInt(string str)
        {
            return int.Parse(str);
        }

        /// <summary>
        /// 将字符串转换为整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的整数，如果转换失败则返回默认值</returns>
        public static int ToInt(string str, int defaultValue)
        {
            if (int.TryParse(str, out int result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串转换为长整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的长整数</returns>
        public static long ToLong(string str)
        {
            return long.Parse(str);
        }

        /// <summary>
        /// 将字符串转换为长整数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的长整数，如果转换失败则返回默认值</returns>
        public static long ToLong(string str, long defaultValue)
        {
            if (long.TryParse(str, out long result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns>转换后的浮点数</returns>
        public static double ToDouble(string str)
        {
            return double.Parse(str);
        }

        /// <summary>
        /// 将字符串转换为浮点数
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>转换后的浮点数，如果转换失败则返回默认值</returns>
        public static double ToDouble(string str, double defaultValue)
        {
            if (double.TryParse(str, out double result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 加法运算
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之和</returns>
        public static int Add(int a, int b)
        {
            return a + b;
        }

        /// <summary>
        /// 加法运算
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之和</returns>
        public static long Add(long a, long b)
        {
            return a + b;
        }

        /// <summary>
        /// 加法运算
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之和</returns>
        public static double Add(double a, double b)
        {
            return a + b;
        }

        /// <summary>
        /// 减法运算
        /// </summary>
        /// <param name="a">被减数</param>
        /// <param name="b">减数</param>
        /// <returns>两数之差</returns>
        public static int Subtract(int a, int b)
        {
            return a - b;
        }

        /// <summary>
        /// 减法运算
        /// </summary>
        /// <param name="a">被减数</param>
        /// <param name="b">减数</param>
        /// <returns>两数之差</returns>
        public static long Subtract(long a, long b)
        {
            return a - b;
        }

        /// <summary>
        /// 减法运算
        /// </summary>
        /// <param name="a">被减数</param>
        /// <param name="b">减数</param>
        /// <returns>两数之差</returns>
        public static double Subtract(double a, double b)
        {
            return a - b;
        }

        /// <summary>
        /// 乘法运算
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之积</returns>
        public static int Multiply(int a, int b)
        {
            return a * b;
        }

        /// <summary>
        /// 乘法运算
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之积</returns>
        public static long Multiply(long a, long b)
        {
            return a * b;
        }

        /// <summary>
        /// 乘法运算
        /// </summary>
        /// <param name="a">第一个数</param>
        /// <param name="b">第二个数</param>
        /// <returns>两数之积</returns>
        public static double Multiply(double a, double b)
        {
            return a * b;
        }

        /// <summary>
        /// 除法运算
        /// </summary>
        /// <param name="a">被除数</param>
        /// <param name="b">除数</param>
        /// <returns>两数之商</returns>
        public static double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            return a / b;
        }

        /// <summary>
        /// 取模运算
        /// </summary>
        /// <param name="a">被除数</param>
        /// <param name="b">除数</param>
        /// <returns>两数之模</returns>
        public static int Mod(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            return a % b;
        }

        /// <summary>
        /// 取模运算
        /// </summary>
        /// <param name="a">被除数</param>
        /// <param name="b">除数</param>
        /// <returns>两数之模</returns>
        public static long Mod(long a, long b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException();
            }
            return a % b;
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="values">值数组</param>
        /// <returns>最大值</returns>
        public static int Max(params int[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }
            return values.Max();
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="values">值数组</param>
        /// <returns>最大值</returns>
        public static long Max(params long[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }
            return values.Max();
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <param name="values">值数组</param>
        /// <returns>最大值</returns>
        public static double Max(params double[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }
            return values.Max();
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="values">值数组</param>
        /// <returns>最小值</returns>
        public static int Min(params int[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }
            return values.Min();
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="values">值数组</param>
        /// <returns>最小值</returns>
        public static long Min(params long[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }
            return values.Min();
        }

        /// <summary>
        /// 获取最小值
        /// </summary>
        /// <param name="values">值数组</param>
        /// <returns>最小值</returns>
        public static double Min(params double[] values)
        {
            if (values == null || values.Length == 0)
            {
                throw new ArgumentException("Values cannot be null or empty");
            }
            return values.Min();
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="value">要四舍五入的值</param>
        /// <param name="decimals">小数位数</param>
        /// <returns>四舍五入后的值</returns>
        public static double Round(double value, int decimals)
        {
            return Math.Round(value, decimals);
        }

        /// <summary>
        /// 向上取整
        /// </summary>
        /// <param name="value">要向上取整的值</param>
        /// <returns>向上取整后的值</returns>
        public static double Ceiling(double value)
        {
            return Math.Ceiling(value);
        }

        /// <summary>
        /// 向下取整
        /// </summary>
        /// <param name="value">要向下取整的值</param>
        /// <returns>向下取整后的值</returns>
        public static double Floor(double value)
        {
            return Math.Floor(value);
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">要取绝对值的值</param>
        /// <returns>绝对值</returns>
        public static int Abs(int value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">要取绝对值的值</param>
        /// <returns>绝对值</returns>
        public static long Abs(long value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// 取绝对值
        /// </summary>
        /// <param name="value">要取绝对值的值</param>
        /// <returns>绝对值</returns>
        public static double Abs(double value)
        {
            return Math.Abs(value);
        }
    }
}
