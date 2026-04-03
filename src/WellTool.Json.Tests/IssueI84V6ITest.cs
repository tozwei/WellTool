using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI84V6I 测试
    /// </summary>
    public class IssueI84V6ITest
    {
        [Fact]
        public void TestSerializeStatic()
        {
            var bean = new StaticBean { InstanceValue = "test" };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("InstanceValue", jsonStr);
            Assert.DoesNotContain("StaticValue", jsonStr);
        }

        public class StaticBean
        {
            public static string StaticValue = "static";
            public string InstanceValue { get; set; }
        }
    }
}
