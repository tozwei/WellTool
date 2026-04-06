using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class NetUtilTest
{
    [Fact]
    public void GetLocalHostTest()
    {
        var localHost = NetUtil.GetLocalHost();
        Assert.NotNull(localHost);
        Assert.NotEmpty(localHost.ToString());
    }

    [Fact]
    public void GetLocalHostStrTest()
    {
        var localHostStr = NetUtil.GetLocalHostStr();
        Assert.NotNull(localHostStr);
        Assert.NotEmpty(localHostStr);
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
        Assert.True(NetUtil.IsInnerIP("172.16.0.1"));
        Assert.False(NetUtil.IsInnerIP("8.8.8.8"));
    }

    [Fact]
    public void Ip2LongTest()
    {
        var ip = "192.168.1.1";
        var ipLong = NetUtil.Ip2Long(ip);
        Assert.True(ipLong > 0);
        Assert.Equal(ip, NetUtil.Long2Ip(ipLong));
    }

    [Fact]
    public void GetMultistageReverseDnsLookupTest()
    {
        var result = NetUtil.GetMultistageReverseDnsLookup("127.0.0.1");
        Assert.NotNull(result);
    }

    [Fact]
    public void IsValidIPv4Test()
    {
        Assert.True(NetUtil.IsValidIPv4("192.168.1.1"));
        Assert.False(NetUtil.IsValidIPv4("256.256.256.256"));
        Assert.False(NetUtil.IsValidIPv4("invalid"));
    }

    [Fact]
    public void IsValidPortTest()
    {
        Assert.True(NetUtil.IsValidPort(80));
        Assert.True(NetUtil.IsValidPort(8080));
        Assert.True(NetUtil.IsValidPort(65535));
        Assert.False(NetUtil.IsValidPort(-1));
        Assert.False(NetUtil.IsValidPort(65536));
    }

    [Fact]
    public void GetUrlParametersTest()
    {
        var url = "http://example.com?a=1&b=2";
        var parameters = NetUtil.GetUrlParameters(url);
        Assert.Equal("1", parameters["a"]);
        Assert.Equal("2", parameters["b"]);
    }
}
