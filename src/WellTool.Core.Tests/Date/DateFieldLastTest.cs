using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateFieldLastTest
{
    [Fact]
    public void OfTest()
    {
        var field = DateField.Of(1);
        Assert.NotNull(field);
    }

    [Fact]
    public void GetValueTest()
    {
        var dateTime = DateTime.Now;
        var value = DateField.YEAR.GetValue(dateTime);
        Assert.Equal(dateTime.Year, value);
    }
}
