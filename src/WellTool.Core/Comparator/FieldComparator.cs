using System;
using System.Reflection;

namespace WellTool.Core.Comparator
{
    /// <summary>
    /// Bean 字段排序器
    /// </summary>
    /// <typeparam name="T">被比较的Bean</typeparam>
    public class FieldComparator<T> : FuncComparator<T>
    {
        private static readonly long SerialVersionUID = 9157326766723846313L;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="beanClass">Bean类</param>
        /// <param name="fieldName">字段名</param>
        public FieldComparator(Type beanClass, string fieldName)
            : this(GetNonNullField(beanClass, fieldName))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="field">字段</param>
        public FieldComparator(FieldInfo field)
            : this(true, true, field)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="nullGreater">是否 null 在后</param>
        /// <param name="compareSelf">在字段值相同情况下，是否比较对象本身。如果此项为 false，字段值比较后为0会导致对象被认为相同，可能导致被去重。</param>
        /// <param name="field">字段</param>
        public FieldComparator(bool nullGreater, bool compareSelf, FieldInfo field)
            : base(nullGreater, compareSelf, bean =>
            {
                if (field == null)
                {
                    throw new ArgumentNullException(nameof(field), "Field must be not null!");
                }
                return (IComparable)field.GetValue(bean);
            })
        {
        }

        /// <summary>
        /// 获取字段，附带检查字段不存在的问题
        /// </summary>
        private static FieldInfo GetNonNullField(Type beanClass, string fieldName)
        {
            var field = beanClass.GetField(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            if (field == null)
            {
                throw new ArgumentException($"Field [{fieldName}] not found in Class [{beanClass.FullName}]");
            }
            return field;
        }
    }
}
