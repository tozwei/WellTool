namespace WellTool.Core.Date.Format;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

/// <summary>
/// 全局自定义格式
/// 用于定义用户指定的日期格式和输出日期的关系
/// </summary>
public class GlobalCustomFormat
{
	/// <summary>
	/// 格式：秒时间戳（Unix时间戳）
	/// </summary>
	public static readonly string FORMAT_SECONDS = "#sss";

	/// <summary>
	/// 格式：毫秒时间戳
	/// </summary>
	public static readonly string FORMAT_MILLISECONDS = "#SSS";

	private static readonly ConcurrentDictionary<string, Func<DateTime, string>> FormatterMap;
	private static readonly ConcurrentDictionary<string, Func<string, DateTime>> ParserMap;

	static GlobalCustomFormat()
	{
		FormatterMap = new ConcurrentDictionary<string, Func<DateTime, string>>();
		ParserMap = new ConcurrentDictionary<string, Func<string, DateTime>>();

		// 预设的几种自定义格式
		PutFormatter(FORMAT_SECONDS, date => (date.Ticks / 1000).ToString());
		PutParser(FORMAT_SECONDS, dateStr => new DateTime(long.Parse(dateStr) * 1000));

		PutFormatter(FORMAT_MILLISECONDS, date => date.Ticks.ToString());
		PutParser(FORMAT_MILLISECONDS, dateStr => new DateTime(long.Parse(dateStr)));
	}

	/// <summary>
	/// 加入日期格式化规则
	/// </summary>
	/// <param name="format">格式</param>
	/// <param name="func">格式化函数</param>
	public static void PutFormatter(string format, Func<DateTime, string> func)
	{
		if (format == null)
			throw new ArgumentNullException(nameof(format), "Format must be not null!");
		if (func == null)
			throw new ArgumentNullException(nameof(func), "Function must be not null!");
		FormatterMap.TryAdd(format, func);
	}

	/// <summary>
	/// 加入日期解析规则
	/// </summary>
	/// <param name="format">格式</param>
	/// <param name="func">解析函数</param>
	public static void PutParser(string format, Func<string, DateTime> func)
	{
		if (format == null)
			throw new ArgumentNullException(nameof(format), "Format must be not null!");
		if (func == null)
			throw new ArgumentNullException(nameof(func), "Function must be not null!");
		ParserMap.TryAdd(format, func);
	}

	/// <summary>
	/// 检查指定格式是否为自定义格式
	/// </summary>
	/// <param name="format">格式</param>
	/// <returns>是否为自定义格式</returns>
	public static bool IsCustomFormat(string format)
	{
		return FormatterMap.ContainsKey(format);
	}

	/// <summary>
	/// 使用自定义格式格式化日期
	/// </summary>
	/// <param name="date">日期</param>
	/// <param name="format">自定义格式</param>
	/// <returns>格式化后的日期</returns>
	public static string Format(DateTime date, string format)
	{
		if (FormatterMap.TryGetValue(format, out var func))
		{
			return func(date);
		}
		return null;
	}

	/// <summary>
	/// 使用自定义格式解析日期
	/// </summary>
	/// <param name="dateStr">日期字符串</param>
	/// <param name="format">自定义格式</param>
	/// <returns>解析后的日期</returns>
	public static DateTime? Parse(string dateStr, string format)
	{
		if (ParserMap.TryGetValue(format, out var func))
		{
			return func(dateStr);
		}
		return null;
	}
}
