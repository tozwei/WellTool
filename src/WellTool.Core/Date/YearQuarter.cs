namespace WellTool.Core.date;

using System;

/// <summary>
/// 季度枚举
/// </summary>
public enum YearQuarter
{
	Q1 = 1,
	Q2 = 2,
	Q3 = 3,
	Q4 = 4
}

/// <summary>
/// 季度工具类
/// </summary>
public static class YearQuarterUtil
{
	/// <summary>
	/// 从日期获取季度
	/// </summary>
	/// <param name="date">日期</param>
	/// <returns>季度</returns>
	public static YearQuarter FromDate(DateTime date)
	{
		return (YearQuarter)((date.Month - 1) / 3 + 1);
	}

	/// <summary>
	/// 获取季度的开始月份
	/// </summary>
	/// <param name="quarter">季度</param>
	/// <returns>开始月份 (1-12)</returns>
	public static int GetStartMonth(YearQuarter quarter)
	{
		return ((int)quarter - 1) * 3 + 1;
	}

	/// <summary>
	/// 获取季度的结束月份
	/// </summary>
	/// <param name="quarter">季度</param>
	/// <returns>结束月份 (1-12)</returns>
	public static int GetEndMonth(YearQuarter quarter)
	{
		return ((int)quarter) * 3;
	}
}
