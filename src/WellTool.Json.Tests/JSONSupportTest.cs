using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONSupport 测试
    /// </summary>
    public class JSONSupportTest
    {
        [Fact]
        public void TestIsValidJSON()
        {
            Assert.True(JSONUtil.IsValidJSON("{}"));
            Assert.True(JSONUtil.IsValidJSON("[]"));
            Assert.True(JSONUtil.IsValidJSON("{\"key\":\"value\"}"));
            Assert.True(JSONUtil.IsValidJSON("[1,2,3]"));
            Assert.False(JSONUtil.IsValidJSON("{key:value}"));
            Assert.False(JSONUtil.IsValidJSON("{key:\"value\"}"));
            Assert.False(JSONUtil.IsValidJSON("{key:}"));
        }

        [Fact]
        public void TestIsValidJSONArray()
        {
            Assert.True(JSONUtil.IsValidJSONArray("[1,2,3]"));
            Assert.True(JSONUtil.IsValidJSONArray("[]"));
            Assert.True(JSONUtil.IsValidJSONArray("[{}]"));
            Assert.False(JSONUtil.IsValidJSONArray("{}"));
            Assert.False(JSONUtil.IsValidJSONArray("{}"));
        }

        [Fact]
        public void TestIsValidJSONObject()
        {
            Assert.True(JSONUtil.IsValidJSONObject("{}"));
            Assert.True(JSONUtil.IsValidJSONObject("{\"key\":\"value\"}"));
            Assert.False(JSONUtil.IsValidJSONObject("[]"));
            Assert.False(JSONUtil.IsValidJSONObject("[]"));
        }

        [Fact]
        public void TestParseEmptyObject()
        {
            var json = JSONUtil.Parse("{}");
            Assert.NotNull(json);
            Assert.IsType<JSONObject>(json);
        }

        [Fact]
        public void TestParseEmptyArray()
        {
            var json = JSONUtil.Parse("[]");
            Assert.NotNull(json);
            Assert.IsType<JSONArray>(json);
        }
    }
}
