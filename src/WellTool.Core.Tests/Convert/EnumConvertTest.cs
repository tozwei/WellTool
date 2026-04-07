using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class EnumConvertTest
{
    [Fact]
    public void ToEnumTest()
    {
        var status = WellTool.Core.Convert.ConvertUtil.ToEnum<TestStatus>("Active");
        Assert.Equal(TestStatus.Active, status);
    }

    [Fact]
    public void ToEnumIntTest()
    {
        var status = WellTool.Core.Convert.ConvertUtil.ToEnum<TestStatus>(0);
        Assert.Equal(TestStatus.Active, status);
    }

    [Fact]
    public void EnumToStringTest()
    {
        var str = WellTool.Core.Convert.ConvertUtil.ToStr(TestStatus.Active);
        Assert.Equal("Active", str);
    }

    [Fact]
    public void EnumToIntTest()
    {
        var num = (int)TestStatus.Active;
        Xunit.Assert.Equal(0, num);
    }

    public enum TestStatus
    {
        Active = 0,
        Inactive = 1,
        Deleted = 2
    }
}
