namespace WellTool.Extra.Tests;

/// <summary>
/// ExtraUtil 测试类
/// </summary>
public class ExtraUtilTest
{
    [Fact]
    public void TestExtraUtil_Instance_NotNull()
    {
        // 测试实例不为空
        var util = new ExtraUtil();
        Assert.NotNull(util);
    }
}
