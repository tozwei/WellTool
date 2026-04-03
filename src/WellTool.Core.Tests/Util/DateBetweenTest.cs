using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateBetweenExtraTest
{
    [Fact]
    public void BetweenTest()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 11:00:00");
        var between = DateBetween.Between(start, end);
        Assert.Equal(60, between.GetTotalMinutes());
    }

    [Fact]
    public void BetweenMsTest()
    {
        var start = DateTime.Now;
        var end = start.AddSeconds(5);
        var between = DateBetween.Between(start, end);
        Assert.True(between.GetTotalSeconds() >= 5);
    }

    [Fact]
    public void BetweenMonthTest()
    {
        var start = DateTime.Parse("2021-01-01");
        var end = DateTime.Parse("2021-04-01");
        var between = DateBetween.Between(start, end);
        Assert.Equal(3, between.GetTotalMonths());
    }

    [Fact]
    public void BetweenYearTest()
    {
        var start = DateTime.Parse("2020-01-01");
        var end = DateTime.Parse("2022-01-01");
        var between = DateBetween.Between(start, end);
        Assert.Equal(2, between.GetTotalYears());
    }

    [Fact]
    public void ToStringTest()
    {
        var start = DateTime.Parse("2021-01-01 10:00:00");
        var end = DateTime.Parse("2021-01-01 11:00:00");
        var between = DateBetween.Between(start, end);
        var str = between.ToString();
        Assert.NotEmpty(str);
    }
}
