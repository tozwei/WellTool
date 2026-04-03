using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueIA5YOE 测试
    /// </summary>
    public class IssueIA5YOETest
    {
        [Fact]
        public void TestArrayIndex()
        {
            var jsonStr = "[1,2,3,4,5]";
            var arr = JSONUtil.ParseArray(jsonStr);
            Assert.Equal(1L, arr[0]);
            Assert.Equal(5L, arr[4]);
        }
    }
}
