using WellTool.Http;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// HtmlUtil 测试类
/// </summary>
public class HtmlUtilTests
{
    [Fact]
    public void RemoveHtmlTagTest()
    {
        // 非闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\">";
        var result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);

        // 闭合标签
        str = "pre<img>";
        result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);

        // 自闭合标签
        str = "pre<img src=\"xxx/dfdsfds/test.jpg\" />";
        result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);

        // 自闭合标签无属性
        str = "pre<img />";
        result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);

        // 包含内容的标签
        str = "pre<div class=\"test_div\">dfdsfdsfdsf</div>";
        result = HtmlUtil.RemoveHtmlTag(str, "div");
        Assert.Equal("pre", result);

        // 带换行符
        str = "pre<div class=\"test_div\">\r\n\t\tdfdsfdsfdsf\r\n</div>";
        result = HtmlUtil.RemoveHtmlTag(str, "div");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void CleanHtmlTagTest()
    {
        // 非闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\">";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);

        // 闭合标签
        str = "pre<img>";
        result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);

        // 自闭合标签
        str = "pre<img src=\"xxx/dfdsfds/test.jpg\" />";
        result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);

        // 自闭合标签无属性
        str = "pre<img />";
        result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);

        // 包含内容的标签
        str = "pre<div class=\"test_div\">dfdsfdsfdsf</div>";
        result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("predfdsfdsfdsf", result);

        // 带换行符
        str = "pre<div class=\"test_div\">\r\n\t\tdfdsfdsfdsf\r\n</div><div class=\"test_div\">BBBB</div>";
        result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre\r\n\t\tdfdsfdsfdsf\r\nBBBB", result);
    }

    [Fact]
    public void EscapeTest()
    {
        var html = "<div>Hello & World</div>";
        var escaped = HtmlUtil.Escape(html);

        Assert.Contains("&lt;", escaped);
        Assert.Contains("&gt;", escaped);
        Assert.Contains("&amp;", escaped);
    }

    [Fact]
    public void UnescapeTest()
    {
        var escaped = "&lt;div&gt;Hello &amp; World&lt;/div&gt;";
        var unescaped = HtmlUtil.Unescape(escaped);

        Assert.Equal("<div>Hello & World</div>", unescaped);
    }

    [Fact]
    public void EncodeTest()
    {
        var text = "Hello <World> & Friends";
        var encoded = HtmlUtil.Encode(text);

        Assert.Contains("&lt;", encoded);
        Assert.Contains("&gt;", encoded);
        Assert.Contains("&amp;", encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var encoded = "Hello &lt;World&gt; &amp; Friends";
        var decoded = HtmlUtil.Decode(encoded);

        Assert.Equal("Hello <World> & Friends", decoded);
    }
}
