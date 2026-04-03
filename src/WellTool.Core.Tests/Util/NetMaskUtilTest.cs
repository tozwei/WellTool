using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class NetMaskUtilTest
{
    [Fact]
    public void GetNetMaskTest()
    {
        var mask = NetMaskUtil.GetNetMask(24);
        Assert.NotNull(mask);
        Assert.Equal("255.255.255.0", mask);
    }

    [Fact]
    public void GetMaskLengthTest()
    {
        var length = NetMaskUtil.GetMaskLength("255.255.255.0");
        Assert.Equal(24, length);
    }

    [Fact]
    public void GetBroadcastTest()
    {
        var broadcast = NetMaskUtil.GetBroadcast("192.168.1.1", "255.255.255.0");
        Assert.NotNull(broadcast);
        Assert.Equal("192.168.1.255", broadcast);
    }

    [Fact]
    public void GetNetworkTest()
    {
        var network = NetMaskUtil.GetNetwork("192.168.1.100", "255.255.255.0");
        Assert.NotNull(network);
        Assert.Equal("192.168.1.0", network);
    }

    [Fact]
    public void GetHostMinMaxTest()
    {
        var network = "192.168.1.0";
        var mask = "255.255.255.0";
        var (min, max) = NetMaskUtil.GetHostMinMax(network, mask);
        Assert.NotNull(min);
        Assert.NotNull(max);
    }
}
