using Xunit;
using WellTool.Core.Compress;
using System.IO;

namespace WellTool.Core.Tests.Compress;

/// <summary>
/// ZipWriter 测试
/// </summary>
public class ZipWriterTest
{
    [Fact]
    public void WriteTest()
    {
        using var memoryStream = new MemoryStream();
        using var writer = new ZipWriter(memoryStream);
        writer.Write("test.txt", "Hello World"u8);
        writer.Finish();
        
        Assert.True(memoryStream.Length > 0);
    }

    [Fact]
    public void WriteMultipleTest()
    {
        using var memoryStream = new MemoryStream();
        using var writer = new ZipWriter(memoryStream);
        writer.Write("test1.txt", "Hello"u8);
        writer.Write("test2.txt", "World"u8);
        writer.Finish();
        
        Assert.True(memoryStream.Length > 0);
    }
}
