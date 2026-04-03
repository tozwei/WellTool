using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class CalendarUtilTest
{
    [Fact]
    public void GetDaysInMonthTest()
    {
        Assert.Equal(31, CalendarUtil.GetDaysInMonth(2021, 1));
        Assert.Equal(28, CalendarUtil.GetDaysInMonth(2021, 2));
        Assert.Equal(29, CalendarUtil.GetDaysInMonth(2020, 2));
    }

    [Fact]
    public void GetDaysInYearTest()
    {
        Assert.Equal(365, CalendarUtil.GetDaysInYear(2021));
        Assert.Equal(366, CalendarUtil.GetDaysInYear(2020));
    }

    [Fact]
    public void IsLeapYearTest()
    {
        Assert.False(CalendarUtil.IsLeapYear(2021));
        Assert.True(CalendarUtil.IsLeapYear(2020));
        Assert.True(CalendarUtil.IsLeapYear(2000));
        Assert.False(CalendarUtil.IsLeapYear(1900));
    }

    [Fact]
    public void GetWeekOfYearTest()
    {
        var date = DateTime.Parse("2021-01-01");
        Assert.True(CalendarUtil.GetWeekOfYear(date) >= 1);
    }

    [Fact]
    public void GetDayOfYearTest()
    {
        var date = DateTime.Parse("2021-03-01");
        Assert.Equal(60, CalendarUtil.GetDayOfYear(date));
    }

    [Fact]
    public void AddDaysTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var result = CalendarUtil.AddDays(date, 10);
        Assert.Equal(11, result.Day);
    }

    [Fact]
    public void AddMonthsTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var result = CalendarUtil.AddMonths(date, 2);
        Assert.Equal(3, result.Month);
    }

    [Fact]
    public void AddYearsTest()
    {
        var date = DateTime.Parse("2021-01-01");
        var result = CalendarUtil.AddYears(date, 1);
        Assert.Equal(2022, result.Year);
    }

    [Fact]
    public void GetFirstDayOfMonthTest()
    {
        var date = DateTime.Parse("2021-01-15");
        var first = CalendarUtil.GetFirstDayOfMonth(date);
        Assert.Equal(1, first.Day);
    }

    [Fact]
    public void GetLastDayOfMonthTest()
    {
        var date = DateTime.Parse("2021-01-15");
        var last = CalendarUtil.GetLastDayOfMonth(date);
        Assert.Equal(31, last.Day);
    }

    [Fact]
    public void IsSameDayTest()
    {
        var date1 = DateTime.Parse("2021-01-01 10:00:00");
        var date2 = DateTime.Parse("2021-01-01 20:00:00");
        Assert.True(CalendarUtil.IsSameDay(date1, date2));

        var date3 = DateTime.Parse("2021-01-02");
        Assert.False(CalendarUtil.IsSameDay(date1, date3));
    }
}
