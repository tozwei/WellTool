using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class EnumUtilTest
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
    public void GetDescriptionsTest()
    {
        var descriptions = EnumUtil.GetDescriptions<TestStatus>();
        Assert.NotEmpty(descriptions);
    }

    [Fact]
    public void GetDescriptionTest()
    {
        var description = EnumUtil.GetDescription(TestStatus.Active);
        Assert.NotNull(description);
    }

    [Fact]
    public void IsValidTest()
    {
        Assert.True(EnumUtil.IsValid<TestStatus>("Active"));
        Assert.False(EnumUtil.IsValid<TestStatus>("Invalid"));
    }

    [Fact]
    public void GetIndexTest()
    {
        var index = EnumUtil.GetIndex(TestStatus.Active);
        Assert.Equal(0, index);
    }

    public enum TestStatus
    {
        Active,
        Inactive,
        Deleted
    }
}
