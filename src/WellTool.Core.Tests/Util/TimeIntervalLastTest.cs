using WellTool.Core.Date;
using WellTool.Core.Util;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class TimeIntervalLastTest
{
    [Fact]
    public void StartStopTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(100);
        timer.Stop();
        Assert.True(timer.IntervalMillis >= 100);
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
        var seconds = timer.IntervalSeconds();
        Assert.True(seconds >= 1);
    }

    [Fact]
    public void RestartTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(50);
        timer.Restart();
        ThreadUtil.Sleep(30);
        var interval = timer.Interval();
        Assert.True(interval < 50);
    }
}
