using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONUtil 单元测试
    /// </summary>
    public class JSONUtilTest
    {
        #region Create 方法测试

        [Fact]
        public void TestCreateObj()
        {
            var obj = JSONUtil.CreateObj();
            Assert.NotNull(obj);
            Assert.Empty(obj.Keys);
        }

        [Fact]
        public void TestCreateObjWithConfig()
        {
            var config = JSONConfig.Create().SetIgnoreNullValue(false);
            var obj = JSONUtil.CreateObj(config);
            Assert.NotNull(obj);
        }

        [Fact]
        public void TestCreateArray()
        {
            var array = JSONUtil.CreateArray();
            Assert.NotNull(array);
            Assert.Empty(array);
        }

        [Fact]
        public void TestCreateArrayWithConfig()
        {
            var config = JSONConfig.Create().SetIgnoreNullValue(false);
            var array = JSONUtil.CreateArray(config);
            Assert.NotNull(array);
        }

        #endregion

        #region ParseObj 方法测试

        [Fact]
        public void TestParseObjFromString()
        {
            var json = "{\"name\":\"test\",\"age\":18}";
            var obj = JSONUtil.ParseObj(json);
            Assert.NotNull(obj);
            Assert.Equal("test", obj.GetStr("name"));
            Assert.Equal(18, obj.GetInt("age"));
        }

        [Fact]
        public void TestParseObjFromObject()
        {
            var dict = new Dictionary<string, object>
            {
                { "name", "test" },
                { "age", 18 }
            };
            var obj = JSONUtil.ParseObj(dict);
            Assert.NotNull(obj);
            Assert.Equal("test", obj.GetStr("name"));
            Assert.Equal(18, obj.GetInt("age"));
        }

        [Fact]
        public void TestParseObjFromNull()
        {
            var obj = JSONUtil.ParseObj((string)null);
            Assert.Null(obj);
        }

        [Fact]
        public void TestParseObjWithIgnoreNull()
        {
            var obj = JSONUtil.ParseObj(new { name = "test", age = (string)null }, true);
            Assert.False(obj.ContainsKey("age"));
        }

        #endregion

        #region ParseArray 方法测试

        [Fact]
        public void TestParseArrayFromString()
        {
            var json = "[1,2,3,\"test\"]";
            var array = JSONUtil.ParseArray(json);
            Assert.NotNull(array);
            Assert.Equal(4, array.Count);
            Assert.Equal(1, array.GetInt(0));
            Assert.Equal("test", array.GetStr(3));
        }

        [Fact]
        public void TestParseArrayFromList()
        {
            var list = new List<object> { 1, 2, 3 };
            var array = JSONUtil.ParseArray(list);
            Assert.NotNull(array);
            Assert.Equal(3, array.Count);
        }

        [Fact]
        public void TestParseArrayFromNull()
        {
            var array = JSONUtil.ParseArray((string)null);
            Assert.Null(array);
        }

        #endregion

        #region Parse 方法测试

        [Fact]
        public void TestParseJSONObject()
        {
            var json = "{\"name\":\"test\"}";
            var result = JSONUtil.Parse(json);
            Assert.NotNull(result);
            Assert.True(result is JSONObject);
        }

        [Fact]
        public void TestParseJSONArray()
        {
            var json = "[1,2,3]";
            var result = JSONUtil.Parse(json);
            Assert.NotNull(result);
            Assert.True(result is JSONArray);
        }

        [Fact]
        public void TestParseNull()
        {
            var result = JSONUtil.Parse(null);
            Assert.Null(result);
        }

        #endregion

        #region ToJsonStr 方法测试

        [Fact]
        public void TestToJsonStrFromObject()
        {
            var obj = new { name = "test", age = 18 };
            var json = JSONUtil.ToJsonStr(obj);
            Assert.Contains("test", json);
            Assert.Contains("18", json);
        }

        [Fact]
        public void TestToJsonStrFromJSONObject()
        {
            var obj = JSONUtil.CreateObj()
                .Set("name", "test")
                .Set("age", 18);
            var json = JSONUtil.ToJsonStr(obj);
            Assert.Contains("test", json);
            Assert.Contains("18", json);
        }

        [Fact]
        public void TestToJsonStrWithNull()
        {
            var json = JSONUtil.ToJsonStr((object)null);
            Assert.Null(json);
        }

        [Fact]
        public void TestToJsonPrettyStr()
        {
            var obj = JSONUtil.CreateObj()
                .Set("name", "test")
                .Set("age", 18);
            var json = JSONUtil.ToJsonPrettyStr(obj);
            Assert.Contains("\n", json);
        }

        #endregion

        #region ToBean 方法测试

        [Fact]
        public void TestToBean()
        {
            var json = "{\"Name\":\"test\",\"Age\":18}";
            var person = JSONUtil.ToBean<Person>(json);
            Assert.NotNull(person);
            Assert.Equal("test", person.Name);
            Assert.Equal(18, person.Age);
        }

        [Fact]
        public void TestToBeanFromJSONObject()
        {
            var obj = JSONUtil.CreateObj()
                .Set("Name", "test")
                .Set("Age", 18);
            var person = JSONUtil.ToBean<Person>(obj);
            Assert.NotNull(person);
            Assert.Equal("test", person.Name);
            Assert.Equal(18, person.Age);
        }

        [Fact]
        public void TestToList()
        {
            var json = "[{\"Name\":\"a\",\"Age\":1},{\"Name\":\"b\",\"Age\":2}]";
            var list = JSONUtil.ToList<Person>(json);
            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
            Assert.Equal("a", list[0].Name);
            Assert.Equal("b", list[1].Name);
        }

        #endregion

        #region 类型判断方法测试

        [Fact]
        public void TestIsTypeJSON()
        {
            Assert.True(JSONUtil.IsTypeJSON("{}"));
            Assert.True(JSONUtil.IsTypeJSON("[]"));
            Assert.False(JSONUtil.IsTypeJSON("test"));
        }

        [Fact]
        public void TestIsTypeJSONObject()
        {
            Assert.True(JSONUtil.IsTypeJSONObject("{}"));
            Assert.True(JSONUtil.IsTypeJSONObject(" { } "));
            Assert.False(JSONUtil.IsTypeJSONObject("[]"));
            Assert.False(JSONUtil.IsTypeJSONObject("test"));
        }

        [Fact]
        public void TestIsTypeJSONArray()
        {
            Assert.True(JSONUtil.IsTypeJSONArray("[]"));
            Assert.True(JSONUtil.IsTypeJSONArray(" [ ] "));
            Assert.False(JSONUtil.IsTypeJSONArray("{}"));
            Assert.False(JSONUtil.IsTypeJSONArray("test"));
        }

        [Fact]
        public void TestIsNull()
        {
            Assert.True(JSONUtil.IsNull(null));
            Assert.True(JSONUtil.IsNull(JSONNull.NULL));
            Assert.False(JSONUtil.IsNull("test"));
        }

        #endregion

        #region GetByPath 方法测试

        [Fact]
        public void TestGetByPath()
        {
            var json = JSONUtil.ParseObj("{\"user\":{\"name\":\"test\",\"age\":18},\"list\":[1,2,3]}");
            Assert.Equal("test", JSONUtil.GetByPath(json, "user.name"));
            Assert.Equal(18, JSONUtil.GetByPath(json, "user.age"));
            Assert.Equal(2, JSONUtil.GetByPath(json, "list[1]"));
        }

        [Fact]
        public void TestPutByPath()
        {
            var json = JSONUtil.CreateObj();
            JSONUtil.PutByPath(json, "user.name", "test");
            Assert.Equal("test", JSONUtil.GetByPath(json, "user.name"));
        }

        #endregion

        #region Quote 和 Escape 方法测试

        [Fact]
        public void TestQuote()
        {
            var result = JSONUtil.Quote("test");
            Assert.Equal("\"test\"", result);
        }

        [Fact]
        public void TestQuoteWithSpecialChars()
        {
            var result = JSONUtil.Quote("test\"quote");
            Assert.Equal("\"test\\\"quote\"", result);
        }

        [Fact]
        public void TestEscape()
        {
            var result = JSONUtil.Escape("line1\nline2");
            Assert.Contains("\\n", result);
        }

        #endregion

        #region FormatJsonStr 方法测试

        [Fact]
        public void TestFormatJsonStr()
        {
            var json = "{\"name\":\"test\",\"age\":18}";
            var formatted = JSONUtil.FormatJsonStr(json);
            Assert.Contains("\n", formatted);
        }

        #endregion

        #region 支持类

        private class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        #endregion
    }
}
