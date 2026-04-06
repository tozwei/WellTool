using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class NetUtilLastTest
{
    [Fact]
    public void GetLocalIPTest()
    {
        var localIP = NetUtil.GetLocalIP();
        Assert.NotNull(localIP);
    }

    [Fact]
    public void GetLocalIPAddressTest()
    {
        var localIPAddress = NetUtil.GetLocalIPAddress();
        Assert.NotNull(localIPAddress);
    }

    [Fact]
    public void IsValidIPTest()
    {
        Assert.True(NetUtil.IsValidIP("192.168.1.1"));
        Assert.False(NetUtil.IsValidIP("256.256.256.256"));
    }

    [Fact]
    public void IsValidPortTest()
    {
        Assert.True(NetUtil.IsValidPort(80));
        Assert.False(NetUtil.IsValidPort(-1));
    }

    [Fact]
    public void IsNetworkAvailableTest()
    {
        var isAvailable = NetUtil.IsNetworkAvailable();
        // 网络可用性测试可能因环境而异，这里不做断言
    }

    [Fact]
    public void GetNetworkTypeTest()
    {
        var networkType = NetUtil.GetNetworkType();
        Assert.NotNull(networkType);
    }
}
