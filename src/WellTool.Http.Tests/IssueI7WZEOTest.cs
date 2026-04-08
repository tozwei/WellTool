using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueI7WZEOTest
{
    [Fact]
    public void HttpBaseGetHeaderTest()
    {
        // 测试获取头部
        var request = HttpRequest.Get("http://example.com");
        request.SetHeader("Authorization", "Bearer token123");
        
        // 获取头部值
        var headerValue = request.GetHeader("Authorization");
        Assert.Equal("Bearer token123", headerValue);
        
        // 获取不存在的头部
        var nonExistentHeader = request.GetHeader("Non-Existent");
        Assert.Null(nonExistentHeader);
    }

    [Fact]
    public void HttpBaseGetHeaderListTest()
    {
        // 测试获取头部列表
        var request = HttpRequest.Get("http://example.com");
        
        // 添加多个相同名称的头部
        request.SetHeader("X-Test", "value1", false);
        request.SetHeader("X-Test", "value2", false);
        
        // 获取头部列表
        var headerList = request.GetHeaderList("X-Test");
        Assert.NotNull(headerList);
        Assert.Equal(2, headerList.Count);
        Assert.Contains("value1", headerList);
        Assert.Contains("value2", headerList);
    }

    [Fact]
    public void HttpBaseHeaderMapTest()
    {
        // 测试设置多个头部
        var request = HttpRequest.Get("http://example.com");
        var headers = new Dictionary<string, string>
        {
            { "Authorization", "Bearer token123" },
            { "User-Agent", "WellTool/1.0" },
            { "Accept", "application/json" }
        };
        
        // 设置多个头部
        request.HeaderMap(headers, true);
        
        // 验证头部是否设置成功
        Assert.Equal("Bearer token123", request.GetHeader("Authorization"));
        Assert.Equal("WellTool/1.0", request.GetHeader("User-Agent"));
        Assert.Equal("application/json", request.GetHeader("Accept"));
    }

    [Fact]
    public void HttpBaseAddHeadersTest()
    {
        // 测试添加多个头部（不覆盖）
        var request = HttpRequest.Get("http://example.com");
        
        // 先设置一个头部
        request.SetHeader("X-Test", "value1");
        
        // 添加多个头部
        var headers = new Dictionary<string, string>
        {
            { "X-Test", "value2" },
            { "X-Another", "value3" }
        };
        request.AddHeaders(headers);
        
        // 验证头部是否添加成功
        var headerList = request.GetHeaderList("X-Test");
        Assert.NotNull(headerList);
        Assert.Equal(2, headerList.Count);
        Assert.Contains("value1", headerList);
        Assert.Contains("value2", headerList);
        Assert.Equal("value3", request.GetHeader("X-Another"));
    }

    [Fact]
    public void HttpBaseSetHeadersTest()
    {
        // 测试设置多个头部（带列表）
        var request = HttpRequest.Get("http://example.com");
        var headers = new Dictionary<string, List<string>>
        {
            { "X-Test", new List<string> { "value1", "value2" } },
            { "X-Another", new List<string> { "value3" } }
        };
        
        // 设置多个头部
        request.SetHeaders(headers, true);
        
        // 验证头部是否设置成功
        var testHeaderList = request.GetHeaderList("X-Test");
        Assert.NotNull(testHeaderList);
        Assert.Equal(2, testHeaderList.Count);
        Assert.Contains("value1", testHeaderList);
        Assert.Contains("value2", testHeaderList);
        Assert.Equal("value3", request.GetHeader("X-Another"));
    }

    [Fact]
    public void HttpBaseClearHeadersTest()
    {
        // 测试清除所有头部
        var request = HttpRequest.Get("http://example.com");
        request.SetHeader("Authorization", "Bearer token123");
        request.SetHeader("User-Agent", "WellTool/1.0");
        
        // 验证头部是否设置成功
        Assert.Equal("Bearer token123", request.GetHeader("Authorization"));
        Assert.Equal("WellTool/1.0", request.GetHeader("User-Agent"));
        
        // 清除所有头部
        request.ClearHeaders();
        
        // 验证头部是否清除成功
        Assert.Null(request.GetHeader("Authorization"));
        Assert.Null(request.GetHeader("User-Agent"));
    }

    [Fact]
    public void HttpBaseContentLengthTest()
    {
        // 测试设置内容长度
        var request = HttpRequest.Get("http://example.com");
        
        // 设置内容长度
        request.ContentLength(100);
        
        // 验证内容长度是否设置成功
        var contentLength = request.GetHeader(Header.CONTENT_LENGTH);
        Assert.Equal("100", contentLength);
    }

    [Fact]
    public void HttpBaseHttpVersionTest()
    {
        // 测试设置 HTTP 版本
        var request = HttpRequest.Get("http://example.com");
        
        // 获取默认 HTTP 版本
        var defaultVersion = request.GetHttpVersion();
        Assert.Equal(HttpBase<HttpRequest>.HTTP_1_1, defaultVersion);
        
        // 设置 HTTP 版本为 HTTP/1.0
        request.SetHttpVersion(HttpBase<HttpRequest>.HTTP_1_0);
        
        // 验证 HTTP 版本是否设置成功
        var newVersion = request.GetHttpVersion();
        Assert.Equal(HttpBase<HttpRequest>.HTTP_1_0, newVersion);
    }

    [Fact]
    public void HttpBaseCharsetTest()
    {
        // 测试设置字符集
        var request = HttpRequest.Get("http://example.com");
        
        // 获取默认字符集
        var defaultCharset = request.GetCharset();
        Assert.Equal(Encoding.UTF8.WebName, defaultCharset);
        
        // 设置字符集为 UTF-16
        request.SetCharset(Encoding.Unicode);
        
        // 验证字符集是否设置成功
        var newCharset = request.GetCharset();
        Assert.Equal(Encoding.Unicode.WebName, newCharset);
        
        // 设置字符集为 UTF-8（使用字符串）
        request.SetCharset("utf-8");
        
        // 验证字符集是否设置成功
        var utf8Charset = request.GetCharset();
        Assert.Equal(Encoding.UTF8.WebName, utf8Charset);
    }

    [Fact]
    public void HttpBaseBodyBytesTest()
    {
        // 测试获取请求体字节数组
        var request = HttpRequest.Get("http://example.com");
        
        // 设置请求体
        var body = "{\"name\": \"value\"}";
        request.Body(body);
        
        // 获取请求体字节数组
        var bodyBytes = request.BodyBytes();
        Assert.NotNull(bodyBytes);
        Assert.NotEmpty(bodyBytes);
        
        // 验证字节数组是否正确
        var decodedBody = Encoding.UTF8.GetString(bodyBytes);
        Assert.Equal(body, decodedBody);
    }
}
