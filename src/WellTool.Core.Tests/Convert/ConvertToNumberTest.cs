using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertToNumberTest
{
    [Fact]
    public void ToIntTest()
    {
        Assert.Equal(123, Convert.ToInt("123"));
        Assert.Equal(0, Convert.ToInt(null));
        Assert.Equal(-123, Convert.ToInt("-123"));
    }

    [Fact]
    public void ToLongTest()
    {
        Assert.Equal(123456789L, Convert.ToLong("123456789"));
        Assert.Equal(0L, Convert.ToLong(null));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Assert.Equal(123.45, Convert.ToDouble("123.45"), 0.001);
        Assert.Equal(0.0, Convert.ToDouble(null));
    }

    [Fact]
    public void ToFloatTest()
    {
        Assert.Equal(123.45f, Convert.ToFloat("123.45"), 0.001f);
    }

    [Fact]
    public void ToDecimalTest()
    {
        Assert.Equal(123.45m, Convert.ToDecimal("123.45"));
    }

    [Fact]
    public void ToShortTest()
    {
        Assert.Equal((short)123, Convert.ToShort("123"));
    }

    [Fact]
    public void ToByteTest()
    {
        Assert.Equal((byte)65, Convert.ToByte("65"));
    }

    [Fact]
    public void NumberToStringTest()
    {
        Assert.Equal("123", Convert.ToStr(123));
        Assert.Equal("123.45", Convert.ToStr(123.45));
    }
}
