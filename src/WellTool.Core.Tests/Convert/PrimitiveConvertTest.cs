using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class PrimitiveConvertTest
{
    [Fact]
    public void ToIntTest()
    {
        var convert = Convert.Convert<int>("123");
        Assert.Equal(123, convert);
    }

    [Fact]
    public void ToLongTest()
    {
        var convert = Convert.Convert<long>("123456789");
        Assert.Equal(123456789L, convert);
    }

    [Fact]
    public void ToDoubleTest()
    {
        var convert = Convert.Convert<double>("123.456");
        Assert.Equal(123.456, convert, 0.001);
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(Convert.ToBool("true"));
        Assert.False(Convert.ToBool("false"));
    }
}
