namespace WellTool.Core.Util;

/// <summary>
/// 对象工具类，包括判空、克隆、序列化等操作
/// </summary>
public static class ObjectUtil
{
    /// <summary>
    /// 比较两个对象是否相等
    /// </summary>
    /// <param name="obj1">对象1</param>
    /// <param name="obj2">对象2</param>
    /// <returns>是否相等</returns>
    public static bool Equals(object? obj1, object? obj2)
    {
        return equal(obj1, obj2);
    }

    /// <summary>
    /// 比较两个对象是否相等
    /// </summary>
    public static bool equal(object? obj1, object? obj2)
    {
        if (obj1 is System.Numerics.BigInteger bi1 && obj2 is System.Numerics.BigInteger bi2)
        {
            return bi1.CompareTo(bi2) == 0;
        }
        return Equals(obj1, obj2);
    }

    /// <summary>
    /// 比较两个对象是否不相等
    /// </summary>
    public static bool notEqual(object? obj1, object? obj2)
    {
        return !equal(obj1, obj2);
    }

    /// <summary>
    /// 计算对象长度
    /// </summary>
    public static int length(object? obj)
    {
        if (obj == null)
        {
            return 0;
        }
        if (obj is string str)
        {
            return str.Length;
        }
        if (obj is System.Collections.ICollection collection)
        {
            return collection.Count;
        }
        if (obj is Array array)
        {
            return array.Length;
        }
        if (obj is System.Collections.IEnumerable enumerable)
        {
            int count = 0;
            foreach (var _ in enumerable)
            {
                count++;
            }
            return count;
        }
        return -1;
    }

    /// <summary>
    /// 对象中是否包含元素
    /// </summary>
    public static bool contains(object? obj, object? element)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is string str && element is string elementStr)
        {
            return str.Contains(elementStr);
        }
        if (obj is System.Collections.ICollection collection)
        {
            return collection.Contains(element);
        }
        if (obj is Array array)
        {
            return array.Cast<object>().Any(o => Equals(o, element));
        }
        return false;
    }

    /// <summary>
    /// 检查对象是否为null
    /// </summary>
    public static bool isNull(object? obj)
    {
        return obj == null;
    }

    /// <summary>
    /// 检查对象是否不为null
    /// </summary>
    public static bool isNotNull(object? obj)
    {
        return obj != null;
    }

    /// <summary>
    /// 判断指定对象是否为空
    /// </summary>
    public static bool isEmpty(object? obj)
    {
        if (obj == null)
        {
            return true;
        }
        if (obj is string str)
        {
            return string.IsNullOrEmpty(str);
        }
        if (obj is System.Collections.ICollection collection)
        {
            return collection.Count == 0;
        }
        if (obj is Array array)
        {
            return array.Length == 0;
        }
        return false;
    }

    /// <summary>
    /// 判断指定对象是否为非空
    /// </summary>
    public static bool isNotEmpty(object? obj)
    {
        return !isEmpty(obj);
    }

    /// <summary>
    /// 如果给定对象为null返回默认值
    /// </summary>
    public static T defaultIfNull<T>(T? obj, T defaultValue) where T : class
    {
        return obj ?? defaultValue;
    }

    /// <summary>
    /// 克隆对象
    /// </summary>
    public static T? clone<T>(T obj) where T : class
    {
        if (obj is ICloneable cloneable)
        {
            return (T?)cloneable.Clone();
        }
        // 使用序列化方式克隆
        try
        {
            using var ms = new System.IO.MemoryStream();
            var serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            serializer.Serialize(ms, obj);
            ms.Position = 0;
            return (T?)serializer.Deserialize(ms);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// 返回克隆后的对象，如果克隆失败，返回原对象
    /// </summary>
    public static T cloneIfPossible<T>(T obj) where T : class
    {
        try
        {
            var clone = clone(obj);
            return clone ?? obj;
        }
        catch
        {
            return obj;
        }
    }

    /// <summary>
    /// 是否为基本类型
    /// </summary>
    public static bool isBasicType(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        return ClassUtil.IsBasicType(obj.GetType());
    }

    /// <summary>
    /// 是否为有效的数字
    /// </summary>
    public static bool isValidIfNumber(object? obj)
    {
        if (obj is double d)
        {
            return !double.IsNaN(d) && !double.IsInfinity(d);
        }
        if (obj is float f)
        {
            return !float.IsNaN(f) && !float.IsInfinity(f);
        }
        return true;
    }

    /// <summary>
    /// null安全的对象比较，null对象排在末尾
    /// </summary>
    public static int compare<T>(T? c1, T? c2) where T : IComparable<T>
    {
        return CompareUtil.Compare(c1, c2);
    }

    /// <summary>
    /// null安全的对象比较
    /// </summary>
    public static int compare<T>(T? c1, T? c2, bool nullGreater) where T : IComparable<T>
    {
        return CompareUtil.Compare(c1, c2, nullGreater);
    }

    /// <summary>
    /// 调用对象的toString方法，null会返回"null"
    /// </summary>
    public static string toString(object? obj)
    {
        return obj?.ToString() ?? "null";
    }

    /// <summary>
    /// 存在多少个null或空对象
    /// </summary>
    public static int emptyCount(params object?[] objs)
    {
        return objs.Count(IsNull);
    }

    /// <summary>
    /// 是否存在null对象
    /// </summary>
    public static bool hasNull(params object?[] objs)
    {
        return objs.Any(IsNull);
    }

    /// <summary>
    /// 是否存在空对象
    /// </summary>
    public static bool hasEmpty(params object?[] objs)
    {
        return objs.Any(isEmpty);
    }

    /// <summary>
    /// 是否全都为null或空对象
    /// </summary>
    public static bool isAllEmpty(params object?[] objs)
    {
        return objs.All(isEmpty);
    }

    /// <summary>
    /// 是否全都不为null或空对象
    /// </summary>
    public static bool isAllNotEmpty(params object?[] objs)
    {
        return objs.All(isNotEmpty);
    }
}
