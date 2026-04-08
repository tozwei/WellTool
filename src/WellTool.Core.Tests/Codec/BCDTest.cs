using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// BCD 测试
/// </summary>
public class BCDTest
{
    [Fact]
    public void EncodeTest()
    {
        var bcd = BCD.Encode("1234");
        Assert.NotNull(bcd);
    }

    [Fact]
    public void DecodeTest()
    {
        var bcd = BCD.Encode("1234");
        var decoded = BCD.Decode(bcd);
        Assert.Equal("1234", decoded);
    }

    [Fact]
    public void EncodeBytesTest()
    {
        var bcd = BCD.EncodeBytes(new byte[] { 0x12, 0x34 });
        Assert.NotNull(bcd);
    }
}
