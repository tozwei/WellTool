using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 针对 Comparable 对象的默认比较器
    /// </summary>
    /// <typeparam name="E">比较对象类型</typeparam>
    [Serializable]
    public class ComparableComparator<E> : IComparer<E> where E : IComparable<E>
    {
        private static readonly long SerialVersionUID = 3020871676147289162L;

        /// <summary>
        /// 单例
        /// </summary>
        public static readonly ComparableComparator<E> Instance = new ComparableComparator<E>();

        /// <summary>
        /// 构造
        /// </summary>
        public ComparableComparator()
        {
        }

        /// <summary>
        /// 比较两个 Comparable 对象
        /// </summary>
        /// <param name="x">被比较的第一个对象</param>
        /// <param name="y">第二个被比较的对象</param>
        /// <returns>x小返回负数，大返回正数，否则返回0</returns>
        public int Compare(E x, E y)
        {
            return x.CompareTo(y);
        }
    }
}
