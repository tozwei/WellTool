using WellTool.Core.Net;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class NetMaskUtilTest
{
    [Fact]
    public void GetNetMaskTest()
    {
        var mask = Ipv4Util.GetMaskByMaskBit(24);
        Assert.NotNull(mask);
        Assert.Equal("255.255.255.0", mask);
    }

    [Fact]
    public void GetMaskLengthTest()
    {
        var length = Ipv4Util.GetMaskBitByMask("255.255.255.0");
        Assert.Equal(24, length);
    }

    [Fact]
    public void GetBroadcastTest()
    {
        var broadcast = Ipv4Util.GetBroadcastAddress("192.168.1.1", "255.255.255.0");
        Assert.NotNull(broadcast);
        Assert.Equal("192.168.1.255", broadcast);
    }

    [Fact]
    public void GetNetworkTest()
    {
        var network = Ipv4Util.GetNetworkAddress("192.168.1.100", "255.255.255.0");
        Assert.NotNull(network);
        Assert.Equal("192.168.1.0", network);
    }

    [Fact]
    public void GetHostMinMaxTest()
    {
        var network = "192.168.1.0";
        var mask = "255.255.255.0";
        var min = network;
        var max = Ipv4Util.GetBroadcastAddress(network, mask);
        Assert.NotNull(min);
        Assert.NotNull(max);
    }
}
