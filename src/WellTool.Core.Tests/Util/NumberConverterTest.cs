using WellTool.Core.Convert.impl;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class NumberConverterTest
{
    [Fact]
    public void ConvertTest()
    {
        var converter = new NumberConverter(typeof(int));
        Assert.Equal(123, (int)converter.Convert("123", typeof(int)));
    }

    [Fact]
    public void ParseTest()
    {
        var converter = new NumberConverter(typeof(double));
        Assert.Equal(123.45, (double)converter.Convert("123.45", typeof(double)), 0.001);
    }

    [Fact]
    public void ToStringTest()
    {
        var value = 123;
        Assert.Equal("123", value.ToString());
    }
}
