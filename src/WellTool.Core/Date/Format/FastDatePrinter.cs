namespace WellTool.Core.Date.Format;

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text;
using SystemTimeZone = System.TimeZone;

/// <summary>
/// 日期格式化器 - SimpleDateFormat的线程安全版本
/// </summary>
public class FastDatePrinter : AbstractDateBasic, DatePrinter
{
	private const long SerialVersionUid = -6305750172255764887L;

	/// <summary>
	/// 规则列表
	/// </summary>
	private readonly Rule[] Rules;

	/// <summary>
	/// 最大估算长度
	/// </summary>
	private readonly int MaxLengthEstimate;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	public FastDatePrinter(string pattern, SystemTimeZone timeZone, CultureInfo locale)
		: base(pattern, timeZone, locale)
	{
		Rules = ParsePattern();
		var len = 0;
		for (var i = Rules.Length - 1; i >= 0; i--)
		{
			len += Rules[i].EstimateLength();
		}
		MaxLengthEstimate = len;
	}

	/// <summary>
	/// 解析格式
	/// </summary>
	private Rule[] ParsePattern()
	{
		var rules = new System.Collections.Generic.List<Rule>();
		var symbols = new DateTimeFormatInfo(Locale);
		var length = Pattern.Length;
		var index = 0;

		while (index < length)
		{
			var token = ParseToken(Pattern, ref index);
			if (string.IsNullOrEmpty(token))
				break;

			var rule = CreateRule(token, symbols);
			if (rule != null)
				rules.Add(rule);
		}

		return rules.ToArray();
	}

	private string ParseToken(string pattern, ref int index)
	{
		var sb = new StringBuilder();
		var length = pattern.Length;

		if (index >= length)
			return string.Empty;

		var c = pattern[index];

		if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
		{
			// 扫描相同字符的连续序列
			sb.Append(c);
			while (index + 1 < length && pattern[index + 1] == c)
			{
				sb.Append(c);
				index++;
			}
		}
		else
		{
			// 文本
			while (index < length)
			{
				c = pattern[index];
				if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
					break;
				sb.Append(c);
				index++;
			}
		}

		return sb.ToString();
	}

	private Rule CreateRule(string token, DateTimeFormatInfo symbols)
	{
		if (string.IsNullOrEmpty(token))
			return null;

		var c = token[0];
		var len = token.Length;

		switch (c)
		{
			case 'y': // 年份
				return len == 2 ? new TwoDigitYearRule() : new NumberRule(1, 4, 1);
			case 'M': // 月份
				return len switch
				{
					1 => new UnpaddedMonthRule(),
					2 => new TwoDigitMonthRule(),
					3 => new TextField(2, symbols.AbbreviatedMonthNames),
					_ => new TextField(2, symbols.MonthNames)
				};
			case 'd': // 日期
				return new NumberRule(3, len, 1);
			case 'H': // 24小时
				return new NumberRule(3, len, 0);
			case 'h': // 12小时
				return new TwelveHourRule(new NumberRule(10, len, 0));
			case 'm': // 分钟
				return new NumberRule(2, len, 0);
			case 's': // 秒
				return new NumberRule(1, len, 0);
			case 'S': // 毫秒
				return new NumberRule(0, len, 0);
			case 'E': // 星期
				return new TextField(0, symbols.DayNames);
			case 'a': // AM/PM
				return new TextField(1, symbols.AMDesignators != null ? symbols.AMDesignators : new[] { "AM", "PM" });
			case 'z': // 时区
				return new TimeZoneRule(TimeZone);
			case 'Z': // 时区偏移
				return new TimeZoneOffsetRule();
			default:
				return new StringLiteralRule(token);
		}
	}

	/// <summary>
	/// 格式化毫秒
	/// </summary>
	public string Format(long millis)
	{
		return Format(new DateTime(millis));
	}

	/// <summary>
	/// 格式化日期
	/// </summary>
	public string Format(DateTime date)
	{
		return Format(new Calendar(date));
	}

	/// <summary>
	/// 格式化日历
	/// </summary>
	public string Format(Calendar calendar)
	{
		return Format(new StringBuilder(MaxLengthEstimate), calendar).ToString();
	}

	/// <summary>
	/// 格式化到缓冲区
	/// </summary>
	public StringBuilder Format(long millis, StringBuilder buf)
	{
		return Format(buf, new Calendar(new DateTime(millis)));
	}

	/// <summary>
	/// 格式化到缓冲区
	/// </summary>
	public StringBuilder Format(DateTime date, StringBuilder buf)
	{
		return Format(buf, new Calendar(date));
	}

	/// <summary>
	/// 格式化到缓冲区
	/// </summary>
	public StringBuilder Format(Calendar calendar, StringBuilder buf)
	{
		return Format(buf, calendar);
	}

	private StringBuilder Format(StringBuilder buf, Calendar calendar)
	{
		foreach (var rule in Rules)
		{
			rule.AppendTo(buf, calendar);
		}
		return buf;
	}

	/// <summary>
	/// 获取最大估算长度
	/// </summary>
	public int GetMaxLengthEstimate() => MaxLengthEstimate;

	/// <summary>
	/// 规则接口
	/// </summary>
	private interface Rule
	{
		int EstimateLength();
		void AppendTo(StringBuilder buffer, Calendar calendar);
	}

	/// <summary>
	/// 数字规则
	/// </summary>
	private class NumberRule : Rule
	{
		private readonly int Field;
		private readonly int MinWidth;
		private readonly int MaxWidth;

		public NumberRule(int field, int maxWidth, int minWidth = 1)
		{
			Field = field;
			MinWidth = minWidth;
			MaxWidth = maxWidth;
		}

		public int EstimateLength() => MaxWidth;

		public void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			var value = calendar.GetValue(Field);
			AppendDigits(buffer, value);
		}

		protected virtual void AppendDigits(StringBuilder buffer, int value)
		{
			if (value < 10)
				buffer.Append((char)('0' + value));
			else if (value < 100)
			{
				buffer.Append((char)('0' + value / 10));
				buffer.Append((char)('0' + value % 10));
			}
			else
			{
				var digits = value.ToString();
				for (var i = 0; i < digits.Length; i++)
					buffer.Append(digits[i]);
			}
		}
	}

	/// <summary>
	/// 文本字段规则
	/// </summary>
	private class TextField : Rule
	{
		private readonly int Field;
		private readonly string[] Values;

		public TextField(int field, string[] values)
		{
			Field = field;
			Values = values;
		}

		public int EstimateLength()
		{
			var max = 0;
			foreach (var v in Values)
				if (v != null && v.Length > max)
					max = v.Length;
			return max;
		}

		public void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			var value = calendar.GetValue(Field);
			if (value >= 0 && value < Values.Length && Values[value] != null)
				buffer.Append(Values[value]);
		}
	}

	/// <summary>
	/// 字符串字面量规则
	/// </summary>
	private class StringLiteralRule : Rule
	{
		private readonly string Value;

		public StringLiteralRule(string value)
		{
			Value = value;
		}

		public int EstimateLength() => Value.Length;

		public void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			buffer.Append(Value);
		}
	}

	/// <summary>
	/// 两位数年份规则
	/// </summary>
	private class TwoDigitYearRule : NumberRule
	{
		public TwoDigitYearRule() : base(1, 2, 2) { }

		protected override void AppendDigits(StringBuilder buffer, int value)
		{
			buffer.Append((char)('0' + (value / 10) % 10));
			buffer.Append((char)('0' + value % 10));
		}
	}

	/// <summary>
	/// 未填充月份规则
	/// </summary>
	private class UnpaddedMonthRule : NumberRule
	{
		public UnpaddedMonthRule() : base(2, 2, 1) { }
	}

	/// <summary>
	/// 两位数月份规则
	/// </summary>
	private class TwoDigitMonthRule : NumberRule
	{
		public TwoDigitMonthRule() : base(2, 2, 2) { }

		protected override void AppendDigits(StringBuilder buffer, int value)
		{
			// 月份从1开始
			buffer.Append((char)('0' + (value / 10) % 10));
			buffer.Append((char)('0' + value % 10));
		}

		public new void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			var value = calendar.GetValue(2) + 1; // 月份从0开始
			AppendDigits(buffer, value);
		}
	}

	/// <summary>
	/// 12小时制规则
	/// </summary>
	private class TwelveHourRule : NumberRule
	{
		public TwelveHourRule(NumberRule rule) : base(10, rule.EstimateLength(), 1) { }

		public new void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			var value = calendar.GetValue(10);
			if (value == 0) value = 12;
			AppendDigits(buffer, value);
		}
	}

	/// <summary>
	/// 时区规则
	/// </summary>
	private class TimeZoneRule : Rule
	{
		private readonly SystemTimeZone TimeZone;

		public TimeZoneRule(SystemTimeZone timeZone)
		{
			TimeZone = timeZone;
		}

		public int EstimateLength() => 5;

		public void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			buffer.Append(TimeZone.StandardName);
		}
	}

	/// <summary>
	/// 时区偏移规则
	/// </summary>
	private class TimeZoneOffsetRule : Rule
	{
		public int EstimateLength() => 5;

		public void AppendTo(StringBuilder buffer, Calendar calendar)
		{
			var offset = TimeZone.CurrentTimeZone.GetUtcOffset(calendar.GetDateTime());
			var sign = offset.TotalMinutes >= 0 ? '+' : '-';
			var hours = (int)Math.Abs(offset.TotalMinutes / 60);
			var minutes = (int)Math.Abs(offset.TotalMinutes % 60);
			buffer.Append(sign);
			buffer.Append((char)('0' + hours / 10));
			buffer.Append((char)('0' + hours % 10));
			buffer.Append(':');
			buffer.Append((char)('0' + minutes / 10));
			buffer.Append((char)('0' + minutes % 10));
		}
	}
}
