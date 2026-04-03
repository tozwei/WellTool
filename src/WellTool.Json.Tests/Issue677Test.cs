using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue677 测试
    /// </summary>
    public class Issue677Test
    {
        [Fact]
        public void TestNullValue()
        {
            var jsonStr = "{\"data\":null}";
            var bean = JSONUtil.ToBean<NullBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class NullBean
        {
            public object Data { get; set; }
        }
    }
}
