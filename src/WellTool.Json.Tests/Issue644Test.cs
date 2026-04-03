using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue644 测试
    /// </summary>
    public class Issue644Test
    {
        [Fact]
        public void TestEmptyString()
        {
            var jsonStr = "{\"text\":\"\"}";
            var bean = JSONUtil.ToBean<EmptyStrBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("", bean.Text);
        }

        public class EmptyStrBean
        {
            public string Text { get; set; }
        }
    }
}
