using Xunit;
using WellTool.Cron;
using System;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// Cron 测试
    /// </summary>
    public class CronTests
    {
        [Fact]
        public void SchedulerBasicTest()
        {
            var scheduler = new Scheduler();
            bool executed = false;

            // 添加任务，1秒后执行
            scheduler.Schedule("*/1 * * * * *", () =>
            {
                executed = true;
            });

            // 启动调度器
            scheduler.Start();

            // 等待一段时间
            System.Threading.Thread.Sleep(1500);

            // 停止调度器
            scheduler.Stop();

            // 任务应该执行
            Assert.True(executed, "任务应该在 1 秒后执行");
        }
    }
}