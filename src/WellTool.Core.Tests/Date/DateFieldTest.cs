using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateFieldTest
{
    [Fact]
    public void OfTest()
    {
        var field = DateField.Of(11);
        Assert.Equal(DateField.HOUR_OF_DAY, field);

        field = DateField.Of(12);
        Assert.Equal(DateField.MINUTE, field);
    }

    [Fact]
    public void YearTest()
    {
        Assert.Equal(DateField.YEAR, DateField.Of(1));
    }

    [Fact]
    public void MonthTest()
    {
        Assert.Equal(DateField.MONTH, DateField.Of(2));
    }

    [Fact]
    public void DayOfMonthTest()
    {
        Assert.Equal(DateField.DAY_OF_MONTH, DateField.Of(5));
    }
}
