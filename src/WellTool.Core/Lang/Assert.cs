using System;

namespace WellTool.Core.Lang
{
    /// <summary>
    /// 断言工具类
    /// </summary>
    public static class Assert
    {
        /// <summary>
        /// 断言对象不为空
        /// </summary>
        public static void NotNull(object obj, string message = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(message ?? "Object cannot be null");
            }
        }

        /// <summary>
        /// 断言对象不为空
        /// </summary>
        public static void NotNull(object obj, string message, params object[] args)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(string.Format(message, args));
            }
        }

        /// <summary>
        /// 断言字符串不为空
        /// </summary>
        public static void NotBlank(string str, string message = null)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(message ?? "String cannot be blank");
            }
        }

        /// <summary>
        /// 断言条件为真
        /// </summary>
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
            {
                throw new ArgumentException(message ?? "Condition must be true");
            }
        }

        /// <summary>
        /// 断言条件为假
        /// </summary>
        public static void IsFalse(bool condition, string message = null)
        {
            if (condition)
            {
                throw new ArgumentException(message ?? "Condition must be false");
            }
        }

        /// <summary>
        /// 断言两个对象相等
        /// </summary>
        public static void Equals(object expected, object actual, string message = null)
        {
            if (!Equals(expected, actual))
            {
                throw new ArgumentException(message ?? $"Expected {expected} but was {actual}");
            }
        }

        /// <summary>
        /// 断言两个对象不相等
        /// </summary>
        public static void NotEquals(object expected, object actual, string message = null)
        {
            if (Equals(expected, actual))
            {
                throw new ArgumentException(message ?? $"Expected not {expected}");
            }
        }

        /// <summary>
        /// 断言数组不包含空元素
        /// </summary>
        public static void NoNullElements(object[] array, string message = null)
        {
            NotNull(array, message);
            foreach (var item in array)
            {
                if (item == null)
                {
                    throw new ArgumentException(message ?? "Array contains null element");
                }
            }
        }

        /// <summary>
        /// 断言集合不为空
        /// </summary>
        public static void NotEmpty<T>(System.Collections.Generic.ICollection<T> collection, string message = null)
        {
            if (collection == null || collection.Count == 0)
            {
                throw new ArgumentException(message ?? "Collection cannot be empty");
            }
        }

        /// <summary>
        /// 断言值在范围内
        /// </summary>
        public static void Between<T>(T value, T min, T max, string message = null) where T : IComparable<T>
        {
            if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), message ?? $"Value {value} is out of range [{min}, {max}]");
            }
        }
    }
}
