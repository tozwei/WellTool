using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONConvert 测试
    /// </summary>
    public class JSONConvertTest
    {
        [Fact]
        public void TestToJson()
        {
            var testObj = new TestClass { Id = 1, Name = "test" };
            var jsonStr = JSONUtil.ToJsonStr(testObj);
            Assert.Contains("\"Id\":1", jsonStr);
            Assert.Contains("\"Name\":\"test\"", jsonStr);
        }

        [Fact]
        public void TestToJsonPretty()
        {
            var testObj = new TestClass { Id = 1, Name = "test" };
            var jsonStr = JSONUtil.ToJsonPrettyStr(testObj);
            Assert.Contains("\n", jsonStr);
            Assert.Contains("\"Id\": 1", jsonStr);
        }

        [Fact]
        public void TestParse()
        {
            var jsonStr = "{\"id\":1,\"name\":\"test\"}";
            var json = JSONUtil.Parse(jsonStr);
            Assert.NotNull(json);
            var jsonObject = (JSONObject)json;
            Assert.Equal(1L, jsonObject["id"]);
            Assert.Equal("test", jsonObject["name"]);
        }

        [Fact]
        public void TestParseArray()
        {
            var jsonStr = "[1,2,3]";
            var json = JSONUtil.Parse(jsonStr);
            Assert.NotNull(json);
            Assert.True(json is JSONArray);
            var arr = (JSONArray)json;
            Assert.Equal(3, arr.Count);
        }

        [Fact]
        public void TestToBean()
        {
            var jsonStr = "{\"id\":1,\"name\":\"test\"}";
            var obj = JSONUtil.ToBean<TestClass>(jsonStr);
            Assert.NotNull(obj);
            Assert.Equal(1, obj.Id);
            Assert.Equal("test", obj.Name);
        }

        private class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
