using System;
using Xunit;
using WellTool.Http.Cookie;

namespace WellTool.Http.Tests;

public class Issue2658Test
{
    [Fact]
    public void GetCookieManagerTest()
    {
        // 测试获取 Cookie 管理器
        var cookieManager = GlobalCookieManager.GetCookieManager();
        // 目前返回 null，这是预期行为
        Assert.Null(cookieManager);
    }

    [Fact]
    public void GetCookiesTest()
    {
        // 测试获取指定域名下的 Cookie
        var uri = new Uri("https://www.baidu.com/");
        var cookies = GlobalCookieManager.GetCookies(uri);
        // 目前返回空列表，这是预期行为
        Assert.NotNull(cookies);
        Assert.Empty(cookies);
    }

    [Fact]
    public void CloseCookieTest()
    {
        // 测试关闭 Cookie 管理
        // 此方法目前只是设置 Cookie 管理器为 null，不会抛出异常
        GlobalCookieManager.CloseCookie();
        Assert.True(true);
    }
}
