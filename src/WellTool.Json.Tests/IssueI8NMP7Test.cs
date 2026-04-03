using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI8NMP7 测试
    /// </summary>
    public class IssueI8NMP7Test
    {
        [Fact]
        public void TestBoolFromString()
        {
            var jsonStr = "{\"flag\":\"True\"}";
            var bean = JSONUtil.ToBean<BoolStrBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class BoolStrBean
        {
            public string Flag { get; set; }
        }
    }
}
