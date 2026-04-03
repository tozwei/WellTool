using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class PrimitiveConvertLastTest
{
    [Fact]
    public void ToIntTest()
    {
        var result = Convert.ToInt("123");
        Assert.Equal(123, result);
    }

    [Fact]
    public void ToLongTest()
    {
        var result = Convert.ToLong("123456789");
        Assert.Equal(123456789L, result);
    }

    [Fact]
    public void ToDoubleTest()
    {
        var result = Convert.ToDouble("123.45");
        Assert.Equal(123.45, result, 2);
    }
}
