using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertTest
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
        Assert.Equal(123L, Convert.ToLong("123"));
        Assert.Equal(0L, Convert.ToLong(null));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Assert.Equal(123.45, Convert.ToDouble("123.45"), 0.001);
        Assert.Equal(0.0, Convert.ToDouble(null));
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(Convert.ToBool("true"));
        Assert.True(Convert.ToBool("1"));
        Assert.False(Convert.ToBool("false"));
        Assert.False(Convert.ToBool("0"));
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", Convert.ToStr(123));
        Assert.Equal("abc", Convert.ToStr("abc"));
        Assert.Equal("", Convert.ToStr(null));
    }

    [Fact]
    public void ToListTest()
    {
        var list = Convert.ToList("a,b,c", ',');
        Assert.Equal(3, list.Count);
        Assert.Equal("a", list[0]);
        Assert.Equal("b", list[1]);
        Assert.Equal("c", list[2]);
    }

    [Fact]
    public void ToDateTest()
    {
        var date = Convert.ToDate("2021-01-01");
        Assert.NotNull(date);
        Assert.Equal(2021, date.Year);
        Assert.Equal(1, date.Month);
        Assert.Equal(1, date.Day);
    }

    [Fact]
    public void ToByteTest()
    {
        Assert.Equal((byte)65, Convert.ToByte("65"));
        Assert.Equal((byte)0, Convert.ToByte(null));
    }
}
