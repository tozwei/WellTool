using WellTool.System;
using WellTool.System.Oshi;

namespace WellTool.System.Tests;

/// <summary>
/// Oshi 系统信息测试
/// </summary>
public class OshiTest
{
    [Fact]
    public void GetMemoryInfoTest()
    {
        var (totalMemory, availableMemory) = OshiUtil.GetMemoryInfo();
        Assert.True(totalMemory > 0);
        Assert.True(availableMemory >= 0);
    }

    [Fact]
    public void GetCpuInfoTest()
    {
        var cpuInfo = OshiUtil.GetCpuInfo();
        Assert.NotNull(cpuInfo);
        Assert.True(cpuInfo.CoreCount > 0);
    }

    [Fact]
    public void GetCpuUsageTest()
    {
        var usage = OshiUtil.GetCpuUsage();
        Assert.True(usage >= 0);
    }

    [Fact]
    public void GetCpuTicksTest()
    {
        var cpuTicks = OshiUtil.GetCpuTicks();
        Assert.NotNull(cpuTicks);
    }

    [Fact]
    public void CpuInfoUsageTest()
    {
        var cpuInfo = OshiUtil.GetCpuInfo();
        Assert.True(cpuInfo.Usage >= 0);
    }

    [Fact]
    public void GetDiskInfoTest()
    {
        var (totalSpace, availableSpace) = OshiUtil.GetDiskInfo();
        Assert.True(totalSpace >= 0);
        Assert.True(availableSpace >= 0);
    }
}
