using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI71BE6 测试
    /// </summary>
    public class IssueI71BE6Test
    {
        [Fact]
        public void TestDateMillis()
        {
            var millis = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var jsonStr = $"{{\"ts\":{millis}}}";
            var bean = JSONUtil.ToBean<TsBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class TsBean
        {
            public long Ts { get; set; }
        }
    }
}
