using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertToBooleanTest
{
    [Fact]
    public void ToBoolTest()
    {
        Assert.True(Convert.ToBool("true"));
        Assert.True(Convert.ToBool("True"));
        Assert.True(Convert.ToBool("1"));
        Assert.True(Convert.ToBool("yes"));
        Assert.True(Convert.ToBool("on"));

        Assert.False(Convert.ToBool("false"));
        Assert.False(Convert.ToBool("0"));
        Assert.False(Convert.ToBool("no"));
        Assert.False(Convert.ToBool(""));
    }

    [Fact]
    public void ToBoolDefaultTest()
    {
        Assert.False(Convert.ToBool("invalid"));
        Assert.False(Convert.ToBool(null));
    }

    [Fact]
    public void BoolToStringTest()
    {
        Assert.Equal("true", Convert.ToStr(true));
        Assert.Equal("false", Convert.ToStr(false));
    }
}
