using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// PR1431 测试 - 测试日期格式处理
    /// </summary>
    public class Pr1431Test
    {
        [Fact]
        public void TestDateFormat()
        {
            var bean = new DateBean
            {
                Date = new DateTime(2023, 12, 25, 10, 30, 45)
            };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("2023", jsonStr);
            Assert.Contains("12", jsonStr);
            Assert.Contains("25", jsonStr);
        }

        [Fact]
        public void TestDateDeserialize()
        {
            var jsonStr = "{\"Date\":\"2023-12-25 10:30:45\"}";
            var bean = JSONUtil.ToBean<DateBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(2023, bean.Date.Year);
            Assert.Equal(12, bean.Date.Month);
            Assert.Equal(25, bean.Date.Day);
        }

        [Fact]
        public void TestIsoDate()
        {
            var jsonStr = "{\"Date\":\"2023-12-25T10:30:45\"}";
            var bean = JSONUtil.ToBean<DateBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class DateBean
        {
            public DateTime Date { get; set; }
        }
    }
}
