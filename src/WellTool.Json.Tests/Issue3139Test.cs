using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3139 测试
    /// </summary>
    public class Issue3139Test
    {
        [Fact]
        public void TestTimestamp()
        {
            var timestamp = DateTimeUtil.CurrentSeconds();
            var jsonStr = $"{{\"ts\":{timestamp}}}";
            var bean = JSONUtil.ToBean<TimestampBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestDateTimeMillis()
        {
            var millis = DateTimeUtil.CurrentMillis();
            var jsonStr = $"{{\"ts\":{millis}}}";
            var bean = JSONUtil.ToBean<TimestampBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class TimestampBean
        {
            public long Ts { get; set; }
        }
    }
}
