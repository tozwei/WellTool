using Xunit;
using System.IO;
using System.Text;

namespace WellTool.Core.Tests;

public class BufferedUtilTest
{
    [Fact]
    public void WrapTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var buffered = new BufferedStream(stream);
        Assert.NotNull(buffered);
    }

    [Fact]
    public void WrapWithBufferTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var buffered = new BufferedStream(stream, 8192);
        Assert.NotNull(buffered);
    }

    [Fact]
    public void IsBufferedTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var buffered = new BufferedStream(stream);
        Assert.True(buffered is BufferedStream);
    }
}
