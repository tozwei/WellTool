using System.Globalization;

namespace WellTool.Core.Convert.impl;

/// <summary>
/// 货币转换器
/// </summary>
public class CurrencyConverter : AbstractConverter<decimal>
{
    protected override decimal ConvertInternal(object value)
    {
        var valueStr = ConvertToStr(value);
        return decimal.Parse(valueStr, NumberStyles.Currency, CultureInfo.InvariantCulture);
    }
}
