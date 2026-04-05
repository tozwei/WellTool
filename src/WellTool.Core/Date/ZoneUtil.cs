using System;
using SystemTimeZone = System.TimeZoneInfo;

namespace WellTool.Core.Date;

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
	/// 获取本地时区
	/// </summary>
	/// <returns>SystemTimeZone</returns>
	public static SystemTimeZone GetLocalTimeZone()
	{
		return SystemTimeZone.Local;
	}
}
