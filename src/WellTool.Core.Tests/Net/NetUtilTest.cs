using Xunit;
using WellTool.Core.Net;

namespace WellTool.Core.Tests.Net;

/// <summary>
/// NetUtil 测试
/// </summary>
public class NetUtilTest
{
    [Fact]
    public void IsValidIpTest()
    {
        Assert.True(NetUtil.IsValidIp("192.168.1.1"));
        Assert.False(NetUtil.IsValidIp("999.999.999.999"));
    }

    [Fact]
    public void LocalIpTest()
    {
        var localIp = NetUtil.LocalIp;
        Assert.NotNull(localIp);
    }

    [Fact]
    public void GetLocalHostNameTest()
    {
        var hostName = NetUtil.GetLocalHostName();
        Assert.NotNull(hostName);
    }

    [Fact]
    public void Ipv4ToLongTest()
    {
        var ipLong = NetUtil.Ipv4ToLong("192.168.1.1");
        Assert.True(ipLong > 0);
    }

    [Fact]
    public void LongToIpv4Test()
    {
        var ipLong = NetUtil.Ipv4ToLong("192.168.1.1");
        var ip = NetUtil.LongToIpv4(ipLong);
        Assert.Equal("192.168.1.1", ip);
    }
}
