using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 任务执行测试（对应 Hutool JobMainTest）
    /// </summary>
    public class JobTests
    {
        [Fact]
        public void SimpleJobTest()
        {
            // 简单任务测试
            var scheduler = new Scheduler();

            try
            {
                scheduler.Start();

                int counter = 0;
                scheduler.Schedule("*/1 * * * * *", () =>
                {
                    counter++;
                });

                // 等待执行 3 次
                System.Threading.Thread.Sleep(3500);

                Assert.True(counter >= 3, $"任务应该至少执行 3 次，实际执行：{counter}");
            }
            finally
            {
                scheduler.Stop();
            }
        }

        [Fact]
        public void MultipleJobsTest()
        {
            // 多个任务测试
            var scheduler = new Scheduler();

            try
            {
                scheduler.Start();

                int job1Count = 0;
                int job2Count = 0;

                // 每秒执行的任务
                scheduler.Schedule("*/1 * * * * *", () =>
                {
                    job1Count++;
                });

                // 每 2 秒执行的任务
                scheduler.Schedule("*/2 * * * * *", () =>
                {
                    job2Count++;
                });

                // 等待 3 秒
                System.Threading.Thread.Sleep(3500);

                Assert.True(job1Count >= 3, $"任务 1 应该至少执行 3 次，实际执行：{job1Count}");
                Assert.True(job2Count >= 1, $"任务 2 应该至少执行 1 次，实际执行：{job2Count}");
            }
            finally
            {
                scheduler.Stop();
            }
        }
    }
}
