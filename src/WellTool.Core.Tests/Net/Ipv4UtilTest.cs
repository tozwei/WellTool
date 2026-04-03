using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class Ipv4UtilTest
{
    [Fact]
    public void GetMaskBitByMaskTest()
    {
        var maskBitByMask = Ipv4Util.GetMaskBitByMask("255.255.255.0");
        Assert.Equal(24, maskBitByMask);
    }

    [Fact]
    public void GetMaskByMaskBitTest()
    {
        var mask = Ipv4Util.GetMaskByMaskBit(24);
        Assert.Equal("255.255.255.0", mask);
    }

    [Fact]
    public void LongToIpTest()
    {
        var ip = "192.168.1.255";
        var ipLong = Ipv4Util.Ipv4ToLong(ip);
        var ipv4 = Ipv4Util.LongToIpv4(ipLong);
        Assert.Equal(ip, ipv4);
    }

    [Fact]
    public void GetEndIpStrTest()
    {
        var ip = "192.168.1.1";
        var maskBitByMask = Ipv4Util.GetMaskBitByMask("255.255.255.0");
        var endIpStr = Ipv4Util.GetEndIpStr(ip, maskBitByMask);
        Assert.Equal("192.168.1.255", endIpStr);
    }

    [Fact]
    public void ListTest()
    {
        var maskBit = Ipv4Util.GetMaskBitByMask("255.255.255.0");
        var list = Ipv4Util.Ipv4RangeList("192.168.100.2", maskBit);
        Assert.Equal(254, list.Count);
    }

    [Fact]
    public void IsMaskValidTest()
    {
        Assert.True(Ipv4Util.IsMaskValid("255.255.255.0"));
        Assert.False(Ipv4Util.IsMaskValid("255.255.0.255"));
    }

    [Fact]
    public void MatchesTest()
    {
        Assert.True(Ipv4Util.Matches("127.*.*.1", "127.0.0.1"));
        Assert.False(Ipv4Util.Matches("192.168.*.1", "127.0.0.1"));
    }

    [Fact]
    public void Ipv4ToLongTest()
    {
        var l = Ipv4Util.Ipv4ToLong("127.0.0.1");
        Assert.Equal(2130706433L, l);
        l = Ipv4Util.Ipv4ToLong("0.0.0.0");
        Assert.Equal(0L, l);
        l = Ipv4Util.Ipv4ToLong("255.255.255.255");
        Assert.Equal(4294967295L, l);
    }
}
