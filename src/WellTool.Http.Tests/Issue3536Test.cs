using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3536Test
{
    [Fact]
    public void ContentTypeBuildWithEncodingTest()
    {
        // 测试使用不同编码构建 Content-Type
        var contentType1 = ContentType.Build(ContentType.JSON, Encoding.UTF8);
        Assert.Contains("application/json", contentType1);
        Assert.Contains("charset=UTF-8", contentType1);

        var contentType2 = ContentType.Build(ContentType.JSON, Encoding.UTF32);
        Assert.Contains("application/json", contentType2);
        Assert.Contains("charset=UTF-32", contentType2);

        var contentType3 = ContentType.Build(ContentType.JSON, Encoding.Unicode);
        Assert.Contains("application/json", contentType3);
        Assert.Contains("charset=UTF-16", contentType3);
    }

    [Fact]
    public void ContentTypeBuildWithExistingCharsetTest()
    {
        // 测试构建已经包含 charset 的 Content-Type
        var contentType = ContentType.Build("application/json;charset=utf-8", Encoding.UTF8);
        Assert.Equal("application/json;charset=utf-8", contentType);
    }

    [Fact]
    public void ContentTypeBuildWithEmptyContentTypeTest()
    {
        // 测试构建空的 Content-Type
        var contentType = ContentType.Build(string.Empty, Encoding.UTF8);
        Assert.Equal(string.Empty, contentType);

        contentType = ContentType.Build(null, Encoding.UTF8);
        Assert.Null(contentType);
    }

    [Fact]
    public void ContentTypeGetWithVariousContentTest()
    {
        // 测试根据不同内容获取 Content-Type
        // JSON 内容
        Assert.Equal(ContentType.JSON, ContentType.Get("{\"name\": \"value\"}"));
        Assert.Equal(ContentType.JSON, ContentType.Get("[1, 2, 3]"));
        
        // HTML 内容
        Assert.Equal(ContentType.HTML, ContentType.Get("<html><body>Hello</body></html>"));
        Assert.Equal(ContentType.HTML, ContentType.Get("<!DOCTYPE html><html><body>Hello</body></html>"));
        Assert.Equal(ContentType.HTML, ContentType.Get("<body>Hello</body>"));
        Assert.Equal(ContentType.HTML, ContentType.Get("<div>Hello</div>"));
        Assert.Equal(ContentType.HTML, ContentType.Get("<p>Hello</p>"));
        Assert.Equal(ContentType.HTML, ContentType.Get("<span>Hello</span>"));
        
        // XML 内容
        Assert.Equal(ContentType.XML, ContentType.Get("<?xml version=\"1.0\"?><root></root>"));
        Assert.Equal(ContentType.XML, ContentType.Get("<root><child>value</child></root>"));
        
        // 普通文本
        Assert.Null(ContentType.Get("plain text"));
        
        // 空内容
        Assert.Null(ContentType.Get(null));
        Assert.Null(ContentType.Get(string.Empty));
        Assert.Null(ContentType.Get("   "));
    }

    [Fact]
    public void ContentTypeGetEncodingTest()
    {
        // 测试从 Content-Type 中获取编码
        // UTF-8 编码
        var encoding1 = ContentType.GetEncoding("application/json;charset=utf-8");
        Assert.NotNull(encoding1);
        Assert.Equal(Encoding.UTF8.WebName, encoding1.WebName);
        
        // UTF-16 编码
        var encoding2 = ContentType.GetEncoding("application/json;charset=utf-16");
        Assert.NotNull(encoding2);
        Assert.Equal(Encoding.Unicode.WebName, encoding2.WebName);
        
        // UTF-32 编码
        var encoding3 = ContentType.GetEncoding("application/json;charset=utf-32");
        Assert.NotNull(encoding3);
        Assert.Equal(Encoding.UTF32.WebName, encoding3.WebName);
        
        // 没有 charset
        var encoding4 = ContentType.GetEncoding("application/json");
        Assert.Null(encoding4);
        
        // 空 Content-Type
        var encoding5 = ContentType.GetEncoding(null);
        Assert.Null(encoding5);
        
        // 空字符串
        var encoding6 = ContentType.GetEncoding(string.Empty);
        Assert.Null(encoding6);
    }

    [Fact]
    public void ContentTypeIsDefaultTest()
    {
        // 测试判断是否为默认 Content-Type
        Assert.True(ContentType.IsDefault(null));
        Assert.True(ContentType.IsDefault(string.Empty));
        Assert.True(ContentType.IsDefault(ContentType.TEXT));
        Assert.True(ContentType.IsDefault("text/plain"));
        Assert.True(ContentType.IsDefault("text/html"));
        Assert.True(ContentType.IsDefault("text/css"));
        Assert.True(ContentType.IsDefault("text/javascript"));
        
        Assert.False(ContentType.IsDefault(ContentType.JSON));
        Assert.False(ContentType.IsDefault(ContentType.XML));
        Assert.False(ContentType.IsDefault(ContentType.FORM_URLENCODED));
        Assert.False(ContentType.IsDefault(ContentType.MULTIPART));
        Assert.False(ContentType.IsDefault(ContentType.OCTET_STREAM));
    }

    [Fact]
    public void HttpRequestContentTypeTest()
    {
        // 测试 HttpRequest 中的 Content-Type 设置
        var request = HttpRequest.Post("http://example.com");
        
        // 设置 Content-Type
        request.ContentType(ContentType.JSON);
        
        // 验证 Content-Type 是否设置成功
        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Equal(ContentType.JSON, contentType);
        
        // 再次设置 Content-Type，应该覆盖之前的值
        request.ContentType(ContentType.XML);
        contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Equal(ContentType.XML, contentType);
    }

    [Fact]
    public void HttpRequestBodyWithContentTypeTest()
    {
        // 测试设置请求体时自动设置 Content-Type
        var request = HttpRequest.Post("http://example.com");
        
        // 设置 JSON 内容，应该自动设置 Content-Type 为 application/json
        request.Body("{\"name\": \"value\"}");
        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Contains("application/json", contentType);
        
        // 设置 XML 内容，应该自动设置 Content-Type 为 application/xml
        request.Body("<root><child>value</child></root>");
        contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Contains("application/xml", contentType);
    }
}
