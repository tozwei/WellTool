using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class SystemUtilTest
{
    [Fact]
    public void MachineNameTest()
    {
        var hostName = SystemUtil.MachineName();
        Assert.NotNull(hostName);
    }

    [Fact]
    public void UserNameTest()
    {
        var userName = SystemUtil.UserName();
        Assert.NotNull(userName);
    }

    [Fact]
    public void OsVersionTest()
    {
        var osVersion = SystemUtil.OsVersion();
        Assert.NotNull(osVersion);
    }

    [Fact]
    public void MemoryUsedTest()
    {
        var usedMemory = SystemUtil.MemoryUsed();
        Assert.True(usedMemory >= 0);
    }

    [Fact]
    public void CpuCountTest()
    {
        var processors = SystemUtil.CpuCount();
        Assert.True(processors > 0);
    }
}
