using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI90ADX 测试
    /// </summary>
    public class IssueI90ADXTest
    {
        [Fact]
        public void TestDotInKey()
        {
            var jsonStr = "{\"a.b\":\"value\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.Equal("value", obj["a.b"]);
        }
    }
}
