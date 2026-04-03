using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// null 友好的比较器包装，如果 nullGreater，则 null &gt; non-null，否则反之。
    /// 如果二者皆为 null，则为相等，返回0。
    /// 如果二者都非 null，则使用传入的比较器排序。
    /// </summary>
    /// <typeparam name="T">被比较的对象</typeparam>
    [Serializable]
    public class NullComparator<T> : IComparer<T>
    {
        private static readonly long SerialVersionUID = 1L;

        private readonly bool _nullGreater;
        private readonly IComparer<T> _comparator;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nullGreater">是否 null 最大，排在最后</param>
        /// <param name="comparator">实际比较器</param>
        public NullComparator(bool nullGreater, IComparer<T> comparator)
        {
            _nullGreater = nullGreater;
            _comparator = comparator;
        }

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(T x, T y)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }
            if (x == null)
            {
                return _nullGreater ? 1 : -1;
            }
            if (y == null)
            {
                return _nullGreater ? -1 : 1;
            }
            return DoCompare(x, y);
        }

        /// <summary>
        /// 不检查 null 的比较方法，用户可自行重写此方法自定义比较方式
        /// </summary>
        protected virtual int DoCompare(T x, T y)
        {
            if (_comparator == null)
            {
                if (x is IComparable comparable && y is IComparable comparableY)
                {
                    return comparable.CompareTo(comparableY);
                }
                return 0;
            }

            return _comparator.Compare(x, y);
        }
    }
}
