using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI6H0XF 测试
    /// </summary>
    public class IssueI6H0XFTest
    {
        [Fact]
        public void TestNestedArray()
        {
            var jsonStr = "[[1,2],[3,4],[5,6]]";
            var arr = JSONUtil.ParseArray(jsonStr);
            Assert.NotNull(arr);
            Assert.Equal(3, arr.Count);
        }
    }
}
