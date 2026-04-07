using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class CrcTest
{
    [Fact]
    public void Crc16Test()
    {
        var crc = CrcUtil.Crc16("Hello");
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Crc32Test()
    {
        var crc = CrcUtil.Crc32("Hello");
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Crc16BytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var crc = CrcUtil.Crc16(bytes);
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Crc32BytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var crc = CrcUtil.Crc32(bytes);
        Assert.True(crc >= 0);
    }
}
