using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3506 测试
    /// </summary>
    public class Issue3506Test
    {
        [Fact]
        public void TestLongString()
        {
            var longStr = new string('a', 100000);
            var jsonStr = $"{{\"data\":\"{longStr}\"}}";
            var bean = JSONUtil.ToBean<LongStrBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class LongStrBean
        {
            public string Data { get; set; }
        }
    }
}
