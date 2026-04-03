using System;
using System.Collections.Generic;

namespace WellTool.Core.Converter
{
    /// <summary>
    /// 将浮点数类型的number转换成英语的表达方式
    /// 本质上此类为金额转英文表达，因此没有四舍五入考虑，小数点超过两位直接忽略。
    /// </summary>
    public static class NumberWordFormatter
    {
        private static readonly string[] NUMBER = new[] {
            "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE"
        };
        private static readonly string[] NUMBER_TEEN = new[] {
            "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
        private static readonly string[] NUMBER_TEN = new[] {
            "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
        private static readonly string[] NUMBER_MORE = new[] {
            "", "THOUSAND", "MILLION", "BILLION", "TRILLION"
        };
        private static readonly string[] NUMBER_SUFFIX = new[] {
            "k", "w", "m", "b", "t", "p", "e"
        };
        private static readonly int[] STANDARD_UNIT_INDICES = { 0, 2, 3, 4, 5, 6 };

        /// <summary>
        /// 将阿拉伯数字转为英文表达式
        /// </summary>
        /// <param name="x">阿拉伯数字</param>
        /// <returns>英文表达式</returns>
        public static string Format(object x)
        {
            if (x != null)
            {
                return Format(x.ToString());
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 将阿拉伯数字转化为简洁计数单位，例如 2100 =》 2.1k
        /// 范围默认只到w
        /// </summary>
        /// <param name="value">被格式化的数字</param>
        /// <returns>格式化后的数字</returns>
        public static string FormatSimple(long value)
        {
            return FormatSimple(value, true);
        }

        /// <summary>
        /// 将阿拉伯数字转化为简介计数单位，例如 2100 =》 2.1k
        /// </summary>
        /// <param name="value">对应数字的值</param>
        /// <param name="isTwo">控制是否为只为k、w，例如当为false时返回4.38m，true返回438.43w</param>
        /// <returns>格式化后的数字</returns>
        public static string FormatSimple(long value, bool isTwo)
        {
            if (value < 1000)
            {
                return value.ToString();
            }

            double res = value;
            int index;
            if (isTwo)
            {
                if (value >= 10000)
                {
                    res = value / 10000.0;
                    index = 1;
                }
                else
                {
                    res = value / 1000.0;
                    index = 0;
                }
            }
            else
            {
                int unitIndex = -1;
                while (res >= 1000 && unitIndex < STANDARD_UNIT_INDICES.Length - 1)
                {
                    res = res / 1000;
                    unitIndex++;
                }
                index = STANDARD_UNIT_INDICES[unitIndex];
            }

            return string.Format("{0}{1}", res.ToString("0.##"), NUMBER_SUFFIX[index]);
        }

        /// <summary>
        /// 将阿拉伯数字转为英文表达式
        /// </summary>
        private static string Format(string x)
        {
            int z = x.IndexOf(".");
            string lstr, rstr = "";
            if (z > -1)
            {
                lstr = x.Substring(0, z);
                rstr = x.Substring(z + 1);
            }
            else
            {
                lstr = x;
            }

            string lstrrev = Reverse(lstr);

            int parts = (int)Math.Ceiling(lstrrev.Length / 3.0);
            string[] a = new string[parts];

            switch (lstrrev.Length % 3)
            {
                case 1:
                    lstrrev += "00";
                    break;
                case 2:
                    lstrrev += "0";
                    break;
            }

            var lm = new System.Text.StringBuilder();
            for (int i = 0; i < lstrrev.Length / 3; i++)
            {
                a[i] = Reverse(lstrrev.Substring(3 * i, 3));
                if (a[i] != "000")
                {
                    if (i != 0)
                    {
                        lm.Insert(0, TransThree(a[i]) + " " + ParseMore(i) + " ");
                    }
                    else
                    {
                        lm = new System.Text.StringBuilder(TransThree(a[i]));
                    }
                }
                else
                {
                    lm.Append(TransThree(a[i]));
                }
            }

            string xs = lm.Length == 0 ? "ZERO " : " ";
            if (z > -1)
            {
                xs += "AND CENTS " + TransTwo(rstr) + " ";
            }

            return lm.ToString().Trim() + xs + "ONLY";
        }

        private static string ParseTeen(string s)
        {
            return NUMBER_TEEN[int.Parse(s) - 10];
        }

        private static string ParseTen(string s)
        {
            return NUMBER_TEN[int.Parse(s.Substring(0, 1)) - 1];
        }

        private static string ParseMore(int i)
        {
            return NUMBER_MORE[i];
        }

        private static string TransTwo(string s)
        {
            string value;
            if (s.Length > 2)
            {
                s = s.Substring(0, 2);
            }
            else if (s.Length < 2)
            {
                s += "0";
            }

            if (s.StartsWith("0"))
            {
                value = ParseLast(s);
            }
            else if (s.StartsWith("1"))
            {
                value = ParseTeen(s);
            }
            else if (s.EndsWith("0"))
            {
                value = ParseTen(s);
            }
            else
            {
                value = ParseTen(s) + " " + ParseLast(s);
            }
            return value;
        }

        private static string TransThree(string s)
        {
            string value;
            if (s.StartsWith("0"))
            {
                value = TransTwo(s.Substring(1));
            }
            else if (s.Substring(1) == "00")
            {
                value = ParseLast(s.Substring(0, 1)) + " HUNDRED";
            }
            else
            {
                value = ParseLast(s.Substring(0, 1)) + " HUNDRED AND " + TransTwo(s.Substring(1));
            }
            return value;
        }

        private static string ParseLast(string s)
        {
            return NUMBER[int.Parse(s.Substring(s.Length - 1))];
        }

        private static string Reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}