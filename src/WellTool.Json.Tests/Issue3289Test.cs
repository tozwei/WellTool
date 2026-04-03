using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3289 测试
    /// </summary>
    public class Issue3289Test
    {
        [Fact]
        public void TestNumberAsString()
        {
            var jsonStr = "{\"numStr\":\"12345678901234567890\"}";
            var bean = JSONUtil.ToBean<NumStrBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("12345678901234567890", bean.NumStr);
        }

        [Fact]
        public void TestLeadingZeros()
        {
            var jsonStr = "{\"num\":\"00123\"}";
            var bean = JSONUtil.ToBean<NumStrBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class NumStrBean
        {
            public string NumStr { get; set; }
            public string Num { get; set; }
        }
    }
}
