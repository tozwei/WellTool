using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class ChineseDateTest2
{
    [Fact]
    public void TestLunarDate()
    {
        var chineseDate = new ChineseDate(2020, 1, 1);
        Assert.Equal(2020, chineseDate.ChineseYear);
        Assert.Equal(1, chineseDate.Month);
        Assert.Equal(1, chineseDate.Day);
    }

    [Fact]
    public void TestZodiac()
    {
        var chineseDate = new ChineseDate(2020, 1, 25);
        Assert.Equal("鼠", chineseDate.ChineseZodiac);
    }

    [Fact]
    public void TestCyclical()
    {
        var chineseDate = new ChineseDate(2020, 1, 25);
        Assert.Equal("庚子", chineseDate.Cyclical);
    }

    [Fact]
    public void TestSolarTerms()
    {
        var chineseDate = new ChineseDate(2020, 1, 1);
        var solarTerms = chineseDate.GetSolarTerm();
        Assert.NotNull(solarTerms);
    }

    [Fact]
    public void TestIsLeapYear()
    {
        var chineseDate = new ChineseDate(2020, 1, 1);
        Assert.True(chineseDate.IsLeapYear());
    }
}
