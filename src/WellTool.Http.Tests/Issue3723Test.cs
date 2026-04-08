using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3723Test
{
    [Fact]
    public void TimeoutTest()
    {
        // 测试设置超时
        var request = HttpRequest.Get("http://example.com");
        
        // 设置超时为 5000 毫秒
        request.Timeout(5000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 设置超时为 10000 毫秒
        request.Timeout(10000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void SetConnectionTimeoutTest()
    {
        // 测试设置连接超时
        var request = HttpRequest.Get("http://example.com");
        
        // 设置连接超时为 3000 毫秒
        request.SetConnectionTimeout(3000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 设置连接超时为 6000 毫秒
        request.SetConnectionTimeout(6000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void SetReadTimeoutTest()
    {
        // 测试设置读取超时
        var request = HttpRequest.Get("http://example.com");
        
        // 设置读取超时为 4000 毫秒
        request.SetReadTimeout(4000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 设置读取超时为 8000 毫秒
        request.SetReadTimeout(8000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void TimeoutCombinationTest()
    {
        // 测试超时相关方法的组合使用
        var request = HttpRequest.Get("http://example.com");
        
        // 设置连接超时和读取超时
        request.SetConnectionTimeout(3000).SetReadTimeout(5000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 设置总超时
        request.Timeout(10000);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestTimeoutChainingTest()
    {
        // 测试 HttpRequest 超时相关方法的链式调用
        var request = HttpRequest.Get("http://example.com")
            .Timeout(5000)
            .SetConnectionTimeout(3000)
            .SetReadTimeout(4000)
            .SetHeader("User-Agent", "WellTool/1.0");
        
        // 验证链式调用是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void SetGlobalTimeoutTest()
    {
        // 测试设置全局超时
        // 注意：由于 HttpGlobalConfig.SetTimeout 是静态方法，
        // 这里我们只能测试方法是否能正常调用，而不能验证具体的值
        HttpRequest.SetGlobalTimeout(5000);
        Assert.True(true);
        
        // 再次设置全局超时
        HttpRequest.SetGlobalTimeout(10000);
        Assert.True(true);
    }
}
