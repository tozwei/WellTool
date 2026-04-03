using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class SystemUtilTest
{
    [Fact]
    public void GetHostNameTest()
    {
        var hostName = SystemUtil.GetHostName();
        Assert.NotNull(hostName);
    }

    [Fact]
    public void GetUserNameTest()
    {
        var userName = SystemUtil.GetUserName();
        Assert.NotNull(userName);
    }

    [Fact]
    public void GetOsVersionTest()
    {
        var osVersion = SystemUtil.GetOsVersion();
        Assert.NotNull(osVersion);
    }

    [Fact]
    public void GetTotalMemoryTest()
    {
        var totalMemory = SystemUtil.GetTotalMemory();
        Assert.True(totalMemory > 0);
    }

    [Fact]
    public void GetFreeMemoryTest()
    {
        var freeMemory = SystemUtil.GetFreeMemory();
        Assert.True(freeMemory >= 0);
    }

    [Fact]
    public void GetUsedMemoryTest()
    {
        var usedMemory = SystemUtil.GetUsedMemory();
        Assert.True(usedMemory >= 0);
    }

    [Fact]
    public void GetAvailableProcessorsTest()
    {
        var processors = SystemUtil.GetAvailableProcessors();
        Assert.True(processors > 0);
    }

    [Fact]
    public void GetJvmInfoTest()
    {
        var jvmInfo = SystemUtil.GetJvmInfo();
        Assert.NotNull(jvmInfo);
    }
}
