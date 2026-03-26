using WellTool.Setting;
using Xunit;
using System.IO;
using System;
using System.Reflection;

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
        // 使用测试程序集的路径来导航到项目目录
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var projectDir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(assemblyPath)!, "..", "..", ".."));
        var testDataPath = Path.Combine(projectDir, "WellTool.Setting.Tests", "TestData", "simple.test.setting");
        
        Console.WriteLine($"Assembly path: {assemblyPath}");
        Console.WriteLine($"Project directory: {projectDir}");
        Console.WriteLine($"Test data path: {testDataPath}");
        Console.WriteLine($"File exists: {File.Exists(testDataPath)}");
        
        // 读取文件内容，检查是否正确
        if (File.Exists(testDataPath))
        {
            var content = File.ReadAllText(testDataPath);
            Console.WriteLine($"File content:\n{content}");
        }
        
        var setting = SettingUtil.Get(testDataPath);
        var groups = setting.Groups();
        Console.WriteLine($"Groups: {string.Join(", ", groups)}");
        
        var driver = setting.GetByGroup("driver", "demo");
        Console.WriteLine($"Driver value: {driver}");
        
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
        var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData");
        var nonexistentPath = Path.Combine(testDataPath, "nonexistent.setting");
        var testPath = Path.Combine(testDataPath, "test.setting");
        
        var setting = SettingUtil.GetFirstFound(nonexistentPath, testPath);
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
        var testDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "test.setting");
        var setting1 = SettingUtil.Get(testDataPath);
        var setting2 = SettingUtil.Get(testDataPath);

        // 应该是同一个实例（缓存）
        Assert.Same(setting1, setting2);
    }
}
