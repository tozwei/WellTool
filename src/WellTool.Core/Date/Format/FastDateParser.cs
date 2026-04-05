namespace WellTool.Core.Date.Format;

using System;
using System.Globalization;
using SystemTimeZone = System.TimeZone;
using Calendar = WellTool.Core.Date.Calendar;

/// <summary>
/// 日期解析器 - SimpleDateFormat的线程安全版本
/// </summary>
public class FastDateParser : AbstractDateBasic, DateParser
{
	private const long SerialVersionUid = -3199383897950947498L;

	/// <summary>
	/// 世纪开始年份
	/// </summary>
	private readonly int Century;

	/// <summary>
	/// 开始年份
	/// </summary>
	private readonly int StartYear;

	/// <summary>
	/// 日期格式信息
	/// </summary>
	private readonly DateTimeFormatInfo FormatInfo;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	public FastDateParser(string pattern, SystemTimeZone timeZone, CultureInfo locale)
		: base(pattern, timeZone, locale)
	{
		var definingCalendar = DateTime.Now;
		var centuryStartYear = definingCalendar.Year - 80;
		Century = (centuryStartYear / 100) * 100;
		StartYear = centuryStartYear - Century;
		FormatInfo = locale.DateTimeFormat;
	}

	/// <summary>
	/// 解析日期字符串
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <returns>{DateTime}</returns>
	public DateTime Parse(string source)
	{
		var pos = 0;
		var date = Parse(source, ref pos);
		if (date == null)
			throw new FormatException($"Unparseable date: {source}");
		return date;
	}

	/// <summary>
	/// 解析日期字符串
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <returns>{DateTime}</returns>
	public DateTime Parse(string source, ref int pos)
	{
		var calendar = new Calendar(TimeZoneInfo.Local, Locale);
		if (Parse(source, ref pos, calendar))
			return calendar.Time;
		return DateTime.MinValue;
	}

	/// <summary>
	/// 解析日期字符串到日历
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <param name="calendar">日历</param>
	/// <returns>是否成功</returns>
	public bool Parse(string source, ref int pos, Calendar calendar)
	{
		var index = pos;
		var length = Pattern.Length;

		while (index < length && pos < source.Length)
		{
			var token = ParseToken(Pattern, ref index);
			if (string.IsNullOrEmpty(token))
				break;

			// 跳过空白
			while (pos < source.Length && char.IsWhiteSpace(source[pos]))
				pos++;

			if (pos >= source.Length)
				return false;

			if (!ParseTokenValue(calendar, token, source, ref pos))
				return false;
		}

		return true;
	}

	private string ParseToken(string pattern, ref int index)
	{
		var sb = new System.Text.StringBuilder();
		var length = pattern.Length;

		if (index >= length)
			return string.Empty;

		var c = pattern[index];

		if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
		{
			sb.Append(c);
			while (index + 1 < length && pattern[index + 1] == c)
			{
				sb.Append(c);
				index++;
			}
		}
		else
		{
			sb.Append(c);
			index++;
		}

		return sb.ToString();
	}

	private bool ParseTokenValue(Calendar calendar, string token, string source, ref int pos)
	{
		if (string.IsNullOrEmpty(token))
			return true;

		var c = token[0];
		var len = token.Length;

		switch (c)
		{
			case 'y': // 年份
				{
					var yearStr = ParseNumber(source, ref pos, len);
					if (yearStr == null) return false;
					var year = int.Parse(yearStr);
					if (year < 100) year = AdjustYear(year);
					calendar.Set(1, year);
					return true;
				}
			case 'M': // 月份
				{
					if (len >= 3)
					{
						// 文本月份
						var monthName = ParseText(source, ref pos, FormatInfo.AbbreviatedMonthNames);
						if (monthName >= 0)
						{
							calendar.Set(2, monthName);
							return true;
						}
					}
					var monthStr = ParseNumber(source, ref pos, len);
					if (monthStr == null) return false;
					var month = int.Parse(monthStr);
					calendar.Set(2, month - 1); // 月份从0开始
					return true;
				}
			case 'd': // 日期
				{
					var dayStr = ParseNumber(source, ref pos, len);
					if (dayStr == null) return false;
					var day = int.Parse(dayStr);
					calendar.Set(5, day);
					return true;
				}
			case 'H': // 24小时
			case 'h': // 12小时
				{
					var hourStr = ParseNumber(source, ref pos, len);
					if (hourStr == null) return false;
					var hour = int.Parse(hourStr);
					calendar.Set(10, hour);
					return true;
				}
			case 'm': // 分钟
				{
					var minStr = ParseNumber(source, ref pos, len);
					if (minStr == null) return false;
					var minute = int.Parse(minStr);
					calendar.Set(12, minute);
					return true;
				}
			case 's': // 秒
				{
					var secStr = ParseNumber(source, ref pos, len);
					if (secStr == null) return false;
					var second = int.Parse(secStr);
					calendar.Set(13, second);
					return true;
				}
			case 'S': // 毫秒
				{
					var msStr = ParseNumber(source, ref pos, len);
					if (msStr == null) return false;
					var ms = int.Parse(msStr);
					calendar.Set(14, ms);
					return true;
				}
			default:
				// 字面量 - 跳过
				for (var i = 0; i < token.Length && pos < source.Length; i++)
				{
					if (source[pos] == token[i])
						pos++;
				}
				return true;
		}
	}

	private string ParseNumber(string source, ref int pos, int maxLen)
	{
		var sb = new System.Text.StringBuilder();
		var count = 0;

		while (pos < source.Length && count < maxLen)
		{
			var c = source[pos];
			if (char.IsDigit(c))
			{
				sb.Append(c);
				pos++;
				count++;
			}
			else
			{
				break;
			}
		}

		return count > 0 ? sb.ToString() : null;
	}

	private int ParseText(string source, ref int pos, string[] values)
	{
		if (values == null)
			return -1;

		for (var i = 0; i < values.Length && pos < source.Length; i++)
		{
			var value = values[i];
			if (value != null && source.Length >= pos + value.Length)
			{
				var match = true;
				for (var j = 0; j < value.Length; j++)
				{
					if (char.ToLower(source[pos + j]) != char.ToLower(value[j]))
					{
						match = false;
						break;
					}
				}
				if (match)
				{
					pos += value.Length;
					return i;
				}
			}
		}
		return -1;
	}

	private int AdjustYear(int twoDigitYear)
	{
		var trial = Century + twoDigitYear;
		return twoDigitYear >= StartYear ? trial : trial + 100;
	}

	/// <summary>
	/// 解析对象
	/// </summary>
	public object ParseObject(string source)
	{
		return Parse(source);
	}

	/// <summary>
	/// 解析对象
	/// </summary>
	public object ParseObject(string source, ref int pos)
	{
		return Parse(source, ref pos);
	}
}
