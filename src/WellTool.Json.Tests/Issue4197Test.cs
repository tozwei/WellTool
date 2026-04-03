using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue4197 测试
    /// </summary>
    public class Issue4197Test
    {
        [Fact]
        public void TestDynamicKey()
        {
            var jsonStr = "{\"dynamicKey\":\"dynamicValue\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.Equal("dynamicValue", obj["dynamicKey"]);
        }

        [Fact]
        public void TestNumericKey()
        {
            var jsonStr = "{\"123\":\"numericKey\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.NotNull(obj);
        }
    }
}
