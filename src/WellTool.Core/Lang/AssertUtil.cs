using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WellTool.Core.Lang;

/// <summary>
/// 断言工具类
/// </summary>
public static class AssertUtil
{
    #region IsNull
    /// <summary>
    /// 断言对象为null
    /// </summary>
    public static bool IsNull(object obj)
    {
        return obj == null;
    }

    /// <summary>
    /// 断言对象必须为null，否则抛出异常
    /// </summary>
    public static void IsNull(object obj, string errorMsgTemplate, params object[] param)
    {
        if (obj != null)
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }

    /// <summary>
    /// 断言对象必须为null，否则抛出异常
    /// </summary>
    public static void IsNull(object obj, System.Func<System.Exception> errorSupplier)
    {
        if (obj != null)
        {
            throw errorSupplier();
        }
    }
    #endregion

    #region NotNull
    /// <summary>
    /// 断言对象不为null
    /// </summary>
    public static bool NotNull(object obj)
    {
        return obj != null;
    }

    /// <summary>
    /// 断言对象不为null
    /// </summary>
    public static object NotNull(object obj, System.Func<System.Exception> exceptionProducer)
    {
        if (obj == null)
        {
            throw exceptionProducer();
        }
        return obj;
    }

    /// <summary>
    /// 断言对象不为null
    /// </summary>
    public static object NotNull(object obj, string message)
    {
        return NotNull(obj, () => new ArgumentNullException(message));
    }

    /// <summary>
    /// 断言对象不为null，否则抛出异常
    /// </summary>
    public static T NotNull<T>(T obj, string errorMsgTemplate, params object[] param)
    {
        if (obj == null)
        {
            throw new ArgumentNullException(string.Format(errorMsgTemplate, param));
        }
        return obj;
    }

    /// <summary>
    /// 断言对象不为null，否则抛出异常
    /// </summary>
    public static T NotNull<T>(T obj, System.Func<System.Exception> errorSupplier)
    {
        if (obj == null)
        {
            throw errorSupplier();
        }
        return obj;
    }
    #endregion

    #region IsTrue
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
    public static bool IsTrue(bool condition, System.Func<System.Exception> exceptionProducer)
    {
        if (!condition)
        {
            throw exceptionProducer();
        }
        return true;
    }

    /// <summary>
    /// 断言表达式必须为true，否则抛出异常
    /// </summary>
    public static void IsTrue(bool expression, string errorMsgTemplate, params object[] param)
    {
        if (!expression)
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }
    #endregion

    #region IsFalse
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
    public static bool IsFalse(bool condition, System.Func<System.Exception> exceptionProducer)
    {
        if (condition)
        {
            throw exceptionProducer();
        }
        return true;
    }

    /// <summary>
    /// 断言表达式必须为false，否则抛出异常
    /// </summary>
    public static void IsFalse(bool expression, string errorMsgTemplate, params object[] param)
    {
        if (expression)
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }
    #endregion

    #region CheckBetween
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
    /// 断言值在范围内
    /// </summary>
    public static int CheckBetween(int value, int min, int max)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {min} and {max}");
        }
        return value;
    }

    /// <summary>
    /// 断言值在范围内
    /// </summary>
    public static long CheckBetween(long value, long min, long max)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {min} and {max}");
        }
        return value;
    }

    /// <summary>
    /// 断言值在范围内
    /// </summary>
    public static double CheckBetween(double value, double min, double max)
    {
        if (value < min || value > max)
        {
            throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between {min} and {max}");
        }
        return value;
    }
    #endregion

    #region CheckIndex
    /// <summary>
    /// 检查下标是否在 [0, size) 范围内
    /// </summary>
    /// <param name="index">下标</param>
    /// <param name="size">范围大小</param>
    /// <returns>下标</returns>
    public static int CheckIndex(int index, int size)
    {
        if (index < 0 || index >= size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), $"Index {index} out of range [0, {size})");
        }
        return index;
    }

    /// <summary>
    /// 检查下标是否在 [0, size) 范围内
    /// </summary>
    public static int CheckIndex(int index, int size, string errorMsgTemplate, params object[] param)
    {
        if (index < 0 || index >= size)
        {
            throw new ArgumentOutOfRangeException(nameof(index), string.Format(errorMsgTemplate, param));
        }
        return index;
    }
    #endregion

    #region NotEmpty (Array)
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
    public static Array NotEmpty(Array array, System.Func<System.Exception> exceptionProducer)
    {
        if (array == null || array.Length == 0)
        {
            throw exceptionProducer();
        }
        return array;
    }

    /// <summary>
    /// 断言泛型数组不为空
    /// </summary>
    public static bool NotEmpty<T>(T[] array)
    {
        return array != null && array.Length > 0;
    }

    /// <summary>
    /// 断言数组不为空，否则抛出异常
    /// </summary>
    public static T[] NotEmpty<T>(T[] array, string errorMsgTemplate, params object[] param)
    {
        if (array == null || array.Length == 0)
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
        return array;
    }

    /// <summary>
    /// 断言数组不为空，否则抛出异常
    /// </summary>
    public static T[] NotEmpty<T>(T[] array, System.Func<System.Exception> errorSupplier)
    {
        if (array == null || array.Length == 0)
        {
            throw errorSupplier();
        }
        return array;
    }
    #endregion

    #region NotEmpty (Collection)
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
    public static ICollection<T> NotEmpty<T>(ICollection<T> collection, System.Func<System.Exception> exceptionProducer)
    {
        if (collection == null || collection.Count == 0)
        {
            throw exceptionProducer();
        }
        return collection;
    }

    /// <summary>
    /// 断言集合不为空，否则抛出异常
    /// </summary>
    public static ICollection<T> NotEmpty<T>(ICollection<T> collection, string errorMsgTemplate, params object[] param)
    {
        if (collection == null || collection.Count == 0)
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
        return collection;
    }
    #endregion

    #region NotEmpty (Map)
    /// <summary>
    /// 断言字典不为空
    /// </summary>
    public static bool NotEmpty<TKey, TValue>(IDictionary<TKey, TValue> map)
    {
        return map != null && map.Count > 0;
    }

    /// <summary>
    /// 断言字典不为空，否则抛出异常
    /// </summary>
    public static IDictionary<TKey, TValue> NotEmpty<TKey, TValue>(IDictionary<TKey, TValue> map, string errorMsgTemplate, params object[] param)
    {
        if (map == null || map.Count == 0)
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
        return map;
    }

    /// <summary>
    /// 断言字典不为空，否则抛出异常
    /// </summary>
    public static IDictionary<TKey, TValue> NotEmpty<TKey, TValue>(IDictionary<TKey, TValue> map, System.Func<System.Exception> errorSupplier)
    {
        if (map == null || map.Count == 0)
        {
            throw errorSupplier();
        }
        return map;
    }
    #endregion

    #region NoNullElements
    /// <summary>
    /// 断言数组不包含null元素
    /// </summary>
    public static void NoNullElements<T>(T[] array)
    {
        if (array != null && array.Any(t => t == null))
        {
            throw new ArgumentException("Array contains null elements");
        }
    }

    /// <summary>
    /// 断言数组不包含null元素
    /// </summary>
    public static void NoNullElements<T>(T[] array, string errorMsgTemplate, params object[] param)
    {
        if (array != null && array.Any(t => t == null))
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }

    /// <summary>
    /// 断言数组不包含null元素
    /// </summary>
    public static void NoNullElements<T>(T[] array, System.Func<System.Exception> errorSupplier)
    {
        if (array != null && array.Any(t => t == null))
        {
            throw errorSupplier();
        }
    }
    #endregion

    #region Empty
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
    public static IEnumerable Empty(IEnumerable collection, System.Func<System.Exception> exceptionProducer)
    {
        if (collection == null)
            return collection;
        if (collection.GetEnumerator().MoveNext())
        {
            throw exceptionProducer();
        }
        return collection;
    }

    /// <summary>
    /// 断言集合必须为空，否则抛出异常
    /// </summary>
    public static void Empty(IEnumerable collection, string errorMsgTemplate, params object[] param)
    {
        if (collection != null && collection.GetEnumerator().MoveNext())
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }
    #endregion

    #region NotBlank
    /// <summary>
    /// 断言字符串不为空白
    /// </summary>
    public static bool NotBlank(string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// 断言字符串不为空白
    /// </summary>
    public static string NotBlank(string str, System.Func<System.Exception> exceptionProducer)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw exceptionProducer();
        }
        return str;
    }

    /// <summary>
    /// 断言字符串不为空白
    /// </summary>
    public static string NotBlank(string str, string message)
    {
        return NotBlank(str, () => new ArgumentException(message));
    }

    /// <summary>
    /// 断言字符串不为空白，否则抛出异常
    /// </summary>
    public static string NotBlank(string str, string errorMsgTemplate, params object[] param)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
        return str;
    }
    #endregion

    #region NotContain
    /// <summary>
    /// 断言字符串不包含指定子串
    /// </summary>
    public static void NotContain(string textToSearch, string substring)
    {
        if (string.IsNullOrEmpty(textToSearch) || string.IsNullOrEmpty(substring))
            return;
        if (textToSearch.Contains(substring))
        {
            throw new ArgumentException($"String should not contain '{substring}'");
        }
    }

    /// <summary>
    /// 断言字符串不包含指定子串
    /// </summary>
    public static void NotContain(string textToSearch, string substring, string errorMsgTemplate, params object[] param)
    {
        if (string.IsNullOrEmpty(textToSearch) || string.IsNullOrEmpty(substring))
            return;
        if (textToSearch.Contains(substring))
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }

    /// <summary>
    /// 断言字符串不包含指定子串
    /// </summary>
    public static void NotContain(string textToSearch, string substring, System.Func<System.Exception> errorSupplier)
    {
        if (string.IsNullOrEmpty(textToSearch) || string.IsNullOrEmpty(substring))
            return;
        if (textToSearch.Contains(substring))
        {
            throw errorSupplier();
        }
    }
    #endregion

    #region IsInstanceOf
    /// <summary>
    /// 断言对象是指定类的实例
    /// </summary>
    public static void IsInstanceOf(Type type, object obj)
    {
        if (obj == null || !type.IsInstanceOfType(obj))
        {
            throw new ArgumentException($"Object is not an instance of {type}");
        }
    }

    /// <summary>
    /// 断言对象是指定类的实例
    /// </summary>
    public static void IsInstanceOf(Type type, object obj, string errorMsgTemplate, params object[] param)
    {
        if (obj == null || !type.IsInstanceOfType(obj))
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }

    /// <summary>
    /// 断言对象是指定类的实例
    /// </summary>
    public static void IsInstanceOf(Type type, object obj, System.Func<System.Exception> errorSupplier)
    {
        if (obj == null || !type.IsInstanceOfType(obj))
        {
            throw errorSupplier();
        }
    }
    #endregion

    #region IsAssignable
    /// <summary>
    /// 断言子类型可以赋值给父类型
    /// </summary>
    public static void IsAssignable(Type superType, Type subType)
    {
        if (subType == null || !superType.IsAssignableFrom(subType))
        {
            throw new ArgumentException($"{subType} is not assignable to {superType}");
        }
    }

    /// <summary>
    /// 断言子类型可以赋值给父类型
    /// </summary>
    public static void IsAssignable(Type superType, Type subType, string errorMsgTemplate, params object[] param)
    {
        if (subType == null || !superType.IsAssignableFrom(subType))
        {
            throw new ArgumentException(string.Format(errorMsgTemplate, param));
        }
    }

    /// <summary>
    /// 断言子类型可以赋值给父类型
    /// </summary>
    public static void IsAssignable(Type superType, Type subType, System.Func<System.Exception> errorSupplier)
    {
        if (subType == null || !superType.IsAssignableFrom(subType))
        {
            throw errorSupplier();
        }
    }
    #endregion

    #region State
    /// <summary>
    /// 断言状态表达式为true，否则抛出IllegalStateException
    /// </summary>
    public static void State(bool expression)
    {
        if (!expression)
        {
            throw new InvalidOperationException("State expression is false");
        }
    }

    /// <summary>
    /// 断言状态表达式为true，否则抛出IllegalStateException
    /// </summary>
    public static void State(bool expression, string errorMsgTemplate, params object[] param)
    {
        if (!expression)
        {
            throw new InvalidOperationException(string.Format(errorMsgTemplate, param));
        }
    }

    /// <summary>
    /// 断言状态表达式为true，否则抛出IllegalStateException
    /// </summary>
    public static void State(bool expression, System.Func<string> errorMsgSupplier)
    {
        if (!expression)
        {
            throw new InvalidOperationException(errorMsgSupplier());
        }
    }
    #endregion
} 
