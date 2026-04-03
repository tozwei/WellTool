using System;
using System.Globalization;

namespace WellDone.Core.Date;

/// <summary>
/// TemporalAccessor 工具类封装
/// </summary>
public static class TemporalAccessorUtil
{
	/// <summary>
	/// 安全获取时间的某个属性，属性不存在返回最小值，一般为0
	/// </summary>
	/// <param name="dateTime">需要获取的时间对象</param>
	/// <returns>时间的值，如果无法获取则获取最小值，一般为0</returns>
	public static int Get(DateTime dateTime, CalendarField field)
	{
		return dateTime.Get(field);
	}

	/// <summary>
	/// 格式化日期时间为指定格式
	/// </summary>
	/// <param name="time">DateTime</param>
	/// <param name="format">日期格式</param>
	/// <returns>格式化后的字符串</returns>
	public static string Format(DateTime time, string format)
	{
		if (time == null)
			return null;
		return time.ToString(format);
	}

	/// <summary>
	/// 当前日期是否在日期指定范围内
	/// </summary>
	/// <param name="date">被检查的日期</param>
	/// <param name="beginDate">起始日期（包含）</param>
	/// <param name="endDate">结束日期（包含）</param>
	/// <param name="includeBegin">时间范围是否包含起始日期</param>
	/// <param name="includeEnd">时间范围是否包含结束日期</param>
	/// <returns>是否在范围内</returns>
	public static bool IsIn(DateTime date, DateTime beginDate, DateTime endDate, bool includeBegin = true, bool includeEnd = true)
	{
		var thisTicks = date.Ticks;
		var beginTicks = beginDate.Ticks;
		var endTicks = endDate.Ticks;
		var rangeMin = Math.Min(beginTicks, endTicks);
		var rangeMax = Math.Max(beginTicks, endTicks);

		// 先判断是否满足 date ∈ (beginDate, endDate)
		bool isIn = rangeMin < thisTicks && thisTicks < rangeMax;

		// 若不满足，则再判断是否在时间范围的边界上
		if (!isIn && includeBegin)
		{
			isIn = thisTicks == rangeMin;
		}

		if (!isIn && includeEnd)
		{
			isIn = thisTicks == rangeMax;
		}

		return isIn;
	}
}

/// <summary>
/// 日历字段
/// </summary>
public enum CalendarField
{
	/// <summary>年</summary>
	Year = 1,
	/// <summary>月</summary>
	Month = 2,
	/// <summary>日</summary>
	Day = 5,
	/// <summary>小时</summary>
	Hour = 10,
	/// <summary>分钟</summary>
	Minute = 12,
	/// <summary>秒</summary>
	Second = 13
}
