using Xunit;
using WellTool.Cron.Pattern;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式构建器测试
    /// </summary>
    public class CronPatternBuilderTests
    {
        [Fact]
        public void TestBuild()
        {
            // 测试构建 cron 表达式
            var pattern = CronPatternBuilder.New()
                .Second(0)
                .Minute(30)
                .Hour(8)
                .DayOfMonth(1)
                .Month(1)
                .DayOfWeek(1)
                .Build();

            Assert.NotNull(pattern);
        }
    }
}