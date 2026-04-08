using Xunit;
using WellTool.Core.IO;
using WellTool.Core.IO.Resource;
using System.Text;

namespace WellTool.Core.Tests;

/// <summary>
/// IO 测试
/// </summary>
public class IOTests
{
    [Fact]
    public void ReadBytesTest()
    {
        using var stream = ResourceUtil.GetStream("hutool.jpg");
        var bytes = IoUtil.ReadBytes(stream);
        Assert.True(bytes.Length > 0);
    }

    [Fact]
    public void ReadUtf8StrTest()
    {
        using var stream = ResourceUtil.GetStream("test_lines.csv");
        var content = IoUtil.ReadUtf8Str(stream);
        Assert.NotNull(content);
        Assert.Contains("name", content);
    }

    [Fact]
    public void ReadStrTest()
    {
        using var stream = ResourceUtil.GetStream("test_lines.csv");
        var content = IoUtil.ReadStr(stream, Encoding.UTF8);
        Assert.NotNull(content);
    }
}
