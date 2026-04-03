using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3713 测试
    /// </summary>
    public class Issue3713Test
    {
        [Fact]
        public void TestBooleanString()
        {
            var jsonStr = "{\"flag\":\"true\"}";
            var bean = JSONUtil.ToBean<BoolStrBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestNumberString()
        {
            var jsonStr = "{\"numStr\":\"123\"}";
            var bean = JSONUtil.ToBean<NumStrBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("123", bean.NumStr);
        }

        public class BoolStrBean
        {
            public string Flag { get; set; }
        }

        public class NumStrBean
        {
            public string NumStr { get; set; }
        }
    }
}
