using Xunit;
using System;

namespace WellTool.Core.Tests;

public class ConvertToNumberTest
{
    [Fact]
    public void ToIntTest()
    {
        Xunit.Assert.Equal(123, int.Parse("123"));
        Xunit.Assert.Equal(-123, int.Parse("-123"));
    }

    [Fact]
    public void ToLongTest()
    {
        Xunit.Assert.Equal(123456789L, long.Parse("123456789"));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Xunit.Assert.Equal(123.45, double.Parse("123.45"), 0.001);
    }

    [Fact]
    public void ToFloatTest()
    {
        Xunit.Assert.Equal(123.45f, float.Parse("123.45"), 0.001f);
    }

    [Fact]
    public void ToDecimalTest()
    {
        Xunit.Assert.Equal(123.45m, decimal.Parse("123.45"));
    }

    [Fact]
    public void ToShortTest()
    {
        Xunit.Assert.Equal((short)123, short.Parse("123"));
    }

    [Fact]
    public void ToByteTest()
    {
        Xunit.Assert.Equal((byte)65, byte.Parse("65"));
    }

    [Fact]
    public void NumberToStringTest()
    {
        Xunit.Assert.Equal("123", 123.ToString());
        Xunit.Assert.Equal("123.45", 123.45.ToString());
    }
}
