using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3649 测试
    /// </summary>
    public class Issue3649Test
    {
        [Fact]
        public void TestExtraField()
        {
            var jsonStr = "{\"id\":1,\"extra\":\"data\"}";
            var bean = JSONUtil.ToBean<ExtraBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(1, bean.Id);
        }

        public class ExtraBean
        {
            public int Id { get; set; }
        }
    }
}
