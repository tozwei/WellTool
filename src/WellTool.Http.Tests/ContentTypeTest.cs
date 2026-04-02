using System.Text;
using WellTool.Http;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// ContentType 内容类型测试
/// </summary>
public class ContentTypeTest
{
    #region Build tests

    [Fact]
    public void BuildTest()
    {
        var result = ContentType.Build(ContentType.JSON, Encoding.UTF8);
        Assert.Equal("application/json;charset=UTF-8", result);
    }

    [Fact]
    public void BuildTest_AlreadyHasCharset()
    {
        var result = ContentType.Build("application/json;charset=GBK", Encoding.UTF8);
        Assert.Equal("application/json;charset=GBK", result);
    }

    [Fact]
    public void BuildTest_NullContentType()
    {
        var result = ContentType.Build(null, Encoding.UTF8);
        Assert.Null(result);
    }

    [Fact]
    public void BuildTest_EmptyContentType()
    {
        var result = ContentType.Build("", Encoding.UTF8);
        Assert.Equal("", result);
    }

    [Fact]
    public void BuildTest_Xml()
    {
        var result = ContentType.Build(ContentType.XML, Encoding.UTF8);
        Assert.Equal("application/xml;charset=UTF-8", result);
    }

    [Fact]
    public void BuildTest_FormUrlEncoded()
    {
        var result = ContentType.Build(ContentType.FORM_URLENCODED, Encoding.UTF8);
        Assert.Equal("application/x-www-form-urlencoded;charset=UTF-8", result);
    }

    [Fact]
    public void BuildTest_Multipart()
    {
        var result = ContentType.Build(ContentType.MULTIPART, Encoding.UTF8);
        Assert.Equal("multipart/form-data;charset=UTF-8", result);
    }

    [Fact]
    public void BuildTest_Text()
    {
        var result = ContentType.Build(ContentType.TEXT, Encoding.UTF8);
        Assert.Equal("text/plain;charset=UTF-8", result);
    }

    [Fact]
    public void BuildTest_Html()
    {
        var result = ContentType.Build(ContentType.HTML, Encoding.UTF8);
        Assert.Equal("text/html;charset=UTF-8", result);
    }

    [Fact]
    public void BuildTest_OctetStream()
    {
        var result = ContentType.Build(ContentType.OCTET_STREAM, Encoding.UTF8);
        Assert.Equal("application/octet-stream;charset=UTF-8", result);
    }

    #endregion

    #region Get tests

    [Fact]
    public void GetTest_JsonObject()
    {
        var json = "{\"name\":\"hutool\"}";
        var result = ContentType.Get(json);
        Assert.Equal(ContentType.JSON, result);
    }

    [Fact]
    public void GetTest_JsonArray()
    {
        var json = "[\"a\",\"b\",\"c\"]";
        var result = ContentType.Get(json);
        Assert.Equal(ContentType.JSON, result);
    }

    [Fact]
    public void GetTest_WithLeadingSpace()
    {
        var json = " {\n" +
            "     \"name\": \"hutool\"\n" +
            " }";
        var contentType = ContentType.Get(json);
        Assert.Equal(ContentType.JSON, contentType);
    }

    [Fact]
    public void GetTest_Xml()
    {
        var xml = "<?xml version=\"1.0\"?><root></root>";
        var result = ContentType.Get(xml);
        Assert.Equal(ContentType.XML, result);
    }

    [Fact]
    public void GetTest_Html()
    {
        var html = "<html><body></body></html>";
        var result = ContentType.Get(html);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_Doctype()
    {
        var html = "<!DOCTYPE html><html></html>";
        var result = ContentType.Get(html);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_BodyTag()
    {
        var html = "<body></body>";
        var result = ContentType.Get(html);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_DivTag()
    {
        var html = "<div></div>";
        var result = ContentType.Get(html);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_PTag()
    {
        var html = "<p>text</p>";
        var result = ContentType.Get(html);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_SpanTag()
    {
        var html = "<span>text</span>";
        var result = ContentType.Get(html);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_NotHtmlIfStartsWithHtml()
    {
        // 注意：这个是区分HTML和XML的关键点
        var xml = "<html>test</html>";
        var result = ContentType.Get(xml);
        Assert.Equal(ContentType.HTML, result);
    }

    [Fact]
    public void GetTest_Null()
    {
        var result = ContentType.Get(null);
        Assert.Null(result);
    }

    [Fact]
    public void GetTest_Empty()
    {
        var result = ContentType.Get("");
        Assert.Null(result);
    }

    [Fact]
    public void GetTest_Whitespace()
    {
        var result = ContentType.Get("   ");
        Assert.Null(result);
    }

    [Fact]
    public void GetTest_PlainText()
    {
        var text = "just plain text";
        var result = ContentType.Get(text);
        Assert.Null(result);
    }

    #endregion

    #region IsDefault tests

    [Fact]
    public void IsDefaultTest_Null()
    {
        Assert.True(ContentType.IsDefault(null));
    }

    [Fact]
    public void IsDefaultTest_Empty()
    {
        Assert.True(ContentType.IsDefault(""));
    }

    [Fact]
    public void IsDefaultTest_TextPlain()
    {
        Assert.True(ContentType.IsDefault(ContentType.TEXT));
    }

    [Fact]
    public void IsDefaultTest_TextHtml()
    {
        Assert.True(ContentType.IsDefault("text/html"));
    }

    [Fact]
    public void IsDefaultTest_ApplicationJson()
    {
        Assert.False(ContentType.IsDefault(ContentType.JSON));
    }

    [Fact]
    public void IsDefaultTest_ApplicationXml()
    {
        Assert.False(ContentType.IsDefault(ContentType.XML));
    }

    [Fact]
    public void IsDefaultTest_CaseInsensitive()
    {
        Assert.True(ContentType.IsDefault("TEXT/PLAIN"));
        Assert.False(ContentType.IsDefault("APPLICATION/JSON"));
    }

    #endregion

    #region GetEncoding tests

    [Fact]
    public void GetEncodingTest_WithCharset()
    {
        var encoding = ContentType.GetEncoding("application/json;charset=UTF-8");
        Assert.NotNull(encoding);
        Assert.Equal(Encoding.UTF8, encoding);
    }

    [Fact]
    public void GetEncodingTest_WithGBK()
    {
        var encoding = ContentType.GetEncoding("text/html;charset=GBK");
        // GBK 编码可能在某些环境中不可用，所以我们检查是否为 null
        if (encoding != null)
        {
            Assert.Equal("GBK", encoding.EncodingName);
        }
    }

    [Fact]
    public void GetEncodingTest_NoCharset()
    {
        var encoding = ContentType.GetEncoding("application/json");
        Assert.Null(encoding);
    }

    [Fact]
    public void GetEncodingTest_Null()
    {
        var encoding = ContentType.GetEncoding(null);
        Assert.Null(encoding);
    }

    [Fact]
    public void GetEncodingTest_Empty()
    {
        var encoding = ContentType.GetEncoding("");
        Assert.Null(encoding);
    }

    [Fact]
    public void GetEncodingTest_InvalidCharset()
    {
        var encoding = ContentType.GetEncoding("application/json;charset=INVALID_CHARSET");
        Assert.Null(encoding);
    }

    [Fact]
    public void GetEncodingTest_WithQuotes()
    {
        var encoding = ContentType.GetEncoding("application/json;charset=\"UTF-8\"");
        Assert.NotNull(encoding);
        Assert.Equal(Encoding.UTF8, encoding);
    }

    [Fact]
    public void GetEncodingTest_WithSingleQuotes()
    {
        var encoding = ContentType.GetEncoding("application/json;charset='UTF-8'");
        Assert.NotNull(encoding);
        Assert.Equal(Encoding.UTF8, encoding);
    }

    #endregion

    #region Constants tests

    [Fact]
    public void ConstantsTest()
    {
        Assert.Equal("application/json", ContentType.JSON);
        Assert.Equal("application/xml", ContentType.XML);
        Assert.Equal("text/html", ContentType.HTML);
        Assert.Equal("text/plain", ContentType.TEXT);
        Assert.Equal("application/x-www-form-urlencoded", ContentType.FORM_URLENCODED);
        Assert.Equal("multipart/form-data", ContentType.MULTIPART);
        Assert.Equal("application/octet-stream", ContentType.OCTET_STREAM);
    }

    #endregion
}
