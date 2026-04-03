using WellTool.Core.IO;
using Xunit;
using System.Text;

namespace WellTool.Core.Tests;

public class BufferUtilLastTest
{
    [Fact]
    public void CreateByteBufferTest()
    {
        var buffer = BufferUtil.CreateByteBuffer(16);
        Assert.NotNull(buffer);
        Assert.Equal(16, buffer.Capacity);
    }

    [Fact]
    public void CreateCharBufferTest()
    {
        var buffer = BufferUtil.CreateCharBuffer(16);
        Assert.NotNull(buffer);
        Assert.Equal(16, buffer.Capacity);
    }
}
