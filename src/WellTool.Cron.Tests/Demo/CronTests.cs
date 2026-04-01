using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// Cron 调度器基础测试（对应 Hutool CronTest）
    /// </summary>
    public class CronTests
    {
        [Fact]
        public void SchedulerBasicTest()
        {
            // 基础调度器功能测试
            var scheduler = new Scheduler();

            try
            {
                scheduler.Start();

                bool executed = false;
                var taskId = scheduler.Schedule("*/1 * * * * *", () =>
                {
                    executed = true;
                });

                // 等待执行
                System.Threading.Thread.Sleep(1500);

                Assert.True(executed, "任务应该在 1 秒后执行");
            }
            finally
            {
                scheduler.Stop();
            }
        }
    }
}
