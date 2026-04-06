using System;
using System.Linq;

namespace WellTool.Core.Util;

/// <summary>
/// 时区工具类
/// </summary>
public static class TimeZoneUtil
{
    /// <summary>
    /// 获取默认时区
    /// </summary>
    /// <returns>默认时区</returns>
    public static TimeZoneInfo GetDefault()
    {
        return TimeZoneInfo.Local;
    }

    /// <summary>
    /// 获取所有可用的时区ID
    /// </summary>
    /// <returns>时区ID数组</returns>
    public static string[] GetAvailableIDs()
    {
        return TimeZoneInfo.GetSystemTimeZones()
            .Select(tz => tz.Id)
            .ToArray();
    }

    /// <summary>
    /// 根据ID获取时区
    /// </summary>
    /// <param name="id">时区ID</param>
    /// <returns>时区</returns>
    public static TimeZoneInfo Get(string id)
    {
        return TimeZoneInfo.FindSystemTimeZoneById(id);
    }

    /// <summary>
    /// 获取时区偏移量（分钟）
    /// </summary>
    /// <param name="id">时区ID</param>
    /// <returns>偏移量（分钟）</returns>
    public static int GetOffset(string id)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(id);
        var now = DateTime.Now;
        var offset = tz.GetUtcOffset(now);
        return (int)offset.TotalMinutes;
    }

    /// <summary>
    /// 获取时区显示名称
    /// </summary>
    /// <param name="id">时区ID</param>
    /// <returns>显示名称</returns>
    public static string GetDisplayName(string id)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(id);
        return tz.DisplayName;
    }

    /// <summary>
    /// 判断指定时间是否在夏令时
    /// </summary>
    /// <param name="dateTime">时间</param>
    /// <returns>是否在夏令时</returns>
    public static bool InDaylightTime(DateTime dateTime)
    {
        return TimeZoneInfo.Local.IsDaylightSavingTime(dateTime);
    }
}
