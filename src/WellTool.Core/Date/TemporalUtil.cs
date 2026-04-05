using System;

namespace WellTool.Core.Date;

/// <summary>
/// Temporal 工具类封装
/// </summary>
public static class TemporalUtil
{
	/// <summary>
	/// 获取两个日期的差，如果结束时间早于开始时间，获取结果为负
	/// </summary>
	/// <param name="startTimeInclude">开始时间（包含）</param>
	/// <param name="endTimeExclude">结束时间（不包含）</param>
	/// <returns>时间差</returns>
	public static TimeSpan Between(DateTime startTimeInclude, DateTime endTimeExclude)
	{
		return endTimeExclude - startTimeInclude;
	}

	/// <summary>
	/// 获取两个日期的差
	/// </summary>
	/// <param name="startTimeInclude">开始时间（包括）</param>
	/// <param name="endTimeExclude">结束时间（不包括）</param>
	/// <param name="unit">时间差单位</param>
	/// <returns>时间差</returns>
	public static long Between(DateTime startTimeInclude, DateTime endTimeExclude, DateUnit unit)
	{
		var diff = endTimeExclude - startTimeInclude;
		return unit switch
		{
			DateUnit.Millisecond => diff.Milliseconds,
			DateUnit.Second => (long)diff.TotalSeconds,
			DateUnit.Minute => (long)diff.TotalMinutes,
			DateUnit.Hour => (long)diff.TotalHours,
			DateUnit.Day => (long)diff.TotalDays,
			_ => (long)diff.TotalMilliseconds
		};
	}

	/// <summary>
	/// 日期偏移,根据field不同加不同值
	/// </summary>
	/// <param name="time">日期</param>
	/// <param name="number">偏移量，正数为向后偏移，负数为向前偏移</param>
	/// <param name="field">偏移单位</param>
	/// <returns>偏移后的日期时间</returns>
	public static DateTime? Offset(DateTime? time, long number, DateUnit field)
	{
		if (time == null)
			return null;

		return field switch
		{
			DateUnit.Millisecond => time.Value.AddMilliseconds(number),
			DateUnit.Second => time.Value.AddSeconds(number),
			DateUnit.Minute => time.Value.AddMinutes(number),
			DateUnit.Hour => time.Value.AddHours(number),
			DateUnit.Day => time.Value.AddDays(number),
			DateUnit.Month => time.Value.AddMonths((int)number),
			DateUnit.Year => time.Value.AddYears((int)number),
			_ => time.Value.AddMilliseconds(number)
		};
	}
}
