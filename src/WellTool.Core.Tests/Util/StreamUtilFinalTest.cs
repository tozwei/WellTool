using WellTool.Core.IO;
using WellTool.Core.Streams;
using Xunit;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests;

public class StreamUtilFinalTest
{
    [Fact]
    public void ReadUtf8Test()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var result = StreamUtil.ReadUtf8(stream);
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void ReadBytesTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var bytes = StreamUtil.ReadBytes(stream);
        Assert.Equal(5, bytes.Length);
    }

    [Fact]
    public void CopyTest()
    {
        using var input = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        using var output = new MemoryStream();
        StreamUtil.Copy(input, output);
        output.Position = 0;
        using var reader = new StreamReader(output, Encoding.UTF8);
        Assert.Equal("Hello", reader.ReadToEnd());
    }

    [Fact]
    public void EmptyStreamTest()
    {
        var empty = StreamUtil.EmptyStream();
        Assert.Equal(0, empty.Length);
    }

    [Fact]
    public void GetAvailableTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var available = StreamUtil.GetAvailable(stream);
        Assert.Equal(5, available);
    }
}
