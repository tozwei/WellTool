using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 字符串长度比较器，短在前
    /// </summary>
    public class LengthComparator : IComparer<string>
    {
        /// <summary>
        /// 单例的字符串长度比较器，短在前
        /// </summary>
        public static readonly LengthComparator Instance = new LengthComparator();

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(string x, string y)
        {
            int result = x.Length.CompareTo(y.Length);
            if (result == 0)
            {
                result = string.Compare(x, y, StringComparison.Ordinal);
            }
            return result;
        }
    }
}
