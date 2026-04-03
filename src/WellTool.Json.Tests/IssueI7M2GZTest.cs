using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI7M2GZ 测试
    /// </summary>
    public class IssueI7M2GZTest
    {
        [Fact]
        public void TestNullableInt()
        {
            var jsonStr = "{\"value\":null}";
            var bean = JSONUtil.ToBean<NullableBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Null(bean.Value);
        }

        public class NullableBean
        {
            public int? Value { get; set; }
        }
    }
}
