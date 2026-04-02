using System;
using WellTool.Http;
using WellTool.Http.UserAgent;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// UserAgentUtil UserAgent解析工具测试
/// </summary>
public class UserAgentUtilTest
{
    #region Basic tests

    [Fact]
    public void ParseDesktopTest()
    {
        var uaStr = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.163 Safari/535.1";

        var ua = UserAgentUtil.Parse(uaStr);

        Assert.Equal("Chrome", ua.Browser?.Name);
        Assert.Equal("14.0.835.163", ua.Version);
        Assert.Contains("Windows", ua.OS?.Name);
        Assert.Equal("6.1", ua.OsVersion);
        Assert.False(ua.Mobile);
    }

    [Fact]
    public void ParseMobileTest()
    {
        var uaStr = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8J2 Safari/6533.18.5";

        var ua = UserAgentUtil.Parse(uaStr);

        Assert.Equal("Safari", ua.Browser?.Name);
        Assert.Contains("Mac OS X", ua.OS?.Name);
        Assert.True(ua.Mobile);
    }

    [Fact]
    public void ParseNullTest()
    {
        var ua = UserAgentUtil.Parse(null);

        Assert.NotNull(ua);
        Assert.Equal("Unknown", ua.Browser?.Name);
        Assert.Equal("Unknown", ua.OS?.Name);
        Assert.False(ua.Mobile);
    }

    [Fact]
    public void ParseEmptyTest()
    {
        var ua = UserAgentUtil.Parse("");

        Assert.NotNull(ua);
        Assert.Equal("Unknown", ua.Browser?.Name);
    }

    [Fact]
    public void ParseWhitespaceTest()
    {
        var ua = UserAgentUtil.Parse("   ");

        Assert.NotNull(ua);
        Assert.Equal("Unknown", ua.Browser?.Name);
    }

    #endregion
}
