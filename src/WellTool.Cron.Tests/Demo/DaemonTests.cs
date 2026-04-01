using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 守护线程模式测试（对应 Hutool DeamonMainTest）
    /// </summary>
    public class DaemonTests
    {
        [Fact]
        public void DaemonSchedulerTest()
        {
            // 守护线程调度器测试
            var scheduler = new Scheduler(true); // true 表示守护线程模式

            try
            {
                scheduler.Start();

                bool executed = false;
                scheduler.Schedule("*/1 * * * * *", () =>
                {
                    executed = true;
                });

                // 等待执行
                System.Threading.Thread.Sleep(1500);

                Assert.True(executed, "守护线程模式下的任务应该执行");
            }
            finally
            {
                scheduler.Stop();
            }
        }
    }
}
