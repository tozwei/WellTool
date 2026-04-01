using WellTool.Cron.Pattern;
using Xunit;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式工具类测试
    /// </summary>
    public class CronPatternUtilTests
    {
        [Fact]
        public void IsValidPatternTest()
        {
            // 验证有效表达式
            Assert.True(CronPatternUtil.IsValidPattern("*/5 * * * * *"));
            Assert.True(CronPatternUtil.IsValidPattern("0 0 12 * * ?"));
        }

        [Fact]
        public void IsInvalidPatternTest()
        {
            // 验证无效表达式
            Assert.False(CronPatternUtil.IsValidPattern("invalid"));
            Assert.False(CronPatternUtil.IsValidPattern("* * *"));
        }

        [Fact]
        public void GetNextMatchingTimeTest()
        {
            // 获取下次匹配时间
            var pattern = "0 0 12 * * ?";
            var next = CronPatternUtil.GetNextMatchingTime(pattern, DateTime.Now);

            Assert.True(next > DateTime.Now);
        }

        [Fact]
        public void FormatPatternTest()
        {
            // 格式化表达式
            var formatted = CronPatternUtil.Format("*/5 * * * * *");

            Assert.NotNull(formatted);
        }
    }
}
