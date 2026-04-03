using WellTool.Core.Text;
using Xunit;
using System.Text;

namespace WellTool.Core.Tests;

public class CharsetDetectorLastTest
{
    [Fact]
    public void DetectTest()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello");
        var charset = CharsetDetector.Detect(bytes);
        Assert.NotNull(charset);
    }

    [Fact]
    public void DetectChineseTest()
    {
        var bytes = Encoding.UTF8.GetBytes("你好");
        var charset = CharsetDetector.Detect(bytes);
        Assert.NotNull(charset);
    }

    [Fact]
    public void IsUtf8Test()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello");
        Assert.True(CharsetDetector.IsUtf8(bytes));
    }

    [Fact]
    public void IsAsciiTest()
    {
        var bytes = Encoding.ASCII.GetBytes("Hello");
        Assert.True(CharsetDetector.IsAscii(bytes));
    }
}
