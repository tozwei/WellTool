using WellTool.Core.Convert;
using Xunit;


namespace WellTool.Core.Convert.Tests;


public class ConvertToBooleanTest
{
    [Fact]
    public void ToBoolTest()
    {
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("true"));
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("1"));
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("yes"));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("false"));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("0"));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("no"));
    }

    [Fact]
    public void ToBoolDefaultTest()
    {
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("invalid", true));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("invalid", false));
    }

    [Fact]
    public void BoolToStringTest()
    {
        Assert.Equal("true", WellTool.Core.Convert.ConvertUtil.ToStr(true));
        Assert.Equal("false", WellTool.Core.Convert.ConvertUtil.ToStr(false));
    }
}
