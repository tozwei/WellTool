using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateUtilLastTest
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
    public void BetweenTest()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 11:00:00");
        var between = DateBetween.Between(start, end);
        Assert.Equal(60, between.GetTotalMinutes());
    }

    [Fact]
    public void AddDaysTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var result = DateUtil.AddDays(date, 10);
        Assert.Equal(11, result.Day);
    }

    [Fact]
    public void AddMonthsTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var result = DateUtil.AddMonths(date, 2);
        Assert.Equal(3, result.Month);
    }

    [Fact]
    public void AddYearsTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var result = DateUtil.AddYears(date, 1);
        Assert.Equal(2022, result.Year);
    }

    [Fact]
    public void GetDaysInMonthTest()
    {
        Assert.Equal(31, DateUtil.GetDaysInMonth(2021, 1));
        Assert.Equal(28, DateUtil.GetDaysInMonth(2021, 2));
    }

    [Fact]
    public void IsLeapYearTest()
    {
        Assert.True(DateUtil.IsLeapYear(2020));
        Assert.False(DateUtil.IsLeapYear(2021));
    }

    [Fact]
    public void GetDayOfYearTest()
    {
        var date = DateTime.Parse("2021-03-01");
        Assert.Equal(60, DateUtil.GetDayOfYear(date));
    }

    [Fact]
    public void IsSameDayTest()
    {
        var date1 = DateTime.Parse("2021-01-01 10:00:00");
        var date2 = DateTime.Parse("2021-01-01 20:00:00");
        Assert.True(DateUtil.IsSameDay(date1, date2));
    }

    [Fact]
    public void DateRangeTest()
    {
        var start = DateTime.Parse("2021-01-01");
        var end = DateTime.Parse("2021-01-05");
        var range = new DateRange(start, end, DateField.Day);
        Assert.Equal(5, range.Count());
    }

    [Fact]
    public void QuarterTest()
    {
        var q1 = new DateTime(2021, 1, 15);
        var q2 = new DateTime(2021, 4, 15);
        Assert.Equal(Quarter.Q1, DateUtil.GetQuarterEnum(q1));
        Assert.Equal(Quarter.Q2, DateUtil.GetQuarterEnum(q2));
    }
}
