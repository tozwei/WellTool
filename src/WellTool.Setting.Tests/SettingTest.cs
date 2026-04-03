namespace WellTool.Setting.Tests;

using Well.Setting;
using Xunit;

public class SettingTest
{
    [Fact]
    public void GetStrTest()
    {
        var setting = new Setting();
        var value = setting.GetStr("key", "default");
        Assert.Equal("default", value);
    }

    [Fact]
    public void GetIntTest()
    {
        var setting = new Setting();
        var value = setting.GetInt("key", 123);
        Assert.Equal(123, value);
    }

    [Fact]
    public void GetBoolTest()
    {
        var setting = new Setting();
        var value = setting.GetBool("key", true);
        Assert.True(value);
    }

    [Fact]
    public void GetDoubleTest()
    {
        var setting = new Setting();
        var value = setting.GetDouble("key", 1.5);
        Assert.Equal(1.5, value);
    }

    [Fact]
    public void GetLongTest()
    {
        var setting = new Setting();
        var value = setting.GetLong("key", 999);
        Assert.Equal(999, value);
    }

    [Fact]
    public void GetGroupTest()
    {
        var setting = new Setting();
        var group = setting.GetGroup("test");
        Assert.NotNull(group);
    }

    [Fact]
    public void SetTest()
    {
        var setting = new Setting();
        setting.Set("key", "value");
        Assert.Equal("value", setting.GetStr("key"));
    }

    [Fact]
    public void SaveTest()
    {
        var setting = new Setting();
        setting.Set("key", "value");
        var tempFile = Path.GetTempFileName();
        try
        {
            setting.Save(tempFile);
            Assert.True(File.Exists(tempFile));
        }
        finally
        {
            if (File.Exists(tempFile))
                File.Delete(tempFile);
        }
    }

    [Fact]
    public void CloneTest()
    {
        var setting = new Setting();
        setting.Set("key", "value");
        var cloned = setting.Clone();
        Assert.Equal("value", cloned.GetStr("key"));
    }

    [Fact]
    public void GetSectionsTest()
    {
        var setting = new Setting();
        var sections = setting.GetSections();
        Assert.NotNull(sections);
    }

    [Fact]
    public void ContainsKeyTest()
    {
        var setting = new Setting();
        setting.Set("test", "value");
        Assert.True(setting.ContainsKey("test"));
        Assert.False(setting.ContainsKey("nonexistent"));
    }

    [Fact]
    public void RemoveTest()
    {
        var setting = new Setting();
        setting.Set("test", "value");
        setting.Remove("test");
        Assert.False(setting.ContainsKey("test"));
    }

    [Fact]
    public void ClearTest()
    {
        var setting = new Setting();
        setting.Set("key1", "value1");
        setting.Set("key2", "value2");
        setting.Clear();
        Assert.False(setting.ContainsKey("key1"));
        Assert.False(setting.ContainsKey("key2"));
    }
}
