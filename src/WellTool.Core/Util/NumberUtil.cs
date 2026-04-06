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

	/// <summary>
	/// 加法
	/// </summary>
	public static int Add(int a, int b) => a + b;

	/// <summary>
	/// 加法
	/// </summary>
	public static long Add(long a, long b) => a + b;

	/// <summary>
	/// 加法
	/// </summary>
	public static double Add(double a, double b) => a + b;

	/// <summary>
	/// 取反
	/// </summary>
	public static int Negate(int value) => -value;

	/// <summary>
	/// 取反
	/// </summary>
	public static long Negate(long value) => -value;

	/// <summary>
	/// 取反
	/// </summary>
	public static double Negate(double value) => -value;

	/// <summary>
	/// 向下取整
	/// </summary>
	public static double Floor(double value) => System.Math.Floor(value);

	/// <summary>
	/// 向上取整
	/// </summary>
	public static double Ceil(double value) => System.Math.Ceiling(value);

	/// <summary>
	/// 是否能被整除
	/// </summary>
	public static bool IsDivisible(int dividend, int divisor) => divisor != 0 && dividend % divisor == 0;

	/// <summary>
	/// 生成指定范围内的数字数组
	/// </summary>
	public static int[] Range(int start, int end) {
		if (start > end) {
			return Array.Empty<int>();
		}
		int[] result = new int[end - start + 1];
		for (int i = 0; i < result.Length; i++) {
			result[i] = start + i;
		}
		return result;
	}

	/// <summary>
	/// 计算百分比
	/// </summary>
	public static double PercentOf(double value, double total) => total != 0 ? (value / total) * 100 : 0;

	/// <summary>
	/// 平方根
	/// </summary>
	public static double Sqrt(double value) => System.Math.Sqrt(value);

	/// <summary>
	/// 幂运算
	/// </summary>
	public static double Pow(double value, double exponent) => System.Math.Pow(value, exponent);

	/// <summary>
	/// 阶乘
	/// </summary>
	public static long Factorial(int n) {
		if (n < 0) {
			throw new ArgumentException("Factorial is not defined for negative numbers");
		}
		if (n == 0 || n == 1) {
			return 1;
		}
		long result = 1;
		for (int i = 2; i <= n; i++) {
			result *= i;
		}
		return result;
	}

	/// <summary>
	/// 中位数
	/// </summary>
	public static double Median(params double[] values) {
		if (values == null || values.Length == 0) {
			throw new ArgumentException("Values array cannot be null or empty");
		}
		double[] sorted = (double[])values.Clone();
		Array.Sort(sorted);
		int mid = sorted.Length / 2;
		if (sorted.Length % 2 == 0) {
			return (sorted[mid - 1] + sorted[mid]) / 2;
		} else {
			return sorted[mid];
		}
	}
}
