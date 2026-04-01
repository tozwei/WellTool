using WellTool.Cron;
using Xunit;

namespace WellTool.Cron.Tests
{
    /// <summary>
    /// Cron 配置测试
    /// </summary>
    public class CronConfigTests
    {
        [Fact]
        public void DefaultConfigTest()
        {
            var config = new CronConfig();

            // 验证默认配置
            Assert.NotNull(config);
            Assert.True(config.SecondCount == 60);
            Assert.True(config.MinuteCount == 60);
            Assert.True(config.HourCount == 24);
        }

        [Fact]
        public void CustomConfigTest()
        {
            var config = new CronConfig
            {
                SecondCount = 60,
                MinuteCount = 60,
                HourCount = 24
            };

            Assert.Equal(60, config.SecondCount);
            Assert.Equal(60, config.MinuteCount);
            Assert.Equal(24, config.HourCount);
        }

        [Fact]
        public void CloneConfigTest()
        {
            var config1 = new CronConfig
            {
                SecondCount = 60,
                MinuteCount = 60,
                HourCount = 24
            };

            var config2 = config1.Clone();

            Assert.NotNull(config2);
            Assert.Equal(config1.SecondCount, config2.SecondCount);
            Assert.Equal(config1.MinuteCount, config2.MinuteCount);
            Assert.Equal(config1.HourCount, config2.HourCount);
        }
    }
}
