using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class EnumUtilTest
{
    [Fact]
    public void ParseTest()
    {
        var status = EnumUtil.Parse<TestStatus>("Active");
        Assert.Equal(TestStatus.Active, status);
    }

    [Fact]
    public void GetNamesTest()
    {
        var names = EnumUtil.GetNames<TestStatus>();
        Assert.Contains("Active", names);
        Assert.Contains("Inactive", names);
    }

    [Fact]
    public void GetValuesTest()
    {
        var values = EnumUtil.GetValues<TestStatus>();
        Assert.Contains(TestStatus.Active, values);
        Assert.Contains(TestStatus.Inactive, values);
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
        Inactive,
        Deleted
    }
}
