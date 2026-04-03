using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileReaderUtilTest
{
    [Fact]
    public void ReadUtf8Test()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllTextAsync(tempFile, "Hello, 世界!", Encoding.UTF8);
            var content = FileReaderUtil.ReadUtf8(tempFile);
            Assert.Equal("Hello, 世界!", content);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadLinesTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            await File.WriteAllLinesAsync(tempFile, new[] { "line1", "line2", "line3" }, Encoding.UTF8);
            var lines = FileReaderUtil.ReadLines(tempFile);
            Assert.Equal(3, lines.Count);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadBytesTest()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            var content = "Hello";
            await File.WriteAllBytesAsync(tempFile, Encoding.UTF8.GetBytes(content));
            var bytes = FileReaderUtil.ReadBytes(tempFile);
            Assert.Equal(5, bytes.Length);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }
}
