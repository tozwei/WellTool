using WellTool.Setting;
using Xunit;

namespace WellTool.Setting.Tests;

/// <summary>
/// SettingUtil 单元测试
/// </summary>
public class SettingUtilTest
{
    /// <summary>
    /// 测试 SettingUtil.Get 方法
    /// </summary>
    [Fact]
    public void GetTest()
    {
        var setting = SettingUtil.Get("TestData/test.setting");
        var driver = setting.GetByGroup("demo", "driver");
        Assert.Equal("com.mysql.jdbc.Driver", driver);
    }

    /// <summary>
    /// 测试 SettingUtil.GetFirstFound 方法
    /// </summary>
    [Fact]
    public void GetFirstFoundTest()
    {
        // 测试查找第一个存在的文件
        // 先找不存在的文件，再找存在的文件
        var setting = SettingUtil.GetFirstFound("TestData/nonexistent.setting", "TestData/test.setting");
        Assert.NotNull(setting);

        using (setting)
        {
            var driver = setting.GetByGroup("driver", "demo");
            Assert.Equal("com.mysql.jdbc.Driver", driver);
        }
    }

    /// <summary>
    /// 测试缓存机制
    /// </summary>
    [Fact]
    public void CachingTest()
    {
        var setting1 = SettingUtil.Get("TestData/test.setting");
        var setting2 = SettingUtil.Get("TestData/test.setting");

        // 应该是同一个实例（缓存）
        Assert.Same(setting1, setting2);
    }
}
