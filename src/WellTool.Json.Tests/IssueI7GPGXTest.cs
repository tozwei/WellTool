using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI7GPGX 测试
    /// </summary>
    public class IssueI7GPGXTest
    {
        [Fact]
        public void TestScientificNotationParse()
        {
            var jsonStr = "{\"num\":1e10}";
            var bean = JSONUtil.ToBean<SciBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class SciBean
        {
            public double Num { get; set; }
        }
    }
}
