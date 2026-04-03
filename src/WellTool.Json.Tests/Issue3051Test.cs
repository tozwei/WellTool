using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3051 测试
    /// </summary>
    public class Issue3051Test
    {
        [Fact]
        public void TestSensitiveData()
        {
            var bean = new SensitiveBean { Password = "secret123" };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("secret123", jsonStr);
        }

        [Fact]
        public void TestNullSensitive()
        {
            var bean = new SensitiveBean { Password = null };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.DoesNotContain("null", jsonStr);
        }

        public class SensitiveBean
        {
            public string Password { get; set; }
        }
    }
}
