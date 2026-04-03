using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI6YN2A 测试
    /// </summary>
    public class IssueI6YN2ATest
    {
        [Fact]
        public void TestMultiByte()
        {
            var jsonStr = "{\"text\":\"日本語\"}";
            var bean = JSONUtil.ToBean<MultiByteBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class MultiByteBean
        {
            public string Text { get; set; }
        }
    }
}
