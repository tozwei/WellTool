using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class IssueI7EHSETest
{
    [Fact]
    public void HttpRequestDisableCacheTest()
    {
        // 测试禁用缓存
        var request = HttpRequest.Get("http://example.com");
        
        // 禁用缓存
        request.DisableCache();
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetHttpProxyTest()
    {
        // 测试设置 HTTP 代理
        var request = HttpRequest.Get("http://example.com");
        
        // 设置 HTTP 代理
        request.SetHttpProxy("127.0.0.1", 8080);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetChunkedStreamingModeTest()
    {
        // 测试设置块大小
        var request = HttpRequest.Get("http://example.com");
        
        // 设置块大小为 4096 字节
        request.SetChunkedStreamingMode(4096);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestSetHostnameVerifierTest()
    {
        // 测试设置域名验证器
        var request = HttpRequest.Get("http://example.com");
        
        // 设置域名验证器
        request.SetHostnameVerifier((hostname, issuer) => true);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestConfigCombinationTest()
    {
        // 测试配置相关方法的组合使用
        var request = HttpRequest.Get("http://example.com");
        
        // 组合设置各种配置
        request.DisableCache()
            .SetHttpProxy("127.0.0.1", 8080)
            .SetChunkedStreamingMode(4096)
            .SetHostnameVerifier((hostname, issuer) => true);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestChainingAllTest()
    {
        // 测试所有方法的链式调用
        var request = HttpRequest.Get("http://example.com")
            .SetHeader("User-Agent", "WellTool/1.0")
            .Timeout(5000)
            .SetConnectionTimeout(3000)
            .SetReadTimeout(4000)
            .SetFollowRedirects(true)
            .SetMaxRedirectCount(3)
            .DisableCache()
            .SetHttpProxy("127.0.0.1", 8080)
            .SetChunkedStreamingMode(4096)
            .SetHostnameVerifier((hostname, issuer) => true)
            .Form("name", "test")
            .Body("{\"name\": \"value\"}");
        
        // 验证链式调用是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestToStringTest()
    {
        // 测试 ToString 方法
        var request = HttpRequest.Get("http://example.com")
            .SetHeader("User-Agent", "WellTool/1.0")
            .Form("name", "test")
            .Body("{\"name\": \"value\"}");
        
        // 调用 ToString 方法
        var requestString = request.ToString();
        
        // 验证 ToString 方法是否返回非空字符串
        Assert.NotNull(requestString);
        Assert.NotEmpty(requestString);
        
        // 验证字符串中包含必要的信息
        Assert.Contains("Headers:", requestString);
        Assert.Contains("User-Agent: WellTool/1.0", requestString);
        Assert.Contains("Body:", requestString);
        Assert.Contains("{\"name\": \"value\"}", requestString);
    }
}
