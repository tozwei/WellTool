using System;
using System.Text;

namespace WellTool.Core.Convert;

/// <summary>
/// 数字中文转换工具类
/// </summary>
public static class NumberWordFormatter
{
	private static readonly string[] UNITS = { "", "十", "百", "千", "万", "亿" };
	private static readonly string[] NUMBERS = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };

	/// <summary>
	/// 格式化为中文数字
	/// </summary>
	/// <param name="number">数字</param>
	/// <param name="isUpper">是否大写</param>
	/// <returns>中文数字</returns>
	public static string Format(long number, bool isUpper = false)
	{
		if (number == 0)
			return isUpper ? "零" : "零";

		var negative = number < 0;
		if (negative)
			number = -number;

		var sb = new StringBuilder();
		var level = 0;

		while (number > 0)
		{
			var n = number % 10;
			if (sb.Length > 0 && n > 0)
				sb.Insert(0, UNITS[level]);
			sb.Insert(0, isUpper ? ToChineseUpper((int)n) : ToChinese((int)n));
			number /= 10;
			level++;
		}

		if (negative)
			sb.Insert(0, "负");

		return sb.ToString();
	}

	/// <summary>
	/// 转换为中文数字
	/// </summary>
	private static string ToChinese(int n) => NUMBERS[n];

	/// <summary>
	/// 转换为中文大写数字
	/// </summary>
	private static string ToChineseUpper(int n)
	{
		var upper = new[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
		return upper[n];
	}

	/// <summary>
	/// 格式化为中文大写数字
	/// </summary>
	/// <param name="number">数字</param>
	/// <returns>中文大写数字</returns>
	public static string FormatUpper(long number)
	{
		return Format(number, true);
	}

	/// <summary>
	/// 格式化为中文数字（支持小数）
	/// </summary>
	/// <param name="number">数字</param>
	/// <param name="isUpper">是否大写</param>
	/// <returns>中文数字</returns>
	public static string FormatDecimal(double number, bool isUpper = false)
	{
		var parts = number.ToString().Split('.');
		var integerPart = long.Parse(parts[0]);
		var result = Format(integerPart, isUpper);

		if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
		{
			var decimalPart = parts[1];
			var decimalSb = new StringBuilder();
			decimalSb.Append("点");

			foreach (var c in decimalPart)
			{
				var digit = int.Parse(c.ToString());
				decimalSb.Append(isUpper ? ToChineseUpper(digit) : ToChinese(digit));
			}

			result += decimalSb.ToString();
		}

		return result;
	}
}
