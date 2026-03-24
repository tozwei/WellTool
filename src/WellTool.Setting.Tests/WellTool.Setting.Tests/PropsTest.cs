using WellTool.Setting.Dialect;
using Xunit;

namespace WellTool.Setting.Tests;

/// <summary>
/// Props 单元测试
/// </summary>
public class PropsTest
{
    /// <summary>
    /// 测试 Props 基本功能
    /// </summary>
    [Fact]
    public void PropTest()
    {
        var props = new Props("TestData/test.properties");

        var user = props.GetStr("user");
        Assert.Equal("root", user);

        var driver = props.GetStr("driver");
        Assert.Equal("com.mysql.jdbc.Driver", driver);
    }

    /// <summary>
    /// 测试类型转换
    /// </summary>
    [Fact]
    public void TypeConversionTest()
    {
        var props = new Props();

        props["port"] = "8080";
        props["debug"] = "true";
        props["rate"] = "3.14";
        props["count"] = "100";

        Assert.Equal(8080, props.GetInt("port", 0));
        Assert.True(props.GetBool("debug", false));
        Assert.Equal(3.14, props.GetDouble("rate", 0.0));
        Assert.Equal(100L, props.GetLong("count", 0L));
    }

    /// <summary>
    /// 测试注释处理
    /// </summary>
    [Fact]
    public void CommentHandlingTest()
    {
        var props = new Props();

        // 手动添加带注释的配置
        props["key1"] = "value1";
        props["#comment"] = "this is a comment";
        props["key2"] = "value2";

        Assert.Equal("value1", props.GetStr("key1"));
        Assert.Equal("value2", props.GetStr("key2"));
    }

    /// <summary>
    /// 测试分隔符（=和：）
    /// </summary>
    [Fact]
    public void SeparatorTest()
    {
        var content = @"
key1=value1
key2:value2
key3 = value3
key4 : value4
";
        var tempFile = Path.GetTempFileName();
        try
        {
            File.WriteAllText(tempFile, content);
            var props = new Props(tempFile);

            Assert.Equal("value1", props.GetStr("key1"));
            Assert.Equal("value2", props.GetStr("key2"));
            Assert.Equal("value3", props.GetStr("key3"));
            Assert.Equal("value4", props.GetStr("key4"));
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    /// <summary>
    /// 测试空值和默认值
    /// </summary>
    [Fact]
    public void NullAndDefaultTest()
    {
        var props = new Props();
        props["empty"] = "";
        props["null_key"] = null!;

        Assert.Equal("", props.GetStr("empty", "default"));
        Assert.Equal("default", props.GetStr("nonexistent", "default"));
    }
}
