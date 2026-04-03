using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class HexUtilTest
{
    [Fact]
    public void HexStrTest()
    {
        var str = "我是一个字符串";
        var hex = HexUtil.EncodeHexStr(str, System.Text.Encoding.UTF8);
        var decodedStr = HexUtil.DecodeHexStr(hex);
        Assert.Equal(str, decodedStr);
    }

    [Fact]
    public void IsHexNumberTest()
    {
        Assert.True(HexUtil.IsHexNumber("0"));
        Assert.True(HexUtil.IsHexNumber("002c"));
        Assert.True(HexUtil.IsHexNumber("0x3544534F444"));
        Assert.True(HexUtil.IsHexNumber("0x0000000000000001158e460913d00000"));

        Assert.False(HexUtil.IsHexNumber("0x0000001000T00001158e460913d00000"));
        Assert.False(HexUtil.IsHexNumber("-1"));
        Assert.False(HexUtil.IsHexNumber("abc"));
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
        var hex = "48656c6c6f";
        var bytes = HexUtil.DecodeHex(hex);
        var str = System.Text.Encoding.UTF8.GetString(bytes);
        Assert.Equal("Hello", str);
    }

    [Fact]
    public void ToUnicodeHexTest()
    {
        var unicodeHex = HexUtil.ToUnicodeHex('\u2001');
        Assert.Equal("\\u2001", unicodeHex);

        unicodeHex = HexUtil.ToUnicodeHex('你');
        Assert.Equal("\\u4f60", unicodeHex);
    }
}
