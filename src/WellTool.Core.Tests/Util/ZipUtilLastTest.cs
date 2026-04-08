using Xunit;
using System.IO;
using System.IO.Compression;

namespace WellTool.Core.Tests;

public class ZipUtilLastTest
{
    [Fact]
    public void GzipTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        using var compressedStream = new MemoryStream();
        using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress))
        {
            gzipStream.Write(data, 0, data.Length);
        }
        var compressed = compressedStream.ToArray();
        Assert.NotNull(compressed);
    }

    [Fact]
    public void UnGzipTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressedStream = new MemoryStream();
        using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress, true))
        {
            gzipStream.Write(data, 0, data.Length);
        }
        compressedStream.Position = 0;
        using var decompressedStream = new MemoryStream();
        using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress, true))
        {
            gzipStream.CopyTo(decompressedStream);
        }
        var decompressed = decompressedStream.ToArray();
        var str = System.Text.Encoding.UTF8.GetString(decompressed);
        Assert.Equal("Hello, World!", str);
    }

    [Fact]
    public void DeflateTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressedStream = new MemoryStream();
        using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress, true))
        {
            deflateStream.Write(data, 0, data.Length);
        }
        var compressed = compressedStream.ToArray();
        Assert.NotNull(compressed);
    }

    [Fact]
    public void InflateTest()
    {
        var data = System.Text.Encoding.UTF8.GetBytes("Hello, World!");
        var compressedStream = new MemoryStream();
        using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress, true))
        {
            deflateStream.Write(data, 0, data.Length);
        }
        compressedStream.Position = 0;
        using var decompressedStream = new MemoryStream();
        using (var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress, true))
        {
            deflateStream.CopyTo(decompressedStream);
        }
        var decompressed = decompressedStream.ToArray();
        var str = System.Text.Encoding.UTF8.GetString(decompressed);
        Assert.Equal("Hello, World!", str);
    }
}
