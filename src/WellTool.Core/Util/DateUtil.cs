using System;

namespace WellTool.Core.Util;

/// <summary>
/// DateUtil日期工具类
/// </summary>
public static class DateUtil
{
	/// <summary>
	/// 日期格式化
	/// </summary>
	public static string Format(DateTime date) => date.ToString("yyyy-MM-dd HH:mm:ss");

	/// <summary>
	/// 日期格式化
	/// </summary>
	public static string Format(DateTime date, string pattern) => date.ToString(pattern);

	/// <summary>
	/// 日期格式化
	/// </summary>
	public static string FormatDate(DateTime date) => date.ToString("yyyy-MM-dd");

	/// <summary>
	/// 日期格式化
	/// </summary>
	public static string FormatTime(DateTime date) => date.ToString("HH:mm:ss");

	/// <summary>
	/// 日期格式化
	/// </summary>
	public static string FormatDateTime(DateTime date) => date.ToString("yyyy-MM-dd HH:mm:ss");

	/// <summary>
	/// 获取一天的开始
	/// </summary>
	public static DateTime BeginOfDay(DateTime date) => date.Date;

	/// <summary>
	/// 获取一天的结束
	/// </summary>
	public static DateTime EndOfDay(DateTime date) => date.Date.AddDays(1).AddTicks(-1);

	/// <summary>
	/// 是否为同一天
	/// </summary>
	public static bool IsSameDay(DateTime date1, DateTime date2) => date1.Date == date2.Date;

	/// <summary>
	/// 添加年
	/// </summary>
	public static DateTime AddYears(DateTime date, int amount) => date.AddYears(amount);

	/// <summary>
	/// 添加月
	/// </summary>
	public static DateTime AddMonths(DateTime date, int amount) => date.AddMonths(amount);

	/// <summary>
	/// 添加天
	/// </summary>
	public static DateTime AddDays(DateTime date, int amount) => date.AddDays(amount);

	/// <summary>
	/// 添加小时
	/// </summary>
	public static DateTime AddHours(DateTime date, int amount) => date.AddHours(amount);

	/// <summary>
	/// 添加分钟
	/// </summary>
	public static DateTime AddMinutes(DateTime date, int amount) => date.AddMinutes(amount);

	/// <summary>
	/// 添加秒
	/// </summary>
	public static DateTime AddSeconds(DateTime date, int amount) => date.AddSeconds(amount);

	/// <summary>
	/// 是否闰年
	/// </summary>
	public static bool IsLeapYear(int year) => DateTime.IsLeapYear(year);
}
