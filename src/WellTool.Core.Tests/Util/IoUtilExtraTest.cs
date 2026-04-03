using WellTool.Core.IO;
using Xunit;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests;

public class IoUtilExtraTest
{
    [Fact]
    public void ReadUtf8Test()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var result = IoUtil.ReadUtf8(stream);
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void ReadUtf8StrTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var result = IoUtil.ReadUtf8Str(stream);
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void ReadBytesTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var bytes = IoUtil.ReadBytes(stream);
        Assert.Equal(5, bytes.Length);
    }

    [Fact]
    public void CopyTest()
    {
        using var input = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        using var output = new MemoryStream();
        IoUtil.Copy(input, output);
        output.Position = 0;
        using var reader = new StreamReader(output, Encoding.UTF8);
        Assert.Equal("Hello", reader.ReadToEnd());
    }

    [Fact]
    public void ReadLinesTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("line1\r\nline2\r\nline3"));
        var lines = IoUtil.ReadLines(stream);
        Assert.Equal(3, lines.Count);
    }

    [Fact]
    public void SkipTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        IoUtil.Skip(stream, 3);
        var bytes = IoUtil.ReadBytes(stream);
        var result = Encoding.UTF8.GetString(bytes);
        Assert.Equal("lo", result);
    }

    [Fact]
    public void AvailableTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        Assert.Equal(5, IoUtil.Available(stream));
    }

    [Fact]
    public void EmptyStreamTest()
    {
        var empty = IoUtil.EmptyStream();
        Assert.Equal(0, empty.Length);
    }

    [Fact]
    public void CloseTest()
    {
        var stream = new MemoryStream();
        IoUtil.Close(stream);
        Assert.True(true);
    }
}
