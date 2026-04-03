using System;
using System.Collections.Generic;
using System.Globalization;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 按照 GBK 拼音顺序对给定的汉字字符串排序
    /// </summary>
    [Serializable]
    public class PinyinComparator : IComparer<string>
    {
        private static readonly long SerialVersionUID = 1L;

        private readonly CompareInfo _collator;

        /// <summary>
        /// 构造
        /// </summary>
        public PinyinComparator()
        {
            _collator = CultureInfo.GetCultureInfo("zh-CN").CompareInfo;
        }

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(string x, string y)
        {
            return _collator.Compare(x, y, CompareOptions.Ordinal);
        }
    }
}
