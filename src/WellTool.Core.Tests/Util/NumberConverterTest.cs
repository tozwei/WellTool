using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class NumberConverterTest
{
    [Fact]
    public void ConvertTest()
    {
        var converter = new NumberConverter();
        Assert.Equal(123, converter.Convert("123"));
    }

    [Fact]
    public void ParseTest()
    {
        var converter = new NumberConverter();
        Assert.Equal(123.45, converter.Parse("123.45"), 0.001);
    }

    [Fact]
    public void ToStringTest()
    {
        var converter = new NumberConverter();
        Assert.Equal("123", converter.ToString(123));
    }
}
