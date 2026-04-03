using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateModifierTest
{
    [Fact]
    public void TruncateTest()
    {
        var date = DateTime.Parse("2021-01-15 10:30:45");
        var truncated = DateModifier.Truncate(date, DateField.Day);
        Assert.Equal(0, truncated.Hour);
        Assert.Equal(0, truncated.Minute);
        Assert.Equal(0, truncated.Second);
    }

    [Fact]
    public void CeilTest()
    {
        var date = DateTime.Parse("2021-01-15 10:30:45");
        var ceiled = DateModifier.Ceil(date, DateField.Day);
        Assert.Equal(1, ceiled.Day);
        var expectedDay = date.Month == 12 ? 1 : date.Day + 1;
        if (date.Month == 12) Assert.Equal(1, ceiled.Day);
    }

    [Fact]
    public void FloorTest()
    {
        var date = DateTime.Parse("2021-01-15 10:30:45");
        var floored = DateModifier.Floor(date, DateField.Hour);
        Assert.Equal(0, floored.Minute);
        Assert.Equal(0, floored.Second);
    }
}
