using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 反转比较器
    /// </summary>
    /// <typeparam name="E">被比较对象类型</typeparam>
    [Serializable]
    public class ReverseComparator<E> : IComparer<E>
    {
        private static readonly long SerialVersionUID = 8083701245147495562L;

        private readonly IComparer<E> _comparator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="comparator">原始比较器</param>
        public ReverseComparator(IComparer<E> comparator)
        {
            _comparator = comparator ?? new DefaultComparer<E>();
        }

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(E x, E y)
        {
            return _comparator.Compare(y, x);
        }

        /// <summary>
        /// 默认比较器
        /// </summary>
        private class DefaultComparer<T> : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return 1;
                if (y == null) return -1;
                if (x is IComparable comparable && y is IComparable comparableY)
                {
                    return comparable.CompareTo(comparableY);
                }
                return 0;
            }
        }
    }
}
