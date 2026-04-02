using WellTool.Http;
using Xunit;
using System.Text;

namespace WellTool.Http.Tests;

/// <summary>
/// Issue 相关测试类
/// </summary>
public class IssueTests
{
    [Fact]
    public void Issue2658Test()
    {
        // 测试 Issue 2658: HTTP 请求超时设置
        var request = HttpRequest.Get("http://example.com")
            .Timeout(5000);
        
        // 验证请求对象不为空
        Assert.NotNull(request);
    }

    [Fact]
    public void Issue3074Test()
    {
        // 测试 Issue 3074: HTTP 请求头设置
        var request = HttpRequest.Get("http://example.com")
            .SetHeader("X-Custom-Header", "test-value");
        
        // 验证请求对象不为空
        Assert.NotNull(request);
        var headerValue = request.GetHeader("X-Custom-Header");
        Assert.Equal("test-value", headerValue);
    }

    [Fact]
    public void Issue3197Test()
    {
        // 测试 Issue 3197: HTTP 请求参数处理
        var request = HttpRequest.Get("http://example.com")
            .Form("param1", "value1")
            .Form("param2", "value2");
        
        // 验证请求对象不为空
        Assert.NotNull(request);
    }

    [Fact]
    public void Issue3314Test()
    {
        // 测试 Issue 3314: HTTP 请求体处理
        var request = HttpRequest.Post("http://example.com")
            .Body("test body content");
        
        // 验证请求对象不为空
        Assert.NotNull(request);
    }

    [Fact]
    public void Issue3536Test()
    {
        // 测试 Issue 3536: HTTP 响应处理
        var request = HttpRequest.Get("http://example.com");
        
        // 验证请求对象不为空
        Assert.NotNull(request);
    }

    [Fact]
    public void IssueI5TPSYTest()
    {
        // 测试 Issue I5TPSY: HTTP 编码处理
        // 由于 UrlEncode 是 internal 方法，我们通过 ToParams 方法间接测试
        var parameters = new System.Collections.Generic.Dictionary<string, object?>
        {
            { "test", "测试中文" }
        };
        var encoded = HttpUtil.ToParams(parameters);
        Assert.NotNull(encoded);
        Assert.Contains("test=", encoded);
        // 验证编码结果包含中文的编码形式
        Assert.Contains("%E6%B5%8B%E8%AF%95%E4%B8%AD%E6%96%87", encoded);
    }

    [Fact]
    public void IssueI6RE7JTest()
    {
        // 测试 Issue I6RE7J: HTTP 解码处理
        // 由于 UrlDecode 是 internal 方法，我们通过 DecodeParamMap 方法间接测试
        var paramsStr = "test=%E6%B5%8B%E8%AF%95%E4%B8%AD%E6%96%87";
        var decodedMap = HttpUtil.DecodeParamMap(paramsStr);
        Assert.NotNull(decodedMap);
        Assert.Contains("test", decodedMap);
        Assert.Equal("测试中文", decodedMap["test"]);
    }

    [Fact]
    public void IssueI7EHSETest()
    {
        // 测试 Issue I7EHSE: HTTP 工具类方法
        var isHttp = HttpUtil.IsHttp("http://example.com");
        var isHttps = HttpUtil.IsHttps("https://example.com");
        
        Assert.True(isHttp);
        Assert.True(isHttps);
    }

    [Fact]
    public void IssueI7WZEOTest()
    {
        // 测试 Issue I7WZEO: HTTP 请求方法设置
        var request = HttpRequest.Get("http://example.com")
            .SetMethod(Method.POST);
        
        Assert.NotNull(request);
        Assert.Equal(Method.POST, request.GetMethod());
    }

    [Fact]
    public void IssueI7ZRJUTest()
    {
        // 测试 Issue I7ZRJUT: HTTP 表单参数处理
        var parameters = new System.Collections.Generic.Dictionary<string, object?>
        {
            { "name", "test" },
            { "age", 18 }
        };
        var paramString = HttpUtil.ToParams(parameters);
        
        Assert.NotNull(paramString);
        Assert.Contains("name=test", paramString);
        Assert.Contains("age=18", paramString);
    }

    [Fact]
    public void IssueI8YV0KTest()
    {
        // 测试 Issue I8YV0K: HTTP 重定向处理
        var request = HttpRequest.Get("http://example.com")
            .SetFollowRedirects(true);
        
        Assert.NotNull(request);
    }

    [Fact]
    public void IssueIB7REWTest()
    {
        // 测试 Issue IB7REW: HTTP 超时设置
        var request = HttpRequest.Get("http://example.com")
            .Timeout(10000);
        
        Assert.NotNull(request);
    }

    [Fact]
    public void IssueIBQIYQTest()
    {
        // 测试 Issue IBQIYQ: HTTP 代理设置
        var request = HttpRequest.Get("http://example.com")
            .SetHttpProxy("localhost", 8888);
        
        Assert.NotNull(request);
    }

    [Fact]
    public void IssueIBRVE4Test()
    {
        // 测试 Issue IBRVE4: HTTP 认证设置
        var request = HttpRequest.Get("http://example.com")
            .BasicAuth("username", "password");
        
        Assert.NotNull(request);
    }
}
