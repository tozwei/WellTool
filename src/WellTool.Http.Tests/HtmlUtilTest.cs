using System.Text;
using WellTool.Http;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// HtmlUtil HTML工具类测试
/// </summary>
public class HtmlUtilTest
{
    #region RemoveHtmlTag tests

    [Fact]
    public void RemoveHtmlTagTest_NonClosingTag()
    {
        // 非闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\">";
        var result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void RemoveHtmlTagTest_ClosingTag()
    {
        // 闭合标签
        var str = "pre<img>";
        var result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void RemoveHtmlTagTest_SelfClosingTag()
    {
        // 自闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\" />";
        var result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void RemoveHtmlTagTest_SelfClosingTag2()
    {
        // 自闭合标签无空格
        var str = "pre<img />";
        var result = HtmlUtil.RemoveHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void RemoveHtmlTagTest_WithContent()
    {
        // 包含内容标签
        var str = "pre<div class=\"test_div\">dfdsfdsfdsf</div>";
        var result = HtmlUtil.RemoveHtmlTag(str, "div");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void RemoveHtmlTagTest_WithNewline()
    {
        // 带换行
        var str = "pre<div class=\"test_div\">\r\n\t\tdfdsfdsfdsf\r\n</div>";
        var result = HtmlUtil.RemoveHtmlTag(str, "div");
        Assert.Equal("pre", result);
    }

    #endregion

    #region CleanHtmlTag tests

    [Fact]
    public void CleanHtmlTagTest_NonClosingTag()
    {
        // 非闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\">";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);
    }

    [Fact]
    public void CleanHtmlTagTest_ClosingTag()
    {
        // 闭合标签
        var str = "pre<img>";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);
    }

    [Fact]
    public void CleanHtmlTagTest_SelfClosingTag()
    {
        // 自闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\" />";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);
    }

    [Fact]
    public void CleanHtmlTagTest_SelfClosingTag2()
    {
        // 自闭合标签无空格
        var str = "pre<img />";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre", result);
    }

    [Fact]
    public void CleanHtmlTagTest_WithContent()
    {
        // 包含内容标签
        var str = "pre<div class=\"test_div\">dfdsfdsfdsf</div>";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("predfdsfdsfdsf", result);
    }

    [Fact]
    public void CleanHtmlTagTest_WithNewline()
    {
        // 带换行
        var str = "pre<div class=\"test_div\">\r\n\t\tdfdsfdsfdsf\r\n</div><div class=\"test_div\">BBBB</div>";
        var result = HtmlUtil.CleanHtmlTag(str);
        Assert.Equal("pre\r\n\t\tdfdsfdsfdsf\r\nBBBB", result);
    }

    #endregion

    #region CleanEmptyTag tests

    [Fact]
    public void CleanEmptyTagTest_AllEmpty()
    {
        var str = "<p></p><div></div>";
        var result = HtmlUtil.CleanEmptyTag(str);
        Assert.Equal("", result);
    }

    [Fact]
    public void CleanEmptyTagTest_Mixed1()
    {
        var str = "<p>TEXT</p><div></div>";
        var result = HtmlUtil.CleanEmptyTag(str);
        Assert.Equal("<p>TEXT</p>", result);
    }

    [Fact]
    public void CleanEmptyTagTest_Mixed2()
    {
        var str = "<p></p><div>TEXT</div>";
        var result = HtmlUtil.CleanEmptyTag(str);
        Assert.Equal("<div>TEXT</div>", result);
    }

    [Fact]
    public void CleanEmptyTagTest_Mixed3()
    {
        var str = "<p>TEXT</p><div>TEXT</div>";
        var result = HtmlUtil.CleanEmptyTag(str);
        Assert.Equal("<p>TEXT</p><div>TEXT</div>", result);
    }

    [Fact]
    public void CleanEmptyTagTest_Edges()
    {
        var str = "TEXT<p></p><div></div>TEXT";
        var result = HtmlUtil.CleanEmptyTag(str);
        Assert.Equal("TEXTTEXT", result);
    }

    #endregion

    #region UnwrapHtmlTag tests

    [Fact]
    public void UnwrapHtmlTagTest_NonClosingTag()
    {
        // 非闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\">";
        var result = HtmlUtil.UnwrapHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void UnwrapHtmlTagTest_ClosingTag()
    {
        // 闭合标签
        var str = "pre<img>";
        var result = HtmlUtil.UnwrapHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void UnwrapHtmlTagTest_SelfClosingTag()
    {
        // 自闭合标签
        var str = "pre<img src=\"xxx/dfdsfds/test.jpg\" />";
        var result = HtmlUtil.UnwrapHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void UnwrapHtmlTagTest_SelfClosingTag2()
    {
        // 自闭合标签无空格
        var str = "pre<img />";
        var result = HtmlUtil.UnwrapHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void UnwrapHtmlTagTest_SelfClosingTag3()
    {
        // 自闭合标签无空格无斜杠
        var str = "pre<img/>";
        var result = HtmlUtil.UnwrapHtmlTag(str, "img");
        Assert.Equal("pre", result);
    }

    [Fact]
    public void UnwrapHtmlTagTest_WithContent()
    {
        // 包含内容标签
        var str = "pre<div class=\"test_div\">abc</div>";
        var result = HtmlUtil.UnwrapHtmlTag(str, "div");
        Assert.Equal("preabc", result);
    }

    [Fact]
    public void UnwrapHtmlTagTest_WithNewline()
    {
        // 带换行
        var str = "pre<div class=\"test_div\">\r\n\t\tabc\r\n</div>";
        var result = HtmlUtil.UnwrapHtmlTag(str, "div");
        Assert.Equal("pre\r\n\t\tabc\r\n", result);
    }

    [Fact]
    public void UnwrapTest_AvoidFalsePositive()
    {
        // 避免移除i却误删img标签的情况
        var htmlString = "<html><img src='aaa'><i>测试文本</i></html>";
        var tagString = "i,br";
        var cleanTxt = HtmlUtil.RemoveHtmlTag(htmlString, false, tagString.Split(','));
        Assert.Equal("<html><img src='aaa'>测试文本</html>", cleanTxt);
    }

    #endregion

    #region Escape/Unescape tests

    [Fact]
    public void EscapeTest()
    {
        var html = "<html><body>123'123'</body></html>";
        var escape = HtmlUtil.Escape(html);
        Assert.Equal("&lt;html&gt;&lt;body&gt;123&#039;123&#039;&lt;/body&gt;&lt;/html&gt;", escape);
    }

    [Fact]
    public void UnescapeTest()
    {
        var html = "<html><body>123'123'</body></html>";
        var escape = HtmlUtil.Escape(html);
        var restoreEscaped = HtmlUtil.Unescape(escape);
        Assert.Equal(html, restoreEscaped);
    }

    [Fact]
    public void UnescapeTest_SingleQuote()
    {
        Assert.Equal("'", HtmlUtil.Unescape("&apos;"));
    }

    [Fact]
    public void EscapeTest_Nbsp()
    {
        var c = (char)160; // 不断开空格
        var html = $"<html><body> {c}</body></html>";
        var escape = HtmlUtil.Escape(html);
        Assert.Equal("&lt;html&gt;&lt;body&gt; &nbsp;&lt;/body&gt;&lt;/html&gt;", escape);
    }

    [Fact]
    public void UnescapeTest_Nbsp()
    {
        var result = HtmlUtil.Unescape("&nbsp;");
        Assert.Equal(1, result.Length);
        Assert.Equal((char)160, result[0]);
    }

    #endregion

    #region Filter tests

    [Fact]
    public void FilterTest()
    {
        var html = "<alert></alert>";
        var filter = HtmlUtil.Filter(html);
        Assert.Equal("", filter);
    }

    [Fact]
    public void FilterTest_AllowedTags()
    {
        var html = "<b>Hello</b><script>alert('xss')</script><i>World</i>";
        var filter = HtmlUtil.Filter(html);
        Assert.Contains("<b>Hello</b>", filter);
        Assert.Contains("<i>World</i>", filter);
        Assert.DoesNotContain("<script>", filter);
    }

    #endregion

    #region RemoveHtmlAttr tests

    [Fact]
    public void RemoveHtmlAttrTest_DoubleQuotes()
    {
        // 去除的属性加双引号测试
        var html = "<div class=\"test_div\"></div><span class=\"test_div\"></span>";
        var result = HtmlUtil.RemoveHtmlAttr(html, "class");
        Assert.Equal("<div></div><span></span>", result);
    }

    [Fact]
    public void RemoveHtmlAttrTest_MixedQuotes()
    {
        // 去除的属性后跟空格、加单引号、不加引号测试
        var html = "<div class=test_div></div><span Class='test_div' ></span>";
        var result = HtmlUtil.RemoveHtmlAttr(html, "class");
        Assert.Equal("<div></div><span></span>", result);
    }

    [Fact]
    public void RemoveHtmlAttrTest_MultipleAttrs()
    {
        // 去除的属性位于标签末尾、其它属性前测试
        var html = "<div style=\"margin:100%\" class=test_div></div><span Class='test_div' width=100></span>";
        var result = HtmlUtil.RemoveHtmlAttr(html, "class");
        Assert.Equal("<div style=\"margin:100%\"></div><span width=100></span>", result);
    }

    [Fact]
    public void RemoveHtmlAttrTest_SpacesBetween()
    {
        // 去除的属性名和值之间存在空格
        var html = "<div style = \"margin:100%\" class = test_div></div><span Class = 'test_div' width=100></span>";
        var result = HtmlUtil.RemoveHtmlAttr(html, "class");
        Assert.Equal("<div style = \"margin:100%\"></div><span width=100></span>", result);
    }

    #endregion

    #region RemoveAllHtmlAttr tests

    [Fact]
    public void RemoveAllHtmlAttrTest()
    {
        var html = "<div class=\"test_div\" width=\"120\"></div>";
        var result = HtmlUtil.RemoveAllHtmlAttr(html, "div");
        Assert.Equal("<div></div>", result);
    }

    #endregion

    #region Issue tests

    [Fact]
    public void IssueI6YNTFTest()
    {
        var html = "<html><body><div class=\"a1 a2\">hello world</div></body></html>";
        var cleanText = HtmlUtil.RemoveHtmlAttr(html, "class");
        Assert.Equal("<html><body><div>hello world</div></body></html>", cleanText);

        html = "<html><body><div class=a1>hello world</div></body></html>";
        cleanText = HtmlUtil.RemoveHtmlAttr(html, "class");
        Assert.Equal("<html><body><div>hello world</div></body></html>", cleanText);
    }

    #endregion

    #region Utility tests

    [Fact]
    public void GetCharsetFromHeaderTest()
    {
        var charsetName = HtmlUtil.GetCharsetFromHeader("Charset=UTF-8;fq=0.9");
        Assert.Equal("UTF-8", charsetName);
    }

    [Fact]
    public void GetCharsetFromHeaderTest_Null()
    {
        var charsetName = HtmlUtil.GetCharsetFromHeader(null);
        Assert.Null(charsetName);
    }

    [Fact]
    public void GetCharsetFromHeaderTest_Empty()
    {
        var charsetName = HtmlUtil.GetCharsetFromHeader("");
        Assert.Null(charsetName);
    }

    #endregion
}
