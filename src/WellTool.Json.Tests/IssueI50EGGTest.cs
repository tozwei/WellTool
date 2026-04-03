using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI50EGG 测试
    /// </summary>
    public class IssueI50EGGTest
    {
        [Fact]
        public void TestComment()
        {
            var jsonStr = "{\"data\":\"value\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.Equal("value", obj["data"]);
        }
    }
}
