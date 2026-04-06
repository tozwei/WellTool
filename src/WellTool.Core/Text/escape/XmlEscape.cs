using WellTool.Core.Text.Replacer;

namespace WellTool.Core.Text.Escape;

/// <summary>
/// XML特殊字符转义
/// </summary>
public class XmlEscape : ReplacerChain
{
    public static readonly string[][] BASIC_ESCAPE = {
        new[] { "'", "&apos;" },
        new[] { "\"", "&quot;" },
        new[] { "&", "&amp;" },
        new[] { "<", "&lt;" },
        new[] { ">", "&gt;" },
    };

    /// <summary>
    /// 构造
    /// </summary>
    public XmlEscape()
    {
        AddChain(new LookupReplacer(BASIC_ESCAPE));
    }
}
