using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2447 测试 - 测试 JSON 属性顺序
    /// </summary>
    public class Issue2447Test
    {
        [Fact]
        public void TestPropertyOrder()
        {
            var bean = new OrderedBean { A = 1, B = 2, C = 3 };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("A", jsonStr);
            Assert.Contains("B", jsonStr);
            Assert.Contains("C", jsonStr);
        }

        [Fact]
        public void TestDeserializeOrder()
        {
            var jsonStr = "{\"a\":1,\"b\":2,\"c\":3}";
            var bean = JSONUtil.ToBean<OrderedBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(1, bean.A);
            Assert.Equal(2, bean.B);
            Assert.Equal(3, bean.C);
        }

        public class OrderedBean
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
        }
    }
}
