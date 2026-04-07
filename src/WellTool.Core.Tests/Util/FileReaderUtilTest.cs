using Xunit;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests;

public class FileReaderUtilTest
{
    [Fact]
    public void ReadUtf8Test()
    {
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, "Hello, 世界!", Encoding.UTF8);
            var content = File.ReadAllText(tempFile, Encoding.UTF8);
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
            File.WriteAllLines(tempFile, new[] { "line1", "line2", "line3" }, Encoding.UTF8);
            var lines = File.ReadAllLines(tempFile, Encoding.UTF8);
            Assert.Equal(3, lines.Length);
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
            File.WriteAllBytes(tempFile, Encoding.UTF8.GetBytes(content));
            var bytes = File.ReadAllBytes(tempFile);
            Assert.Equal(5, bytes.Length);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }
}
