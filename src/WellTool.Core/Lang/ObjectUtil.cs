using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 对象工具类
/// </summary>
public static class ObjectUtil
{
    /// <summary>
    /// 判断是否为空
    /// </summary>
    public static bool IsNull(object obj)
    {
        return obj == null;
    }

    /// <summary>
/// 判断是否不为空
/// </summary>
    public static bool IsNotNull(object obj)
    {
        return obj != null;
    }

    /// <summary>
    /// 判断是否为空（null或DBNull）
    /// </summary>
    public static bool IsEmpty(object obj)
    {
        if (obj == null)
        {
            return true;
        }

        if (obj is string str)
        {
            return string.IsNullOrEmpty(str);
        }

        if (obj is Array arr)
        {
            return arr.Length == 0;
        }

        if (obj is System.Collections.ICollection coll)
        {
            return coll.Count == 0;
        }

        return false;
    }

    /// <summary>
    /// 判断是否不为空
    /// </summary>
    public static bool IsNotEmpty(object obj)
    {
        return !IsEmpty(obj);
    }

    /// <summary>
    /// 如果为空，返回默认值
    /// </summary>
    public static T DefaultIfNull<T>(T obj, T defaultValue)
    {
        return obj ?? defaultValue;
    }

    /// <summary>
        /// 如果为空，返回默认值（使用工厂方法）
        /// </summary>
        public static T DefaultIfNull<T>(T obj, System.Func<T> factory)
        {
            return obj ?? factory();
        }

    /// <summary>
    /// 获取哈希码
    /// </summary>
    public static int HashCode(object obj)
    {
        return obj?.GetHashCode() ?? 0;
    }

    /// <summary>
    /// 获取字符串表示
    /// </summary>
    public static string ToString(object obj)
    {
        return obj?.ToString() ?? "";
    }

    /// <summary>
    /// 获取字符串表示（带默认值）
    /// </summary>
    public static string ToString(object obj, string defaultValue)
    {
        return obj?.ToString() ?? defaultValue;
    }

    /// <summary>
    /// 检查类型是否为原始类型
    /// </summary>
    public static bool IsPrimitive(Type type)
    {
        return type.IsPrimitive || type == typeof(string) || type == typeof(decimal) ||
               type == typeof(DateTime) || type == typeof(Guid);
    }

    /// <summary>
    /// 获取类型的默认值
    /// </summary>
    public static object GetDefault(Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        return null;
    }

    /// <summary>
    /// 引用相等
    /// </summary>
    public static bool IsSame(object a, object b)
    {
        return ReferenceEquals(a, b);
    }

    /// <summary>
    /// 值相等
    /// </summary>
    public static bool IsEqual(object a, object b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a == null || b == null)
        {
            return false;
        }

        return a.Equals(b);
    }
}
