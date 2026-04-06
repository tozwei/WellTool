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
    public void IntervalMinutesTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(60000);
        var minutes = timer.IntervalMinutes();
        Assert.True(minutes >= 1);
    }

    [Fact]
    public void PauseResumeTest()
    {
        var timer = new TimeInterval();
        ThreadUtil.Sleep(50);
        timer.Pause();
        ThreadUtil.Sleep(100);
        timer.Resume();
        ThreadUtil.Sleep(50);
        timer.Stop();

        Assert.True(timer.IntervalMillis >= 50 && timer.IntervalMillis < 150);
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
