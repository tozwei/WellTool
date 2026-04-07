using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class StringConvertTest
{
    [Fact]
    public void TimezoneToStrTest()
    {
        var s = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time").Id;
        Assert.Equal("China Standard Time", s);
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", WellTool.Core.Convert.Convert.ToStr(123));
        Assert.Equal("abc", WellTool.Core.Convert.Convert.ToStr('a') + "bc");
    }

    [Fact]
    public void ToStrWithCharsetTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var s = System.Text.Encoding.UTF8.GetString(bytes);
        Assert.Equal("Hello", s);
    }
}
