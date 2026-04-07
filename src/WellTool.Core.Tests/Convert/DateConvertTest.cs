using WellTool.Core.Convert;
using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateConvertTest
{
    [Fact]
    public void ToDateTest()
    {
        var a = "2017-05-06";
        var value = WellTool.Core.Convert.Convert.ToDateTime(a);
        Assert.NotNull(value);
    }

    [Fact]
    public void ToDateFromLongTest()
    {
        var timeLong = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
        var value = WellTool.Core.Convert.Convert.ToDateTime(timeLong);
        Assert.NotNull(value);
    }

    [Fact]
    public void ToSqlDateTest()
    {
        var a = "2017-05-06";
        var value = WellTool.Core.Convert.Convert.ToDateTime(a);
        Assert.NotNull(value);
    }

    [Fact]
    public void ToLocalDateTimeTest()
    {
        var str = "2020-12-12 12:12:12";
        var value = WellTool.Core.Convert.Convert.ToDateTime(str);
        Assert.NotNull(value);
    }
}
