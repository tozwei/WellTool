using System;
using System.Collections.Generic;
using System.Linq;
using WellTool.Core.Math;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 版本号比较器
    /// </summary>
    public class VersionComparator : IComparer<string>
    {
        /// <summary>
        /// 比较两个版本号
        /// </summary>
        /// <param name="x">第一个版本号</param>
        /// <param name="y">第二个版本号</param>
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

            var xParts = x.Split('.').Select(int.Parse).ToArray();
            var yParts = y.Split('.').Select(int.Parse).ToArray();

            int minLength = MathUtil.Min(xParts.Length, yParts.Length);
            for (int i = 0; i < minLength; i++)
            {
                int result = xParts[i].CompareTo(yParts[i]);
                if (result != 0)
                {
                    return result;
                }
            }

            return xParts.Length.CompareTo(yParts.Length);
        }
    }
}