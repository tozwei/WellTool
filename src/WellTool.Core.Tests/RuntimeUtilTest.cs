using Xunit;
using WellTool.Core.Util;

namespace WellTool.Core.Tests;

/// <summary>
/// RuntimeUtil 测试
/// </summary>
public class RuntimeUtilTest
{
    [Fact]
    public void GetFreeMemoryTest()
    {
        var freeMemory = RuntimeUtil.GetFreeMemory();
        Assert.True(freeMemory > 0);
    }

    [Fact]
    public void GetTotalMemoryTest()
    {
        var totalMemory = RuntimeUtil.GetTotalMemory();
        Assert.True(totalMemory > 0);
    }

    [Fact]
    public void GetMaxMemoryTest()
    {
        var maxMemory = RuntimeUtil.GetMaxMemory();
        Assert.True(maxMemory >= 0);
    }

    [Fact]
    public void AvailableProcessorsTest()
    {
        var processors = RuntimeUtil.AvailableProcessors;
        Assert.True(processors > 0);
    }
}
