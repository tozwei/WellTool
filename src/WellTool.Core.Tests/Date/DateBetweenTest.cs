using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateBetweenTest
{
    [Fact]
    public void TestDaysBetween()
    {
        var start = DateTime.Parse("2021-01-01");
        var end = DateTime.Parse("2021-01-10");
        var between = new DateBetween(start, end);

        Assert.Equal(9, between.Between(DateUnit.Day));
    }

    [Fact]
    public void TestHoursBetween()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 15:00:00");
        var between = new DateBetween(start, end);

        Assert.Equal(5, between.Between(DateUnit.Hour));
    }

    [Fact]
    public void TestMinutesBetween()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 10:30:00");
        var between = new DateBetween(start, end);

        Assert.Equal(30, between.Between(DateUnit.Minute));
    }

    [Fact]
    public void TestSecondsBetween()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 10:00:45");
        var between = new DateBetween(start, end);

        Assert.Equal(45, between.Between(DateUnit.Second));
    }

    [Fact]
    public void TestAbsBetween()
    {
        var start = DateTime.Parse("2021-01-10");
        var end = DateTime.Parse("2021-01-01");
        var between = new DateBetween(start, end);

        // DateBetween.Between 返回 duration.TotalDays，当 start > end 时返回负数
        Assert.Equal(-9, between.Between(DateUnit.Day));
    }

    [Fact]
    public void TestToString()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 12:30:00");
        var between = new DateBetween(start, end);

        var str = between.ToString(DateUnit.Hour, WellTool.Core.Date.BetweenFormatter.Level.HOUR);
        Assert.NotEmpty(str);
    }
}
