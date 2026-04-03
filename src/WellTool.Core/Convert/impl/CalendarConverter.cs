using System.Globalization;

namespace WellTool.Core.Convert.impl;

/// <summary>
/// 日期转换器
/// </summary>
public class CalendarConverter : AbstractConverter<DateTime>
{
    /// <summary>
    /// 日期格式
    /// </summary>
    public string? Format { get; set; }

    protected override DateTime ConvertInternal(object value)
    {
        // Handle DateTime
        if (value is DateTime dt)
        {
            return dt;
        }

        // Handle long (timestamp)
        if (value is long l)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(l).DateTime;
        }

        // Handle string
        var valueStr = ConvertToStr(value);
        if (string.IsNullOrWhiteSpace(Format))
        {
            return DateTime.Parse(valueStr);
        }
        return DateTime.ParseExact(valueStr, Format, CultureInfo.InvariantCulture);
    }
}
