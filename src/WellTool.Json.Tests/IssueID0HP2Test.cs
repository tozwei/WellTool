using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueID0HP2 测试
    /// </summary>
    public class IssueID0HP2Test
    {
        [Fact]
        public void TestKeyWithSpecialChars()
        {
            var jsonStr = "{\"key@#$%\":\"value\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.Equal("value", obj["key@#$%"]);
        }
    }
}
