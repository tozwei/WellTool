using System;
using System.Text;

namespace WellTool.Core.Convert;

/// <summary>
/// 数字中文格式化工具
/// </summary>
public static class NumberChineseFormatter
{
	private static readonly string[] CHINESE_DIGITS = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
	private static readonly string[] UNITS = { "", "十", "百", "千" };

	/// <summary>
	/// 格式化为中文金额
	/// </summary>
	/// <param name="value">数值</param>
	/// <returns>中文金额</returns>
	public static string Format(long value) => Format(value, false);

	/// <summary>
	/// 格式化为中文金额
	/// </summary>
	/// <param name="value">数值</param>
	/// <param name="isUpper">是否大写</param>
	/// <returns>中文金额</returns>
	public static string Format(long value, bool isUpper)
	{
		if (value == 0)
			return isUpper ? "零" : "零";

		var negative = value < 0;
		if (negative)
			value = -value;

		var sb = new StringBuilder();
		var unitsIndex = 0;

		while (value > 0)
		{
			var n = value % 10;
			if (sb.Length > 0 && n > 0)
				sb.Insert(0, UNITS[unitsIndex]);
			sb.Insert(0, isUpper ? ToUpper(n) : CHINESE_DIGITS[n]);
			value /= 10;
			unitsIndex++;
		}

		if (negative)
			sb.Insert(0, "负");

		return sb.ToString();
	}

	/// <summary>
	/// 转换为中文数字（简写）
	/// </summary>
	/// <param name="number">数字</param>
	/// <param name="isSimple">是否简写</param>
	/// <returns>中文数字</returns>
	public static string Format(int number, bool isSimple = true)
	{
		if (number == 0)
			return CHINESE_DIGITS[0];

		var negative = number < 0;
		if (negative)
			number = -number;

		var sb = new StringBuilder();
		var unitsIndex = 0;

		while (number > 0)
		{
			var n = number % 10;
			if (sb.Length > 0 && n > 0)
				sb.Insert(0, UNITS[unitsIndex]);
			sb.Insert(0, CHINESE_DIGITS[n]);
			number /= 10;
			unitsIndex++;
		}

		if (negative)
			sb.Insert(0, "负");

		return sb.ToString();
	}

	private static string ToUpper(int n)
	{
		var upper = new[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
		return upper[n];
	}
}
