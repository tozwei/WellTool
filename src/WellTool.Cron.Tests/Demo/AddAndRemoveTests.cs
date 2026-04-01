using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests.Demo
{
    /// <summary>
    /// 任务添加和移除测试（对应 Hutool AddAndRemoveMainTest）
    /// </summary>
    public class AddAndRemoveTests
    {
        [Fact]
        public void AddAndRemoveTaskTest()
        {
            // 添加和移除任务测试
            var scheduler = new Scheduler();

            try
            {
                scheduler.Start();

                int executeCount = 0;
                var taskId = scheduler.Schedule("*/1 * * * * *", () =>
                {
                    executeCount++;
                });

                // 等待执行 2 次
                System.Threading.Thread.Sleep(2500);

                Assert.True(executeCount >= 2, $"任务应该至少执行 2 次，实际执行：{executeCount}");

                // 移除任务
                scheduler.Unschedule(taskId);

                int countBefore = executeCount;

                // 再等待 2 秒，确认任务已被移除
                System.Threading.Thread.Sleep(2000);

                Assert.Equal(countBefore, executeCount);
            }
            finally
            {
                scheduler.Stop();
            }
        }
    }
}
