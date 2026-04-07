using WellTool.Core.Util;
using System.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CharsetUtilLastTest
{
    [Fact]
    public void Utf8Test()
    {
        var charset = CharsetUtil.UTF_8;
        Assert.NotNull(charset);
    }

    [Fact]
    public void GbkTest()
    {
        var charset = CharsetUtil.GBK;
        Assert.NotNull(charset);
    }

    [Fact]
    public void Iso88591Test()
    {
        var charset = CharsetUtil.ISO_8859_1;
        Assert.NotNull(charset);
    }

    [Fact]
    public void AsciiTest()
    {
        var charset = CharsetUtil.ASCII;
        Assert.NotNull(charset);
    }

    [Fact]
    public void GetEncodingTest()
    {
        var charset = CharsetUtil.GetEncoding("UTF-8");
        Assert.NotNull(charset);
    }

    [Fact]
    public void EncodeDecodeTest()
    {
        var str = "Hello";
        var bytes = CharsetUtil.UTF_8.GetBytes(str);
        var decoded = CharsetUtil.UTF_8.GetString(bytes);
        Assert.Equal(str, decoded);
    }
}
