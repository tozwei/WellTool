using Xunit;
using WellTool.Core.IO;
using WellTool.Core.IO.Resource;
using WellTool.Core.Util;

namespace WellTool.Core.Tests.Util;

/// <summary>
/// CharsetDetector 测试
/// </summary>
public class CharsetDetectorLastTest
{
    [Fact]
    public void DetectTest()
    {
        using var stream = ResourceUtil.GetStream("test.xml");
        var detect = CharsetDetector.Detect(stream, CharsetUtil.CHARSET_GBK, CharsetUtil.CHARSET_UTF_8);
        Assert.Equal(CharsetUtil.CHARSET_UTF_8, detect);
    }
}
