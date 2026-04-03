using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3619 测试
    /// </summary>
    public class Issue3619Test
    {
        [Fact]
        public void TestPrivateField()
        {
            var jsonStr = "{\"value\":\"test\"}";
            var bean = JSONUtil.ToBean<PrivateBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class PrivateBean
        {
            private string _value;
            public string Value { get => _value; set => _value = value; }
        }
    }
}
