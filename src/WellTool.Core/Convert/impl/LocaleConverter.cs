using System.Globalization;

namespace WellTool.Core.Convert.impl;

/// <summary>
/// Locale对象转换器
/// </summary>
public class LocaleConverter : AbstractConverter<CultureInfo>
{
    protected override CultureInfo ConvertInternal(object value)
    {
        try
        {
            var str = ConvertToStr(value);
            if (string.IsNullOrEmpty(str))
            {
                return CultureInfo.InvariantCulture;
            }

            var items = str.Split('_');
            if (items.Length == 1)
            {
                return new CultureInfo(items[0]);
            }
            if (items.Length == 2)
            {
                return new CultureInfo($"{items[0]}-{items[1]}");
            }
            if (items.Length == 3)
            {
                return new CultureInfo($"{items[0]}-{items[1]}-{items[2]}");
            }
            return CultureInfo.InvariantCulture;
        }
        catch
        {
            return CultureInfo.InvariantCulture;
        }
    }
}
