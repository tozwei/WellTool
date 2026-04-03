using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueID61QR 测试
    /// </summary>
    public class IssueID61QRTest
    {
        [Fact]
        public void TestParseConfig()
        {
            var jsonStr = "{\"enabled\":true,\"timeout\":30,\"retry\":3}";
            var bean = JSONUtil.ToBean<ConfigBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.True(bean.Enabled);
            Assert.Equal(30, bean.Timeout);
            Assert.Equal(3, bean.Retry);
        }

        public class ConfigBean
        {
            public bool Enabled { get; set; }
            public int Timeout { get; set; }
            public int Retry { get; set; }
        }
    }
}
