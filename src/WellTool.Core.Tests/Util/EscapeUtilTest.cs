using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class EscapeUtilTest
{
    [Fact]
    public void EscapeHtmlTest()
    {
        var escaped = EscapeUtil.Escape("<div>Hello</div>");
        Assert.Contains("&lt;", escaped);
        Assert.Contains("&gt;", escaped);
    }

    [Fact]
    public void UnescapeHtmlTest()
    {
        var unescaped = EscapeUtil.Unescape("&lt;div&gt;Hello&lt;/div&gt;");
        Assert.Contains("<div>", unescaped);
    }

    [Fact]
    public void EscapeJsTest()
    {
        var escaped = EscapeUtil.EscapeJs("Hello 'World'");
        Assert.Contains("\\'", escaped);
    }

    [Fact]
    public void EscapeXmlTest()
    {
        var escaped = EscapeUtil.EscapeXml("<element>content</element>");
        Assert.Contains("&lt;", escaped);
        Assert.Contains("&gt;", escaped);
    }

    [Fact]
    public void UnescapeXmlTest()
    {
        var unescaped = EscapeUtil.UnescapeXml("&lt;div&gt;content&lt;/div&gt;");
        Assert.Contains("<div>", unescaped);
    }
}
