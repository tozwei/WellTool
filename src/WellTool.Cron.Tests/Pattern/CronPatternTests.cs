using Xunit;
using WellTool.Cron.Pattern;
using System;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式解析测试
    /// </summary>
    public class CronPatternTests
    {
        [Fact]
        public void ParseTest()
        {
            // 解析标准 cron 表达式
            var pattern = new CronPattern("*/5 * * * * *");

            Assert.NotNull(pattern);
            Assert.Equal("*/5 * * * * *", pattern.ToString());
        }

        [Fact]
        public void ParseWithSecondLevelTest()
        {
            // 解析带秒级的表达式
            var pattern = new CronPattern("0 30 8 * * ? *");

            Assert.NotNull(pattern);
        }

        [Fact]
        public void ParseInvalidPatternTest()
        {
            // 解析无效表达式（应该抛出异常）
            Assert.ThrowsAny<Exception>(() =>
            {
                new CronPattern("invalid pattern");
            });
        }

        [Fact]
        public void MatchTest()
        {
            // 测试时间匹配
            var pattern = new CronPattern("* * * * * *");
            Assert.True(pattern.Match(DateTime.Now));
        }

        [Fact]
        public void NextExecutionTimeTest()
        {
            // 测试下次执行时间
            var pattern = new CronPattern("0 0 12 * * ?");
            var next = pattern.NextMatchingAfter(DateTime.Now);

            Assert.True(next > DateTime.Now);
        }
    }
}