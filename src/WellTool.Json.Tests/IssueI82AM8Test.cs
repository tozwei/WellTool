using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI82AM8 测试
    /// </summary>
    public class IssueI82AM8Test
    {
        [Fact]
        public void TestEmptyNestedObj()
        {
            var jsonStr = "{\"outer\":{\"inner\":{}}}";
            var bean = JSONUtil.ToBean<NestedEmptyBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class NestedEmptyBean
        {
            public InnerBean Outer { get; set; }
        }

        public class InnerBean
        {
            public object Inner { get; set; }
        }
    }
}
