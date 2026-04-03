using WellTool.Core.IO;
using Xunit;

namespace WellTool.Core.Tests;

public class BufferUtilTest
{
    [Fact]
    public void CopyTest()
    {
        var bytes = "AAABBB"u8.ToArray();
        var buffer = ByteBuffer.Wrap(bytes);

        var buffer2 = BufferUtil.Copy(buffer, ByteBuffer.Allocate(5));
        Assert.Equal("AAABB", StrUtil.Utf8Str(buffer2));
    }

    [Fact]
    public void ReadBytesTest()
    {
        var bytes = "AAABBB"u8.ToArray();
        var buffer = ByteBuffer.Wrap(bytes);

        var bs = BufferUtil.ReadBytes(buffer, 5);
        Assert.Equal("AAABB", StrUtil.Utf8Str(bs));
    }

    [Fact]
    public void ReadLineTest()
    {
        var text = "aa\r\nbbb\ncc";
        var buffer = ByteBuffer.Wrap(text);

        // 第一行
        var line = BufferUtil.ReadLine(buffer, StrUtil.CHARSET_UTF_8);
        Assert.Equal("aa", line);

        // 第二行
        line = BufferUtil.ReadLine(buffer, StrUtil.CHARSET_UTF_8);
        Assert.Equal("bbb", line);

        // 第三行因为没有行结束标志，因此返回null
        line = BufferUtil.ReadLine(buffer, StrUtil.CHARSET_UTF_8);
        Assert.Null(line);

        // 读取剩余部分
        Assert.Equal("cc", StrUtil.Utf8Str(BufferUtil.ReadBytes(buffer)));
    }

    [Fact]
    public void CopyNormalRangeTest()
    {
        var originalData = new byte[] { 65, 66, 67, 68, 69, 70 }; // "ABCDEF"
        var srcBuffer = ByteBuffer.Wrap(originalData);

        var resultBuffer = BufferUtil.Copy(srcBuffer, 1, 4);

        var resultArray = new byte[3];
        resultBuffer.Get(resultArray);
        Assert.Equal(new byte[] { 66, 67, 68 }, resultArray); // BCD
    }

    [Fact]
    public void CopyFromStartTest()
    {
        var originalData = new byte[] { 65, 66, 67, 68, 69, 70 }; // "ABCDEF"
        var srcBuffer = ByteBuffer.Wrap(originalData);

        var resultBuffer = BufferUtil.Copy(srcBuffer, 0, 3);

        var resultArray = new byte[3];
        resultBuffer.Get(resultArray);
        Assert.Equal(new byte[] { 65, 66, 67 }, resultArray); // ABC
    }

    [Fact]
    public void CopyToEndTest()
    {
        var originalData = new byte[] { 65, 66, 67, 68, 69, 70 }; // "ABCDEF"
        var srcBuffer = ByteBuffer.Wrap(originalData);

        var resultBuffer = BufferUtil.Copy(srcBuffer, 3, 6);

        var resultArray = new byte[3];
        resultBuffer.Get(resultArray);
        Assert.Equal(new byte[] { 68, 69, 70 }, resultArray); // DEF
    }

    [Fact]
    public void CopyEmptyRangeTest()
    {
        var originalData = new byte[] { 65, 66, 67, 68, 69, 70 }; // "ABCDEF"
        var srcBuffer = ByteBuffer.Wrap(originalData);

        var resultBuffer = BufferUtil.Copy(srcBuffer, 2, 2);

        Assert.Equal(0, resultBuffer.Remaining);
    }
}
