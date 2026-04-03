using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI6SZYB 测试
    /// </summary>
    public class IssueI6SZYBTest
    {
        [Fact]
        public void TestPropertyCaseSensitive()
        {
            var jsonStr = "{\"Name\":\"test\",\"name\":\"Test2\"}";
            var bean = JSONUtil.ToBean<CaseBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class CaseBean
        {
            public string Name { get; set; }
        }
    }
}
