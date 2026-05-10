using WellTool.Core.Date;
using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class TimeIntervalTest
{
    [Fact]
    public void StartStopTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(100);
        var interval = timer.IntervalMs();
        Assert.True(interval >= 100);
    }

    [Fact]
    public void IntervalTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(50);
        var interval = timer.Interval();
        Assert.True(interval >= 50);
    }

    [Fact]
    public void IntervalSecondsTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(1000);
        var seconds = timer.IntervalSecond();
        Assert.True(seconds >= 1);
    }

    [Fact]
    public void IntervalMinutesTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(60000);
        var minutes = timer.IntervalMinute();
        Assert.True(minutes >= 1);
    }

    [Fact]
    public void RestartTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(50);
        timer.Restart();
        ThreadUtil.Sleep(30);
        var interval = timer.Interval();
        Assert.True(interval >= 30 && interval < 80, $"Expected interval >= 30 and < 80, but got {interval}");
    }
}
