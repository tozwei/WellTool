using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3074Test
{
    [Fact]
    public void ContentTypeConstantsTest()
    {
        // 测试 ContentType 常量
        Assert.Equal("application/json", ContentType.JSON);
        Assert.Equal("application/xml", ContentType.XML);
        Assert.Equal("text/html", ContentType.HTML);
        Assert.Equal("text/plain", ContentType.TEXT);
        Assert.Equal("application/x-www-form-urlencoded", ContentType.FORM_URLENCODED);
        Assert.Equal("multipart/form-data", ContentType.MULTIPART);
        Assert.Equal("application/octet-stream", ContentType.OCTET_STREAM);
    }

    [Fact]
    public void ContentTypeIsDefaultTest()
    {
        // 测试 IsDefault 方法
        Assert.True(ContentType.IsDefault(null));
        Assert.True(ContentType.IsDefault(string.Empty));
        Assert.True(ContentType.IsDefault("text/plain"));
        Assert.True(ContentType.IsDefault("text/html"));
        Assert.False(ContentType.IsDefault("application/json"));
        Assert.False(ContentType.IsDefault("application/xml"));
    }

    [Fact]
    public void ContentTypeBuildTest()
    {
        // 测试 Build 方法
        var contentType = ContentType.Build(ContentType.JSON, Encoding.UTF8);
        Assert.Contains("application/json", contentType);
        Assert.Contains("charset=UTF-8", contentType);

        // 测试已经包含 charset 的情况
        var contentTypeWithCharset = ContentType.Build("application/json;charset=utf-8", Encoding.UTF8);
        Assert.Equal("application/json;charset=utf-8", contentTypeWithCharset);
    }

    [Fact]
    public void ContentTypeGetTest()
    {
        // 测试 Get 方法
        Assert.Equal(ContentType.JSON, ContentType.Get("{\"key\": \"value\"}"));
        Assert.Equal(ContentType.JSON, ContentType.Get("[1, 2, 3]"));
        Assert.Equal(ContentType.HTML, ContentType.Get("<html><body>Hello</body></html>"));
        Assert.Equal(ContentType.XML, ContentType.Get("<?xml version=\"1.0\"?><root></root>"));
        Assert.Null(ContentType.Get("plain text"));
        Assert.Null(ContentType.Get(null));
        Assert.Null(ContentType.Get(string.Empty));
    }

    [Fact]
    public void ContentTypeGetEncodingTest()
    {
        // 测试 GetEncoding 方法
        var encoding = ContentType.GetEncoding("application/json;charset=utf-8");
        Assert.NotNull(encoding);
        Assert.Equal(Encoding.UTF8.WebName, encoding.WebName);

        // 测试没有 charset 的情况
        Assert.Null(ContentType.GetEncoding("application/json"));
        Assert.Null(ContentType.GetEncoding(null));
        Assert.Null(ContentType.GetEncoding(string.Empty));
    }

    [Fact]
    public void CreatePostTest()
    {
        // 测试 HttpUtil.CreatePost 方法
        var request = HttpUtil.CreatePost("http://localhost:8888/body");
        Assert.NotNull(request);
        
        // 测试设置 Content-Type 和 Body
        request.SetContentType(ContentType.JSON).Body("{\"key\": \"value\"}");
        Assert.NotNull(request);
    }
}
