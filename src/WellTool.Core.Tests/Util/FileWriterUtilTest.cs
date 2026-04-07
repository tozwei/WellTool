using Xunit;
using System.IO;
using System;

namespace WellTool.Core.Tests;

public class FileWriterUtilTest
{
    [Fact]
    public void WriteUtf8Test()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), "test_" + Guid.NewGuid().ToString("N") + ".txt");
        try
        {
            File.WriteAllText(tempFile, "Hello, 世界!", System.Text.Encoding.UTF8);
            Assert.True(File.Exists(tempFile));
            Assert.Equal("Hello, 世界!", File.ReadAllText(tempFile, System.Text.Encoding.UTF8));
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
            File.WriteAllText(tempFile, "Hello", System.Text.Encoding.UTF8);
            File.AppendAllText(tempFile, " World", System.Text.Encoding.UTF8);
            Assert.Equal("Hello World", File.ReadAllText(tempFile, System.Text.Encoding.UTF8));
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
            File.WriteAllLines(tempFile, new[] { "line1", "line2", "line3" });
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
            File.WriteAllBytes(tempFile, bytes);
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
        }
    }
}
