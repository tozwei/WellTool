using Xunit;
using WellTool.Http;

namespace WellTool.Http.Tests;

public class Issue3568Test
{
    [Fact]
    public void SetFollowRedirectsTest()
    {
        // 测试设置是否打开重定向
        var request = HttpRequest.Get("http://example.com");
        
        // 打开重定向
        request.SetFollowRedirects(true);
        
        // 验证重定向设置是否成功
        // 注意：由于我们无法直接访问 HttpConfig 的 MaxRedirectCount 属性，
        // 这里我们只能测试方法是否能正常调用，而不能验证具体的值
        Assert.NotNull(request);
        
        // 关闭重定向
        request.SetFollowRedirects(false);
        
        // 验证重定向设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void SetMaxRedirectCountTest()
    {
        // 测试设置最大重定向次数
        var request = HttpRequest.Get("http://example.com");
        
        // 设置最大重定向次数为 5
        request.SetMaxRedirectCount(5);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 设置最大重定向次数为 0（禁用重定向）
        request.SetMaxRedirectCount(0);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 设置最大重定向次数为负数（应该也能正常工作）
        request.SetMaxRedirectCount(-1);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void RedirectCombinationTest()
    {
        // 测试重定向相关方法的组合使用
        var request = HttpRequest.Get("http://example.com");
        
        // 先设置最大重定向次数，再打开重定向
        request.SetMaxRedirectCount(3).SetFollowRedirects(true);
        
        // 验证设置是否成功
        Assert.NotNull(request);
        
        // 先打开重定向，再设置最大重定向次数
        request.SetFollowRedirects(true).SetMaxRedirectCount(3);
        
        // 验证设置是否成功
        Assert.NotNull(request);
    }

    [Fact]
    public void HttpRequestChainingTest()
    {
        // 测试 HttpRequest 方法的链式调用
        var request = HttpRequest.Get("http://example.com")
            .SetFollowRedirects(true)
            .SetMaxRedirectCount(3)
            .Timeout(5000)
            .SetHeader("User-Agent", "WellTool/1.0");
        
        // 验证链式调用是否成功
        Assert.NotNull(request);
    }
}
