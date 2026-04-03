using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI59LW4 测试
    /// </summary>
    public class IssueI59LW4Test
    {
        [Fact]
        public void TestTrailingComma()
        {
            var jsonStr = "{\"a\":1,\"b\":2,}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.NotNull(obj);
        }
    }
}
