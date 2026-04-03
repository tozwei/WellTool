using System;

namespace WellTool.Core.Lang;

/// <summary>
/// 断言工具类
/// </summary>
public static class Assert
{
	/// <summary>
	/// 断言对象不为空
	/// </summary>
	/// <param name="obj">对象</param>
	/// <param name="errorMsg">错误信息</param>
	public static void NotNull(object obj, string errorMsg = "Object cannot be null")
	{
		if (obj == null)
		{
			throw new ArgumentNullException(nameof(obj), errorMsg);
		}
	}

	/// <summary>
	/// 断言字符串不为空
	/// </summary>
	/// <param name="str">字符串</param>
	/// <param name="errorMsg">错误信息</param>
	public static void NotBlank(string str, string errorMsg = "String cannot be blank")
	{
		if (string.IsNullOrWhiteSpace(str))
		{
			throw new ArgumentException(errorMsg, nameof(str));
		}
	}

	/// <summary>
	/// 断言条件为真
	/// </summary>
	/// <param name="condition">条件</param>
	/// <param name="errorMsg">错误信息</param>
	public static void IsTrue(bool condition, string errorMsg = "Condition must be true")
	{
		if (!condition)
		{
			throw new InvalidOperationException(errorMsg);
		}
	}

	/// <summary>
	/// 断言条件为假
	/// </summary>
	/// <param name="condition">条件</param>
	/// <param name="errorMsg">错误信息</param>
	public static void IsFalse(bool condition, string errorMsg = "Condition must be false")
	{
		if (condition)
		{
			throw new InvalidOperationException(errorMsg);
		}
	}

	/// <summary>
	/// 断言值在范围内
	/// </summary>
	/// <typeparam name="T">值类型</typeparam>
	/// <param name="value">值</param>
	/// <param name="min">最小值</param>
	/// <param name="max">最大值</param>
	/// <param name="errorMsg">错误信息</param>
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
	/// <param name="array">数组</param>
	/// <param name="errorMsg">错误信息</param>
	public static void NotEmpty(Array array, string errorMsg = "Array cannot be empty")
	{
		if (array == null || array.Length == 0)
		{
			throw new ArgumentException(errorMsg, nameof(array));
		}
	}

	/// <summary>
	/// 断言集合不为空
	/// </summary>
	/// <param name="collection">集合</param>
	/// <param name="errorMsg">错误信息</param>
	public static void NotEmpty<T>(System.Collections.Generic.ICollection<T> collection, string errorMsg = "Collection cannot be empty")
	{
		if (collection == null || collection.Count == 0)
		{
			throw new ArgumentException(errorMsg, nameof(collection));
		}
	}
}
