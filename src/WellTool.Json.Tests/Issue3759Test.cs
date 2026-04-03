using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3759 测试
    /// </summary>
    public class Issue3759Test
    {
        [Fact]
        public void TestDeepCopy()
        {
            var jsonStr = "{\"data\":{\"nested\":{\"value\":123}}}";
            var obj = JSONUtil.ParseObj(jsonStr);
            var copy = obj.Clone();
            Assert.NotNull(copy);
        }

        [Fact]
        public void TestArrayClone()
        {
            var jsonStr = "[1,2,3]";
            var arr = JSONUtil.ParseArray(jsonStr);
            var copy = arr.Clone();
            Assert.NotNull(copy);
        }
    }
}
