using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式构建器测试
    /// </summary>
    public class CronPatternBuilderTests
    {
        [Fact]
        public void BuildTest()
        {
            // 每秒执行一次
            var pattern = CronPatternBuilder.New()
                .SetSecond("0/1")
                .Build();

            Assert.NotNull(pattern);
            Assert.NotNull(pattern.ToString());
        }

        [Fact]
        public void BuildComplexTest()
        {
            // 工作日早上 8:30:00 执行
            var pattern = CronPatternBuilder.New()
                .SetSecond("0")
                .SetMinute("30")
                .SetHour("8")
                .SetDayOfWeek("1-5")
                .Build();

            Assert.NotNull(pattern);
            Assert.NotNull(pattern.ToString());
        }

        [Fact]
        public void BuildEveryMinuteTest()
        {
            // 每分钟执行
            var pattern = CronPatternBuilder.New()
                .SetSecond("0")
                .SetMinute("*/1")
                .Build();

            Assert.NotNull(pattern);
        }

        [Fact]
        public void BuildEveryHourTest()
        {
            // 每小时执行
            var pattern = CronPatternBuilder.New()
                .SetSecond("0")
                .SetMinute("0")
                .SetHour("*/1")
                .Build();

            Assert.NotNull(pattern);
        }

        [Fact]
        public void BuildSpecificTimeTest()
        {
            // 特定时间执行
            var pattern = CronPatternBuilder.New()
                .SetSecond("30")
                .SetMinute("15")
                .SetHour("10")
                .Build();

            Assert.NotNull(pattern);
        }
    }
}
