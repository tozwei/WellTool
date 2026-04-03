using System;
using SystemTimeZone = System.TimeZoneInfo;

namespace WellDone.Core.Date;

/// <summary>
/// Zone相关封装
/// </summary>
public static class ZoneUtil
{
	/// <summary>
	/// SystemTimeZone转换为DateTimeOffset
	/// </summary>
	/// <param name="timeZone">时区，null则返回系统默认值</param>
	/// <returns>DateTimeOffset</returns>
	public static DateTimeOffset ToDateTimeOffset(SystemTimeZone timeZone)
	{
		if (timeZone == null)
		{
			timeZone = SystemTimeZone.Local;
		}
		return TimeZoneInfo.ConvertTime(DateTimeOffset.Now, timeZone);
	}

	/// <summary>
	/// DateTimeOffset转换为SystemTimeZone
	/// </summary>
	/// <param name="dateTimeOffset">日期时间偏移</param>
	/// <returns>SystemTimeZone</returns>
	public static SystemTimeZone ToTimeZone(DateTimeOffset dateTimeOffset)
	{
		return dateTimeOffset.Offset;
	}
}
