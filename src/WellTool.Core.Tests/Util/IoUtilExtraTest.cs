using Xunit;
using WellTool.Core.IO;
using WellTool.Core.IO.Resource;

namespace WellTool.Core.Tests.Util;

/// <summary>
/// IoUtil 额外测试
/// </summary>
public class IoUtilExtraTest
{
    [Fact]
    public void ReadBytesTest()
    {
        using var stream = ResourceUtil.GetStream("hutool.jpg");
        var bytes = IoUtil.ReadBytes(stream);
        Assert.Equal(22807, bytes.Length);
    }

    [Fact]
    public void ReadBytesWithLengthTest()
    {
        var limit = RandomUtil.RandomInt(22807);
        using var stream = ResourceUtil.GetStream("hutool.jpg");
        var bytes = IoUtil.ReadBytes(stream, limit);
        Assert.Equal(limit, bytes.Length);
    }

    [Fact]
    public void ReadLinesTest()
    {
        using var reader = ResourceUtil.GetUtf8Reader("test_lines.csv");
        IoUtil.ReadLines(reader, line => Assert.NotNull(line));
    }
}
