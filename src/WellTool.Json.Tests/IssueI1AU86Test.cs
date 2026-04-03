using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI1AU86 测试
    /// </summary>
    public class IssueI1AU86Test
    {
        [Fact]
        public void TestSpecialChars()
        {
            var jsonStr = "{\"data\":\"test\\u002Fpath\"}";
            var bean = JSONUtil.ToBean<SpecialBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class SpecialBean
        {
            public string Data { get; set; }
        }
    }
}
