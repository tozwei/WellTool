using WellTool.Setting.Dialect;
using Xunit;

namespace WellTool.Setting.Tests;

/// <summary>
/// PropsUtil 单元测试
/// </summary>
public class PropsUtilTest
{
    /// <summary>
    /// 测试 PropsUtil.Get 方法
    /// </summary>
    [Fact]
    public void GetTest()
    {
        var props = PropsUtil.GetProp("TestData/test.properties");
        var driver = props.GetStr("driver");
        Assert.Equal("com.mysql.jdbc.Driver", driver);
    }

    /// <summary>
    /// 测试 PropsUtil.GetFirstFound 方法
    /// </summary>
    [Fact]
    public void GetFirstFoundTest()
    {
        // 测试查找第一个存在的文件
        // 先找不存在的文件，再找存在的文件
        var props = PropsUtil.GetFirstFoundProp("TestData/nonexistent.properties", "TestData/test.properties");
        Assert.NotNull(props);

        using (props)
        {
            var driver = props.GetStr("driver");
            Assert.Equal("com.mysql.jdbc.Driver", driver);
        }
    }
}
