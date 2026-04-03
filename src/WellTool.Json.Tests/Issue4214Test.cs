using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue4214 测试
    /// </summary>
    public class Issue4214Test
    {
        [Fact]
        public void TestNegativeNumber()
        {
            var jsonStr = "{\"num\":-123.456}";
            var bean = JSONUtil.ToBean<NegativeBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(-123.456, bean.Num, 2);
        }

        public class NegativeBean
        {
            public double Num { get; set; }
        }
    }
}
