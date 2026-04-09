using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class SimpleTest
{
    [Fact]
    public void TestHttpRequestCreation()
    {
        // 测试 HTTP GET 请求创建
        var getRequest = HttpRequest.Get("http://example.com");
        Assert.NotNull(getRequest);

        // 测试 HTTP POST 请求创建
        var postRequest = HttpRequest.Post("http://example.com");
        Assert.NotNull(postRequest);

        // 测试 HTTP PUT 请求创建
        var putRequest = HttpRequest.Put("http://example.com");
        Assert.NotNull(putRequest);

        // 测试 HTTP DELETE 请求创建
        var deleteRequest = HttpRequest.Delete("http://example.com");
        Assert.NotNull(deleteRequest);
    }

    [Fact]
    public void TestHttpRequestChaining()
    {
        // 测试 HTTP 请求的链式调用
        var request = HttpRequest.Get("http://example.com")
            .SetHeader("Content-Type", "application/json")
            .SetHeader("User-Agent", "WellTool/1.0")
            .Timeout(5000);

        Assert.NotNull(request);
    }
}
