using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertOtherTest
{
    [Fact]
    public void ToCharTest()
    {
        Assert.Equal('A', Convert.ToChar("A"));
        Assert.Equal('a', Convert.ToChar(97));
    }

    [Fact]
    public void ToDateTest()
    {
        var date = Convert.ToDate("2021-01-01");
        Assert.Equal(2021, date.Year);
        Assert.Equal(1, date.Month);
        Assert.Equal(1, date.Day);
    }

    [Fact]
    public void ToBytesTest()
    {
        var bytes = Convert.ToBytes("Hello");
        Assert.Equal(5, bytes.Length);
    }

    [Fact]
    public void ToBigIntegerTest()
    {
        var bi = Convert.ToBigInteger("12345678901234567890");
        Assert.NotNull(bi);
    }

    [Fact]
    public void ToBigDecimalTest()
    {
        var bd = Convert.ToBigDecimal("123.456789");
        Assert.NotNull(bd);
    }

    [Fact]
    public void ToSBCTest()
    {
        var sbc = Convert.ToSBC("abc123");
        Assert.NotEqual("abc123", sbc);
    }

    [Fact]
    public void ToDBCTest()
    {
        var dbc = Convert.ToDBC("ａｂｃ１２３");
        Assert.NotEqual("ａｂｃ１２３", dbc);
    }
}
