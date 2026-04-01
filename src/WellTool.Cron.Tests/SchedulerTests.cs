using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// 调度器测试
    /// </summary>
    public class SchedulerTests
    {
        private Scheduler scheduler;

        public SchedulerTests()
        {
            scheduler = new Scheduler();
        }

        ~SchedulerTests()
        {
            scheduler?.Stop();
        }

        [Fact]
        public void StartTest()
        {
            // 启动调度器
            var result = scheduler.Start();
            Assert.NotNull(result);
            Assert.True(scheduler.IsStarted);
        }

        [Fact]
        public void StopTest()
        {
            // 启动并停止
            scheduler.Start();
            scheduler.Stop();
            Assert.False(scheduler.IsStarted);
        }

        [Fact]
        public void ScheduleTest()
        {
            // 添加任务
            var taskId = scheduler.Schedule("*/5 * * * * *", () => { });
            Assert.NotNull(taskId);
            Assert.False(string.IsNullOrEmpty(taskId));
        }

        [Fact]
        public void ScheduleMultipleTasksTest()
        {
            // 添加多个任务
            var task1 = scheduler.Schedule("*/5 * * * * *", () => { });
            var task2 = scheduler.Schedule("*/10 * * * * *", () => { });
            var task3 = scheduler.Schedule("*/15 * * * * *", () => { });

            Assert.NotNull(task1);
            Assert.NotNull(task2);
            Assert.NotNull(task3);
        }

        [Fact]
        public void UnscheduleTest()
        {
            // 添加并移除任务
            var taskId = scheduler.Schedule("*/5 * * * * *", () => { });
            var result = scheduler.Unschedule(taskId);
            Assert.True(result);
        }

        [Fact]
        public void GetTaskCountTest()
        {
            // 获取任务数量
            var initialCount = scheduler.GetTaskCount();
            scheduler.Schedule("*/5 * * * * *", () => { });
            var newCount = scheduler.GetTaskCount();
            Assert.Equal(initialCount + 1, newCount);
        }

        [Fact]
        public void IsDaemonTest()
        {
            // 测试守护线程模式
            var daemonScheduler = new Scheduler(true);
            Assert.True(daemonScheduler.IsDaemon);
        }

        [Fact]
        public void ScheduleWithCustomNameTest()
        {
            // 使用自定义名称添加任务
            var taskId = scheduler.Schedule("myTask", "*/5 * * * * *", () => { });
            Assert.Equal("myTask", taskId);
        }
    }
}
