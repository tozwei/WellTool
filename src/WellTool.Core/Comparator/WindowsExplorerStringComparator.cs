using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// Windows资源管理器风格的字符串比较器
    /// </summary>
    public class WindowsExplorerStringComparator : IComparer<string>
    {
        private static readonly Regex NumberRegex = new Regex(@"\d+", RegexOptions.Compiled);

        /// <summary>
        /// 比较两个字符串
        /// </summary>
        /// <param name="x">第一个字符串</param>
        /// <param name="y">第二个字符串</param>
        /// <returns>比较结果：x &lt; y 返回负数，x == y 返回0，x &gt; y 返回正数</returns>
        public int Compare(string x, string y)
        {
            if (x == null && y == null)
            {
                return 0;
            }
            if (x == null)
            {
                return -1;
            }
            if (y == null)
            {
                return 1;
            }

            int i = 0, j = 0;
            while (i < x.Length && j < y.Length)
            {
                bool xIsDigit = char.IsDigit(x[i]);
                bool yIsDigit = char.IsDigit(y[j]);

                if (xIsDigit && yIsDigit)
                {
                    // 提取连续数字
                    int xStart = i, yStart = j;
                    while (i < x.Length && char.IsDigit(x[i])) i++;
                    while (j < y.Length && char.IsDigit(y[j])) j++;

                    string xNumStr = x.Substring(xStart, i - xStart);
                    string yNumStr = y.Substring(yStart, j - yStart);

                    // 比较数字大小
                    if (int.TryParse(xNumStr, out int xNum) && int.TryParse(yNumStr, out int yNum))
                    {
                        int numCompare = xNum.CompareTo(yNum);
                        if (numCompare != 0)
                        {
                            return numCompare;
                        }
                    }
                    else
                    {
                        // 如果不是有效的数字，按字符串比较
                        int strCompare = string.Compare(xNumStr, yNumStr, StringComparison.OrdinalIgnoreCase);
                        if (strCompare != 0)
                        {
                            return strCompare;
                        }
                    }
                }
                else
                {
                    // 按字符比较
                    int charCompare = char.ToLower(x[i]).CompareTo(char.ToLower(y[j]));
                    if (charCompare != 0)
                    {
                        return charCompare;
                    }
                    i++;
                    j++;
                }
            }

            // 比较长度
            return x.Length.CompareTo(y.Length);
        }
    }
}