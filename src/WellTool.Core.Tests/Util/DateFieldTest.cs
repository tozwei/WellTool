using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateFieldTest
{
    [Fact]
    public void ValuesTest()
    {
        var values = Enum.GetValues<DateField>();
        Assert.NotEmpty(values);
        Assert.Contains(DateField.Year, values);
        Assert.Contains(DateField.Month, values);
        Assert.Contains(DateField.Day, values);
    }

    [Fact]
    public void GetFieldValueTest()
    {
        var date = DateTime.Parse("2021-06-15 10:30:45");
        Assert.Equal(2021, DateField.Year.GetFieldValue(date));
        Assert.Equal(6, DateField.Month.GetFieldValue(date));
        Assert.Equal(15, DateField.Day.GetFieldValue(date));
        Assert.Equal(10, DateField.Hour.GetFieldValue(date));
        Assert.Equal(30, DateField.Minute.GetFieldValue(date));
        Assert.Equal(45, DateField.Second.GetFieldValue(date));
    }

    [Fact]
    public void AddTest()
    {
        var date = DateTime.Parse("2021-01-15");
        Assert.Equal(2022, DateField.Year.Add(date, 1).Year);
        Assert.Equal(2, DateField.Month.Add(date, 1).Month);
        Assert.Equal(16, DateField.Day.Add(date, 1).Day);
    }
}
