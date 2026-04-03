using WellTool.Core.IO;
using Xunit;
using System.Text;

namespace WellTool.Core.Tests;

public class IoUtilTest
{
    [Fact]
    public void ReadUtf8Test()
    {
        var content = "Hello, World!";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var result = IoUtil.ReadUtf8(stream);
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
    public void ReadLinesTest()
    {
        var content = "line1\r\nline2\r\nline3";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        var lines = IoUtil.ReadLines(stream);
        Assert.Equal(3, lines.Count);
        Assert.Equal("line1", lines[0]);
        Assert.Equal("line2", lines[1]);
        Assert.Equal("line3", lines[2]);
    }

    [Fact]
    public void SkipTest()
    {
        var content = "Hello, World!";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        IoUtil.Skip(stream, 7);
        var bytes = IoUtil.ReadBytes(stream);
        var result = Encoding.UTF8.GetString(bytes);
        Assert.Equal("World!", result);
    }

    [Fact]
    public void AvailableTest()
    {
        var content = "Hello";
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
        Assert.Equal(5, IoUtil.Available(stream));
    }

    [Fact]
    public void EmptyStreamTest()
    {
        var empty = IoUtil.EmptyStream();
        Assert.Equal(0, IoUtil.Available(empty));
    }
}
