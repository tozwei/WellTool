using Xunit;
using WellTool.Core;
using WellTool.Core.Util;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Version工具单元测试
    /// </summary>
    public class VersionUtilTest
    {
        [Fact]
    public void IsGreaterOrEqualTest()
    {
        Assert.True(VersionUtil.IsGreaterOrEqual("1.0.0", "1.0.0"));
        Assert.True(VersionUtil.IsGreaterOrEqual("1.1.0", "1.0.0"));
        Assert.True(VersionUtil.IsGreaterOrEqual("2.0.0", "1.0.0"));
        Assert.False(VersionUtil.IsGreaterOrEqual("0.9.0", "1.0.0"));
    }

    [Fact]
    public void IsGreaterTest()
    {
        Assert.False(VersionUtil.IsGreater("1.0.0", "1.0.0"));
        Assert.True(VersionUtil.IsGreater("1.1.0", "1.0.0"));
        Assert.True(VersionUtil.IsGreater("2.0.0", "1.0.0"));
        Assert.False(VersionUtil.IsGreater("0.9.0", "1.0.0"));
    }

    [Fact]
    public void IsLessOrEqualTest()
    {
        Assert.True(VersionUtil.IsLessOrEqual("1.0.0", "1.0.0"));
        Assert.True(VersionUtil.IsLessOrEqual("0.9.0", "1.0.0"));
        Assert.False(VersionUtil.IsLessOrEqual("2.0.0", "1.0.0"));
    }

    [Fact]
    public void IsLessTest()
    {
        Assert.False(VersionUtil.IsLess("1.0.0", "1.0.0"));
        Assert.True(VersionUtil.IsLess("0.9.0", "1.0.0"));
        Assert.False(VersionUtil.IsLess("2.0.0", "1.0.0"));
    }

    [Fact]
    public void CompareTest()
    {
        Assert.Equal(0, VersionUtil.Compare("1.0.0", "1.0.0"));
        Assert.True(VersionUtil.Compare("1.1.0", "1.0.0") > 0);
        Assert.True(VersionUtil.Compare("0.9.0", "1.0.0") < 0);
    }

    [Fact]
    public void IsValidTest()
    {
        Assert.True(VersionUtil.IsValid("1.0.0"));
        Assert.True(VersionUtil.IsValid("1.0"));
        Assert.True(VersionUtil.IsValid("1"));
        Assert.False(VersionUtil.IsValid(""));
        Assert.False(VersionUtil.IsValid("invalid"));
    }
    }
}
