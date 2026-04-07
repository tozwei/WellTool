using WellTool.Core.IO;
using Xunit;
using System.IO;

namespace WellTool.Core.Tests;

public class FileWriterTest
{
    [Fact]
    public void WriteTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_write_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            var writer = new FileWriter(tempFile);
            writer.Write("Hello, 世界!");
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
    public void AppendTest()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_append_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            var writer = new FileWriter(tempFile);
            writer.Write("Hello");
            writer.Flush();
            writer.Close();

            writer = new FileWriter(tempFile, System.Text.Encoding.UTF8, FileMode.Append);
            writer.Write(" World");
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
