using System;
using Xunit;
using WellTool.Cron.Pattern;

namespace WellTool.Cron.Tests
{
    public class TestMatch
    {
        [Fact]
        public void TestCronPatternMatch()
        {
            // 创建一个 cron 表达式，每分钟执行一次
            CronPattern pattern = new CronPattern("0 * * * * *");
            
            // 获取当前时间
            DateTime now = DateTime.Now;
            Console.WriteLine($"当前时间: {now}");
            
            // 测试 Match 方法
            bool isMatch = pattern.Match(now);
            Console.WriteLine($"是否匹配: {isMatch}");
            
            // 测试 NextMatch 方法
            DateTime? nextMatch = pattern.NextMatch(now);
            Console.WriteLine($"下一次匹配时间: {nextMatch}");
            
            // 断言 NextMatch 不应该返回 null
            Assert.NotNull(nextMatch);
        }
    }
}