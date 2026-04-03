using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3541 测试
    /// </summary>
    public class Issue3541Test
    {
        [Fact]
        public void TestBase64Value()
        {
            var base64 = Convert.ToBase64String(new byte[] { 1, 2, 3, 4 });
            var jsonStr = $"{{\"data\":\"{base64}\"}}";
            var bean = JSONUtil.ToBean<Base64Bean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class Base64Bean
        {
            public string Data { get; set; }
        }
    }
}
