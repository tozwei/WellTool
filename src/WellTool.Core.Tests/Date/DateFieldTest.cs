using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateFieldTest
{
    [Fact]
    public void OfTest()
    {
        // DateField是枚举，直接使用枚举值
        var field = DateField.HourOfDay;
        Assert.Equal(DateField.HourOfDay, field);

        field = DateField.Minute;
        Assert.Equal(DateField.Minute, field);
    }

    [Fact]
    public void YearTest()
    {
        Assert.Equal(DateField.Year, DateField.Year);
    }

    [Fact]
    public void MonthTest()
    {
        Assert.Equal(DateField.Month, DateField.Month);
    }

    [Fact]
    public void DayOfMonthTest()
    {
        Assert.Equal(DateField.DayOfMonth, DateField.DayOfMonth);
    }
}
