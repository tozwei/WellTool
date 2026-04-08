using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests.Convert;

public class ConvertTest
{
    [Fact]
    public void ToIntTest()
    {
        Assert.Equal(123, WellTool.Core.Convert.ConvertUtil.ToInt("123"));
        Assert.Equal(0, WellTool.Core.Convert.ConvertUtil.ToInt(null));
        Assert.Equal(-123, WellTool.Core.Convert.ConvertUtil.ToInt("-123"));
    }

    [Fact]
    public void ToLongTest()
    {
        Assert.Equal(123L, WellTool.Core.Convert.ConvertUtil.ToLong("123"));
        Assert.Equal(0L, WellTool.Core.Convert.ConvertUtil.ToLong(null));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Assert.Equal(123.45, WellTool.Core.Convert.ConvertUtil.ToDouble("123.45"), 0.001);
        Assert.Equal(0.0, WellTool.Core.Convert.ConvertUtil.ToDouble(null));
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("true"));
        Assert.True(WellTool.Core.Convert.ConvertUtil.ToBool("1"));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("false"));
        Assert.False(WellTool.Core.Convert.ConvertUtil.ToBool("0"));
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", WellTool.Core.Convert.ConvertUtil.ToStr(123));
        Assert.Equal("abc", WellTool.Core.Convert.ConvertUtil.ToStr("abc"));
        // ConvertUtil.ToStr(null) 返回 null
        Assert.Null(WellTool.Core.Convert.ConvertUtil.ToStr(null));
    }

    [Fact]
    public void ToDateTimeTest()
    {
        var date = WellTool.Core.Convert.ConvertUtil.ToDateTime("2021-01-01");
        Assert.NotNull(date);
        Assert.Equal(2021, date.Value.Year);
        Assert.Equal(1, date.Value.Month);
        Assert.Equal(1, date.Value.Day);
    }

    [Fact]
    public void ToByteTest()
    {
        Assert.Equal((byte)65, WellTool.Core.Convert.ConvertUtil.ToByte("65"));
        Assert.Equal((byte)0, WellTool.Core.Convert.ConvertUtil.ToByte(null));
    }

    [Fact]
    public void ToCharTest()
    {
        Assert.Equal('A', WellTool.Core.Convert.ConvertUtil.ToChar("A"));
    }

    [Fact]
    public void ToHexTest()
    {
        var hex = WellTool.Core.Convert.ConvertUtil.ToHex("Hello", System.Text.Encoding.UTF8);
        Assert.NotNull(hex);
    }

    [Fact]
    public void HexToBytesTest()
    {
        var bytes = WellTool.Core.Convert.ConvertUtil.HexToBytes("48656C6C6F");
        Assert.Equal("Hello", System.Text.Encoding.UTF8.GetString(bytes));
    }
}