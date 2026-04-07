using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ThreadUtilLastTest
{
    [Fact]
    public void NewExecutorTest()
    {
        var executor = ThreadUtil.NewExecutor(5);
        Assert.NotNull(executor);
    }

    [Fact]
    public void ExecuteTest()
    {
        var executed = false;
        ThreadUtil.Execute(() => executed = true);
        ThreadUtil.SafeSleep(100);
        Assert.True(executed);
    }

    [Fact]
    public void SafeSleepTest()
    {
        var sleepMillis = 100;
        var startTime = DateTime.Now;
        ThreadUtil.SafeSleep(sleepMillis);
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Assert.True(elapsed >= sleepMillis);
    }

    [Fact]
    public void CurrentThreadTest()
    {
        var thread = ThreadUtil.CurrentThread();
        Assert.NotNull(thread);
    }

    [Fact]
    public void CurrentThreadIdTest()
    {
        var threadId = ThreadUtil.CurrentThreadId();
        Assert.True(threadId > 0);
    }

    [Fact]
    public void SleepTest()
    {
        var startTime = DateTime.Now;
        ThreadUtil.Sleep(100);
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Assert.True(elapsed >= 100);
    }

    [Fact]
    public void YieldTest()
    {
        ThreadUtil.Yield();
        Assert.True(true);
    }

    [Fact]
    public void AsyncUtilExecuteTest()
    {
        var executed1 = false;
        var executed2 = false;
        AsyncUtil.Execute(
            () => { executed1 = true; },
            () => { executed2 = true; }
        );
        ThreadUtil.SafeSleep(100);
        Assert.True(executed1);
        Assert.True(executed2);
    }
}
