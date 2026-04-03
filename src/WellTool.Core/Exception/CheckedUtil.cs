using System;
using System.Runtime.CompilerServices;

namespace WellTool.Core.Exception;

/// <summary>
/// Checked工具类
/// </summary>
public static class CheckedUtil
{
	/// <summary>
	/// 如果条件不满足则抛出异常
	/// </summary>
	/// <param name="condition">条件</param>
	/// <param name="errorMessage">错误消息</param>
	public static void Check(bool condition, string errorMessage = "Condition not met")
	{
		if (!condition)
			throw new ValidateException(errorMessage);
	}

	/// <summary>
	/// 如果对象为null则抛出异常
	/// </summary>
	/// <param name="obj">对象</param>
	/// <param name="errorMessage">错误消息</param>
	public static void NotNull(object obj, string errorMessage = "Object cannot be null")
	{
		if (obj == null)
			throw new ValidateException(errorMessage);
	}

	/// <summary>
	/// 如果值为空则抛出异常
	/// </summary>
	/// <param name="value">值</param>
	/// <param name="errorMessage">错误消息</param>
	public static void NotEmpty(string value, string errorMessage = "String cannot be empty")
	{
		if (string.IsNullOrEmpty(value))
			throw new ValidateException(errorMessage);
	}
}
