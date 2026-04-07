using WellTool.Core.IO;
using Xunit;
using System.Text;
using System.IO;

namespace WellTool.Core.Tests;

public class IoUtilTest
{
    [Fact]
    public void ReadStringTest()
    {
        var content = "Hello, World!";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var result = IoUtil.ReadString(stream);
        Assert.Equal(content, result);
    }

    [Fact]
    public void ReadBytesTest()
    {
        var content = "Hello";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var bytes = IoUtil.ReadBytes(stream);
        Assert.Equal(content.Length, bytes.Length);
    }

    [Fact]
    public void CopyTest()
    {
        var content = "Hello, World!";
        using var input = new MemoryStream(Encoding.UTF8.GetBytes(content));
        using var output = new MemoryStream();
        IoUtil.Copy(input, output);
        output.Position = 0;
        using var reader = new StreamReader(output, Encoding.UTF8);
        Assert.Equal(content, reader.ReadToEnd());
    }

    [Fact]
    public void WriteBytesTest()
    {
        var content = "Hello";
        var bytes = Encoding.UTF8.GetBytes(content);
        using var stream = new MemoryStream();
        IoUtil.WriteBytes(stream, bytes);
        stream.Position = 0;
        var result = IoUtil.ReadBytes(stream);
        Assert.Equal(bytes, result);
    }

    [Fact]
    public void WriteStringTest()
    {
        var content = "Hello, World!";
        using var stream = new MemoryStream();
        IoUtil.WriteString(stream, content);
        stream.Position = 0;
        var result = IoUtil.ReadString(stream);
        Assert.Equal(content, result);
    }

    [Fact]
    public void CloseTest()
    {
        using var stream = new MemoryStream();
        IoUtil.Close(stream);
    }

    [Fact]
    public void CloseQuietlyTest()
    {
        using var stream = new MemoryStream();
        IoUtil.CloseQuietly(stream);
    }

    [Fact]
    public void FlushAndCloseTest()
    {
        using var stream = new MemoryStream();
        IoUtil.FlushAndClose(stream);
    }

    [Fact]
    public void FlushAndCloseQuietlyTest()
    {
        using var stream = new MemoryStream();
        IoUtil.FlushAndCloseQuietly(stream);
    }

    [Fact]
    public void CreateTempFileTest()
    {
        var tempFile = IoUtil.CreateTempFile();
        Assert.NotNull(tempFile);
        Assert.True(File.Exists(tempFile));
        File.Delete(tempFile);
    }

    [Fact]
    public void CreateTempDirectoryTest()
    {
        var tempDir = IoUtil.CreateTempDirectory();
        Assert.NotNull(tempDir);
    }

    [Fact]
    public void GetTempDirectoryTest()
    {
        var tempDir = IoUtil.GetTempDirectory();
        Assert.NotNull(tempDir);
        Assert.True(Directory.Exists(tempDir));
    }
}
