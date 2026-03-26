using WellTool.Setting;
using Xunit;

namespace WellTool.Setting.Tests;

/// <summary>
/// Setting 单元测试
/// </summary>
public class SettingTest
{
    /// <summary>
    /// 测试 Setting 基本功能
    /// </summary>
    [Fact]
    public void SettingTest1()
    {
        var setting = new Setting("TestData/test.setting", true);

        // 测试分组读取
        var driver = setting.GetByGroup("driver", "demo");
        Assert.Equal("com.mysql.jdbc.Driver", driver);

        // 本分组变量替换
        var user = setting.GetByGroup("user", "demo");
        Assert.Equal("rootcom.mysql.jdbc.Driver", user);

        // 跨分组变量替换
        var user2 = setting.GetByGroup("user2", "demo");
        Assert.Equal("rootcom.mysql.jdbc.Driver", user2);

        // 默认值测试
        var value = setting.GetStr("keyNotExist", "defaultTest");
        Assert.Equal("defaultTest", value);
    }

    /// <summary>
    /// 测试自定义 Setting（手动添加配置）
    /// </summary>
    [Fact]
    public void SettingTestForCustom()
    {
        var setting = new Setting();

        setting.Put("user", "root", "group1");
        setting.Put("user", "root2", "group2");
        setting.Put("user", "root3", "group3");
        setting.Put("user", "root4");

        Assert.Equal("root", setting.GetByGroup("user", "group1"));
        Assert.Equal("root2", setting.GetByGroup("user", "group2"));
        Assert.Equal("root3", setting.GetByGroup("user", "group3"));
        Assert.Equal("root4", setting.GetStr("user"));
    }

    /// <summary>
    /// 测试变量替换功能
    /// </summary>
    [Fact]
    public void VariableReplacementTest()
    {
        var setting = new Setting().SetUseVariable(true);

        // 设置基础变量
        setting.Put("base_path", "/opt/app");
        setting.Put("log_path", "${base_path}/logs");
        setting.Put("data_path", "${base_path}/data");

        // 测试变量替换
        Assert.Equal("/opt/app/logs", setting.GetStr("log_path"));
        Assert.Equal("/opt/app/data", setting.GetStr("data_path"));
    }

    /// <summary>
    /// 测试类型转换
    /// </summary>
    [Fact]
    public void TypeConversionTest()
    {
        var setting = new Setting();

        setting.Put("port", "8080");
        setting.Put("debug", "true");
        setting.Put("rate", "3.14");
        setting.Put("count", "100");

        Assert.Equal(8080, setting.GetInt("port", 0));
        Assert.True(setting.GetBool("debug", false));
        Assert.Equal(3.14, setting.GetDouble("rate", 0.0));
        Assert.Equal(100L, setting.GetLong("count", 0L));
    }

    /// <summary>
    /// 测试数组类型
    /// </summary>
    [Fact]
    public void ArrayTypeTest()
    {
        var setting = new Setting();

        setting.Put("items", "item1,item2,item3");

        var items = setting.GetStrings("items");
        Assert.Equal(3, items.Length);
        Assert.Contains("item1", items);
        Assert.Contains("item2", items);
        Assert.Contains("item3", items);
    }

    /// <summary>
    /// 测试分组管理
    /// </summary>
    [Fact]
    public void GroupManagementTest()
    {
        var setting = new Setting();

        setting.Put("key1", "value1", "Group1");
        setting.Put("key2", "value2", "Group2");
        setting.Put("key3", "value3");

        // 测试包含检查
        Assert.True(setting.ContainsKey("key1", "Group1"));
        Assert.True(setting.ContainsKey("key3"));

        // 测试获取所有分组
        var groups = setting.Groups();
        Assert.NotEmpty(groups);

        // 测试删除
        setting.Remove("key1", "Group1");
        Assert.False(setting.ContainsKey("key1", "Group1"));
    }
}
