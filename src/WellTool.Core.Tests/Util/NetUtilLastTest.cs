using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class NetUtilLastTest
{
    [Fact]
    public void GetLocalHostTest()
    {
        var localHost = NetUtil.GetLocalHost();
        Assert.NotNull(localHost);
    }

    [Fact]
    public void GetLocalHostStrTest()
    {
        var localHostStr = NetUtil.GetLocalHostStr();
        Assert.NotNull(localHostStr);
    }

    [Fact]
    public void IsInRangeTest()
    {
        Assert.True(NetUtil.IsInRange("192.168.1.100", "192.168.1.0/24"));
        Assert.False(NetUtil.IsInRange("192.168.2.100", "192.168.1.0/24"));
    }

    [Fact]
    public void IsInnerIPTest()
    {
        Assert.True(NetUtil.IsInnerIP("192.168.1.100"));
        Assert.True(NetUtil.IsInnerIP("10.0.0.1"));
        Assert.False(NetUtil.IsInnerIP("8.8.8.8"));
    }

    [Fact]
    public void Ip2LongTest()
    {
        var ip = "192.168.1.1";
        var ipLong = NetUtil.Ip2Long(ip);
        Assert.True(ipLong > 0);
    }

    [Fact]
    public void Long2IpTest()
    {
        var ip = "192.168.1.1";
        var ipLong = NetUtil.Ip2Long(ip);
        Assert.Equal(ip, NetUtil.Long2Ip(ipLong));
    }

    [Fact]
    public void IsValidIPv4Test()
    {
        Assert.True(NetUtil.IsValidIPv4("192.168.1.1"));
        Assert.False(NetUtil.IsValidIPv4("256.256.256.256"));
    }

    [Fact]
    public void IsValidPortTest()
    {
        Assert.True(NetUtil.IsValidPort(80));
        Assert.False(NetUtil.IsValidPort(-1));
    }
}
