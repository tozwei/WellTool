namespace WellTool.Setting.Tests;

using Well.Setting;
using Xunit;

public class SettingUtilTest
{
    [Fact]
    public void GetTest()
    {
        var setting = SettingUtil.Get("test");
        Assert.NotNull(setting);
    }

    [Fact]
    public void GetWithPathTest()
    {
        var setting = SettingUtil.Get("D:\\test.setting", "utf-8");
        Assert.NotNull(setting);
    }

    [Fact]
    public void GetByClassPathTest()
    {
        var setting = SettingUtil.GetByClassPath("test.setting");
        Assert.NotNull(setting);
    }

    [Fact]
    public void GetWithGroupTest()
    {
        var setting = SettingUtil.Get("test", "group");
        Assert.NotNull(setting);
    }

    [Fact]
    public void LoadTest()
    {
        var setting = SettingUtil.Load("test.setting");
        Assert.NotNull(setting);
    }

    [Fact]
    public void LoadWithCharsetTest()
    {
        var setting = SettingUtil.Load("test.setting", "utf-8");
        Assert.NotNull(setting);
    }
}
