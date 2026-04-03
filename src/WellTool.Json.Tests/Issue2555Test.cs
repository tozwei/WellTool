using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2555 测试
    /// </summary>
    public class Issue2555Test
    {
        [Fact]
        public void TestSpecialChars()
        {
            var jsonStr = "{\"text\":\"测试\\\\\\\"\\n\\t\"}";
            var bean = JSONUtil.ToBean<SpecialBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestControlChars()
        {
            var jsonStr = "{\"text\":\"a\\u0000b\"}";
            var bean = JSONUtil.ToBean<SpecialBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class SpecialBean
        {
            public string Text { get; set; }
        }
    }
}
