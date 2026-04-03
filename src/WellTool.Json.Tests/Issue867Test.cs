using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue867 测试
    /// </summary>
    public class Issue867Test
    {
        [Fact]
        public void TestQuoteInString()
        {
            var jsonStr = "{\"text\":\"He said \\\"Hello\\\"\"}";
            var bean = JSONUtil.ToBean<QuoteBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("He said \"Hello\"", bean.Text);
        }

        public class QuoteBean
        {
            public string Text { get; set; }
        }
    }
}
