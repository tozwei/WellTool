using System.Collections.Generic;
using System.Text;
using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueIB7REWTest
{
    [Fact]
    public void HttpBaseSetHeaderWithEmptyNameTest()
    {
        // 测试设置空名称的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空名称的头部
        request.SetHeader(string.Empty, "value");
        
        // 验证头部是否设置成功（空名称的头部应该被忽略）
        var headerValue = request.GetHeader(string.Empty);
        Assert.Null(headerValue);
    }

    [Fact]
    public void HttpBaseSetHeaderWithNullValueTest()
    {
        // 测试设置 null 值的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null 值的头部
        request.SetHeader("X-Test", null);
        
        // 验证头部是否设置成功（null 值的头部应该被忽略）
        var headerValue = request.GetHeader("X-Test");
        Assert.Null(headerValue);
    }

    [Fact]
    public void HttpBaseSetHeaderWithEmptyValueTest()
    {
        // 测试设置空值的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空值的头部
        request.SetHeader("X-Test", string.Empty);
        
        // 验证头部是否设置成功
        var headerValue = request.GetHeader("X-Test");
        Assert.Equal(string.Empty, headerValue);
    }

    [Fact]
    public void HttpBaseGetHeaderWithEmptyNameTest()
    {
        // 测试获取空名称的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 获取空名称的头部
        var headerValue = request.GetHeader(string.Empty);
        
        // 验证是否返回 null
        Assert.Null(headerValue);
    }

    [Fact]
    public void HttpBaseGetHeaderWithNullNameTest()
    {
        // 测试获取 null 名称的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 获取 null 名称的头部
        var headerValue = request.GetHeader((string)null);
        
        // 验证是否返回 null
        Assert.Null(headerValue);
    }

    [Fact]
    public void HttpBaseGetHeaderListWithEmptyNameTest()
    {
        // 测试获取空名称的头部列表
        var request = HttpRequest.Get("http://example.com");
        
        // 获取空名称的头部列表
        var headerList = request.GetHeaderList(string.Empty);
        
        // 验证是否返回 null
        Assert.Null(headerList);
    }

    [Fact]
    public void HttpBaseGetHeaderListWithNullNameTest()
    {
        // 测试获取 null 名称的头部列表
        var request = HttpRequest.Get("http://example.com");
        
        // 获取 null 名称的头部列表
        var headerList = request.GetHeaderList(null);
        
        // 验证是否返回 null
        Assert.Null(headerList);
    }

    [Fact]
    public void HttpBaseRemoveHeaderWithEmptyNameTest()
    {
        // 测试移除空名称的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 移除空名称的头部
        request.RemoveHeader(string.Empty);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseRemoveHeaderWithNullNameTest()
    {
        // 测试移除 null 名称的头部
        var request = HttpRequest.Get("http://example.com");
        
        // 移除 null 名称的头部
        request.RemoveHeader((string)null);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseHeaderMapWithNullHeadersTest()
    {
        // 测试设置 null 头部字典
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null 头部字典
        request.HeaderMap(null, true);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseHeaderMapWithEmptyHeadersTest()
    {
        // 测试设置空头部字典
        var request = HttpRequest.Get("http://example.com");
        var headers = new Dictionary<string, string>();
        
        // 设置空头部字典
        request.HeaderMap(headers, true);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseAddHeadersWithNullHeadersTest()
    {
        // 测试添加 null 头部字典
        var request = HttpRequest.Get("http://example.com");
        
        // 添加 null 头部字典
        request.AddHeaders(null);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseAddHeadersWithEmptyHeadersTest()
    {
        // 测试添加空头部字典
        var request = HttpRequest.Get("http://example.com");
        var headers = new Dictionary<string, string>();
        
        // 添加空头部字典
        request.AddHeaders(headers);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseSetHeadersWithNullHeadersTest()
    {
        // 测试设置 null 头部列表字典
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null 头部列表字典
        request.SetHeaders(null, true);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseSetHeadersWithEmptyHeadersTest()
    {
        // 测试设置空头部列表字典
        var request = HttpRequest.Get("http://example.com");
        var headers = new Dictionary<string, List<string>>();
        
        // 设置空头部列表字典
        request.SetHeaders(headers, true);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseSetCharsetWithEmptyStringTest()
    {
        // 测试设置空字符串字符集
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空字符串字符集
        request.SetCharset(string.Empty);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseSetCharsetWithInvalidNameTest()
    {
        // 测试设置无效名称的字符集
        var request = HttpRequest.Get("http://example.com");
        
        // 设置无效名称的字符集
        request.SetCharset("invalid-charset");
        
        // 验证操作是否成功（应该不会抛出异常，而是使用默认字符集）
        Assert.NotNull(request);
        var charset = request.GetCharset();
        Assert.Equal(Encoding.UTF8.WebName, charset);
    }

    [Fact]
    public void HttpBaseSetCharsetWithNullTest()
    {
        // 测试设置 null 字符集
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null 字符集
        request.SetCharset((Encoding)null);
        
        // 验证操作是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpBaseSetHttpVersionWithEmptyStringTest()
    {
        // 测试设置空字符串 HTTP 版本
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空字符串 HTTP 版本
        request.SetHttpVersion(string.Empty);
        
        // 验证 HTTP 版本是否设置成功
        var httpVersion = request.GetHttpVersion();
        Assert.Equal(string.Empty, httpVersion);
    }

    [Fact]
    public void HttpBaseSetHttpVersionWithNullTest()
    {
        // 测试设置 null HTTP 版本
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 null HTTP 版本
        request.SetHttpVersion(null);
        
        // 验证 HTTP 版本是否设置成功
        var httpVersion = request.GetHttpVersion();
        Assert.Null(httpVersion);
    }

    [Fact]
    public void HttpBaseContentLengthWithNegativeTest()
    {
        // 测试设置负的内容长度
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负的内容长度
        request.ContentLength(-1);
        
        // 验证内容长度是否设置成功
        var contentLength = request.GetHeader(Header.CONTENT_LENGTH);
        Assert.Equal("-1", contentLength);
    }

    [Fact]
    public void HttpBaseContentLengthWithZeroTest()
    {
        // 测试设置零内容长度
        var request = HttpRequest.Get("http://example.com");
        
        // 设置零内容长度
        request.ContentLength(0);
        
        // 验证内容长度是否设置成功
        var contentLength = request.GetHeader(Header.CONTENT_LENGTH);
        Assert.Equal("0", contentLength);
    }

    [Fact]
    public void HttpBaseToStringWithNoHeadersTest()
    {
        // 测试没有头部的情况下调用 ToString 方法
        var request = HttpRequest.Get("http://example.com");
        
        // 调用 ToString 方法
        var requestString = request.ToString();
        
        // 验证 ToString 方法是否返回非空字符串
        Assert.NotNull(requestString);
        Assert.NotEmpty(requestString);
        
        // 验证字符串中包含必要的信息
        Assert.Contains("Headers:", requestString);
        Assert.Contains("Body:", requestString);
        Assert.Contains("(null)", requestString);
    }

    [Fact]
    public void HttpBaseToStringWithNoBodyTest()
    {
        // 测试没有请求体的情况下调用 ToString 方法
        var request = HttpRequest.Get("http://example.com")
            .SetHeader("User-Agent", "WellTool/1.0");
        
        // 调用 ToString 方法
        var requestString = request.ToString();
        
        // 验证 ToString 方法是否返回非空字符串
        Assert.NotNull(requestString);
        Assert.NotEmpty(requestString);
        
        // 验证字符串中包含必要的信息
        Assert.Contains("Headers:", requestString);
        Assert.Contains("User-Agent: WellTool/1.0", requestString);
        Assert.Contains("Body:", requestString);
        Assert.Contains("(null)", requestString);
    }
}
