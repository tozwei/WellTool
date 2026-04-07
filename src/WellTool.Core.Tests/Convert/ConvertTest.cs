using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests.Convert;

public class ConvertTest
{
    [Fact]
    public void ToIntTest()
    {
        Assert.Equal(123, WellTool.Core.Convert.Convert.ToInt("123"));
        Assert.Equal(0, WellTool.Core.Convert.Convert.ToInt(null));
        Assert.Equal(-123, WellTool.Core.Convert.Convert.ToInt("-123"));
    }

    [Fact]
    public void ToLongTest()
    {
        Assert.Equal(123L, WellTool.Core.Convert.Convert.ToLong("123"));
        Assert.Equal(0L, WellTool.Core.Convert.Convert.ToLong(null));
    }

    [Fact]
    public void ToDoubleTest()
    {
        Assert.Equal(123.45, WellTool.Core.Convert.Convert.ToDouble("123.45"), 0.001);
        Assert.Equal(0.0, WellTool.Core.Convert.Convert.ToDouble(null));
    }

    [Fact]
    public void ToBoolTest()
    {
        Assert.True(WellTool.Core.Convert.Convert.ToBool("true"));
        Assert.True(WellTool.Core.Convert.Convert.ToBool("1"));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("false"));
        Assert.False(WellTool.Core.Convert.Convert.ToBool("0"));
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", WellTool.Core.Convert.Convert.ToStr(123));
        Assert.Equal("abc", WellTool.Core.Convert.Convert.ToStr("abc"));
        Assert.Equal("", WellTool.Core.Convert.Convert.ToStr(null));
    }

    [Fact]
    public void ToDateTimeTest()
    {
        var date = WellTool.Core.Convert.Convert.ToDateTime("2021-01-01");
        Assert.NotNull(date);
        Assert.Equal(2021, date.Value.Year);
        Assert.Equal(1, date.Value.Month);
        Assert.Equal(1, date.Value.Day);
    }

    [Fact]
    public void ToByteTest()
    {
        Assert.Equal((byte)65, WellTool.Core.Convert.Convert.ToByte("65"));
        Assert.Equal((byte)0, WellTool.Core.Convert.Convert.ToByte(null));
    }

    [Fact]
    public void ToCharTest()
    {
        Assert.Equal('A', WellTool.Core.Convert.Convert.ToChar("A"));
    }

    [Fact]
    public void ToHexTest()
    {
        var hex = WellTool.Core.Convert.Convert.ToHex("Hello", System.Text.Encoding.UTF8);
        Assert.NotNull(hex);
    }

    [Fact]
    public void HexToBytesTest()
    {
        var bytes = WellTool.Core.Convert.Convert.HexToBytes("48656C6C6F");
        Assert.Equal("Hello", System.Text.Encoding.UTF8.GetString(bytes));
    }
}