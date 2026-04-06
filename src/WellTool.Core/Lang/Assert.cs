using System;
using System.Collections;

namespace WellTool.Core.Lang;

/// <summary>
/// 断言工具类
/// </summary>
public static class Assert
{
    /// <summary>
    /// 断言对象为null
    /// </summary>
    public static bool IsNull(object obj)
    {
        return obj == null;
    }

    /// <summary>
    /// 断言对象不为null
    /// </summary>
    public static bool NotNull(object obj)
    {
        return obj != null;
    }

    /// <summary>
    /// 断言对象不为空
    /// </summary>
    public static object NotNull(object obj, Func<System.Exception> exceptionProducer)
    {
        if (obj == null)
        {
            throw exceptionProducer();
        }
        return obj;
    }

    /// <summary>
    /// 断言条件为真
    /// </summary>
    public static bool IsTrue(bool condition)
    {
        return condition;
    }

    /// <summary>
    /// 断言条件为真
    /// </summary>
    public static bool IsTrue(bool condition, Func<System.Exception> exceptionProducer)
    {
        if (!condition)
        {
            throw exceptionProducer();
        }
        return true;
    }

    /// <summary>
    /// 断言条件为假
    /// </summary>
    public static bool IsFalse(bool condition)
    {
        return !condition;
    }

    /// <summary>
    /// 断言条件为假
    /// </summary>
    public static bool IsFalse(bool condition, Func<System.Exception> exceptionProducer)
    {
        if (condition)
        {
            throw exceptionProducer();
        }
        return true;
    }

    /// <summary>
    /// 断言值在范围内
    /// </summary>
    public static T CheckBetween<T>(T value, T min, T max) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {min} and {max}");
        }
        return value;
    }

    /// <summary>
    /// 断言数组不为空
    /// </summary>
    public static bool NotEmpty(Array array)
    {
        return array != null && array.Length > 0;
    }

    /// <summary>
    /// 断言数组不为空
    /// </summary>
    public static Array NotEmpty(Array array, Func<System.Exception> exceptionProducer)
    {
        if (array == null || array.Length == 0)
        {
            throw exceptionProducer();
        }
        return array;
    }

    /// <summary>
    /// 断言集合不为空
    /// </summary>
    public static bool NotEmpty<T>(ICollection<T> collection)
    {
        return collection != null && collection.Count > 0;
    }

    /// <summary>
    /// 断言集合不为空
    /// </summary>
    public static ICollection<T> NotEmpty<T>(ICollection<T> collection, Func<System.Exception> exceptionProducer)
    {
        if (collection == null || collection.Count == 0)
        {
            throw exceptionProducer();
        }
        return collection;
    }

    /// <summary>
    /// 断言集合为空
    /// </summary>
    public static bool Empty(IEnumerable collection)
    {
        if (collection == null)
            return true;
        return !collection.GetEnumerator().MoveNext();
    }

    /// <summary>
    /// 断言集合为空
    /// </summary>
    public static IEnumerable Empty(IEnumerable collection, Func<System.Exception> exceptionProducer)
    {
        if (collection == null)
            return collection;
        if (collection.GetEnumerator().MoveNext())
        {
            throw exceptionProducer();
        }
        return collection;
    }
}
