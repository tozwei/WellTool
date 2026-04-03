using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI49VZB 测试
    /// </summary>
    public class IssueI49VZBTest
    {
        [Fact]
        public void TestNullInArray()
        {
            var jsonStr = "[null,1,\"str\",null]";
            var arr = JSONUtil.ParseArray(jsonStr);
            Assert.NotNull(arr);
            Assert.Equal(4, arr.Count);
        }
    }
}
