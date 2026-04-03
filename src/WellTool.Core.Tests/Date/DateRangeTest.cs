using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateRangeTest
{
    [Fact]
    public void TestDayRange()
    {
        var start = DateTime.Parse("2021-01-01");
        var end = DateTime.Parse("2021-01-05");
        var range = new DateRange(start, end, DateField.Day);

        var count = 0;
        foreach (var date in range)
        {
            count++;
        }
        Assert.Equal(5, count);
    }

    [Fact]
    public void TestMonthRange()
    {
        var start = DateTime.Parse("2021-01-01");
        var end = DateTime.Parse("2021-04-01");
        var range = new DateRange(start, end, DateField.Month);

        var count = 0;
        foreach (var date in range)
        {
            count++;
        }
        Assert.Equal(4, count);
    }

    [Fact]
    public void TestYearRange()
    {
        var start = DateTime.Parse("2020-01-01");
        var end = DateTime.Parse("2023-01-01");
        var range = new DateRange(start, end, DateField.Year);

        var count = 0;
        foreach (var date in range)
        {
            count++;
        }
        Assert.Equal(4, count);
    }

    [Fact]
    public void TestToList()
    {
        var start = DateTime.Parse("2021-01-01");
        var end = DateTime.Parse("2021-01-03");
        var range = new DateRange(start, end, DateField.Day);

        var list = range.ToList();
        Assert.Equal(3, list.Count);
    }
}
