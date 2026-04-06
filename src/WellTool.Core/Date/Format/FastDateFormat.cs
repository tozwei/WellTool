namespace WellTool.Core.Date.Format;

using System;
using System.Globalization;
using System.Text;
using SystemTimeZone = System.TimeZone;
using Calendar = WellTool.Core.Date.Calendar;

/// <summary>
/// FastDateFormat 是一个线程安全的日期格式化实现
/// </summary>
public class FastDateFormat : AbstractDateBasic, DateParser, DatePrinter
{
	private const long SerialVersionUid = 8097890768636183236L;

	/// <summary>
	/// FULL locale dependent date or time style
	/// </summary>
	public const int FULL = 0;

	/// <summary>
	/// LONG locale dependent date or time style
	/// </summary>
	public const int LONG = 1;

	/// <summary>
	/// MEDIUM locale dependent date or time style
	/// </summary>
	public const int MEDIUM = 2;

	/// <summary>
	/// SHORT locale dependent date or time style
	/// </summary>
	public const int SHORT = 3;

	private static readonly FormatCache<FastDateFormat> Cache = new FormatCacheAnonymousInnerClass();

	private class FormatCacheAnonymousInnerClass : FormatCache<FastDateFormat>
	{
		protected override FastDateFormat CreateInstance(string pattern, SystemTimeZone timeZone, CultureInfo locale)
		{
			return new FastDateFormat(pattern, timeZone, locale);
		}
	}

	private readonly FastDatePrinter Printer;
	private readonly FastDateParser Parser;

	/// <summary>
	/// 获得 FastDateFormat实例，使用默认格式和地区
	/// </summary>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetInstance()
	{
		return Cache.GetInstance();
	}

	/// <summary>
	/// 获得 FastDateFormat 实例，使用默认地区
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetInstance(string pattern)
	{
		return Cache.GetInstance(pattern, null, null);
	}

	/// <summary>
	/// 获得 FastDateFormat 实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetInstance(string pattern, SystemTimeZone timeZone)
	{
		return Cache.GetInstance(pattern, timeZone, null);
	}

	/// <summary>
	/// 获得 FastDateFormat 实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetInstance(string pattern, CultureInfo locale)
	{
		return Cache.GetInstance(pattern, null, locale);
	}

	/// <summary>
	/// 获得 FastDateFormat 实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetInstance(string pattern, SystemTimeZone timeZone, CultureInfo locale)
	{
		return Cache.GetInstance(pattern, timeZone, locale);
	}

	/// <summary>
	/// 获得日期实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateInstance(int style)
	{
		return Cache.GetDateInstance(style, null, null);
	}

	/// <summary>
	/// 获得日期实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateInstance(int style, CultureInfo locale)
	{
		return Cache.GetDateInstance(style, null, locale);
	}

	/// <summary>
	/// 获得日期实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateInstance(int style, SystemTimeZone timeZone)
	{
		return Cache.GetDateInstance(style, timeZone, null);
	}

	/// <summary>
	/// 获得日期实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateInstance(int style, SystemTimeZone timeZone, CultureInfo locale)
	{
		return Cache.GetDateInstance(style, timeZone, locale);
	}

	/// <summary>
	/// 获得时间实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetTimeInstance(int style)
	{
		return Cache.GetTimeInstance(style, null, null);
	}

	/// <summary>
	/// 获得时间实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetTimeInstance(int style, CultureInfo locale)
	{
		return Cache.GetTimeInstance(style, null, locale);
	}

	/// <summary>
	/// 获得时间实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetTimeInstance(int style, SystemTimeZone timeZone)
	{
		return Cache.GetTimeInstance(style, timeZone, null);
	}

	/// <summary>
	/// 获得时间实例
	/// </summary>
	/// <param name="style">样式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetTimeInstance(int style, SystemTimeZone timeZone, CultureInfo locale)
	{
		return Cache.GetTimeInstance(style, timeZone, locale);
	}

	/// <summary>
	/// 获得日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateTimeInstance(int dateStyle, int timeStyle)
	{
		return Cache.GetDateTimeInstance(dateStyle, timeStyle, null, null);
	}

	/// <summary>
	/// 获得日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateTimeInstance(int dateStyle, int timeStyle, CultureInfo locale)
	{
		return Cache.GetDateTimeInstance(dateStyle, timeStyle, null, locale);
	}

	/// <summary>
	/// 获得日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateTimeInstance(int dateStyle, int timeStyle, SystemTimeZone timeZone)
	{
		return GetDateTimeInstance(dateStyle, timeStyle, timeZone, null);
	}

	/// <summary>
	/// 获得日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>FastDateFormat</returns>
	public static FastDateFormat GetDateTimeInstance(int dateStyle, int timeStyle, SystemTimeZone timeZone, CultureInfo locale)
	{
		return Cache.GetDateTimeInstance(dateStyle, timeStyle, timeZone, locale);
	}

	/// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pattern">日期格式</param>
    /// <param name="timeZone">时区</param>
    /// <param name="locale">区域</param>
    protected FastDateFormat(string pattern, SystemTimeZone timeZone, CultureInfo locale) : base(pattern, timeZone, locale)
    {
        Printer = new FastDatePrinter(pattern, timeZone, locale);
        Parser = new FastDateParser(pattern, timeZone, locale);
    }

	/// <summary>
    /// 格式化
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="toAppendTo">缓冲区</param>
    /// <param name="pos">位置</param>
    /// <returns>格式化后的字符串</returns>
    public StringBuilder Format(object obj, StringBuilder toAppendTo, ref int pos)
    {
        if (obj is long millis)
        {
            toAppendTo.Append(Printer.Format(millis));
        }
        else if (obj is DateTime date)
        {
            toAppendTo.Append(Printer.Format(date));
        }
        else if (obj is Calendar calendar)
        {
            toAppendTo.Append(Printer.Format(calendar));
        }
        return toAppendTo;
    }

	/// <summary>
	/// 格式化毫秒
	/// </summary>
	/// <param name="millis">毫秒数</param>
	/// <returns>格式化后的字符串</returns>
	public string Format(long millis)
	{
		return Printer.Format(millis);
	}

	/// <summary>
	/// 格式化日期
	/// </summary>
	/// <param name="date">{DateTime}</param>
	/// <returns>格式化后的字符串</returns>
	public string Format(DateTime date)
	{
		return Printer.Format(date);
	}

	/// <summary>
	/// 格式化日历
	/// </summary>
	/// <param name="calendar">{Calendar}</param>
	/// <returns>格式化后的字符串</returns>
	public string Format(Calendar calendar)
	{
		return Printer.Format(calendar);
	}

	/// <summary>
	/// 格式化毫秒到缓冲区
	/// </summary>
	/// <param name="millis">毫秒数</param>
	/// <param name="buf">缓冲区</param>
	/// <returns>缓冲区</returns>
	public StringBuilder Format(long millis, StringBuilder buf)
	{
		return Printer.Format(millis, buf);
	}

	/// <summary>
	/// 格式化日期到缓冲区
	/// </summary>
	/// <param name="date">{DateTime}</param>
	/// <param name="buf">缓冲区</param>
	/// <returns>缓冲区</returns>
	public StringBuilder Format(DateTime date, StringBuilder buf)
	{
		return Printer.Format(date, buf);
	}

	/// <summary>
	/// 格式化日历到缓冲区
	/// </summary>
	/// <param name="calendar">{Calendar}</param>
	/// <param name="buf">缓冲区</param>
	/// <returns>缓冲区</returns>
	public StringBuilder Format(Calendar calendar, StringBuilder buf)
	{
		return Printer.Format(calendar, buf);
	}

	/// <summary>
	/// 解析日期
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <returns>{DateTime}</returns>
	public DateTime Parse(string source)
	{
		return Parser.Parse(source);
	}

	/// <summary>
	/// 解析日期
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <returns>{DateTime}</returns>
	public DateTime Parse(string source, ref int pos)
	{
		return Parser.Parse(source, ref pos);
	}

	/// <summary>
	/// 解析日期
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <param name="calendar">日历</param>
	/// <returns>是否成功</returns>
	public bool Parse(string source, ref int pos, Calendar calendar)
	{
		return Parser.Parse(source, ref pos, calendar);
	}

	/// <summary>
	/// 解析对象
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <returns>{DateTime}</returns>
	public object ParseObject(string source)
	{
		return Parser.Parse(source);
	}

	/// <summary>
	/// 解析对象
	/// </summary>
	/// <param name="source">日期字符串</param>
	/// <param name="pos">解析位置</param>
	/// <returns>{DateTime}</returns>
	public object ParseObject(string source, ref int pos)
	{
		return Parser.ParseObject(source, ref pos);
	}

	/// <summary>
	/// 获得格式
	/// </summary>
	public string GetPattern() => Printer.GetPattern();

	/// <summary>
	/// 获得时区
	/// </summary>
	public SystemTimeZone GetTimeZone() => Printer.GetTimeZone();

	/// <summary>
	/// 获得区域
	/// </summary>
	public CultureInfo GetLocale() => Printer.GetLocale();

	/// <summary>
	/// 获取最大估计长度
	/// </summary>
	/// <returns>最大长度</returns>
	public int GetMaxLengthEstimate() => Printer.GetMaxLengthEstimate();

	/// <summary>
	/// 是否相等
	/// </summary>
	public override bool Equals(object obj)
	{
		if (obj is not FastDateFormat other)
			return false;
		return Printer.Equals(other.Printer);
	}

	/// <summary>
	/// 获取哈希码
	/// </summary>
	public override int GetHashCode() => Printer.GetHashCode();

	/// <summary>
	/// 返回字符串
	/// </summary>
	public override string ToString() => $"FastDateFormat[{Printer.GetPattern()},{Printer.GetLocale()},{Printer.GetTimeZone().StandardName}]";
}
