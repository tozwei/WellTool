using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI5OMSC 测试
    /// </summary>
    public class IssueI5OMSCTest
    {
        [Fact]
        public void TestLargeArray()
        {
            var arr = new System.Text.StringBuilder("[");
            for (int i = 0; i < 1000; i++)
            {
                if (i > 0) arr.Append(",");
                arr.Append($"{{\"id\":{i}}}");
            }
            arr.Append("]");
            var jsonStr = arr.ToString();
            var list = JSONUtil.ToList<LargeArrayBean>(jsonStr);
            Assert.Equal(1000, list.Count);
        }

        public class LargeArrayBean
        {
            public int Id { get; set; }
        }
    }
}
