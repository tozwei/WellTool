using WellTool.Http.UserAgent;
using Xunit;

namespace WellTool.Http.Tests.UserAgent;

/// <summary>
/// UserAgent 工具测试类
/// </summary>
public class UserAgentUtilTest
{
    [Fact]
    public void ParseTest()
    {
        // 测试基本的 User-Agent 解析功能
        var uaStr = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.163 Safari/535.1";

        var ua = UserAgentUtil.Parse(uaStr);
        Assert.NotNull(ua);
        Assert.NotNull(ua.Browser);
        Assert.NotNull(ua.OS);
        Assert.NotNull(ua.Engine);
        Assert.NotNull(ua.Platform);
    }

    [Fact]
    public void ParseEmptyTest()
    {
        // 测试空字符串的情况
        var ua = UserAgentUtil.Parse(string.Empty);
        Assert.NotNull(ua);
        Assert.NotNull(ua.Browser);
        Assert.NotNull(ua.OS);
        Assert.NotNull(ua.Engine);
        Assert.NotNull(ua.Platform);
    }

    [Fact]
    public void ParseMobileTest()
    {
        // 测试移动设备的识别
        var uaStr = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8J2 Safari/6533.18.5";

        var ua = UserAgentUtil.Parse(uaStr);
        Assert.NotNull(ua);
        Assert.True(ua.Mobile);
    }

    [Fact]
    public void ParseDesktopTest()
    {
        // 测试桌面设备的识别
        var uaStr = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.102 Safari/537.36";

        var ua = UserAgentUtil.Parse(uaStr);
        Assert.NotNull(ua);
        Assert.False(ua.Mobile);
    }
}

