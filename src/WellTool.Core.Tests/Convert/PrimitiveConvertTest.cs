using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class PrimitiveConvertTest
{
    [Fact]
    public void ToIntTest()
    {
        var convert = WellTool.Core.Convert.Convert.ToInt("123");
        Assert.Equal(123, convert);
    }

    [Fact]
    public void ToLongTest()
    {
        var convert = WellTool.Core.Convert.Convert.ToLong("123456789");
        Assert.Equal(123456789L, convert);
    }

    [Fact]
    public void ToDoubleTest()
    {
        var convert = WellTool.Core.Convert.Convert.ToDouble("123.456");
        Assert.Equal(123.456, convert, 0.001);
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(WellTool.Core.Convert.Convert.ToBool("true"));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("false"));
    }
}
