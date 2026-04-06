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
	public static string RoundToString(double value, int decimals) => System.Math.Round(value, decimals).ToString($"F{decimals}");

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

	/// <summary>
	/// 是否为长整数
	/// </summary>
	public static bool IsLong(object obj)
	{
		if (obj == null)
			return false;
		return long.TryParse(obj.ToString(), out _);
	}

	/// <summary>
	/// 加法（支持多个参数）
	/// </summary>
	public static double Add(params double[] values)
	{
		if (values == null || values.Length == 0)
			return 0;
		double sum = 0;
		foreach (var v in values)
			sum += v;
		return sum;
	}

	/// <summary>
	/// 加法（float重载）
	/// </summary>
	public static double Add(float a, float b) => a + b;

	/// <summary>
	/// 减法
	/// </summary>
	public static double Sub(double a, double b) => a - b;

	/// <summary>
	/// 乘法
	/// </summary>
	public static double Mul(params double[] values)
	{
		if (values == null || values.Length == 0)
			return 0;
		double product = 1;
		foreach (var v in values)
			product *= v;
		return product;
	}

	/// <summary>
	/// 除法
	/// </summary>
	public static double Div(double a, double b)
	{
		if (b == 0)
			return 0;
		return a / b;
	}

	/// <summary>
	/// 四舍五入
	/// </summary>
	public static double Round(double value, int decimals)
	{
		return System.Math.Round(value, decimals);
	}

	/// <summary>
	/// 是否在范围内
	/// </summary>
	public static bool IsBetween(double value, double min, double max)
	{
		return value >= min && value <= max;
	}

	/// <summary>
	/// 是否为质数
	/// </summary>
	public static bool IsPrime(int n)
	{
		if (n <= 1) return false;
		if (n <= 3) return true;
		if (n % 2 == 0 || n % 3 == 0) return false;
		for (int i = 5; i * i <= n; i += 6)
		{
			if (n % i == 0 || n % (i + 2) == 0)
				return false;
		}
		return true;
	}

	/// <summary>
	/// 最大值（数组）
	/// </summary>
	public static int Max(params int[] values)
	{
		if (values == null || values.Length == 0)
			throw new ArgumentException("Values array cannot be null or empty");
		int max = values[0];
		foreach (var v in values)
			if (v > max) max = v;
		return max;
	}

	/// <summary>
	/// 最小值（数组）
	/// </summary>
	public static int Min(params int[] values)
	{
		if (values == null || values.Length == 0)
			throw new ArgumentException("Values array cannot be null or empty");
		int min = values[0];
		foreach (var v in values)
			if (v < min) min = v;
		return min;
	}
}
