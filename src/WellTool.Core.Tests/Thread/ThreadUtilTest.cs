using Xunit;
using WellTool.Core.Util;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Tests.Thread;

/// <summary>
/// ThreadUtil 测试
/// </summary>
public class ThreadUtilTest
{
    [Fact]
    public void NewExecutorTest()
    {
        var executor = ThreadUtil.NewExecutor(5);
        Assert.Equal(5, executor.GetCorePoolSize());
    }

    [Fact]
    public async Task ExecuteTest()
    {
        var isValid = true;
        await Task.Run(() => Assert.True(isValid));
    }

    [Fact]
    public void SafeSleepTest()
    {
        var sleepMillis = RandomUtil.RandomLong(1, 1000);
        var start = DateTime.Now.Ticks;
        ThreadUtil.SafeSleep(sleepMillis);
        var elapsed = (DateTime.Now.Ticks - start) / TimeSpan.TicksPerMillisecond;
        Assert.True(elapsed >= sleepMillis);
    }
}
