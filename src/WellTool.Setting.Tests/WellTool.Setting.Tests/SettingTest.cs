using WellTool.Setting;
using Xunit;
using System.IO;

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

    /// <summary>
    /// 测试 GetStrNotEmpty 方法
    /// </summary>
    [Fact]
    public void GetStrNotEmptyTest()
    {
        var setting = new Setting();

        // 测试空值情况
        var emptyValue = setting.GetStrNotEmpty("keyNotExist", "group", "defaultValue");
        Assert.Equal("defaultValue", emptyValue);

        // 测试非空值情况
        setting.Put("keyExist", "valueExist", "group");
        var existValue = setting.GetStrNotEmpty("keyExist", "group", "defaultValue");
        Assert.Equal("valueExist", existValue);
    }

    /// <summary>
    /// 测试不同分隔符的数组获取
    /// </summary>
    [Fact]
    public void GetStringsWithCustomSeparatorTest()
    {
        var setting = new Setting();

        // 使用分号作为分隔符
        setting.Put("items", "item1;item2;item3");

        var items = setting.GetStrings("items", null, ";");
        Assert.Equal(3, items.Length);
        Assert.Contains("item1", items);
        Assert.Contains("item2", items);
        Assert.Contains("item3", items);
    }

    /// <summary>
    /// 测试 GetStringsWithDefault 方法
    /// </summary>
    [Fact]
    public void GetStringsWithDefaultTest()
    {
        var setting = new Setting();

        // 测试默认值
        var defaultItems = new string[] { "default1", "default2" };
        var items = setting.GetStringsWithDefault("keyNotExist", defaultItems);
        Assert.Equal(defaultItems, items);

        // 测试非默认值
        setting.Put("items", "item1,item2");
        items = setting.GetStringsWithDefault("items", defaultItems);
        Assert.Equal(2, items.Length);
        Assert.Contains("item1", items);
        Assert.Contains("item2", items);
    }

    /// <summary>
    /// 测试 Clear 方法
    /// </summary>
    [Fact]
    public void ClearTest()
    {
        var setting = new Setting();

        // 添加一些配置
        setting.Put("key1", "value1");
        setting.Put("key2", "value2", "group");

        // 验证配置存在
        Assert.True(setting.ContainsKey("key1"));
        Assert.True(setting.ContainsKey("key2", "group"));

        // 清空配置
        setting.Clear();

        // 验证配置已清空
        Assert.False(setting.ContainsKey("key1"));
        Assert.False(setting.ContainsKey("key2", "group"));
    }

    /// <summary>
    /// 测试异常处理 - 文件不存在
    /// </summary>
    [Fact]
    public void FileNotFoundTest()
    {
        Assert.Throws<FileNotFoundException>(() => new Setting("non_existent_file.setting"));
    }

    /// <summary>
    /// 测试 Dispose 方法
    /// </summary>
    [Fact]
    public void DisposeTest()
    {
        var setting = new Setting();
        setting.Put("key", "value");
        
        // 验证配置存在
        Assert.True(setting.ContainsKey("key"));
        
        // 释放资源
        setting.Dispose();
        
        // 验证仍然可以访问（Dispose 应该是安全的）
        Assert.False(setting.ContainsKey("key"));
    }

    /// <summary>
    /// 测试分组内变量替换
    /// </summary>
    [Fact]
    public void GroupVariableReplacementTest()
    {
        var setting = new Setting().SetUseVariable(true);

        // 在同一分组内设置变量
        setting.Put("db.url", "jdbc:mysql://localhost:3306", "database");
        setting.Put("db.username", "root", "database");
        setting.Put("db.password", "password", "database");
        setting.Put("db.connection", "${db.url}/${db.username}/${db.password}", "database");

        // 测试分组内变量替换
        var connection = setting.GetStr("db.connection", "database");
        Assert.Equal("jdbc:mysql://localhost:3306/root/password", connection);
    }

    /// <summary>
    /// 测试跨分组变量替换
    /// </summary>
    [Fact]
    public void CrossGroupVariableReplacementTest()
    {
        var setting = new Setting().SetUseVariable(true);

        // 在默认分组设置变量
        setting.Put("global.base_url", "https://example.com");
        
        // 在其他分组使用默认分组的变量
        setting.Put("api.url", "${global.base_url}/api", "api");
        setting.Put("auth.url", "${global.base_url}/auth", "auth");

        // 测试跨分组变量替换
        Assert.Equal("https://example.com/api", setting.GetStr("api.url", "api"));
        Assert.Equal("https://example.com/auth", setting.GetStr("auth.url", "auth"));
    }
}

