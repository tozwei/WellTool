using Xunit;
using WellTool.Cron;
using System;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 添加和删除任务测试
    /// </summary>
    public class AddAndRemoveTests
    {
        [Fact]
        public void AddAndRemoveTaskTest()
        {
            var scheduler = new Scheduler();
            int count = 0;

            // 添加任务
            var taskId = scheduler.Schedule("*/1 * * * * *", () =>
            {
                count++;
            });

            // 启动调度器
            scheduler.Start();

            // 等待一段时间
            System.Threading.Thread.Sleep(2500);

            // 移除任务
            scheduler.Unschedule(taskId);

            // 等待一段时间，确保任务不再执行
            System.Threading.Thread.Sleep(1500);

            // 停止调度器
            scheduler.Stop();

            // 任务应该至少执行 2 次
            Assert.True(count >= 2, $"任务应该至少执行 2 次，实际执行：{count}");
        }
    }
}