using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueIB9MH0 测试
    /// </summary>
    public class IssueIB9MH0Test
    {
        [Fact]
        public void TestInt64Parse()
        {
            var jsonStr = "{\"num\":9223372036854775807}";
            var bean = JSONUtil.ToBean<Int64Bean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(9223372036854775807L, bean.Num);
        }

        public class Int64Bean
        {
            public long Num { get; set; }
        }
    }
}
