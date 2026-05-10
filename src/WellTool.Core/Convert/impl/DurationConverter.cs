using System.Globalization;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// 鏃堕棿娈佃浆鎹㈠櫒
/// </summary>
public class DurationConverter : AbstractConverter<TimeSpan>
{
    protected override TimeSpan ConvertInternal(object value)
    {
        if (value is TimeSpan ts)
        {
            return ts;
        }
        if (value is long l)
        {
            return TimeSpan.FromMilliseconds(l);
        }
        return TimeSpan.Parse(ConvertToStr(value), CultureInfo.InvariantCulture);
    }
}

