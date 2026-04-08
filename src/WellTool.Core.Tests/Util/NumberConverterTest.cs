using WellTool.Core.Convert.impl;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class NumberConverterTest
{
    [Fact]
    public void ConvertTest()
    {
        // NumberConverter 在构造函数中指定目标类型
        var converter = new NumberConverter(typeof(int));
        var result = converter.Convert("123", typeof(int));
        Assert.Equal(123, result);
    }

    [Fact]
    public void ParseTest()
    {
        var converter = new NumberConverter(typeof(double));
        var result = converter.Convert("123.45", typeof(double));
        Assert.Equal(123.45, (double)result, 0.001);
    }

    [Fact]
    public void ToStringTest()
    {
        var value = 123;
        Assert.Equal("123", value.ToString());
    }
}
