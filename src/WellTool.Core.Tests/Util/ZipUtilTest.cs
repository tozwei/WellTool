using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ZipUtilTest
{
    [Fact]
    public void CompressTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressed = ZipUtil.Compress(data);
        Assert.NotNull(compressed);
        Assert.NotEmpty(compressed);
    }

    [Fact]
    public void DecompressTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressed = ZipUtil.Compress(data);
        var decompressed = ZipUtil.Decompress(compressed);
        var str = System.Text.Encoding.UTF8.GetString(decompressed);
        Assert.Equal("Hello, World!", str);
    }

    [Fact]
    public void CompressStringTest()
    {
        var text = "Hello, World!";
        var compressed = ZipUtil.CompressString(text);
        Assert.NotNull(compressed);
        Assert.NotEmpty(compressed);
    }

    [Fact]
    public void DecompressStringTest()
    {
        var text = "Hello, World!";
        var compressed = ZipUtil.CompressString(text);
        var decompressed = ZipUtil.DecompressString(compressed);
        Assert.Equal(text, decompressed);
    }
}
