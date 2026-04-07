using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class ChineseDateTest
{
    [Fact]
    public void ChineseDateParseTest()
    {
        var date = new ChineseDate(DateTime.Parse("2020-01-25"));
        Assert.Equal("2020-01-25 00:00:00", date.GregorianDate.ToString("yyyy-MM-dd HH:mm:ss"));
        Assert.Equal(2020, date.ChineseYear);

        Assert.Equal(1, date.Month);
        Assert.Equal("正月", date.ChineseMonthName);

        Assert.Equal(1, date.Day);
        Assert.Equal("初一", date.ChineseDay);

        Assert.Equal("庚子", date.Cyclical);
        Assert.Equal("鼠", date.ChineseZodiac);
    }

    [Fact]
    public void ChineseDateParseTest2()
    {
        var date = new ChineseDate(DateTime.Parse("1996-07-14"));
        Assert.Equal("丙子", date.Cyclical);
        Assert.Equal("鼠", date.ChineseZodiac);
    }

    [Fact]
    public void ToStringNormalTest()
    {
        var date = new ChineseDate(DateTime.Parse("2020-03-01"));
        Assert.Equal("2020-02-08", date.ToStringNormal());
    }

    [Fact]
    public void GetChineseMonthTest()
    {
        var chineseDate = new ChineseDate(2020, 3, 1);
        var monthName = chineseDate.GetChineseMonthName();
        Assert.NotNull(monthName);
    }

    [Fact]
    public void IsLeapMonthTest()
    {
        var chineseDate = new ChineseDate(2020, 3, 1);
        var isLeap = chineseDate.IsLeapMonth;
        Assert.False(isLeap);
    }

    [Fact]
    public void GetSolarTermTest()
    {
        var chineseDate = new ChineseDate(2020, 1, 25);
        var term = chineseDate.Term;
        Assert.NotNull(term);
    }
}
