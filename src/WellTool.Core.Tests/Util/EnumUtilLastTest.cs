using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class EnumUtilLastTest
{
    [Fact]
    public void ParseTest()
    {
        var status = EnumUtil.Parse<TestStatus>("Active");
        Assert.Equal(TestStatus.Active, status);
    }

    [Fact]
    public void GetNameTest()
    {
        var name = Enum.GetName(TestStatus.Active);
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
    public void IsDefinedTest()
    {
        Assert.True(EnumUtil.IsDefined<TestStatus>("Active"));
        Assert.False(EnumUtil.IsDefined<TestStatus>("Invalid"));
    }

    public enum TestStatus
    {
        Active,
        Inactive
    }
}
