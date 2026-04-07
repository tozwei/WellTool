using WellTool.Core.Convert;
using Xunit;

public class ConvertToBooleanTest
{
    [Fact]
    public void ToBoolTest()
    {
        Assert.True(WellTool.Core.Convert.Convert.ToBool("true"));
        Assert.True(WellTool.Core.Convert.Convert.ToBool("1"));
        Assert.True(WellTool.Core.Convert.Convert.ToBool("yes"));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("false"));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("0"));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("no"));
    }

    [Fact]
    public void ToBoolDefaultTest()
    {
        Assert.True(WellTool.Core.Convert.Convert.ToBool("invalid", true));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("invalid", false));
    }

    [Fact]
    public void BoolToStringTest()
    {
        Assert.Equal("true", WellTool.Core.Convert.Convert.ToStr(true));
        Assert.Equal("false", WellTool.Core.Convert.Convert.ToStr(false));
    }
}
