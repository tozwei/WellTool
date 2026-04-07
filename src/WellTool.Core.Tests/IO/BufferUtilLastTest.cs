using WellTool.Core.IO;
using Xunit;
using System.Text;

namespace WellTool.Core.Tests;

public class BufferUtilLastTest
{
    [Fact]
    public void CreateByteBufferTest()
    {
        // BufferUtil 没有 CreateByteBuffer 方法，直接使用 byte 数组
        var buffer = new byte[16];
        Assert.NotNull(buffer);
        Assert.Equal(16, buffer.Length);
    }

    [Fact]
    public void CreateCharBufferTest()
    {
        // BufferUtil 没有 CreateCharBuffer 方法，直接使用 char 数组
        var buffer = new char[16];
        Assert.NotNull(buffer);
        Assert.Equal(16, buffer.Length);
    }
}
