using Xunit;
using WellTool.Cron;
using WellTool.Cron.Task;
using WellTool.Cron.Pattern;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// 任务表测试
    /// </summary>
    public class TaskTableTests
    {
        [Fact]
        public void TestAddAndRemoveTask()
        {
            // 测试添加和删除任务
            var taskTable = new TaskTable();
            var pattern = new CronPattern("*/1 * * * * *");
            var task = new RunnableTask(() => { });

            // 添加任务
            bool added = taskTable.Add("test", pattern, task);
            Assert.True(added, "任务应该添加成功");

            // 检查任务是否存在
            bool contains = taskTable.Contains("test");
            Assert.True(contains, "任务表应该包含添加的任务");

            // 获取任务
            var retrievedTask = taskTable.Get("test");
            Assert.NotNull(retrievedTask);

            // 删除任务
            bool removed = taskTable.Remove("test");
            Assert.True(removed, "任务应该删除成功");

            // 检查任务是否不存在
            contains = taskTable.Contains("test");
            Assert.False(contains, "任务表不应该包含已删除的任务");
        }

        [Fact]
        public void TestClear()
        {
            // 测试清空任务表
            var taskTable = new TaskTable();
            var pattern = new CronPattern("*/1 * * * * *");
            var task1 = new RunnableTask(() => { });
            var task2 = new RunnableTask(() => { });

            // 添加任务
            taskTable.Add("test1", pattern, task1);
            taskTable.Add("test2", pattern, task2);

            // 清空任务表
            taskTable.Clear();

            // 检查任务表是否为空
            Assert.Equal(0, taskTable.Count);
        }
    }
}