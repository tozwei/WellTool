using System.Text;

namespace WellTool.Core.Convert.Impl;

/// <summary>
/// 缂栫爜瀵硅薄杞崲鍣?
/// </summary>
public class CharsetConverter : AbstractConverter<Encoding>
{
    protected override Encoding ConvertInternal(object value)
    {
        var charsetName = ConvertToStr(value);
        return Encoding.GetEncoding(charsetName);
    }
}

