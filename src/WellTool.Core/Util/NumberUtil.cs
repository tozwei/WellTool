using System;

namespace WellTool.Core.Util;

/// <summary>
/// NumberUtil数字工具类
/// </summary>
public static class NumberUtil
{
	/// <summary>
	/// 是否为数字
	/// </summary>
	public static bool IsNumber(object obj)
	{
		if (obj == null)
			return false;
		return double.TryParse(obj.ToString(), out _);
	}

	/// <summary>
	/// 是否为整数
	/// </summary>
	public static bool IsInteger(object obj)
	{
		if (obj == null)
			return false;
		return long.TryParse(obj.ToString(), out _);
	}

	/// <summary>
	/// 解析整数
	/// </summary>
	public static int ParseInt(string str) => int.Parse(str);

	/// <summary>
	/// 解析整数（安全）
	/// </summary>
	public static int ParseInt(string str, int defaultValue)
	{
		if (int.TryParse(str, out var result))
			return result;
		return defaultValue;
	}

	/// <summary>
	/// 解析长整数
	/// </summary>
	public static long ParseLong(string str) => long.Parse(str);

	/// <summary>
	/// 解析长整数（安全）
	/// </summary>
	public static long ParseLong(string str, long defaultValue)
	{
		if (long.TryParse(str, out var result))
			return result;
		return defaultValue;
	}

	/// <summary>
	/// 解析浮点数
	/// </summary>
	public static double ParseDouble(string str) => double.Parse(str);

	/// <summary>
	/// 解析浮点数（安全）
	/// </summary>
	public static double ParseDouble(string str, double defaultValue)
	{
		if (double.TryParse(str, out var result))
			return result;
		return defaultValue;
	}

	/// <summary>
	/// 保留小数位
	/// </summary>
	public static string Round(double value, int decimals) => System.Math.Round(value, decimals).ToString($"F{decimals}");

	/// <summary>
	/// 取绝对值
	/// </summary>
	public static int Abs(int value) => System.Math.Abs(value);

	/// <summary>
	/// 取绝对值
	/// </summary>
	public static long Abs(long value) => System.Math.Abs(value);

	/// <summary>
	/// 取绝对值
	/// </summary>
	public static double Abs(double value) => System.Math.Abs(value);

	/// <summary>
	/// 最大值
	/// </summary>
	public static int Max(int a, int b) => System.Math.Max(a, b);

	/// <summary>
	/// 最大值
	/// </summary>
	public static long Max(long a, long b) => System.Math.Max(a, b);

	/// <summary>
	/// 最大值
	/// </summary>
	public static double Max(double a, double b) => System.Math.Max(a, b);

	/// <summary>
	/// 最小值
	/// </summary>
	public static int Min(int a, int b) => System.Math.Min(a, b);

	/// <summary>
	/// 最小值
	/// </summary>
	public static long Min(long a, long b) => System.Math.Min(a, b);

	/// <summary>
	/// 最小值
	/// </summary>
	public static double Min(double a, double b) => System.Math.Min(a, b);

	/// <summary>
	/// 是否为偶数
	/// </summary>
	public static bool IsEven(int value) => value % 2 == 0;

	/// <summary>
	/// 是否为奇数
	/// </summary>
	public static bool IsOdd(int value) => value % 2 != 0;
}
