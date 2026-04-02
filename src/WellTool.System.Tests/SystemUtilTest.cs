using WellTool.System;

namespace WellTool.System.Tests;

/// <summary>
/// SystemUtil 测试
/// </summary>
public class SystemUtilTest
{
    [Fact]
    public void GetCurrentPIDTest()
    {
        var pid = SystemUtil.GetCurrentPID();
        Assert.True(pid > 0);
    }

    [Fact]
    public void GetNetInfoTest()
    {
        var netInfo = SystemUtil.GetNetInfo();
        Assert.NotNull(netInfo);
    }

    [Fact]
    public void GetOsInfoTest()
    {
        var osInfo = SystemUtil.GetOsInfo();
        Assert.NotNull(osInfo);
    }

    [Fact]
    public void GetHostInfoTest()
    {
        var hostInfo = SystemUtil.GetHostInfo();
        Assert.NotNull(hostInfo);
    }

    [Fact]
    public void GetUserInfoTest()
    {
        var userInfo = SystemUtil.GetUserInfo();
        Assert.NotNull(userInfo);
    }

    [Fact]
    public void GetRuntimeInfoTest()
    {
        var runtimeInfo = SystemUtil.GetRuntimeInfo();
        Assert.NotNull(runtimeInfo);
    }

    [Fact]
    public void GetNetVmInfoTest()
    {
        var vmInfo = SystemUtil.GetNetVmInfo();
        Assert.NotNull(vmInfo);
    }

    [Fact]
    public void GetNetVmSpecInfoTest()
    {
        var vmSpecInfo = SystemUtil.GetNetVmSpecInfo();
        Assert.NotNull(vmSpecInfo);
    }

    [Fact]
    public void GetNetSpecInfoTest()
    {
        var netSpecInfo = SystemUtil.GetNetSpecInfo();
        Assert.NotNull(netSpecInfo);
    }

    [Fact]
    public void GetNetRuntimeInfoTest()
    {
        var netRuntimeInfo = SystemUtil.GetNetRuntimeInfo();
        Assert.NotNull(netRuntimeInfo);
    }
}
