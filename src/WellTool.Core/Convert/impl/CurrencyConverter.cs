using System.Globalization;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// 璐у竵杞崲鍣?
/// </summary>
public class CurrencyConverter : AbstractConverter<decimal>
{
    protected override decimal ConvertInternal(object value)
    {
        var valueStr = ConvertToStr(value);
        return decimal.Parse(valueStr, NumberStyles.Currency, CultureInfo.InvariantCulture);
    }
}

