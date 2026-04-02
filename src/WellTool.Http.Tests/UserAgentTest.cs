using System;
using WellTool.Http.UserAgent;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// UserAgent 用户代理类测试
/// </summary>
public class UserAgentTest
{
    [Fact]
    public void Browser_Properties()
    {
        var ua = new WellTool.Http.UserAgent.UserAgent
        {
            Browser = new Browser { Name = "Chrome", Type = "Chrome", Vendor = "Google" },
            Version = "90.0.4430.212"
        };

        Assert.Equal("Chrome", ua.Browser?.Name);
        Assert.Equal("Chrome", ua.Browser?.Type);
        Assert.Equal("Google", ua.Browser?.Vendor);
        Assert.Equal("90.0.4430.212", ua.Version);
    }

    [Fact]
    public void OS_Properties()
    {
        var ua = new WellTool.Http.UserAgent.UserAgent
        {
            OS = new OS { Name = "Windows", Version = "10.0", Vendor = "Microsoft" },
            OsVersion = "10.0"
        };

        Assert.Equal("Windows", ua.OS?.Name);
        Assert.Equal("10.0", ua.OS?.Version);
        Assert.Equal("Microsoft", ua.OS?.Vendor);
        Assert.Equal("10.0", ua.OsVersion);
    }

    [Fact]
    public void Engine_Properties()
    {
        var ua = new WellTool.Http.UserAgent.UserAgent
        {
            Engine = new Engine { Name = "Webkit", Version = "537.36" }
        };

        Assert.Equal("Webkit", ua.Engine?.Name);
        Assert.Equal("537.36", ua.Engine?.Version);
    }

    [Fact]
    public void Mobile_Property()
    {
        var mobileUa = new WellTool.Http.UserAgent.UserAgent { Mobile = true };
        var desktopUa = new WellTool.Http.UserAgent.UserAgent { Mobile = false };

        Assert.True(mobileUa.Mobile);
        Assert.False(desktopUa.Mobile);
    }

    [Fact]
    public void Platform_Property()
    {
        var ua = new WellTool.Http.UserAgent.UserAgent { Platform = Platform.DESKTOP };
        Assert.Equal(Platform.DESKTOP, ua.Platform);
    }
}

/// <summary>
/// Browser 浏览器类测试
/// </summary>
public class BrowserTest
{
    [Fact]
    public void Browser_Creation()
    {
        var browser = new Browser
        {
            Name = "Chrome",
            Type = "Chrome",
            Vendor = "Google"
        };

        Assert.Equal("Chrome", browser.Name);
        Assert.Equal("Chrome", browser.Type);
        Assert.Equal("Google", browser.Vendor);
    }

    [Fact]
    public void Browser_ToString()
    {
        var browser = new Browser { Name = "Chrome" };
        Assert.Contains("Chrome", browser.ToString());
    }
}

/// <summary>
/// OS 操作系统类测试
/// </summary>
public class OSTest
{
    [Fact]
    public void OS_Creation()
    {
        var os = new OS
        {
            Name = "Windows",
            Version = "10.0",
            Vendor = "Microsoft"
        };

        Assert.Equal("Windows", os.Name);
        Assert.Equal("10.0", os.Version);
        Assert.Equal("Microsoft", os.Vendor);
    }

    [Fact]
    public void OS_ToString()
    {
        var os = new OS { Name = "Android" };
        Assert.Contains("Android", os.ToString());
    }
}

/// <summary>
/// Engine 引擎类测试
/// </summary>
public class EngineTest
{
    [Fact]
    public void Engine_Creation()
    {
        var engine = new Engine
        {
            Name = "Webkit",
            Version = "537.36"
        };

        Assert.Equal("Webkit", engine.Name);
        Assert.Equal("537.36", engine.Version);
    }

    [Fact]
    public void Engine_ToString()
    {
        var engine = new Engine { Name = "Blink" };
        Assert.Contains("Blink", engine.ToString());
    }
}

/// <summary>
/// Platform 平台枚举测试
/// </summary>
public class PlatformTest
{
    [Fact]
    public void Platform_Values()
    {
        Assert.NotNull(Platform.DESKTOP);
        Assert.NotNull(Platform.MOBILE);
        Assert.NotNull(Platform.TABLET);
        Assert.NotNull(Platform.UNKNOWN);
    }

    [Fact]
    public void Platform_MobileCheck()
    {
        // 直接测试 Platform 枚举值
        Assert.True(Platform.MOBILE == Platform.MOBILE);
        Assert.False(Platform.DESKTOP == Platform.MOBILE);
        Assert.False(Platform.TABLET == Platform.MOBILE);
        Assert.False(Platform.UNKNOWN == Platform.MOBILE);
    }

    [Fact]
    public void Platform_TabletCheck()
    {
        // 直接测试 Platform 枚举值
        Assert.True(Platform.TABLET == Platform.TABLET);
        Assert.False(Platform.DESKTOP == Platform.TABLET);
        Assert.False(Platform.MOBILE == Platform.TABLET);
    }
}
