using Xunit;
using WellTool.Cron;
using System;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 守护线程测试
    /// </summary>
    public class DaemonTests
    {
        [Fact]
        public void DaemonSchedulerTest()
        {
            var scheduler = new Scheduler(true); // 守护线程模式
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
            Assert.True(executed, "守护线程模式下的任务应该执行");
        }
    }
}