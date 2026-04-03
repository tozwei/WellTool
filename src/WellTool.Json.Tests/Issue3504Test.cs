using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3504 测试
    /// </summary>
    public class Issue3504Test
    {
        [Fact]
        public void TestUnicodeChars()
        {
            var jsonStr = "{\"emoji\":\"\U0001F600\",\"chinese\":\"中文测试\",\"arabic\":\"مرحبا\"}";
            var bean = JSONUtil.ToBean<UnicodeBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class UnicodeBean
        {
            public string Emoji { get; set; }
            public string Chinese { get; set; }
            public string Arabic { get; set; }
        }
    }
}
