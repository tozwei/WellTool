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
        Assert.Contains("春节", date.Festivals ?? "");
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
        var chineseDate = new ChineseDate(2020, 6, 15);
        Assert.Equal("六月", chineseDate.ChineseMonth);
    }

    [Fact]
    public void IsLeapMonthTest()
    {
        var chineseDate = new ChineseDate(2020, 1, 25);
        // 2020年是闰四月，所以正月不是闰月
        Assert.False(chineseDate.IsLeapMonth());
    }

    [Fact]
    public void GetSolarTermTest()
    {
        var chineseDate = new ChineseDate(2020, 1, 25);
        // 春节是立春后的第一个节
        var solarTerm = chineseDate.GetSolarTerm();
        Assert.NotNull(solarTerm);
    }
}
