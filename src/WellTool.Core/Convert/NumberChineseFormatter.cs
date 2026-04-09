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

		// 特殊处理数字 10
		if (number == 10)
		{
			return negative ? "负" + (isSimple ? "十" : "拾") : (isSimple ? "十" : "拾");
		}

		// 特殊处理数字 100
		if (number == 100)
		{
			return negative ? "负" + (isSimple ? "一百" : "壹佰") : (isSimple ? "一百" : "壹佰");
		}

		var sb = new StringBuilder();
		var unitsIndex = 0;

		while (number > 0)
		{
			var n = number % 10;
			if (n > 0 || unitsIndex == 2) // 处理百位
			{
				if (sb.Length > 0 && unitsIndex < UNITS.Length)
				{
					sb.Insert(0, isSimple ? UNITS[unitsIndex] : ToUpperUnit(unitsIndex));
				}
				else if (unitsIndex == 4) // 万位
				{
					sb.Insert(0, isSimple ? "万" : "万");
				}
				if (n > 0)
				{
					sb.Insert(0, isSimple ? CHINESE_DIGITS[n] : ToUpper(n));
				}
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
		{
			sb.Insert(0, "负");
		}

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
		// 实现中文金额格式化
		if (value == 0)
			return isTraditional ? "零" : "零";

		var negative = value < 0;
		if (negative)
			value = -value;

		// 分离整数部分和小数部分
		long integerPart = (long)value;
		double decimalPart = value - integerPart;

		var sb = new StringBuilder();

		// 处理整数部分
		if (integerPart > 0)
		{
			sb.Append(Format(integerPart, isTraditional));
			if (useUnit)
			{
				sb.Append("元");
			}
		}

		// 处理小数部分
		if (decimalPart > 0)
		{
			// 转换小数部分为两位
			int fen = (int)(decimalPart * 100);
			int jiao = fen / 10;
			fen = fen % 10;

			if (jiao > 0)
			{
				sb.Append(isTraditional ? ToUpper(jiao) : CHINESE_DIGITS[jiao]);
				if (useUnit)
				{
					sb.Append("角");
				}
			}

			if (fen > 0)
			{
				sb.Append(isTraditional ? ToUpper(fen) : CHINESE_DIGITS[fen]);
				if (useUnit)
				{
					sb.Append("分");
				}
			}
		}
		else if (useUnit)
		{
			// 没有小数部分，添加"整"
			sb.Append("整");
		}

		if (negative)
		{
			sb.Insert(0, "负");
		}

		return sb.ToString();
	}

	/// <summary>
	/// 中文金额转数字
	/// </summary>
	/// <param name="chineseMoney">中文金额</param>
	/// <returns>转换后的数字</returns>
	public static decimal? ChineseMoneyToNumber(string chineseMoney)
	{
		// 实现中文金额转数字
		if (string.IsNullOrEmpty(chineseMoney))
			return null;

		// 去除空格
		chineseMoney = chineseMoney.Trim();

		// 处理负数
		bool negative = false;
		if (chineseMoney.StartsWith("负"))
		{
			negative = true;
			chineseMoney = chineseMoney.Substring(1);
		}

		// 定义中文数字映射
		var chineseToNumber = new Dictionary<string, int>
		{
			{ "零", 0 },
			{ "一", 1 }, { "壹", 1 },
			{ "二", 2 }, { "贰", 2 },
			{ "三", 3 }, { "叁", 3 },
			{ "四", 4 }, { "肆", 4 },
			{ "五", 5 }, { "伍", 5 },
			{ "六", 6 }, { "陆", 6 },
			{ "七", 7 }, { "柒", 7 },
			{ "八", 8 }, { "捌", 8 },
			{ "九", 9 }, { "玖", 9 },
			{ "十", 10 }, { "拾", 10 },
			{ "百", 100 }, { "佰", 100 },
			{ "千", 1000 }, { "仟", 1000 },
			{ "万", 10000 },
			{ "亿", 100000000 }
		};

		decimal result = 0;
		decimal currentUnit = 1;
		decimal temp = 0;

		// 处理中文金额
		for (int i = chineseMoney.Length - 1; i >= 0; i--)
		{
			string charStr = chineseMoney[i].ToString();
			if (chineseToNumber.TryGetValue(charStr, out int value))
			{
				if (value >= 10)
				{
					// 单位
					if (value > currentUnit)
					{
						currentUnit = value;
						temp = 0;
					}
					else
					{
						currentUnit *= value;
					}
				}
				else
				{
					// 数字
					temp += value * currentUnit;
				}
			}
			else if (charStr == "元")
			{
				result += temp;
				temp = 0;
				currentUnit = 0.1m; // 角
			}
			else if (charStr == "角")
			{
				result += temp * 0.1m;
				temp = 0;
				currentUnit = 0.01m; // 分
			}
			else if (charStr == "分")
			{
				result += temp * 0.01m;
				temp = 0;
				currentUnit = 1;
			}
		}

		// 处理最后剩下的部分
		if (temp > 0)
		{
			result += temp;
		}

		if (negative)
		{
			result = -result;
		}

		return result;
	}
}
