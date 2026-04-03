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
        var value = Convert.ToDate(a);
        Assert.Equal(a, DateUtil.FormatDate(value));
    }

    [Fact]
    public void ToDateFromLongTest()
    {
        var timeLong = DateUtil.Date().GetTime();
        var value = Convert.ToDate(timeLong);
        Assert.Equal(timeLong, value.GetTime());
    }

    [Fact]
    public void ToSqlDateTest()
    {
        var a = "2017-05-06";
        var value = Convert.ToSqlDate(a);
        Assert.Equal("2017-05-06", value.ToString());
    }

    [Fact]
    public void ToLocalDateTimeTest()
    {
        var str = "2020-12-12 12:12:12";
        var ldt = Convert.ToLocalDateTime(str);
        Assert.NotNull(ldt);
    }
}
