using System.Globalization;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// 鏃ユ湡杞崲鍣?
/// </summary>
public class CalendarConverter : AbstractConverter<DateTime>
{
    /// <summary>
    /// 鏃ユ湡鏍煎紡
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

