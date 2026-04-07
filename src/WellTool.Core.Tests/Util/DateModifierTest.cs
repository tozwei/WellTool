using WellTool.Core.Date;
using Xunit;

namespace WellTool.Core.Tests;

public class DateModifierTest
{
    [Fact]
    public void SetTimeTest()
    {
        var date = DateTime.Parse("2021-01-15 10:30:45");
        var modified = DateModifier.Of(date)
            .SetHour(0)
            .SetMinute(0)
            .SetSecond(0)
            .Get();
        Assert.Equal(0, modified.Hour);
        Assert.Equal(0, modified.Minute);
        Assert.Equal(0, modified.Second);
    }

    [Fact]
    public void AddDaysTest()
    {
        var date = DateTime.Parse("2021-01-15 10:30:45");
        var modified = DateModifier.Of(date).AddDays(1).Get();
        Assert.Equal(16, modified.Day);
    }

    [Fact]
    public void AddHoursTest()
    {
        var date = DateTime.Parse("2021-01-15 10:30:45");
        var modified = DateModifier.Of(date).AddHours(1).Get();
        Assert.Equal(11, modified.Hour);
    }
}
