using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssuesI4V14N 测试
    /// </summary>
    public class IssuesI4V14NTest
    {
        [Fact]
        public void TestJsonPut()
        {
            var obj = new JSONObject();
            obj["key"] = "value";
            obj["num"] = 123;
            obj["bool"] = true;
            obj["arr"] = new JSONArray();
            Assert.Equal("value", obj["key"]);
            Assert.Equal(123, obj["num"]);
            Assert.Equal(true, obj["bool"]);
        }
    }
}
