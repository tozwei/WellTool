using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2365 测试 - 测试大数字处理
    /// </summary>
    public class Issue2365Test
    {
        [Fact]
        public void TestLargeNumber()
        {
            var jsonStr = "{\"bigNumber\":9007199254740993}";
            var bean = JSONUtil.ToBean<LargeNumBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestScientificNotation()
        {
            var jsonStr = "{\"sciNum\":1.23E+10}";
            var bean = JSONUtil.ToBean<SciBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestDecimalPrecision()
        {
            var jsonStr = "{\"decimal\":123.45678901234567890}";
            var bean = JSONUtil.ToBean<DecimalBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class LargeNumBean
        {
            public long BigNumber { get; set; }
        }

        public class SciBean
        {
            public double SciNum { get; set; }
        }

        public class DecimalBean
        {
            public decimal Decimal { get; set; }
        }
    }
}
