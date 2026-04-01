using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Cron 工具类测试
    /// </summary>
    public class CronUtilTests
    {
        [Fact]
        public void ScheduleTest()
        {
            // 静态方法调度测试
            var taskId = CronUtil.Schedule("*/5 * * * * *", () => { });

            Assert.NotNull(taskId);
            Assert.False(string.IsNullOrEmpty(taskId));
        }

        [Fact]
        public void ScheduleWithConfigTest()
        {
            // 使用自定义配置调度
            var config = new CronConfig();
            var taskId = CronUtil.Schedule(config, "*/5 * * * * *", () => { });

            Assert.NotNull(taskId);
        }

        [Fact]
        public void UnscheduleTest()
        {
            // 移除任务
            var taskId = CronUtil.Schedule("*/5 * * * * *", () => { });
            var result = CronUtil.Unschedule(taskId);

            Assert.True(result);
        }

        [Fact]
        public void StartAndStopTest()
        {
            // 启动和停止
            CronUtil.Start();
            Assert.True(CronUtil.IsStarted);

            CronUtil.Stop();
            Assert.False(CronUtil.IsStarted);
        }
    }
}
