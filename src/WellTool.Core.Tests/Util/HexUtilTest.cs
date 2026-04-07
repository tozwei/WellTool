using WellTool.Core.Util;
using System.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class HexUtilTest
{
    [Fact]
    public void HexStrTest()
    {
        var str = "我是一个字符串";
        var hex = HexUtil.Encode(str, Encoding.UTF8);
        var decodedStr = HexUtil.DecodeToString(hex, Encoding.UTF8);
        Assert.Equal(str, decodedStr);
    }

    [Fact]
    public void EncodeTest()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello");
        var hex = HexUtil.Encode(bytes);
        Assert.Equal("48656c6c6f", hex);
    }

    [Fact]
    public void DecodeTest()
    {
        var hex = "48656c6c6f";
        var bytes = HexUtil.Decode(hex);
        var str = Encoding.UTF8.GetString(bytes);
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

    [Fact]
    public void DecodeHexTest()
    {
        var hex = "48656c6c6f";
        var bytes = HexUtil.DecodeHex(hex);
        var str = Encoding.UTF8.GetString(bytes);
        Assert.Equal("Hello", str);
    }
}
