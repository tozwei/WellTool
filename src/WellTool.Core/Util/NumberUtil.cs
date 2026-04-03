using System;
using System.Globalization;
using System.Numerics;

namespace WellTool.Core.Util;

/// <summary>
/// 数字工具类
/// </summary>
public static class NumberUtil
{
	/// <summary>
	/// 判断字符串是否为数字
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>是否为数字</returns>
	public static bool IsNumber(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		return double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
	}

	/// <summary>
	/// 判断字符串是否为整数
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>是否为整数</returns>
	public static bool IsInteger(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		return long.TryParse(text, out _);
	}

	/// <summary>
	/// 判断字符串是否为浮点数
	/// </summary>
	/// <param name="text">文本</param>
	/// <returns>是否为浮点数</returns>
	public static bool IsDecimal(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		return double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
	}

	/// <summary>
	/// 字符串转换为整数
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>整数</returns>
	public static int ToInt(string text, int defaultValue = 0)
	{
		if (int.TryParse(text, out var result))
		{
			return result;
		}
		return defaultValue;
	}

	/// <summary>
	/// 字符串转换为长整数
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>长整数</returns>
	public static long ToLong(string text, long defaultValue = 0)
	{
		if (long.TryParse(text, out var result))
		{
			return result;
		}
		return defaultValue;
	}

	/// <summary>
	/// 字符串转换为浮点数
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>浮点数</returns>
	public static double ToDouble(string text, double defaultValue = 0)
	{
		if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
		{
			return result;
		}
		return defaultValue;
	}

	/// <summary>
	/// 字符串转换为decimal
	/// </summary>
	/// <param name="text">文本</param>
	/// <param name="defaultValue">默认值</param>
	/// <returns>decimal</returns>
	public static decimal ToDecimal(string text, decimal defaultValue = 0)
	{
		if (decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
		{
			return result;
		}
		return defaultValue;
	}

	/// <summary>
	/// 计算加法
	/// </summary>
	public static T Add<T>(T a, T b) where T : INumber<T>
	{
		return a + b;
	}

	/// <summary>
	/// 计算减法
	/// </summary>
	public static T Sub<T>(T a, T b) where T : INumber<T>
	{
		return a - b;
	}

	/// <summary>
	/// 计算乘法
	/// </summary>
	public static T Mul<T>(T a, T b) where T : INumber<T>
	{
		return a * b;
	}

	/// <summary>
	/// 计算除法
	/// </summary>
	public static T Div<T>(T a, T b) where T : INumber<T>
	{
		return a / b;
	}

	/// <summary>
	/// 计算百分比
	/// </summary>
	/// <param name="value">值</param>
	/// <param name="total">总数</param>
	/// <param name="scale">小数位数</param>
	/// <returns>百分比字符串</returns>
	public static string Percent(double value, double total, int scale = 2)
	{
		if (total == 0)
		{
			return "0%";
		}
		return (value / total * 100).ToString($"F{scale}") + "%";
	}

	/// <summary>
	/// 四舍五入
	/// </summary>
	/// <param name="value">值</param>
	/// <param name="scale">小数位数</param>
	/// <returns>四舍五入后的值</returns>
	public static double Round(double value, int scale = 2)
	{
		return Math.Round(value, scale);
	}

	/// <summary>
	/// 格式化数字
	/// </summary>
	/// <param name="value">值</param>
	/// <param name="format">格式</param>
	/// <returns>格式化后的字符串</returns>
	public static string Format(double value, string format)
	{
		return value.ToString(format);
	}
}
