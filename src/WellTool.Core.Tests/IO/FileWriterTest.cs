using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileWriterTest
{
    [Fact]
    public void WriteUtf8Test()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_write_utf8_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            var writer = new FileWriter(tempFile);
            writer.WriteUtf8("Hello, 世界!");
            writer.Flush();
            writer.Close();

            var content = File.ReadAllText(tempFile, System.Text.Encoding.UTF8);
            Assert.Equal("Hello, 世界!", content);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void AppendUtf8Test()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_append_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            var writer = new FileWriter(tempFile);
            writer.WriteUtf8("Hello");
            writer.Flush();
            writer.Close();

            writer = new FileWriter(tempFile, true);
            writer.WriteUtf8(" World");
            writer.Flush();
            writer.Close();

            var content = File.ReadAllText(tempFile, System.Text.Encoding.UTF8);
            Assert.Equal("Hello World", content);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }

    [Fact]
    public void WriteLinesTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_write_lines_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            var writer = new FileWriter(tempFile);
            writer.WriteLines(new[] { "line1", "line2", "line3" });
            writer.Flush();
            writer.Close();

            var content = File.ReadAllText(tempFile, System.Text.Encoding.UTF8);
            Assert.Contains("line1", content);
            Assert.Contains("line2", content);
            Assert.Contains("line3", content);
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }
}
