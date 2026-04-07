using WellTool.Core.Util;
using System.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class HexUtilLastTest
{
    [Fact]
    public void EncodeTest()
    {
        var hex = HexUtil.Encode("Hello");
        Assert.NotNull(hex);
    }

    [Fact]
    public void DecodeToStringTest()
    {
        var decoded = HexUtil.DecodeToString("48656c6c6f");
        Assert.Equal("Hello", decoded);
    }

    [Fact]
    public void EncodeBytesTest()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello");
        var hex = HexUtil.Encode(bytes);
        Assert.Equal("48656c6c6f", hex);
    }

    [Fact]
    public void DecodeHexTest()
    {
        var bytes = HexUtil.DecodeHex("48656c6c6f");
        Assert.Equal("Hello", Encoding.UTF8.GetString(bytes));
    }

    [Fact]
    public void ToUnicodeHexTest()
    {
        var unicodeHex = HexUtil.ToUnicodeHex('你');
        Assert.Equal("\\u4f60", unicodeHex);
    }

    [Fact]
    public void DecodeTest()
    {
        var bytes = HexUtil.Decode("48656c6c6f");
        Assert.Equal("Hello", Encoding.UTF8.GetString(bytes));
    }
}
