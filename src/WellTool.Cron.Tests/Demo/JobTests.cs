using Xunit;
using WellTool.Cron;
using System;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 任务测试
    /// </summary>
    public class JobTests
    {
        [Fact]
        public void SimpleJobTest()
        {
            var scheduler = new Scheduler();
            int count = 0;

            // 添加任务，每1秒执行一次
            scheduler.Schedule("*/1 * * * * *", () =>
            {
                count++;
            });

            // 启动调度器
            scheduler.Start();

            // 等待一段时间
            System.Threading.Thread.Sleep(5500);

            // 停止调度器
            scheduler.Stop();

            // 任务应该至少执行 2 次
            Assert.True(count >= 2, $"任务应该至少执行 2 次，实际执行：{count}");
        }

        [Fact]
        public void MultipleJobsTest()
        {
            var scheduler = new Scheduler();
            int count1 = 0;
            int count2 = 0;

            // 添加任务 1，每1秒执行一次
            scheduler.Schedule("*/1 * * * * *", () =>
            {
                count1++;
            });

            // 添加任务 2，每1秒执行一次
            scheduler.Schedule("*/1 * * * * *", () =>
            {
                count2++;
            });

            // 启动调度器
            scheduler.Start();

            // 等待一段时间
            System.Threading.Thread.Sleep(5500);

            // 停止调度器
            scheduler.Stop();

            // 任务 1 应该至少执行 2 次
            Assert.True(count1 >= 2, $"任务 1 应该至少执行 2 次，实际执行：{count1}");
            // 任务 2 应该至少执行 2 次
            Assert.True(count2 >= 2, $"任务 2 应该至少执行 2 次，实际执行：{count2}");
        }
    }
}