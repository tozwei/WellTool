using WellTool.Core.Thread;
using Xunit;

namespace WellTool.Core.Tests;

public class ThreadUtilTest
{
    [Fact]
    public void NewExecutorTest()
    {
        var executor = ThreadUtil.NewExecutor(5);
        Assert.Equal(5, executor.CorePoolSize);
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
        var sleepMillis = RandomUtil.RandomLong(1, 500);
        var startTime = DateTime.Now;
        ThreadUtil.SafeSleep(sleepMillis);
        var elapsed = (DateTime.Now - startTime).TotalMilliseconds;
        Assert.True(elapsed >= sleepMillis);
    }

    [Fact]
    public void GetThreadsTest()
    {
        var threads = ThreadUtil.GetThreads();
        Assert.NotNull(threads);
        Assert.NotEmpty(threads);
    }

    [Fact]
    public void CurrentTest()
    {
        var thread = ThreadUtil.Current();
        Assert.NotNull(thread);
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
    public void WaitForFinishTest()
    {
        var executor = ThreadUtil.NewExecutor(2);
        executor.Execute(() => ThreadUtil.Sleep(50));
        executor.Execute(() => ThreadUtil.Sleep(50));
        ThreadUtil.WaitForFinish(executor);
        Assert.True(true);
    }
}
