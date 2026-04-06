namespace WellTool.Core.Date.Format;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using SystemTimeZone = System.TimeZone;

/// <summary>
/// 日期格式缓存
/// </summary>
/// <typeparam name="T">格式化类型</typeparam>
public abstract class FormatCache<T> where T : class
{
	private static readonly ConcurrentDictionary<string, T> Cache = new ConcurrentDictionary<string, T>();

	/// <summary>
	/// 创建实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	protected abstract T CreateInstance(string pattern, SystemTimeZone timeZone, CultureInfo locale);

	/// <summary>
	/// 获取实例
	/// </summary>
	/// <returns>格式化实例</returns>
	public T GetInstance()
	{
		return GetInstance("yyyy-MM-dd HH:mm:ss", null, null);
	}

	/// <summary>
	/// 获取实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <returns>格式化实例</returns>
	public T GetInstance(string pattern)
	{
		return GetInstance(pattern, null, null);
	}

	/// <summary>
	/// 获取实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>格式化实例</returns>
	public T GetInstance(string pattern, SystemTimeZone timeZone)
	{
		return GetInstance(pattern, timeZone, null);
	}

	/// <summary>
	/// 获取实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetInstance(string pattern, CultureInfo locale)
	{
		return GetInstance(pattern, null, locale);
	}

	/// <summary>
	/// 获取实例
	/// </summary>
	/// <param name="pattern">日期格式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetInstance(string pattern, SystemTimeZone timeZone, CultureInfo locale)
	{
		timeZone ??= SystemTimeZone.CurrentTimeZone;
		locale ??= CultureInfo.CurrentCulture;
		var key = $"{pattern}|{timeZone.StandardName}|{locale.Name}";

		return Cache.GetOrAdd(key, _ => CreateInstance(pattern, timeZone, locale));
	}

	/// <summary>
	/// 获取日期实例
	/// </summary>
	/// <param name="style">日期样式</param>
	/// <returns>格式化实例</returns>
	public T GetDateInstance(int style)
	{
		return GetDateInstance(style, null, null);
	}

	/// <summary>
	/// 获取日期实例
	/// </summary>
	/// <param name="style">日期样式</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetDateInstance(int style, CultureInfo locale)
	{
		return GetDateInstance(style, null, locale);
	}

	/// <summary>
	/// 获取日期实例
	/// </summary>
	/// <param name="style">日期样式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>格式化实例</returns>
	public T GetDateInstance(int style, SystemTimeZone timeZone)
	{
		return GetDateInstance(style, timeZone, null);
	}

	/// <summary>
	/// 获取日期实例
	/// </summary>
	/// <param name="style">日期样式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetDateInstance(int style, SystemTimeZone timeZone, CultureInfo locale)
	{
		return GetInstance(GetDateFormatPattern(style), timeZone, locale);
	}

	/// <summary>
	/// 获取时间实例
	/// </summary>
	/// <param name="style">时间样式</param>
	/// <returns>格式化实例</returns>
	public T GetTimeInstance(int style)
	{
		return GetTimeInstance(style, null, null);
	}

	/// <summary>
	/// 获取时间实例
	/// </summary>
	/// <param name="style">时间样式</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetTimeInstance(int style, CultureInfo locale)
	{
		return GetTimeInstance(style, null, locale);
	}

	/// <summary>
	/// 获取时间实例
	/// </summary>
	/// <param name="style">时间样式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>格式化实例</returns>
	public T GetTimeInstance(int style, SystemTimeZone timeZone)
	{
		return GetTimeInstance(style, timeZone, null);
	}

	/// <summary>
	/// 获取时间实例
	/// </summary>
	/// <param name="style">时间样式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetTimeInstance(int style, SystemTimeZone timeZone, CultureInfo locale)
	{
		return GetInstance(GetTimeFormatPattern(style), timeZone, locale);
	}

	/// <summary>
	/// 获取日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <returns>格式化实例</returns>
	public T GetDateTimeInstance(int dateStyle, int timeStyle)
	{
		return GetDateTimeInstance(dateStyle, timeStyle, null, null);
	}

	/// <summary>
	/// 获取日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetDateTimeInstance(int dateStyle, int timeStyle, CultureInfo locale)
	{
		return GetDateTimeInstance(dateStyle, timeStyle, null, locale);
	}

	/// <summary>
	/// 获取日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <param name="timeZone">时区</param>
	/// <returns>格式化实例</returns>
	public T GetDateTimeInstance(int dateStyle, int timeStyle, SystemTimeZone timeZone)
	{
		return GetDateTimeInstance(dateStyle, timeStyle, timeZone, null);
	}

	/// <summary>
	/// 获取日期时间实例
	/// </summary>
	/// <param name="dateStyle">日期样式</param>
	/// <param name="timeStyle">时间样式</param>
	/// <param name="timeZone">时区</param>
	/// <param name="locale">区域</param>
	/// <returns>格式化实例</returns>
	public T GetDateTimeInstance(int dateStyle, int timeStyle, SystemTimeZone timeZone, CultureInfo locale)
	{
		return GetInstance($"{GetDateFormatPattern(dateStyle)} {GetTimeFormatPattern(timeStyle)}", timeZone, locale);
	}

	private static string GetDateFormatPattern(int style)
	{
		return style switch
		{
			0 => "yyyy-MM-dd",
			1 => "yyyy-MM-dd",
			2 => "yyyy-MM-dd",
			_ => "yyyy-MM-dd"
		};
	}

	private static string GetTimeFormatPattern(int style)
	{
		return style switch
		{
			0 => "HH:mm:ss",
			1 => "HH:mm:ss",
			2 => "HH:mm:ss",
			_ => "HH:mm:ss"
		};
	}
}
