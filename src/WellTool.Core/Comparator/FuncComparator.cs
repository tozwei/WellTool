using System;
using System.Collections.Generic;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// 指定函数排序器
    /// </summary>
    /// <typeparam name="T">被比较的对象</typeparam>
    public class FuncComparator<T> : IComparer<T>
    {
        private static readonly long SerialVersionUID = 1L;

        private readonly bool _nullGreater;
        private readonly bool _compareSelf;
        private readonly Func<T, IComparable> _func;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nullGreater">是否 null 在后</param>
        /// <param name="func">比较项获取函数，此函数根据传入的一个对象，生成对应的可比较对象，然后根据这个返回值比较</param>
        public FuncComparator(bool nullGreater, Func<T, IComparable> func)
            : this(nullGreater, true, func)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nullGreater">是否 null 在后</param>
        /// <param name="compareSelf">在字段值相同情况下，是否比较对象本身。如果此项为 false，字段值比较后为0会导致对象被认为相同，可能导致被去重。</param>
        /// <param name="func">比较项获取函数</param>
        public FuncComparator(bool nullGreater, bool compareSelf, Func<T, IComparable> func)
        {
            _nullGreater = nullGreater;
            _compareSelf = compareSelf;
            _func = func;
        }

        /// <summary>
        /// 比较
        /// </summary>
        public int Compare(T a, T b)
        {
            // 首先比较用户自定义的转换结果，如果为0，根据 compareSelf 参数决定是否比较对象本身
            IComparable v1 = null;
            IComparable v2 = null;
            try
            {
                v1 = _func(a);
                v2 = _func(b);
            }
            catch (System.Exception)
            {
                throw new ComparatorException();
            }

            int result = CompareUtil.Compare(v1, v2, _nullGreater);
            if (_compareSelf && result == 0)
            {
                // 避免 TreeSet / TreeMap 过滤掉排序字段相同但是对象不相同的情况
                result = CompareUtil.Compare(a, b, _nullGreater);
            }
            return result;
        }
    }
}
