using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateTimeTest
{
    [Fact]
    public void DateTimeParseTest()
    {
        var dateTime = DateTime.Parse("2017-01-05 12:34:23");

        // 年
        int year = dateTime.Year;
        Assert.Equal(2017, year);

        // 日
        int day = dateTime.Day;
        Assert.Equal(5, day);
    }

    [Fact]
    public void DateTimeParseTest2()
    {
        var dateTime = DateTime.Parse("2017-01-05 12:34:23");

        // 年
        int year = dateTime.Year;
        Assert.Equal(2017, year);
    }

    [Fact]
    public void QuarterTest()
    {
        var dateTime = DateTime.Parse("2017-01-05 12:34:23");
        Xunit.Assert.Equal(1, (dateTime.Month - 1) / 3 + 1);

        dateTime = DateTime.Parse("2017-04-05 12:34:23");
        Xunit.Assert.Equal(2, (dateTime.Month - 1) / 3 + 1);

        dateTime = DateTime.Parse("2017-07-05 12:34:23");
        Xunit.Assert.Equal(3, (dateTime.Month - 1) / 3 + 1);

        dateTime = DateTime.Parse("2017-10-05 12:34:23");
        Xunit.Assert.Equal(4, (dateTime.Month - 1) / 3 + 1);
    }

    [Fact]
    public void MonthTest()
    {
        var dateTime = DateTime.Parse("2017-01-05 12:34:23");
        Xunit.Assert.Equal(1, dateTime.Month);

        dateTime = DateTime.Parse("2017-06-05 12:34:23");
        Xunit.Assert.Equal(6, dateTime.Month);

        dateTime = DateTime.Parse("2017-12-05 12:34:23");
        Xunit.Assert.Equal(12, dateTime.Month);
    }

    [Fact]
    public void AddTest()
    {
        var dateTime = DateTime.Parse("2017-01-05 12:34:23");
        var added = dateTime.AddDays(5);
        Assert.Equal(10, added.Day);
    }

    [Fact]
    public void IsWeekendTest()
    {
        var saturday = DateTime.Parse("2021-01-02"); // Saturday
        var isWeekend = DateUtil.IsWeekend(saturday);
        Assert.True(isWeekend);

        var monday = DateTime.Parse("2021-01-04"); // Monday
        var isWeekend2 = DateUtil.IsWeekend(monday);
        Assert.False(isWeekend2);
    }

    [Fact]
    public void IsWorkdayTest()
    {
        var saturday = DateTime.Parse("2021-01-02"); // Saturday
        var isWorkday = !DateUtil.IsWeekend(saturday); // 工作日是周末的反面
        Assert.False(isWorkday);

        var monday = DateTime.Parse("2021-01-04"); // Monday
        var isWorkday2 = !DateUtil.IsWeekend(monday);
        Assert.True(isWorkday2);
    }
}
