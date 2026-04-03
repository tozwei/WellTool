using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueIALQ0N 测试
    /// </summary>
    public class IssueIALQ0NTest
    {
        [Fact]
        public void TestNegativeIndex()
        {
            var jsonStr = "[1,2,3]";
            var arr = JSONUtil.ParseArray(jsonStr);
            Assert.Equal(3, arr.Count);
        }
    }
}
