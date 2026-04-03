using System.Text;

namespace WellTool.Core.Convert.impl;

/// <summary>
/// 编码对象转换器
/// </summary>
public class CharsetConverter : AbstractConverter<Encoding>
{
    protected override Encoding ConvertInternal(object value)
    {
        var charsetName = ConvertToStr(value);
        return Encoding.GetEncoding(charsetName);
    }
}
