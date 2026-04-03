using WellTool.Core.IO;
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
        var buffered = BufferedUtil.Wrap(stream);
        Assert.NotNull(buffered);
    }

    [Fact]
    public void WrapWithBufferTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var buffered = BufferedUtil.Wrap(stream, 8192);
        Assert.NotNull(buffered);
    }

    [Fact]
    public void IsBufferedTest()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes("Hello"));
        var buffered = BufferedUtil.Wrap(stream);
        Assert.True(BufferedUtil.IsBuffered(buffered));
    }
}
