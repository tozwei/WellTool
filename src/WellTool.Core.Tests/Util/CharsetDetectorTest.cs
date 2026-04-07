using System.IO;
using System.Text;
using WellTool.Core.IO;
using Xunit;

namespace WellTool.Core.IO.Tests;

public class CharsetDetectorTest
{
    [Fact]
    public void DetectTest()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello");
        var stream = new MemoryStream(bytes);
        var encoding = CharsetDetector.Detect(stream, Encoding.UTF8, Encoding.ASCII);
        Assert.NotNull(encoding);
    }

    [Fact]
    public void DetectChineseTest()
    {
        var bytes = Encoding.UTF8.GetBytes("你好");
        var stream = new MemoryStream(bytes);
        var encoding = CharsetDetector.Detect(stream);
        Assert.NotNull(encoding);
    }

    [Fact]
    public void IsUtf8Test()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello");
        var stream = new MemoryStream(bytes);
        var encoding = CharsetDetector.Detect(stream, Encoding.UTF8);
        Assert.Equal(Encoding.UTF8, encoding);
    }

    [Fact]
    public void IsAsciiTest()
    {
        var bytes = Encoding.ASCII.GetBytes("Hello");
        var stream = new MemoryStream(bytes);
        var encoding = CharsetDetector.Detect(stream);
        Assert.NotNull(encoding);
    }
}
