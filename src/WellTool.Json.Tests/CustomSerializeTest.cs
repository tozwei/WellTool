using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// 自定义序列化测试
    /// </summary>
    public class CustomSerializeTest
    {
        [Fact]
        public void TestCustomSerializer()
        {
            var obj = new CustomBean { Value = "test" };
            var config = JSONConfig.Create()
                .SetIgnoreNullValue(true);
            var jsonStr = JSONUtil.ToJsonStr(obj, config);
            Assert.DoesNotContain("null", jsonStr);
        }

        [Fact]
        public void TestNullValue()
        {
            var obj = new CustomBean { Value = null };
            var config = JSONConfig.Create()
                .SetIgnoreNullValue(false);
            var jsonStr = JSONUtil.ToJsonStr(obj, config);
            Assert.Contains("null", jsonStr);
        }

        [Fact]
        public void TestDateFormat()
        {
            var obj = new DateBean { Date = new DateTime(2023, 1, 1) };
            var config = JSONConfig.Create()
                .SetDateFormat("yyyy-MM-dd");
            var jsonStr = JSONUtil.ToJsonStr(obj, config);
            Assert.Contains("2023-01-01", jsonStr);
        }

        public class CustomBean
        {
            public string Value { get; set; }
            public string NullField { get; set; }
        }

        public class DateBean
        {
            public DateTime Date { get; set; }
        }
    }
}
