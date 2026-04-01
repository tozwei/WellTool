using Xunit;
using WellTool.Cron.TimingWheel;

namespace WellTool.Cron.Tests.TimingWheel
{
    /// <summary>
    /// 时间轮测试
    /// </summary>
    public class TimingWheelTests
    {
        [Fact]
        public void TestAddTask()
        {
            // 测试添加任务
            var timingWheel = new TimingWheel(1, 10, System.Threading.CancellationToken.None);
            bool executed = false;

            // 添加一个 2 秒后执行的任务
            timingWheel.AddTask(2000, () =>
            {
                executed = true;
            });

            // 启动时间轮
            timingWheel.Start();

            // 等待一段时间
            System.Threading.Thread.Sleep(2500);

            // 停止时间轮
            timingWheel.Stop();

            // 任务应该执行
            Assert.True(executed, "任务应该执行");
        }
    }
}