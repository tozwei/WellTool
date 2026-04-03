using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue4210 测试
    /// </summary>
    public class Issue4210Test
    {
        [Fact]
        public void TestISO8601Date()
        {
            var jsonStr = "{\"date\":\"2023-12-25T10:30:45+08:00\"}";
            var bean = JSONUtil.ToBean<DateIsoBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class DateIsoBean
        {
            public DateTime Date { get; set; }
        }
    }
}
