using System;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// TimeZone杞崲鍣?
/// </summary>
public class TimeZoneConverter : AbstractConverter<TimeZoneInfo>
{
    protected override TimeZoneInfo ConvertInternal(object value)
    {
        var str = ConvertToStr(value);
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById(str);
        }
        catch
        {
            return TimeZoneInfo.Local;
        }
    }
}

