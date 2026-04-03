using WellTool.Core.Net;
using Xunit;

namespace WellTool.Core.Tests;

public class Ipv4UtilLastTest
{
    [Fact]
    public void Ipv4ToLongTest()
    {
        var ip = "192.168.1.1";
        var result = Ipv4Util.Ipv4ToLong(ip);
        Assert.True(result > 0);
    }

    [Fact]
    public void LongToIpv4Test()
    {
        var ipLong = 3232235777L;
        var result = Ipv4Util.LongToIpv4(ipLong);
        Assert.Equal("192.168.1.1", result);
    }
}
