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
            var timingWheel = new WellTool.Cron.TimingWheel.TimingWheel(100, 10, DateTimeOffset.Now.ToUnixTimeMilliseconds());
            bool executed = false;

            // 创建一个 2 秒后执行的任务
            var task = new TimerTask(Guid.NewGuid().ToString(), DateTimeOffset.Now.AddSeconds(2).ToUnixTimeMilliseconds(), (id) =>
            {
                executed = true;
            });

            // 添加任务
            timingWheel.AddTask(task);

            // 模拟时间推进
            System.Threading.Thread.Sleep(3500);
            timingWheel.AdvanceClock(DateTimeOffset.Now.ToUnixTimeMilliseconds());

            // 任务应该执行
            Assert.True(executed, "任务应该执行");
        }
    }
}