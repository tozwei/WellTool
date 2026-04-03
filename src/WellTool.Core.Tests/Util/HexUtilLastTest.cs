using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class HexUtilLastTest
{
    [Fact]
    public void EncodeHexStrTest()
    {
        var hex = HexUtil.EncodeHexStr("Hello");
        Assert.NotNull(hex);
    }

    [Fact]
    public void DecodeHexStrTest()
    {
        var decoded = HexUtil.DecodeHexStr("48656c6c6f");
        Assert.Equal("Hello", decoded);
    }

    [Fact]
    public void IsHexNumberTest()
    {
        Assert.True(HexUtil.IsHexNumber("0x123"));
        Assert.False(HexUtil.IsHexNumber("-1"));
    }

    [Fact]
    public void EncodeHexTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var hex = HexUtil.EncodeHex(bytes);
        Assert.Equal("48656c6c6f", hex);
    }

    [Fact]
    public void DecodeHexTest()
    {
        var bytes = HexUtil.DecodeHex("48656c6c6f");
        Assert.Equal("Hello", System.Text.Encoding.UTF8.GetString(bytes));
    }

    [Fact]
    public void ToUnicodeHexTest()
    {
        var unicodeHex = HexUtil.ToUnicodeHex('你');
        Assert.Equal("\\u4f60", unicodeHex);
    }
}
