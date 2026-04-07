using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileReaderTest
{
    [Fact]
    public void ReadUtf8Test()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_utf8.txt");
        File.WriteAllText(tempFile, "Hello, 世界!", System.Text.Encoding.UTF8);
        try
        {
            var reader = new FileReader(tempFile);
            var content = reader.ReadString();
            Assert.Equal("Hello, 世界!", content);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadLinesTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_lines.txt");
        File.WriteAllText(tempFile, "line1\r\nline2\r\nline3", System.Text.Encoding.UTF8);
        try
        {
            var reader = new FileReader(tempFile);
            var lines = reader.ReadLines();
            Assert.Equal(3, lines.Count);
            Assert.Equal("line1", lines[0]);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void ReadBytesTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_bytes.txt");
        var content = "Hello";
        File.WriteAllBytes(tempFile, System.Text.Encoding.UTF8.GetBytes(content));
        try
        {
            var reader = new FileReader(tempFile);
            var bytes = reader.ReadBytes();
            Assert.Equal(5, bytes.Length);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void ConstructorWithCharsetTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_charset.txt");
        File.WriteAllText(tempFile, "Hello", System.Text.Encoding.UTF8);
        try
        {
            var reader = new WellTool.Core.IO.FileReader(tempFile, System.Text.Encoding.UTF8);
            var content = reader.ReadString();
            Assert.Equal("Hello", content);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
