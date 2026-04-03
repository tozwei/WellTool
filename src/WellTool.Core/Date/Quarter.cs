using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using SystemTimeZone = System.TimeZoneInfo;

namespace WellDone.Core.Date;

/// <summary>
/// 季度
/// </summary>
public enum Quarter
{
	/// <summary>第一季度（1月-3月）</summary>
	Q1 = 1,
	/// <summary>第二季度（4月-6月）</summary>
	Q2 = 2,
	/// <summary>第三季度（7月-9月）</summary>
	Q3 = 3,
	/// <summary>第四季度（10月-12月）</summary>
	Q4 = 4
}

/// <summary>
/// 季度的扩展工具类
/// </summary>
public static class QuarterExtensions
{
	/// <summary>
	/// 获取季度值，从1开始
	/// </summary>
	public static int GetValue(this Quarter quarter) => (int)quarter;

	/// <summary>
	/// 获取季度值，从1开始
	/// </summary>
	/// <param name="quarter">季度枚举</param>
	/// <returns>季度值</returns>
	public static int GetValue(this Month month) => (int)month + 1;

	/// <summary>
	/// 将月份转换为季度
	/// </summary>
	/// <param name="month">月份（1-12）</param>
	/// <returns>季度</returns>
	public static Quarter FromMonth(int month)
	{
		if (month < 1 || month > 12)
			throw new ArgumentOutOfRangeException(nameof(month));
		return (Quarter)((month - 1) / 3 + 1);
	}

	/// <summary>
	/// 获取季度的第一个月
	/// </summary>
	public static int FirstMonthValue(this Quarter quarter) => ((int)quarter - 1) * 3 + 1;

	/// <summary>
	/// 获取季度的最后一个月
	/// </summary>
	public static int LastMonthValue(this Quarter quarter) => ((int)quarter - 1) * 3 + 3;

	/// <summary>
	/// 验证季度值是否有效
	/// </summary>
	public static int CheckValidIntValue(int value)
	{
		if (value < 1 || value > 4)
			throw new ArgumentOutOfRangeException(nameof(value), "Quarter must be between 1 and 4");
		return value;
	}
}
