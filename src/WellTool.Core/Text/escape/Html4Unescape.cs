using System.Text;
using WellTool.Core.Text.Replacer;

namespace WellTool.Core.Text.Escape;

/// <summary>
/// HTML4的UNESCAPE
/// </summary>
public class Html4Unescape : XmlUnescape
{
    private static readonly string[][] ISO8859_1_UNESCAPE = InternalEscapeUtil.Invert(Html4Escape.ISO8859_1_ESCAPE);
    private static readonly string[][] HTML40_EXTENDED_UNESCAPE = InternalEscapeUtil.Invert(Html4Escape.HTML40_EXTENDED_ESCAPE);

    public Html4Unescape() : base()
    {
        AddChain(new LookupReplacer(ISO8859_1_UNESCAPE));
        AddChain(new LookupReplacer(HTML40_EXTENDED_UNESCAPE));
    }
}
