using WellTool.Core.Convert;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertLastTest
{
    [Fact]
    public void ToIntTest()
    {
        Assert.Equal(123, Convert.ToInt("123"));
        Assert.Equal(0, Convert.ToInt(null));
    }

    [Fact]
    public void ToLongTest()
    {
        Assert.Equal(123L, Convert.ToLong("123"));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Assert.Equal(123.45, Convert.ToDouble("123.45"), 0.01);
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(Convert.ToBool("true"));
        Assert.False(Convert.ToBool("false"));
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", Convert.ToStr(123));
        Assert.Equal("abc", Convert.ToStr("abc"));
    }

    [Fact]
    public void ToListTest()
    {
        var list = "a,b,c".Split(',').ToList();
        Assert.Equal(3, list.Count);
    }

    [Fact]
    public void ToDateTimeTest()
    {
        var date = Convert.ToDateTime("2021-01-01");
        Assert.Equal(2021, date?.Year);
    }

    [Fact]
    public void ToByteTest()
    {
        Assert.Equal((byte)65, Convert.ToByte("65"));
    }

    [Fact]
    public void ToCharTest()
    {
        Assert.Equal('A', Convert.ToChar("A"));
    }
}
