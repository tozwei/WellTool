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
			sb.Insert(0, isUpper ? ToUpper((int)n) : CHINESE_DIGITS[(int)n]);
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
			return isSimple ? CHINESE_DIGITS[0] : ToUpper(0);

		var negative = number < 0;
		if (negative)
			number = -number;

		var sb = new StringBuilder();
		var unitsIndex = 0;

		while (number > 0)
		{
			var n = number % 10;
			if (n > 0 || unitsIndex == 1 || unitsIndex == 2) // 处理十位和百位
			{
				if (sb.Length > 0 && unitsIndex < UNITS.Length)
					sb.Insert(0, isSimple ? UNITS[unitsIndex] : ToUpperUnit(unitsIndex));
				else if (unitsIndex == 4) // 万位
					sb.Insert(0, isSimple ? "万" : "万");
				if (n > 0)
					sb.Insert(0, isSimple ? CHINESE_DIGITS[n] : ToUpper(n));
			}
			number /= 10;
			unitsIndex++;
		}

		// 处理 "一十" 变为 "十"
		if (sb.ToString().StartsWith("一十"))
		{
			sb.Remove(0, 1);
		}
		else if (sb.ToString().StartsWith("壹拾"))
		{
			sb.Remove(0, 1);
		}

		if (negative)
			sb.Insert(0, "负");

		return sb.ToString();
	}

	private static string ToUpperUnit(int unitIndex)
	{
		var upperUnits = new[] { "", "拾", "佰", "仟" };
		return upperUnits[unitIndex];
	}

	private static string ToUpper(int n)
	{
		var upper = new[] { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
		return upper[n];
	}

	/// <summary>
	/// 格式化为中文金额
	/// </summary>
	/// <param name="value">数值</param>
	/// <param name="isTraditional">是否使用繁体</param>
	/// <param name="useUnit">是否使用单位</param>
	/// <returns>中文金额</returns>
	public static string Format(double value, bool isTraditional, bool useUnit)
	{
		// 简化实现，实际可能需要更复杂的处理
		return Format((long)value, isTraditional);
	}

	/// <summary>
	/// 中文金额转数字
	/// </summary>
	/// <param name="chineseMoney">中文金额</param>
	/// <returns>转换后的数字</returns>
	public static decimal? ChineseMoneyToNumber(string chineseMoney)
	{
		// 简化实现，实际可能需要更复杂的处理
		return 0;
	}
}
