using System;
using System.Reflection;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// Bean 多个字段排序器
    /// </summary>
    /// <typeparam name="T">被比较的Bean</typeparam>
    public class FieldsComparator<T> : NullComparator<T>
    {
        private static readonly long SerialVersionUID = 8649196282886500803L;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="beanClass">Bean类</param>
        /// <param name="fieldNames">多个字段名</param>
        public FieldsComparator(Type beanClass, params string[] fieldNames)
            : this(true, beanClass, fieldNames)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nullGreater">是否 null 在后</param>
        /// <param name="beanClass">Bean类</param>
        /// <param name="fieldNames">多个字段名</param>
        public FieldsComparator(bool nullGreater, Type beanClass, params string[] fieldNames)
            : base(nullGreater, new FieldsComparer(beanClass, fieldNames))
        {
        }

        /// <summary>
        /// 字段比较器
        /// </summary>
        private class FieldsComparer : IComparer<T>
        {
            private readonly Type _beanClass;
            private readonly string[] _fieldNames;

            public FieldsComparer(Type beanClass, string[] fieldNames)
            {
                _beanClass = beanClass;
                _fieldNames = fieldNames;
            }

            public int Compare(T a, T b)
            {
                FieldInfo field;
                foreach (string fieldName in _fieldNames)
                {
                    field = _beanClass.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                    if (field == null)
                    {
                        throw new ArgumentException($"Field [{fieldName}] not found in Class [{_beanClass.FullName}]");
                    }
                    // 多个字段比较时，允许字段值重复
                    int compare = new FieldComparator<T>(true, false, field).Compare(a, b);
                    if (compare != 0)
                    {
                        return compare;
                    }
                }
                return 0;
            }
        }
    }
}
