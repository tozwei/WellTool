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
                .SetSecond("0")
                .SetMinute("30")
                .SetHour("8")
                .SetDayOfMonth("1")
                .SetMonth("1")
                .SetDayOfWeek("1")
                .Build();

            Assert.NotNull(pattern);
        }
    }
}