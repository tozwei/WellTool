namespace WellTool.Core.lang;

using System;
using System.Collections.Generic;

/*
/// <summary>
/// 包装Date，提供扩展方法，如时区等
/// </summary>
public class DateTime : Date
{
	private bool _mutable = true;
	private Week _firstDayOfWeek = Week.Monday;
	private TimeZoneInfo _timeZone;
	private int _minimalDaysInFirstWeek;

	/// <summary>
	/// 构造函数
	/// </summary>
	public DateTime()
	{
		_timeZone = TimeZoneInfo.Local;
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="dateStr">日期字符串</param>
	public DateTime(string dateStr)
	{
		var dt = System.DateTime.Parse(dateStr);
		_ticks = dt.Ticks;
		_timeZone = TimeZoneInfo.Local;
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="dateStr">日期字符串</param>
	/// <param name="format">格式</param>
	public DateTime(string dateStr, string format)
	{
		var dt = System.DateTime.ParseExact(dateStr, format, null);
		_ticks = dt.Ticks;
		_timeZone = TimeZoneInfo.Local;
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="ticks">刻度数</param>
	public DateTime(long ticks)
	{
		_ticks = ticks;
		_timeZone = TimeZoneInfo.Local;
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="dateTime">日期时间</param>
	public DateTime(System.DateTime dateTime)
	{
		_ticks = dateTime.Ticks;
		_timeZone = TimeZoneInfo.Local;
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="timeZone">时区</param>
	public DateTime(TimeZoneInfo timeZone)
	{
		_timeZone = timeZone;
	}

	/// <summary>
	/// 当前时间
	/// </summary>
	/// <returns>当前时间</returns>
	public static DateTime Now()
	{
		return new DateTime(System.DateTime.Now);
	}

	/// <summary>
	/// 获得年的部分
	/// </summary>
	/// <returns>年</returns>
	public int Year()
	{
		return ToDateTime().Year;
	}

	/// <summary>
	/// 获得月份，从0开始计数
	/// </summary>
	/// <returns>月份</returns>
	public int Month()
	{
		return ToDateTime().Month - 1;
	}

	/// <summary>
	/// 获得指定日期是所在年份的第几周
	/// </summary>
	/// <returns>周</returns>
	public int WeekOfYear()
	{
		var culture = CultureInfo.CurrentCulture;
		return culture.Calendar.GetWeekOfYear(
			ToDateTime(),
			CalendarWeekRule.FirstFourDayWeek,
			DayOfWeek.Monday);
	}

	/// <summary>
	/// 获得指定日期是星期几，1表示周日，2表示周一
	/// </summary>
	/// <returns>星期几</returns>
	public int DayOfWeek()
	{
		return (int)ToDateTime().DayOfWeek + 1;
	}

	/// <summary>
	/// 获得指定日期的小时数部分
	/// </summary>
	/// <param name="is24HourClock">是否24小时制</param>
	/// <returns>小时数</returns>
	public int Hour(bool is24HourClock)
	{
		return is24HourClock ? ToDateTime().Hour : ToDateTime().Hour % 12;
	}

	/// <summary>
	/// 获得指定日期的分钟数部分
	/// </summary>
	/// <returns>分钟数</returns>
	public int Minute()
	{
		return ToDateTime().Minute;
	}

	/// <summary>
	/// 获得指定日期的秒数部分
	/// </summary>
	/// <returns>秒数</returns>
	public int Second()
	{
		return ToDateTime().Second;
	}

	/// <summary>
	/// 获得指定日期的毫秒数部分
	/// </summary>
	/// <returns>毫秒数</returns>
	public int Millisecond()
	{
		return ToDateTime().Millisecond;
	}

	/// <summary>
	/// 是否为上午
	/// </summary>
	/// <returns>是否为上午</returns>
	public bool IsAM()
	{
		return ToDateTime().Hour < 12;
	}

	/// <summary>
	/// 是否为下午
	/// </summary>
	/// <returns>是否为下午</returns>
	public bool IsPM()
	{
		return ToDateTime().Hour >= 12;
	}

	/// <summary>
	/// 是否为周末
	/// </summary>
	/// <returns>是否为周末</returns>
	public bool IsWeekend()
	{
		var dow = ToDateTime().DayOfWeek;
		return dow == DayOfWeek.Saturday || dow == DayOfWeek.Sunday;
	}

	/// <summary>
	/// 是否闰年
	/// </summary>
	/// <returns>是否闰年</returns>
	public bool IsLeapYear()
	{
		return DateUtil.IsLeapYear(Year());
	}

	/// <summary>
	/// 转换为System.DateTime
	/// </summary>
	/// <returns>System.DateTime</returns>
	public System.DateTime ToDateTime()
	{
		return new System.DateTime(_ticks, DateTimeKind.Local);
	}

	/// <summary>
	/// 获得一周的第一天，默认为周一
	/// </summary>
	/// <returns>一周的第一天</returns>
	public Week GetFirstDayOfWeek()
	{
		return _firstDayOfWeek;
	}

	/// <summary>
	/// 设置一周的第一天
	/// </summary>
	/// <param name="firstDayOfWeek">一周的第一天</param>
	/// <returns>this</returns>
	public DateTime SetFirstDayOfWeek(Week firstDayOfWeek)
	{
		_firstDayOfWeek = firstDayOfWeek;
		return this;
	}

	/// <summary>
	/// 获取时区
	/// </summary>
	/// <returns>时区</returns>
	public TimeZoneInfo GetTimeZone()
	{
		return _timeZone;
	}

	/// <summary>
	/// 设置时区
	/// </summary>
	/// <param name="timeZone">时区</param>
	/// <returns>this</returns>
	public DateTime SetTimeZone(TimeZoneInfo timeZone)
	{
		_timeZone = timeZone;
		return this;
	}

	/// <summary>
	/// 对象是否可变
	/// </summary>
	/// <returns>对象是否可变</returns>
	public bool IsMutable()
	{
		return _mutable;
	}

	/// <summary>
	/// 设置对象是否可变
	/// </summary>
	/// <param name="mutable">是否可变</param>
	/// <returns>this</returns>
	public DateTime SetMutable(bool mutable)
	{
		_mutable = mutable;
		return this;
	}

	/// <summary>
	/// 调整日期和时间
	/// </summary>
	/// <param name="datePart">调整的部分</param>
	/// <param name="offset">偏移量</param>
	/// <returns>调整后的DateTime</returns>
	public DateTime Offset(DateField datePart, int offset)
	{
		if (DateField.Era == datePart)
		{
			throw new ArgumentException("ERA is not support offset!");
		}

		var dt = ToDateTime();
		var newDt = datePart switch
		{
			DateField.Year => dt.AddYears(offset),
			DateField.Month => dt.AddMonths(offset),
			DateField.DayOfMonth => dt.AddDays(offset),
			DateField.Hour => dt.AddHours(offset),
			DateField.Minute => dt.AddMinutes(offset),
			DateField.Second => dt.AddSeconds(offset),
			DateField.Millisecond => dt.AddMilliseconds(offset),
			_ => dt.AddDays(offset)
		};

		if (_mutable)
		{
			_ticks = newDt.Ticks;
			return this;
		}
		return new DateTime(newDt);
	}

	/// <summary>
	/// 获得日期的某个部分
	/// </summary>
	/// <param name="field">表示日期的哪个部分</param>
	/// <returns>某个部分的值</returns>
	public int GetField(DateField field)
	{
		return field switch
		{
			DateField.Year => Year(),
			DateField.Month => Month(),
			DateField.DayOfMonth => ToDateTime().Day,
			DateField.Hour => Hour(true),
			DateField.Minute => Minute(),
			DateField.Second => Second(),
			DateField.Millisecond => Millisecond(),
			DateField.DayOfWeek => DayOfWeek(),
			DateField.DayOfYear => ToDateTime().DayOfYear,
			_ => 0
		};
	}

	/// <summary>
	/// 设置日期的某个部分
	/// </summary>
	/// <param name="field">表示日期的哪个部分</param>
	/// <param name="value">值</param>
	/// <returns>this</returns>
	public DateTime SetField(DateField field, int value)
	{
		var dt = ToDateTime();
		System.DateTime newDt;

		switch (field)
		{
			case DateField.Year:
				newDt = dt.AddYears(value - dt.Year);
				break;
			case DateField.Month:
				newDt = dt.AddMonths(value - dt.Month);
				break;
			case DateField.DayOfMonth:
				newDt = dt.AddDays(value - dt.Day);
				break;
			case DateField.Hour:
				newDt = dt.AddHours(value - dt.Hour);
				break;
			case DateField.Minute:
				newDt = dt.AddMinutes(value - dt.Minute);
				break;
			case DateField.Second:
				newDt = dt.AddSeconds(value - dt.Second);
				break;
			default:
				newDt = dt;
				break;
		}

		if (_mutable)
		{
			_ticks = newDt.Ticks;
			return this;
		}
		return new DateTime(newDt);
	}

	/// <summary>
	/// 计算相差时长
	/// </summary>
	/// <param name="date">对比的日期</param>
	/// <param name="unit">单位</param>
	/// <returns>相差时长</returns>
	public long Between(DateTime date, DateUnit unit)
	{
		var diff = ToDateTime().Ticks - date.ToDateTime().Ticks;
		return unit switch
		{
			DateUnit.Millisecond => diff / TimeSpan.TicksPerMillisecond,
			DateUnit.Second => diff / TimeSpan.TicksPerSecond,
			DateUnit.Minute => diff / TimeSpan.TicksPerMinute,
			DateUnit.Hour => diff / TimeSpan.TicksPerHour,
			DateUnit.Day => diff / TimeSpan.TicksPerDay,
			_ => diff
		};
	}

	/// <summary>
	/// 转为字符串
	/// </summary>
	/// <returns>格式字符串</returns>
	public override string ToString()
	{
		return ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
	}

	/// <summary>
	/// 转为"yyyy-MM-dd"格式字符串
	/// </summary>
	/// <returns>日期字符串</returns>
	public string ToDateStr()
	{
		return ToDateTime().ToString("yyyy-MM-dd");
	}

	/// <summary>
	/// 转为"HH:mm:ss"格式字符串
	/// </summary>
	/// <returns>时间字符串</returns>
	public string ToTimeStr()
	{
		return ToDateTime().ToString("HH:mm:ss");
	}

	/// <summary>
	/// 转为字符串
	/// </summary>
	/// <param name="format">日期格式</param>
	/// <returns>格式字符串</returns>
	public string ToString(string format)
	{
		return ToDateTime().ToString(format);
	}
}

/// <summary>
/// 日期部分枚举
/// </summary>
public enum DateField
{
	Era,
	Year,
	Month,
	DayOfMonth,
	Hour,
	Minute,
	Second,
	Millisecond,
	DayOfWeek,
	DayOfYear,
	WeekOfMonth,
	WeekOfYear
}

/// <summary>
/// 星期枚举
/// </summary>
public enum Week
{
	Sunday = 0,
	Monday = 1,
	Tuesday = 2,
	Wednesday = 3,
	Thursday = 4,
	Friday = 5,
	Saturday = 6
}

/// <summary>
/// 日期单位
/// </summary>
public enum DateUnit
{
	Millisecond,
	Second,
	Minute,
	Hour,
	Day
}
*/
