using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueID418B 测试
    /// </summary>
    public class IssueID418BTest
    {
        [Fact]
        public void TestPrettyPrint()
        {
            var obj = new PrettyBean { Id = 1, Name = "test" };
            var pretty = JSONUtil.ToJsonPrettyStr(obj);
            Assert.Contains("\n", pretty);
            Assert.Contains("  ", pretty);
        }

        public class PrettyBean
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
