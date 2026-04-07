using System.Text;
using WellTool.Core.IO;
using Xunit;

public class BufferUtilTest
{
    [Fact]
    public void CopyTest()
    {
        byte[] src = new byte[] { 1, 2, 3, 4, 5 };
        byte[] result = BufferUtil.Copy(src, 1, 4);
        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
        Assert.Equal(2, result[0]);
    }

    [Fact]
    public void ReadBytesTest()
    {
        byte[] buffer = new byte[] { 1, 2, 3, 4, 5 };
        int position = 0;
        byte[] result = BufferUtil.ReadBytes(buffer, ref position);
        Assert.NotNull(result);
        Assert.Equal(1, result.Length);
        Assert.Equal(1, result[0]);
        Assert.Equal(1, position);
    }

    [Fact]
    public void ReadLineTest()
    {
        byte[] buffer = Encoding.UTF8.GetBytes("hello\nworld");
        int position = 0;
        string line = BufferUtil.ReadLine(buffer, ref position, Encoding.UTF8);
        Assert.Equal("hello", line);
    }

    [Fact]
    public void CopyFromStartTest()
    {
        byte[] src = new byte[] { 1, 2, 3, 4, 5 };
        byte[] result = BufferUtil.Copy(src, 0, 3);
        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void CopyToEndTest()
    {
        byte[] src = new byte[] { 1, 2, 3, 4, 5 };
        byte[] result = BufferUtil.Copy(src, 2, 5);
        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void CreateUtf8Test()
    {
        string test = "Hello";
        byte[] result = BufferUtil.CreateUtf8(test);
        Assert.NotNull(result);
        Assert.Equal(test, Encoding.UTF8.GetString(result));
    }
}
