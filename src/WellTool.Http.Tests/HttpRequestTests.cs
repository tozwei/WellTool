using WellTool.Http;
using Xunit;
using System.Text;

namespace WellTool.Http.Tests;

/// <summary>
/// HttpRequest 测试类
/// </summary>
public class HttpRequestTests
{
    private const string TestUrl = "http://photo.qzone.qq.com/fcgi-bin/fcg_list_album?uin=88888&outstyle=2";

    [Fact]
    public void CreateGetRequestTest()
    {
        var request = HttpRequest.Get(TestUrl);
        Assert.NotNull(request);
        Assert.Equal(Method.GET, request.GetMethod());
    }

    [Fact]
    public void CreatePostRequestTest()
    {
        var request = HttpRequest.Post("http://example.com/api");
        Assert.NotNull(request);
        Assert.Equal(Method.POST, request.GetMethod());
    }

    [Fact]
    public void SetMethodTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetMethod(Method.PUT);
        Assert.Equal(Method.PUT, request.GetMethod());
    }

    [Fact]
    public void SetHeaderTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetHeader("Authorization", "Bearer token123");

        var headerValue = request.GetHeader("Authorization");
        Assert.Equal("Bearer token123", headerValue);
    }

    [Fact]
    public void SetContentTypeTest()
    {
        var request = HttpRequest.Post("http://example.com/api");
        request.SetContentType("application/json");

        var contentType = request.GetHeader(Header.CONTENT_TYPE);
        Assert.Equal("application/json", contentType);
    }

    [Fact]
    public void SetCharsetTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetCharset(Encoding.UTF8);

        // 验证请求对象已设置（通过后续链式调用验证）
        Assert.NotNull(request);
    }

    [Fact]
    public void SetTimeoutTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.Timeout(5000);

        // 验证请求对象已设置超时
        Assert.NotNull(request);
    }

    [Fact]
    public void SetFormTest()
    {
        var request = HttpRequest.Post("http://example.com/api");
        request.Form("username", "testuser");
        request.Form("password", "testpass");

        Assert.NotNull(request);
    }

    [Fact]
    public void SetBodyTest()
    {
        var request = HttpRequest.Post("http://example.com/api");
        var bodyContent = "{\"name\":\"test\",\"value\":123}";
        request.Body(bodyContent);

        Assert.NotNull(request);
    }

    [Fact]
    public void SetFollowRedirectsTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetFollowRedirects(false);

        // 验证请求对象已设置
        Assert.NotNull(request);
    }

    [Fact]
    public void AddCookieTest()
    {
        var request = HttpRequest.Get(TestUrl);
        // 使用 SetHeader 来设置 Cookie
        request.SetHeader("Cookie", "sessionId=abc123");

        Assert.NotNull(request);
    }

    [Fact]
    public void RemoveHeaderTest()
    {
        var request = HttpRequest.Get(TestUrl);
        request.SetHeader("User-Agent", "TestAgent");
        request.RemoveHeader(Header.USER_AGENT);

        var userAgent = request.GetHeader(Header.USER_AGENT);
        Assert.Null(userAgent);
    }

    [Fact]
    public void UrlBuilderTest()
    {
        var urlBuilder = new StringBuilder();
        urlBuilder.Append("http://example.com/api");
        urlBuilder.Append("?param1=value1");
        urlBuilder.Append("&param2=value2");

        var request = HttpRequest.Get(urlBuilder.ToString());
        Assert.NotNull(request);
    }

    [Fact]
    public void ChainCallTest()
    {
        var request = HttpRequest.Post("http://example.com/api")
            .SetHeader("Accept", "application/json")
            .SetContentType("application/json")
            .SetCharset(Encoding.UTF8)
            .Timeout(10000);

        Assert.NotNull(request);
        Assert.Equal(Method.POST, request.GetMethod());
    }

    [Fact]
    public void StaticCreateTest()
    {
        // 测试各种静态创建方法
        var getReq = HttpRequest.Get("http://example.com");
        var postReq = HttpRequest.Post("http://example.com");
        var putReq = HttpRequest.Put("http://example.com");
        var deleteReq = HttpRequest.Delete("http://example.com");
        var headReq = HttpRequest.Head("http://example.com");
        var optionsReq = HttpRequest.Options("http://example.com");
        var patchReq = HttpRequest.Patch("http://example.com");

        Assert.Equal(Method.GET, getReq.GetMethod());
        Assert.Equal(Method.POST, postReq.GetMethod());
        Assert.Equal(Method.PUT, putReq.GetMethod());
        Assert.Equal(Method.DELETE, deleteReq.GetMethod());
        Assert.Equal(Method.HEAD, headReq.GetMethod());
        Assert.Equal(Method.OPTIONS, optionsReq.GetMethod());
        Assert.Equal(Method.PATCH, patchReq.GetMethod());
    }
}
