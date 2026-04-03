namespace WellTool.Core.Convert.impl;

/// <summary>
/// 时间段转换器
/// </summary>
public class PeriodConverter : AbstractConverter<TimeSpan>
{
    protected override TimeSpan ConvertInternal(object value)
    {
        if (value is TimeSpan ts)
        {
            return ts;
        }
        if (value is int i)
        {
            return TimeSpan.FromDays(i);
        }
        if (value is long l)
        {
            return TimeSpan.FromDays(l);
        }
        return TimeSpan.Parse(ConvertToStr(value));
    }
}
