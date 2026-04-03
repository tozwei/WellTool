using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateTimeTest
{
    [Fact]
    public void DateTimeParseTest()
    {
        var dateTime = new DateTime("2017-01-05 12:34:23", DateTimePattern.NormDateTimeFormat);

        // 年
        int year = dateTime.Year;
        Assert.Equal(2017, year);

        // 季度
        var season = dateTime.QuarterEnum;
        Assert.Equal(Quarter.Q1, season);

        // 月份
        var month = dateTime.MonthEnum;
        Assert.Equal(Month.January, month);

        // 日
        int day = dateTime.DayOfMonth;
        Assert.Equal(5, day);
    }

    [Fact]
    public void DateTimeParseTest2()
    {
        var dateTime = new DateTime("2017-01-05 12:34:23");

        // 年
        int year = dateTime.Year;
        Assert.Equal(2017, year);

        // 季度
        var season = dateTime.QuarterEnum;
        Assert.Equal(Quarter.Q1, season);
    }

    [Fact]
    public void QuarterTest()
    {
        var dateTime = new DateTime("2017-01-05 12:34:23", DateTimePattern.NormDateTimeFormat);
        var quarter = dateTime.QuarterEnum;
        Assert.Equal(Quarter.Q1, quarter);

        dateTime = new DateTime("2017-04-05 12:34:23");
        Assert.Equal(Quarter.Q2, dateTime.QuarterEnum);

        dateTime = new DateTime("2017-07-05 12:34:23");
        Assert.Equal(Quarter.Q3, dateTime.QuarterEnum);

        dateTime = new DateTime("2017-10-05 12:34:23");
        Assert.Equal(Quarter.Q4, dateTime.QuarterEnum);
    }

    [Fact]
    public void MonthTest()
    {
        var dateTime = new DateTime("2017-01-05 12:34:23");
        Assert.Equal(Month.January, dateTime.MonthEnum);

        dateTime = new DateTime("2017-06-05 12:34:23");
        Assert.Equal(Month.June, dateTime.MonthEnum);

        dateTime = new DateTime("2017-12-05 12:34:23");
        Assert.Equal(Month.December, dateTime.MonthEnum);
    }

    [Fact]
    public void AddTest()
    {
        var dateTime = new DateTime("2017-01-05 12:34:23");
        var added = dateTime.AddDays(5);
        Assert.Equal(10, added.Day);
    }

    [Fact]
    public void IsWeekendTest()
    {
        var saturday = new DateTime("2021-01-02"); // Saturday
        Assert.True(saturday.IsWeekend());

        var monday = new DateTime("2021-01-04"); // Monday
        Assert.False(monday.IsWeekend());
    }

    [Fact]
    public void IsWorkdayTest()
    {
        var saturday = new DateTime("2021-01-02"); // Saturday
        Assert.False(saturday.IsWorkday());

        var monday = new DateTime("2021-01-04"); // Monday
        Assert.True(monday.IsWorkday());
    }
}
