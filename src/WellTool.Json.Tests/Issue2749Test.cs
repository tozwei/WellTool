using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2749 测试
    /// </summary>
    public class Issue2749Test
    {
        [Fact]
        public void TestKeyWithDot()
        {
            var jsonStr = "{\"key.with.dot\":\"value\"}";
            var bean = JSONUtil.ToBean<KeyDotBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestValueWithDot()
        {
            var jsonStr = "{\"key\":\"value.with.dot\"}";
            var bean = JSONUtil.ToBean<KeyDotBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class KeyDotBean
        {
            public object Key { get; set; }
        }
    }
}
