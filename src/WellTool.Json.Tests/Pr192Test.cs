using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// PR192 测试
    /// </summary>
    public class Pr192Test
    {
        [Fact]
        public void TestNumberDeserialize()
        {
            var jsonStr = "{\"value\":9223372036854775807}";
            var bean = JSONUtil.ToBean<Pr192Bean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(9223372036854775807L, bean.Value);
        }

        [Fact]
        public void TestNegativeNumber()
        {
            var jsonStr = "{\"value\":-9223372036854775808}";
            var bean = JSONUtil.ToBean<Pr192Bean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(-9223372036854775808L, bean.Value);
        }

        [Fact]
        public void TestFloatNumber()
        {
            var jsonStr = "{\"value\":3.1415926}";
            var bean = JSONUtil.ToBean<Pr192Bean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(3.1415926, bean.DoubleValue, 6);
        }

        public class Pr192Bean
        {
            public long Value { get; set; }
            public double DoubleValue { get; set; }
        }
    }
}
