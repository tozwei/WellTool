using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI7FQ29 测试
    /// </summary>
    public class IssueI7FQ29Test
    {
        [Fact]
        public void TestDeepNestedArray()
        {
            var jsonStr = "{\"data\":[[{\"a\":1}]]}";
            var bean = JSONUtil.ToBean<DeepNestBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class DeepNestBean
        {
            public object Data { get; set; }
        }
    }
}
