using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueIB3SJFTest
{
    [Fact]
    public void HttpRequestSetUrlWithEmptyTest()
    {
        // 测试设置空 URL
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空 URL
        request.SetUrl(string.Empty);
        
        // 验证 URL 是否设置成功
        var url = request.GetUrl();
        Assert.Equal(string.Empty, url);
    }

    [Fact]
    public void HttpRequestSetMethodWithInvalidTest()
    {
        // 测试设置无效的 HTTP 方法
        var request = HttpRequest.Get("http://example.com");
        
        // 设置无效的 HTTP 方法
        request.SetMethod((Method)999);
        
        // 验证方法是否设置成功（应该不会抛出异常）
        var method = request.GetMethod();
        Assert.Equal((Method)999, method);
    }

    [Fact]
    public void HttpRequestFormWithEmptyNameTest()
    {
        // 测试设置空名称的表单参数
        var request = HttpRequest.Post("http://example.com");
        
        // 设置空名称的表单参数
        request.Form(string.Empty, "value");
        
        // 验证表单数据是否设置成功（空名称的参数应该被忽略）
        var form = request.Form();
        Assert.Null(form);
    }

    [Fact]
    public void HttpRequestFormWithNullValueTest()
    {
        // 测试设置 null 值的表单参数
        var request = HttpRequest.Post("http://example.com");
        
        // 设置 null 值的表单参数
        request.Form("name", null);
        
        // 验证表单数据是否设置成功（null 值的参数应该被忽略）
        var form = request.Form();
        Assert.Null(form);
    }

    [Fact]
    public void HttpRequestBodyWithEmptyStringTest()
    {
        // 测试设置空字符串请求体
        var request = HttpRequest.Post("http://example.com");
        
        // 设置空字符串请求体
        request.Body(string.Empty);
        
        // 验证请求体是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.NotNull(bodyBytes);
        Assert.Empty(bodyBytes);
    }

    [Fact]
    public void HttpRequestBodyWithNullTest()
    {
        // 测试设置 null 请求体
        var request = HttpRequest.Post("http://example.com");
        
        // 设置 null 请求体
        request.Body((string)null);
        
        // 验证请求体是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.Null(bodyBytes);
    }

    [Fact]
    public void HttpRequestBodyWithEmptyBytesTest()
    {
        // 测试设置空字节数组请求体
        var request = HttpRequest.Post("http://example.com");
        
        // 设置空字节数组请求体
        request.Body(new byte[0]);
        
        // 验证请求体是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.NotNull(bodyBytes);
        Assert.Empty(bodyBytes);
    }

    [Fact]
    public void HttpRequestBodyWithNullBytesTest()
    {
        // 测试设置 null 字节数组请求体
        var request = HttpRequest.Post("http://example.com");
        
        // 设置 null 字节数组请求体
        request.Body((byte[])null);
        
        // 验证请求体是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.Null(bodyBytes);
    }

    [Fact]
    public void HttpRequestTimeoutWithNegativeTest()
    {
        // 测试设置负的超时值
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负的超时值
        request.Timeout(-1);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetConnectionTimeoutWithNegativeTest()
    {
        // 测试设置负的连接超时值
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负的连接超时值
        request.SetConnectionTimeout(-1);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetReadTimeoutWithNegativeTest()
    {
        // 测试设置负的读取超时值
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负的读取超时值
        request.SetReadTimeout(-1);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetMaxRedirectCountWithNegativeTest()
    {
        // 测试设置负的最大重定向次数
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负的最大重定向次数
        request.SetMaxRedirectCount(-1);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetChunkedStreamingModeWithZeroTest()
    {
        // 测试设置零块大小
        var request = HttpRequest.Get("http://example.com");
        
        // 设置零块大小
        request.SetChunkedStreamingMode(0);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetChunkedStreamingModeWithNegativeTest()
    {
        // 测试设置负的块大小
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负的块大小
        request.SetChunkedStreamingMode(-1);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetHttpProxyWithEmptyHostTest()
    {
        // 测试设置空主机的代理
        var request = HttpRequest.Get("http://example.com");
        
        // 设置空主机的代理
        request.SetHttpProxy(string.Empty, 8080);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetHttpProxyWithNegativePortTest()
    {
        // 测试设置负端口的代理
        var request = HttpRequest.Get("http://example.com");
        
        // 设置负端口的代理
        request.SetHttpProxy("127.0.0.1", -1);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetHttpProxyWithZeroPortTest()
    {
        // 测试设置零端口的代理
        var request = HttpRequest.Get("http://example.com");
        
        // 设置零端口的代理
        request.SetHttpProxy("127.0.0.1", 0);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetHttpProxyWithInvalidPortTest()
    {
        // 测试设置无效端口的代理
        var request = HttpRequest.Get("http://example.com");
        
        // 设置无效端口的代理
        request.SetHttpProxy("127.0.0.1", 99999);
        
        // 验证设置是否成功（应该不会抛出异常）
        Assert.NotNull(request);
    }
}
