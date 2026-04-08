using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueI5TPSYTest
{
    [Fact]
    public void HttpRequestCreateTest()
    {
        // 测试创建不同类型的 HTTP 请求
        var getRequest = HttpRequest.Get("http://example.com");
        Assert.NotNull(getRequest);
        
        var postRequest = HttpRequest.Post("http://example.com");
        Assert.NotNull(postRequest);
        
        var putRequest = HttpRequest.Put("http://example.com");
        Assert.NotNull(putRequest);
        
        var deleteRequest = HttpRequest.Delete("http://example.com");
        Assert.NotNull(deleteRequest);
        
        var headRequest = HttpRequest.Head("http://example.com");
        Assert.NotNull(headRequest);
        
        var optionsRequest = HttpRequest.Options("http://example.com");
        Assert.NotNull(optionsRequest);
        
        var patchRequest = HttpRequest.Patch("http://example.com");
        Assert.NotNull(patchRequest);
    }

    [Fact]
    public void HttpRequestOfTest()
    {
        // 测试使用 Of 方法创建 HTTP 请求
        var request = HttpRequest.Of("http://example.com");
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetUrlTest()
    {
        // 测试设置 URL
        var request = HttpRequest.Get("http://example.com");
        
        // 获取原始 URL
        var originalUrl = request.GetUrl();
        Assert.Equal("http://example.com", originalUrl);
        
        // 设置新 URL
        request.SetUrl("http://example.org");
        
        // 验证 URL 是否设置成功
        var newUrl = request.GetUrl();
        Assert.Equal("http://example.org", newUrl);
    }

    [Fact]
    public void HttpRequestSetMethodTest()
    {
        // 测试设置 HTTP 方法
        var request = HttpRequest.Get("http://example.com");
        
        // 获取原始方法
        var originalMethod = request.GetMethod();
        Assert.Equal(Method.GET, originalMethod);
        
        // 设置新方法
        request.SetMethod(Method.POST);
        
        // 验证方法是否设置成功
        var newMethod = request.GetMethod();
        Assert.Equal(Method.POST, newMethod);
    }

    [Fact]
    public void HttpRequestFormTest()
    {
        // 测试设置表单数据
        var request = HttpRequest.Post("http://example.com");
        
        // 设置单个表单参数
        request.Form("name", "test");
        
        // 验证表单数据是否设置成功
        var form = request.Form();
        Assert.NotNull(form);
        Assert.Contains("name", form.Keys);
        Assert.Equal("test", form["name"]);
    }

    [Fact]
    public void HttpRequestBodyTest()
    {
        // 测试设置请求体
        var request = HttpRequest.Post("http://example.com");
        
        // 设置请求体
        var body = "{\"name\": \"value\"}";
        request.Body(body);
        
        // 验证请求体是否设置成功
        var bodyBytes = request.BodyBytes();
        Assert.NotNull(bodyBytes);
        Assert.NotEmpty(bodyBytes);
    }

    [Fact]
    public void HttpRequestBasicAuthTest()
    {
        // 测试基本认证
        var request = HttpRequest.Get("http://example.com");
        
        // 设置基本认证
        request.BasicAuth("username", "password");
        
        // 验证认证是否设置成功
        var authHeader = request.GetHeader(Header.AUTHORIZATION);
        Assert.NotNull(authHeader);
        Assert.StartsWith("Basic ", authHeader);
    }

    [Fact]
    public void HttpRequestBearerAuthTest()
    {
        // 测试 Bearer 令牌认证
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 Bearer 令牌认证
        request.BearerAuth("token123");
        
        // 验证认证是否设置成功
        var authHeader = request.GetHeader(Header.AUTHORIZATION);
        Assert.NotNull(authHeader);
        Assert.Equal("Bearer token123", authHeader);
    }
}
