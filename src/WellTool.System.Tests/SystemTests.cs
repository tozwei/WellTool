using WellTool.System;

namespace WellTool.System.Tests;

public class SystemTests
{
    [Fact]
    public void TestHostInfoInstance()
    {
        // Test that HostInfo instance can be created
        var hostInfo = HostInfo.Instance;
        Assert.NotNull(hostInfo);
    }

    [Fact]
    public void TestRuntimeInfoInstance()
    {
        // Test that RuntimeInfo instance can be created
        var runtimeInfo = RuntimeInfo.Instance;
        Assert.NotNull(runtimeInfo);
    }

    [Fact]
    public void TestSystemUtil()
    {
        // Test that SystemUtil can be accessed
        var pid = SystemUtil.GetCurrentPID();
        Assert.True(pid > 0);
    }
}
