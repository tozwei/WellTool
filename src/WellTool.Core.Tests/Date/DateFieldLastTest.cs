using Xunit;
using WellTool.Core.Date;

namespace WellTool.Core.Tests.Date;

/// <summary>
/// DateField 测试
/// </summary>
public class DateFieldLastTest
{
    [Fact]
    public void GetFieldTest()
    {
        var date = DateTime.Now;
        Assert.Equal(date.Year, date.Get(DateField.YEAR));
        Assert.Equal(date.Month, date.Get(DateField.MONTH));
        Assert.Equal(date.Day, date.Get(DateField.DAY_OF_MONTH));
    }

    [Fact]
    public void AddFieldTest()
    {
        var date = DateTime.Now;
        var added = date.Add(DateField.YEAR, 1);
        Assert.Equal(date.Year + 1, added.Year);
    }
}
