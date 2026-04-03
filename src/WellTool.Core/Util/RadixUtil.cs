using System;
using System.Numerics;

namespace WellTool.Core.Util;

/// <summary>
/// 进制工具类
/// </summary>
public static class RadixUtil
{
	private const string DIGITS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
	private const string BASE62 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
	private const string BASE36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

	/// <summary>
	/// 将数字转换为指定进制的字符串
	/// </summary>
	/// <param name="value">数字</param>
	/// <param name="radix">进制（2-36）</param>
	/// <returns>进制字符串</returns>
	public static string ToString(long value, int radix)
	{
		if (radix < 2 || radix > 36)
		{
			throw new ArgumentException("Radix must be between 2 and 36");
		}

		if (value == 0)
		{
			return "0";
		}

		var isNegative = value < 0;
		var absValue = Math.Abs(value);
		var result = new char[32];
		var index = 32;

		while (absValue > 0)
		{
			result[--index] = DIGITS[(int)(absValue % radix)];
			absValue /= radix;
		}

		if (isNegative)
		{
			result[--index] = '-';
		}

		return new string(result, index, 32 - index);
	}

	/// <summary>
	/// 将进制字符串转换为数字
	/// </summary>
	/// <param name="value">进制字符串</param>
	/// <param name="radix">进制（2-36）</param>
	/// <returns>数字</returns>
	public static long ToInt64(string value, int radix)
	{
		if (string.IsNullOrEmpty(value) || radix < 2 || radix > 36)
		{
			throw new ArgumentException("Invalid value or radix");
		}

		var isNegative = value[0] == '-';
		var startIndex = isNegative ? 1 : 0;

		long result = 0;
		for (var i = startIndex; i < value.Length; i++)
		{
			var c = char.ToUpperInvariant(value[i]);
			var digitValue = DIGITS.IndexOf(c);
			if (digitValue < 0 || digitValue >= radix)
			{
				throw new ArgumentException($"Invalid character '{c}' for radix {radix}");
			}
			result = result * radix + digitValue;
		}

		return isNegative ? -result : result;
	}

	/// <summary>
	/// 十进制转二进制
	/// </summary>
	/// <param name="value">十进制数字</param>
	/// <returns>二进制字符串</returns>
	public static string ToBinary(long value)
	{
		return Convert.ToString(value, 2);
	}

	/// <summary>
	/// 二进制转十进制
	/// </summary>
	/// <param name="value">二进制字符串</param>
	/// <returns>十进制数字</returns>
	public static long BinaryToInt64(string value)
	{
		return Convert.ToInt64(value, 2);
	}

	/// <summary>
	/// 十进制转十六进制
	/// </summary>
	/// <param name="value">十进制数字</param>
	/// <returns>十六进制字符串</returns>
	public static string ToHex(long value)
	{
		return Convert.ToString(value, 16).ToUpperInvariant();
	}

	/// <summary>
	/// 十六进制转十进制
	/// </summary>
	/// <param name="value">十六进制字符串</param>
	/// <returns>十进制数字</returns>
	public static long HexToInt64(string value)
	{
		return Convert.ToInt64(value, 16);
	}

	/// <summary>
	/// 将数字转换为Base36字符串
	/// </summary>
	/// <param name="value">数字</param>
	/// <returns>Base36字符串</returns>
	public static string ToBase36(long value)
	{
		return ToString(value, 36);
	}

	/// <summary>
	/// 将Base36字符串转换为数字
	/// </summary>
	/// <param name="value">Base36字符串</param>
	/// <returns>数字</returns>
	public static long Base36ToInt64(string value)
	{
		return ToInt64(value, 36);
	}

	/// <summary>
	/// 将数字转换为Base62字符串
	/// </summary>
	/// <param name="value">数字</param>
	/// <returns>Base62字符串</returns>
	public static string ToBase62(long value)
	{
		if (value == 0)
		{
			return "0";
		}

		var result = new char[32];
		var index = 32;
		var absValue = value;

		while (absValue > 0)
		{
			result[--index] = BASE62[(int)(absValue % 62)];
			absValue /= 62;
		}

		return new string(result, index, 32 - index);
	}
}
