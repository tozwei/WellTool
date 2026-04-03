using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueIAP4GM 测试
    /// </summary>
    public class IssueIAP4GMTest
    {
        [Fact]
        public void TestObjToJson()
        {
            var obj = JSONUtil.ParseObj("{\"key\":\"value\"}");
            var jsonStr = JSONUtil.ToJsonStr(obj);
            Assert.Contains("key", jsonStr);
            Assert.Contains("value", jsonStr);
        }
    }
}
