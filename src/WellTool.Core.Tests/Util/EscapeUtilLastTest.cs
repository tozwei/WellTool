using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class EscapeUtilLastTest
{
    [Fact]
    public void EscapeHtmlTest()
    {
        var escaped = EscapeUtil.Escape("<div>Hello</div>");
        Assert.Contains("&lt;", escaped);
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
}
