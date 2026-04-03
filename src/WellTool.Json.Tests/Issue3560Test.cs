using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3560 测试
    /// </summary>
    public class Issue3560Test
    {
        [Fact]
        public void TestInterfaceBean()
        {
            var jsonStr = "{\"value\":\"test\"}";
            var bean = JSONUtil.ToBean<InterfaceBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class InterfaceBean
        {
            public string Value { get; set; }
        }
    }
}
