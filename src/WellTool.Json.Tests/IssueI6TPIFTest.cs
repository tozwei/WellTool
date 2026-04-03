using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI6TPIF 测试
    /// </summary>
    public class IssueI6TPIFTest
    {
        [Fact]
        public void TestBoolParse()
        {
            var jsonStr = "{\"flag\":True}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.NotNull(obj);
        }
    }
}
