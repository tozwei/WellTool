using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class StringConvertTest
{
    [Fact]
    public void TimezoneToStrTest()
    {
        var s = Convert.ToStr(TimeZoneInfo.GetSystemTimeZone("Asia/Shanghai"));
        Assert.Equal("Asia/Shanghai", s);
    }

    [Fact]
    public void ToStrTest()
    {
        Assert.Equal("123", Convert.ToStr(123));
        Assert.Equal("abc", Convert.ToStr('a') + "bc");
    }

    [Fact]
    public void ToStrWithCharsetTest()
    {
        var bytes = "Hello"u8.ToArray();
        var s = Convert.ToStr(bytes, System.Text.Encoding.UTF8);
        Assert.Equal("Hello", s);
    }
}
