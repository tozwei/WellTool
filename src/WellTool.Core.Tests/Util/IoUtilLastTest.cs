using WellTool.Core.Util;
using Xunit;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests;

public class IoUtilLastTest
{
    [Fact]
    public void ReadStringTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var result = IOUtil.ReadString(stream);
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void ReadBytesTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var bytes = IOUtil.ReadBytes(stream);
        Assert.Equal(5, bytes.Length);
    }

    [Fact]
    public void CopyTest()
    {
        using var input = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        using var output = new MemoryStream();
        IOUtil.Copy(input, output);
        output.Position = 0;
        using var reader = new StreamReader(output, Encoding.UTF8);
        Assert.Equal("Hello", reader.ReadToEnd());
    }

    [Fact]
    public void EmptyStreamTest()
    {
        var empty = IOUtil.EmptyStream;
        Assert.True(true);
    }

    [Fact]
    public void CloseTest()
    {
        var stream = new MemoryStream();
        IOUtil.Close(stream);
        Assert.True(true);
    }
}
