using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueI8YV0KTest
{
    [Fact]
    public void HttpUtilIsHttpTest()
    {
        // 测试 IsHttp 方法
        Assert.True(HttpUtil.IsHttp("http://example.com"));
        Assert.True(HttpUtil.IsHttp("HTTP://example.com"));
        Assert.True(HttpUtil.IsHttp("http://example.com:8080"));
        Assert.True(HttpUtil.IsHttp("http://localhost"));
        Assert.False(HttpUtil.IsHttp("https://example.com"));
        Assert.False(HttpUtil.IsHttp("ftp://example.com"));
        Assert.False(HttpUtil.IsHttp("file:///C:/path"));
        Assert.False(HttpUtil.IsHttp("example.com"));
        Assert.False(HttpUtil.IsHttp(string.Empty));
        Assert.False(HttpUtil.IsHttp(null));
    }

    [Fact]
    public void HttpUtilIsHttpsTest()
    {
        // 测试 IsHttps 方法
        Assert.True(HttpUtil.IsHttps("https://example.com"));
        Assert.True(HttpUtil.IsHttps("HTTPS://example.com"));
        Assert.True(HttpUtil.IsHttps("https://example.com:443"));
        Assert.True(HttpUtil.IsHttps("https://localhost"));
        Assert.False(HttpUtil.IsHttps("http://example.com"));
        Assert.False(HttpUtil.IsHttps("ftp://example.com"));
        Assert.False(HttpUtil.IsHttps("file:///C:/path"));
        Assert.False(HttpUtil.IsHttps("example.com"));
        Assert.False(HttpUtil.IsHttps(string.Empty));
        Assert.False(HttpUtil.IsHttps(null));
    }

    [Fact]
    public void HttpUtilToParamsWithEmptyMapTest()
    {
        // 测试 ToParams 方法处理空字典
        var paramMap = new Dictionary<string, object?>();
        var paramsString = HttpUtil.ToParams(paramMap);
        Assert.Equal(string.Empty, paramsString);
    }

    [Fact]
    public void HttpUtilToParamsWithNullValuesTest()
    {
        // 测试 ToParams 方法处理 null 值
        var paramMap = new Dictionary<string, object?>
        {
            { "name", "test" },
            { "age", null },
            { "active", true }
        };
        var paramsString = HttpUtil.ToParams(paramMap);
        Assert.Contains("name=test", paramsString);
        Assert.Contains("active=True", paramsString);
        // 不应该包含 age 参数，因为它是 null
        Assert.DoesNotContain("age=", paramsString);
    }

    [Fact]
    public void HttpUtilToParamsWithEmptyKeyTest()
    {
        // 测试 ToParams 方法处理空键
        var paramMap = new Dictionary<string, object?>
        {
            { "name", "test" },
            { "", "empty" },
            { "active", true }
        };
        var paramsString = HttpUtil.ToParams(paramMap);
        Assert.Contains("name=test", paramsString);
        Assert.Contains("active=True", paramsString);
        // 不应该包含空键参数
        Assert.DoesNotContain("empty", paramsString);
    }

    [Fact]
    public void HttpUtilDecodeParamMapWithEmptyStringTest()
    {
        // 测试 DecodeParamMap 方法处理空字符串
        var paramMap = HttpUtil.DecodeParamMap(string.Empty);
        Assert.NotNull(paramMap);
        Assert.Empty(paramMap);
    }

    [Fact]
    public void HttpUtilDecodeParamMapWithNullTest()
    {
        // 测试 DecodeParamMap 方法处理 null
        var paramMap = HttpUtil.DecodeParamMap(null);
        Assert.NotNull(paramMap);
        Assert.Empty(paramMap);
    }

    [Fact]
    public void HttpUtilDecodeParamMapWithNoEqualsTest()
    {
        // 测试 DecodeParamMap 方法处理没有等号的参数
        var paramsString = "name&age=18&active";
        var paramMap = HttpUtil.DecodeParamMap(paramsString);
        Assert.NotNull(paramMap);
        Assert.Equal(3, paramMap.Count);
        Assert.Equal(string.Empty, paramMap["name"]);
        Assert.Equal("18", paramMap["age"]);
        Assert.Equal(string.Empty, paramMap["active"]);
    }

    [Fact]
    public void HttpUtilDecodeParamMapWithEmptyValueTest()
    {
        // 测试 DecodeParamMap 方法处理空值
        var paramsString = "name=&age=18&active=";
        var paramMap = HttpUtil.DecodeParamMap(paramsString);
        Assert.NotNull(paramMap);
        Assert.Equal(3, paramMap.Count);
        Assert.Equal(string.Empty, paramMap["name"]);
        Assert.Equal("18", paramMap["age"]);
        Assert.Equal(string.Empty, paramMap["active"]);
    }

    [Fact]
    public void HttpUtilEncodeParamsWithEmptyUrlTest()
    {
        // 测试 EncodeParams 方法处理空 URL
        var encodedUrl = HttpUtil.EncodeParams(string.Empty, Encoding.UTF8);
        Assert.Equal(string.Empty, encodedUrl);
    }

    [Fact]
    public void HttpUtilEncodeParamsWithNullUrlTest()
    {
        // 测试 EncodeParams 方法处理 null URL
        var encodedUrl = HttpUtil.EncodeParams(null, Encoding.UTF8);
        Assert.Equal(string.Empty, encodedUrl);
    }

    [Fact]
    public void HttpUtilGetCharsetWithNullContentTypeTest()
    {
        // 测试 GetCharset 方法处理 null Content-Type
        var charset = HttpUtil.GetCharset(null);
        Assert.Null(charset);
    }

    [Fact]
    public void HttpUtilGetCharsetWithEmptyContentTypeTest()
    {
        // 测试 GetCharset 方法处理空 Content-Type
        var charset = HttpUtil.GetCharset(string.Empty);
        Assert.Null(charset);
    }

    [Fact]
    public void HttpUtilGetMimeTypeWithNullFileNameTest()
    {
        // 测试 GetMimeType 方法处理 null 文件名
        var mimeType = HttpUtil.GetMimeType(null);
        Assert.Null(mimeType);
    }

    [Fact]
    public void HttpUtilGetMimeTypeWithEmptyFileNameTest()
    {
        // 测试 GetMimeType 方法处理空文件名
        var mimeType = HttpUtil.GetMimeType(string.Empty);
        Assert.Null(mimeType);
    }
}
