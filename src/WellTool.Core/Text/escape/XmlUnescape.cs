using WellTool.Core.Text.Replacer;

namespace WellTool.Core.Text.Escape;

/// <summary>
/// XML的UNESCAPE
/// </summary>
public class XmlUnescape : ReplacerChain
{
    protected static readonly string[][] BASIC_UNESCAPE = InternalEscapeUtil.Invert(XmlEscape.BASIC_ESCAPE);

    /// <summary>
    /// 构造
    /// </summary>
    public XmlUnescape()
    {
        AddChain(new LookupReplacer(BASIC_UNESCAPE));
        AddChain(new NumericEntityUnescaper());
    }
}
