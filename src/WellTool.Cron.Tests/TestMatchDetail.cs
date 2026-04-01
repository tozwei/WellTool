using System;
using Xunit;
using WellTool.Cron.Pattern;

namespace WellTool.Cron.Tests
{
    public class TestMatchDetail
    {
        [Fact]
        public void TestCronPatternMatchDetail()
        {
            // 创建一个 cron 表达式，每 5 秒执行一次
            CronPattern pattern = new CronPattern("*/5 * * * * *");
            
            // 获取当前时间
            DateTime now = DateTime.Now;
            Console.WriteLine($"当前时间: {now}");
            Console.WriteLine($"当前秒数: {now.Second}");
            
            // 测试 Match 方法
            bool isMatch = pattern.Match(now);
            Console.WriteLine($"是否匹配: {isMatch}");
            
            // 测试下一个 5 秒的时间
            int next5Second = (now.Second / 5 + 1) * 5;
            if (next5Second >= 60)
            {
                next5Second -= 60;
            }
            DateTime nextTime = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, next5Second);
            Console.WriteLine($"下一个 5 秒时间: {nextTime}");
            Console.WriteLine($"下一个 5 秒秒数: {nextTime.Second}");
            bool isMatchNext = pattern.Match(nextTime);
            Console.WriteLine($"下一个 5 秒是否匹配: {isMatchNext}");
            
            // 断言下一个 5 秒应该匹配
            Assert.True(isMatchNext);
        }
    }
}