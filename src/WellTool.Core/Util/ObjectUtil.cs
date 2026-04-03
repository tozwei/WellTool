using System;

namespace WellDone.Core.Util;

/// <summary>
/// ObjectUtil对象工具类
/// </summary>
public static class ObjectUtil
{
	/// <summary>
	/// 是否为空
	/// </summary>
	public static bool IsNull(object obj) => obj == null;

	/// <summary>
	/// 是否不为空
	/// </summary>
	public static bool IsNotNull(object obj) => obj != null;

	/// <summary>
	/// 是否为空（null或空字符串）
	/// </summary>
	public static bool IsEmpty(object obj)
	{
		if (obj == null)
			return true;
		if (obj is string str)
			return string.IsNullOrEmpty(str);
		return false;
	}

	/// <summary>
	/// 是否不为空
	/// </summary>
	public static bool IsNotEmpty(object obj) => !IsEmpty(obj);

	/// <summary>
	/// 默认值
	/// </summary>
	public static T DefaultIfNull<T>(T obj, T defaultValue) where T : class => obj ?? defaultValue;

	/// <summary>
	/// 默认值（当为空或null时）
	/// </summary>
	public static object DefaultIfEmpty(object obj, object defaultValue) => IsEmpty(obj) ? defaultValue : obj;

	/// <summary>
	/// 是否相等
	/// </summary>
	public static bool Equals(object a, object b) => Equals(a, b);

	/// <summary>
	/// 是否不相等
	/// </summary>
	public static bool NotEqual(object a, object b) => !Equals(a, b);

	/// <summary>
	/// 获取hashCode
	/// </summary>
	public static int HashCode(object obj) => obj?.GetHashCode() ?? 0;

	/// <summary>
	/// 获取类型名称
	/// </summary>
	public static string GetTypeName(object obj) => obj?.GetType().Name;

	/// <summary>
	/// 是否为数组
	/// </summary>
	public static bool IsArray(object obj) => obj is Array;

	/// <summary>
	/// 是否为基础类型
	/// </summary>
	public static bool IsBasicType(object obj)
	{
		if (obj == null)
			return false;
		var type = obj.GetType();
		return type.IsPrimitive || type == typeof(string) || type == typeof(decimal);
	}

	/// <summary>
	/// toString
	/// </summary>
	public static string ToString(object obj) => obj?.ToString();
}
