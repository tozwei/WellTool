using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileWriterUtilTest
{
    [Fact]
    public void WriteUtf8Test()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            FileWriterUtil.WriteUtf8(tempFile, "Hello, 世界!");
            Assert.True(File.Exists(tempFile));
            Assert.Equal("Hello, 世界!", await File.ReadAllTextAsync(tempFile, System.Text.Encoding.UTF8));
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void AppendUtf8Test()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            FileWriterUtil.WriteUtf8(tempFile, "Hello");
            FileWriterUtil.AppendUtf8(tempFile, " World");
            Assert.Equal("Hello World", await File.ReadAllTextAsync(tempFile, System.Text.Encoding.UTF8));
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteLinesTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            FileWriterUtil.WriteLines(tempFile, new[] { "line1", "line2", "line3" });
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteBytesTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
            FileWriterUtil.WriteBytes(tempFile, bytes);
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }
}
