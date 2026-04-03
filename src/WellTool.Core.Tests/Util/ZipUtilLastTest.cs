using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ZipUtilLastTest
{
    [Fact]
    public void GzipTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressed = ZipUtil.Gzip(data);
        Assert.NotNull(compressed);
    }

    [Fact]
    public void UnGzipTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressed = ZipUtil.Gzip(data);
        var decompressed = ZipUtil.UnGzip(compressed);
        var str = System.Text.Encoding.UTF8.GetString(decompressed);
        Assert.Equal("Hello, World!", str);
    }

    [Fact]
    public void DeflateTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressed = ZipUtil.Deflate(data);
        Assert.NotNull(compressed);
    }

    [Fact]
    public void InflateTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressed = ZipUtil.Deflate(data);
        var decompressed = ZipUtil.Inflate(compressed);
        var str = System.Text.Encoding.UTF8.GetString(decompressed);
        Assert.Equal("Hello, World!", str);
    }
}
