using WellTool.Core.Codec;
using Xunit;

namespace WellTool.Core.Tests;

public class CrcTest
{
    [Fact]
    public void Crc8Test()
    {
        var crc = CrcUtil.Crc8("Hello");
        Assert.True(crc >= 0);
    }

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
    public void Crc64Test()
    {
        var crc = CrcUtil.Crc64("Hello");
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Md5Test()
    {
        var md5 = CrcUtil.Md5("Hello");
        Assert.NotNull(md5);
    }

    [Fact]
    public void Sha1Test()
    {
        var sha1 = CrcUtil.Sha1("Hello");
        Assert.NotNull(sha1);
    }
}
