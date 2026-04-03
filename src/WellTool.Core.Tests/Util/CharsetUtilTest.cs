using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class CharsetUtilTest
{
    [Fact]
    public void Utf8Test()
    {
        var charset = CharsetUtil.UTF8;
        Assert.NotNull(charset);
        Assert.Equal("UTF-8", charset.Name);
    }

    [Fact]
    public void Utf16Test()
    {
        var charset = CharsetUtil.UTF16;
        Assert.NotNull(charset);
    }

    [Fact]
    public void Gb2312Test()
    {
        var charset = CharsetUtil.GB2312;
        Assert.NotNull(charset);
    }

    [Fact]
    public void Iso88591Test()
    {
        var charset = CharsetUtil.ISO_8859_1;
        Assert.NotNull(charset);
    }

    [Fact]
    public void ConvertTest()
    {
        var str = "Hello, 世界";
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);
        var converted = CharsetUtil.Convert(bytes, CharsetUtil.UTF8, CharsetUtil.GB2312);
        Assert.NotNull(converted);
    }

    [Fact]
    public void EncodeTest()
    {
        var str = "Hello, 世界";
        var bytes = CharsetUtil.Encode(str);
        Assert.NotNull(bytes);
        Assert.NotEmpty(bytes);
    }

    [Fact]
    public void DecodeTest()
    {
        var str = "Hello, 世界";
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);
        var decoded = CharsetUtil.Decode(bytes);
        Assert.Equal(str, decoded);
    }

    [Fact]
    public void WrapTest()
    {
        var charset = CharsetUtil.Wrap("UTF-8");
        Assert.NotNull(charset);
        Assert.Equal("UTF-8", charset.Name);
    }
}
