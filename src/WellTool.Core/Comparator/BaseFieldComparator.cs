using System;
using System.Reflection;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// Bean字段排序器
    /// </summary>
    /// <typeparam name="T">被比较的Bean</typeparam>
    [Obsolete("此类不再需要，使用 FuncComparator 代替更加灵活")]
    public abstract class BaseFieldComparator<T> : IComparer<T>
    {
        private static readonly long SerialVersionUID = -3482464782340308755L;

        /// <summary>
        /// 比较两个对象的同一个字段值
        /// </summary>
        /// <param name="x">对象1</param>
        /// <param name="y">对象2</param>
        /// <param name="field">字段</param>
        /// <returns>比较结果</returns>
        protected int CompareItem(T x, T y, FieldInfo field)
        {
            if (ReferenceEquals(x, y))
            {
                return 0;
            }
            if (x == null)
            {
                // null 排在后面
                return 1;
            }
            if (y == null)
            {
                return -1;
            }

            IComparable v1;
            IComparable v2;
            try
            {
                v1 = (IComparable)field.GetValue(x);
                v2 = (IComparable)field.GetValue(y);
            }
            catch (System.Exception e)
            {
                throw new ComparatorException(e.Message);
            }

            return Compare(x, y, v1, v2);
        }

        /// <summary>
        /// 比较
        /// </summary>
        protected int Compare(T x, T y, IComparable fieldValue1, IComparable fieldValue2)
        {
            int result = CompareUtil.Compare(fieldValue1, fieldValue2, true);
            if (result == 0)
            {
                // 避免 TreeSet / TreeMap 过滤掉排序字段相同但是对象不相同的情况
                result = CompareUtil.Compare(x, y, true);
            }
            return result;
        }

        /// <summary>
        /// 实现 IComparer
        /// </summary>
        public abstract int Compare(T x, T y);
    }
}
