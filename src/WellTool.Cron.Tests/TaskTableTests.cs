using WellTool.Cron;
using WellTool.Cron.Pattern;
using WellTool.Cron.Task;
using Xunit;
using System;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// 任务表测试
    /// </summary>
    public class TaskTableTests
    {
        private TaskTable taskTable;

        public TaskTableTests()
        {
            taskTable = new TaskTable();
        }

        [Fact]
        public void AddTaskTest()
        {
            // 添加任务
            var taskId = "test1";
            var pattern = new CronPattern("*/5 * * * *");
            var action = () => { };

            var result = taskTable.Add(taskId, pattern, new RunnableTask(action));
            Assert.True(result);
            Assert.Equal(1, taskTable.Count);
        }

        [Fact]
        public void GetTaskTest()
        {
            // 获取任务
            var taskId = "test1";
            var pattern = new CronPattern("*/5 * * * *");
            var action = () => { };

            taskTable.Add(taskId, pattern, new RunnableTask(action));
            var task = taskTable.Get(taskId);

            Assert.NotNull(task);
        }

        [Fact]
        public void RemoveTaskTest()
        {
            // 移除任务
            var taskId = "test1";
            var pattern = new CronPattern("*/5 * * * *");
            var action = () => { };

            taskTable.Add(taskId, pattern, new RunnableTask(action));
            var result = taskTable.Remove(taskId);

            Assert.True(result);
            Assert.Equal(0, taskTable.Count);
        }

        [Fact]
        public void ContainsTaskTest()
        {
            // 检查是否包含任务
            var taskId = "test1";
            var pattern = new CronPattern("*/5 * * * *");
            var action = () => { };

            taskTable.Add(taskId, pattern, new RunnableTask(action));
            var contains = taskTable.Contains(taskId);

            Assert.True(contains);
        }

        [Fact]
        public void ClearTasksTest()
        {
            // 清空所有任务
            taskTable.Add("test1", new CronPattern("*/5 * * * *"), new RunnableTask(() => { }));
            taskTable.Add("test2", new CronPattern("*/10 * * * *"), new RunnableTask(() => { }));

            taskTable.Clear();
            Assert.Equal(0, taskTable.Count);
        }

        [Fact]
        public void GetCountTest()
        {
            // 获取任务数量
            Assert.Equal(0, taskTable.Count);

            taskTable.Add("test1", new CronPattern("*/5 * * * *"), new RunnableTask(() => { }));
            Assert.Equal(1, taskTable.Count);

            taskTable.Add("test2", new CronPattern("*/10 * * * *"), new RunnableTask(() => { }));
            Assert.Equal(2, taskTable.Count);
        }

        [Fact]
        public void GetAllTasksTest()
        {
            // 获取所有任务
            taskTable.Add("test1", new CronPattern("*/5 * * * *"), new RunnableTask(() => { }));
            taskTable.Add("test2", new CronPattern("*/10 * * * *"), new RunnableTask(() => { }));

            var tasks = taskTable.GetAll();

            Assert.NotNull(tasks);
            Assert.Equal(2, tasks.Count);
        }

        [Fact]
        public void ToStringTest()
        {
            TaskTable taskTable = new TaskTable();
            taskTable.Add(Guid.NewGuid().ToString(), new CronPattern("*/10 * * * *"), new RunnableTask(() => Console.WriteLine("Task 1")));
            taskTable.Add(Guid.NewGuid().ToString(), new CronPattern("*/20 * * * *"), new RunnableTask(() => Console.WriteLine("Task 2")));
            taskTable.Add(Guid.NewGuid().ToString(), new CronPattern("*/30 * * * *"), new RunnableTask(() => Console.WriteLine("Task 3")));

            Console.WriteLine(taskTable);
        }
    }
}
