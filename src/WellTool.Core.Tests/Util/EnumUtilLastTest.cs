using WellTool.Core.Enum;
using Xunit;

namespace WellTool.Core.Tests;

public class EnumUtilLastTest
{
    [Fact]
    public void GetEnumTest()
    {
        var status = EnumUtil.GetEnum<TestStatus>("Active");
        Assert.Equal(TestStatus.Active, status);
    }

    [Fact]
    public void GetNameTest()
    {
        var name = EnumUtil.GetName(TestStatus.Active);
        Assert.Equal("Active", name);
    }

    [Fact]
    public void GetNamesTest()
    {
        var names = EnumUtil.GetNames<TestStatus>();
        Assert.Contains("Active", names);
    }

    [Fact]
    public void GetValuesTest()
    {
        var values = EnumUtil.GetValues<TestStatus>();
        Assert.Contains(TestStatus.Active, values);
    }

    [Fact]
    public void IsValidTest()
    {
        Assert.True(EnumUtil.IsValid<TestStatus>("Active"));
        Assert.False(EnumUtil.IsValid<TestStatus>("Invalid"));
    }

    public enum TestStatus
    {
        Active,
        Inactive
    }
}
