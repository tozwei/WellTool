using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateUtilTest
{
    [Fact]
    public void ParseTest()
    {
        var date = DateTime.Parse("2021-01-01");
        Assert.Equal(2021, date.Year);
        Assert.Equal(1, date.Month);
        Assert.Equal(1, date.Day);
    }

    [Fact]
    public void ParseDateTimeTest()
    {
        var dateTime = DateTime.Parse("2021-01-01 12:30:45");
        Assert.Equal(12, dateTime.Hour);
        Assert.Equal(30, dateTime.Minute);
        Assert.Equal(45, dateTime.Second);
    }

    [Fact]
    public void FormatTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var formatted = date.ToString("yyyy-MM-dd");
        Assert.Equal("2021-01-01", formatted);
    }

    [Fact]
    public void OffsetTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var offset = date.AddDays(10);
        Assert.Equal(11, offset.Day);
    }

    [Fact]
    public void CurrentTest()
    {
        var now = DateTime.Now;
        Assert.True(now <= DateTime.Now);
    }

    [Fact]
    public void CurrentMillisTest()
    {
        var millis = DateUtil.CurrentMillis();
        Assert.True(millis > 0);
    }

    [Fact]
    public void CurrentSecondsTest()
    {
        var seconds = DateUtil.CurrentSeconds();
        Assert.True(seconds > 0);
    }

    [Fact]
    public void CompareTest()
    {
        var date1 = DateTime.Parse("2021-01-01");
        var date2 = DateTime.Parse("2021-01-02");
        Assert.True(date1 < date2);
    }
}
