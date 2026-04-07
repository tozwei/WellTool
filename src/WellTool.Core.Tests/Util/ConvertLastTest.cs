using WellTool.Core.Convert;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Core.Tests;

public class ConvertLastTest
{
    [Fact]
    public void ToIntTest()
    {
        Assert.Equal(123, WellTool.Core.Convert.ConvertUtil.ToInt("123"));
        Assert.Equal(0, WellTool.Core.Convert.ConvertUtil.ToInt(null));
    }

    [Fact]
    public void ToLongTest()
    {
        Assert.Equal(123L, WellTool.Core.Convert.ConvertUtil.ToLong("123"));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Assert.Equal(123.45, WellTool.Core.Convert.ConvertUtil.ToDouble("123.45"), 0.01);
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("true"));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("false"));
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", WellTool.Core.Convert.ConvertUtil.ToStr(123));
        Assert.Equal("abc", WellTool.Core.Convert.ConvertUtil.ToStr("abc"));
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
        var date = WellTool.Core.Convert.ConvertUtil.ToDateTime("2021-01-01");
        Assert.Equal(2021, date?.Year);
    }

    [Fact]
    public void ToByteTest()
    {
        Assert.Equal((byte)65, WellTool.Core.Convert.ConvertUtil.ToByte("65"));
    }

    [Fact]
    public void ToCharTest()
    {
        Assert.Equal('A', WellTool.Core.Convert.ConvertUtil.ToChar("A"));
    }
}
