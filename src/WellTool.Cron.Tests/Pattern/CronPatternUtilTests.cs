using Xunit;
using WellTool.Cron.Pattern;
using System;

namespace WellTool.Cron.Tests.Pattern
{
    /// <summary>
    /// Cron 模式工具测试
    /// </summary>
    public class CronPatternUtilTests
    {
        [Fact]
        public void GetNextMatchingTimeTest()
        {
            // 测试获取下一次匹配时间
            var pattern = "0 0 12 * * ?";
            var baseTime = DateTime.Now;
            var nextMatch = CronPatternUtil.GetNextMatchingTime(pattern, baseTime);

            Assert.True(nextMatch > baseTime);
        }

        [Fact]
        public void IsValidPatternTest()
        {
            // 测试有效表达式
            Assert.True(CronPatternUtil.IsValidPattern("0 0 12 * * ?"));

            // 测试无效表达式
            Assert.False(CronPatternUtil.IsValidPattern("invalid pattern"));
        }
    }
}