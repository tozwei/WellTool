using System.Collections.Generic;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3343Test
{
    [Fact]
    public void SetHeaderTest()
    {
        // 测试设置单个头部
        var request = HttpRequest.Get("http://example.com");
        request.SetHeader("Authorization", "Bearer token123");
        
        // 验证头部是否设置成功
        var headerValue = request.GetHeader("Authorization");
        Assert.Equal("Bearer token123", headerValue);
    }

    [Fact]
    public void SetHeaderWithEnumTest()
    {
        // 测试使用 Header 枚举设置头部
        var request = HttpRequest.Get("http://example.com");
        request.SetHeader(Header.AUTHORIZATION, "Bearer token123");
        
        // 验证头部是否设置成功
        var headerValue = request.GetHeader(Header.AUTHORIZATION);
        Assert.Equal("Bearer token123", headerValue);
    }

    [Fact]
    public void SetContentTypeTest()
    {
        // 测试设置 Content-Type 头部
        var request = HttpRequest.Get("http://example.com");
        request.SetContentType(ContentType.JSON);
        
        // 验证 Content-Type 是否设置成功
        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Equal(ContentType.JSON, contentType);
    }

    [Fact]
    public void AddMultipleHeadersTest()
    {
        // 测试添加多个头部
        var request = HttpRequest.Get("http://example.com");
        var headers = new Dictionary<string, string>
        {
            { "Authorization", "Bearer token123" },
            { "User-Agent", "WellTool/1.0" },
            { "Accept", "application/json" }
        };
        request.AddHeaders(headers);
        
        // 验证所有头部是否添加成功
        Assert.Equal("Bearer token123", request.GetHeader("Authorization"));
        Assert.Equal("WellTool/1.0", request.GetHeader("User-Agent"));
        Assert.Equal("application/json", request.GetHeader("Accept"));
    }

    [Fact]
    public void RemoveHeaderTest()
    {
        // 测试移除头部
        var request = HttpRequest.Get("http://example.com");
        request.SetHeader("Authorization", "Bearer token123");
        
        // 验证头部是否设置成功
        var headerValue = request.GetHeader("Authorization");
        Assert.Equal("Bearer token123", headerValue);
        
        // 移除头部
        request.RemoveHeader("Authorization");
        
        // 验证头部是否移除成功
        headerValue = request.GetHeader("Authorization");
        Assert.Null(headerValue);
    }

    [Fact]
    public void RemoveHeaderWithEnumTest()
    {
        // 测试使用 Header 枚举移除头部
        var request = HttpRequest.Get("http://example.com");
        request.SetHeader(Header.AUTHORIZATION, "Bearer token123");
        
        // 验证头部是否设置成功
        var headerValue = request.GetHeader(Header.AUTHORIZATION);
        Assert.Equal("Bearer token123", headerValue);
        
        // 移除头部
        request.RemoveHeader(Header.AUTHORIZATION);
        
        // 验证头部是否移除成功
        headerValue = request.GetHeader(Header.AUTHORIZATION);
        Assert.Null(headerValue);
    }

    [Fact]
    public void ClearHeadersTest()
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
        
        // 验证所有头部是否清除成功
        Assert.Null(request.GetHeader("Authorization"));
        Assert.Null(request.GetHeader("User-Agent"));
    }

    [Fact]
    public void HeaderAggregationTest()
    {
        // 测试头部聚合设置
        var request = HttpRequest.Get("http://example.com");
        
        // 设置头部聚合
        request.HeaderAggregation(true);
        
        // 验证头部聚合设置是否成功
        Assert.True(request.GetIsHeaderAggregated());
        
        // 取消头部聚合
        request.HeaderAggregation(false);
        
        // 验证头部聚合设置是否成功
        Assert.False(request.GetIsHeaderAggregated());
    }

    [Fact]
    public void ContentLengthTest()
    {
        // 测试设置 Content-Length 头部
        var request = HttpRequest.Get("http://example.com");
        request.ContentLength(100);
        
        // 验证 Content-Length 是否设置成功
        var contentLength = request.GetHeader(Header.CONTENT_LENGTH);
        Assert.Equal("100", contentLength);
    }

    [Fact]
    public void HttpVersionTest()
    {
        // 测试设置 HTTP 版本
        var request = HttpRequest.Get("http://example.com");
        
        // 验证默认 HTTP 版本
        Assert.Equal(HttpBase<HttpRequest>.HTTP_1_1, request.GetHttpVersion());
        
        // 设置 HTTP 版本为 HTTP/1.0
        request.SetHttpVersion(HttpBase<HttpRequest>.HTTP_1_0);
        
        // 验证 HTTP 版本是否设置成功
        Assert.Equal(HttpBase<HttpRequest>.HTTP_1_0, request.GetHttpVersion());
    }
}
