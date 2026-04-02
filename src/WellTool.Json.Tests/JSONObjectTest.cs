using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONObject 单元测试
    /// </summary>
    public class JSONObjectTest
    {
        #region 构造函数测试

        [Fact]
        public void TestDefaultConstructor()
        {
            var obj = new JSONObject();
            Assert.NotNull(obj);
            Assert.Empty(obj);
        }

        [Fact]
        public void TestFromString()
        {
            var obj = new JSONObject("{\"name\":\"test\",\"age\":18}");
            Assert.Equal("test", obj.GetStr("name"));
            Assert.Equal(18, obj.GetInt("age"));
        }

        [Fact]
        public void TestFromDictionary()
        {
            var dict = new System.Collections.Generic.Dictionary<string, object>
            {
                { "name", "test" },
                { "age", 18 }
            };
            var obj = new JSONObject(dict);
            Assert.Equal("test", obj.GetStr("name"));
            Assert.Equal(18, obj.GetInt("age"));
        }

        [Fact]
        public void TestFromObject()
        {
            var obj = new JSONObject(new TestClass { Name = "test", Age = 18 });
            Assert.Equal("test", obj.GetStr("Name"));
            Assert.Equal(18, obj.GetInt("Age"));
        }

        [Fact]
        public void TestWithConfig()
        {
            var config = JSONConfig.Create().SetIgnoreNullValue(false);
            var obj = new JSONObject(config);
            obj["key"] = null;
            Assert.True(obj.ContainsKey("key"));
        }

        #endregion

        #region Set 和 Get 方法测试

        [Fact]
        public void TestSet()
        {
            var obj = new JSONObject();
            obj.Set("name", "test");
            Assert.Equal("test", obj.GetStr("name"));
        }

        [Fact]
        public void TestSetReturnsThis()
        {
            var obj = new JSONObject();
            var result = obj.Set("name", "test");
            Assert.Same(obj, result);
        }

        [Fact]
        public void TestGetObjWithDefault()
        {
            var obj = new JSONObject();
            obj["name"] = "test";
            Assert.Equal("test", obj.GetObj("name", "default"));
            Assert.Equal("default", obj.GetObj("notexist", "default"));
        }

        [Fact]
        public void TestGetInt()
        {
            var obj = new JSONObject();
            obj["int"] = 42;
            Assert.Equal(42, obj.GetInt("int"));
            Assert.Equal(0, obj.GetInt("notexist"));
            Assert.Equal(-1, obj.GetInt("notexist", -1));
        }

        [Fact]
        public void TestGetLong()
        {
            var obj = new JSONObject();
            obj["long"] = 12345678901234L;
            Assert.Equal(12345678901234L, obj.GetLong("long"));
        }

        [Fact]
        public void TestGetDouble()
        {
            var obj = new JSONObject();
            obj["double"] = 3.14159;
            Assert.Equal(3.14159, obj.GetDouble("double"), 5);
        }

        [Fact]
        public void TestGetBool()
        {
            var obj = new JSONObject();
            obj["bool"] = true;
            Assert.True(obj.GetBool("bool"));
            Assert.False(obj.GetBool("notexist"));
            Assert.True(obj.GetBool("notexist", true));
        }

        [Fact]
        public void TestGetJSONObject()
        {
            var obj = new JSONObject();
            var nested = new JSONObject();
            nested["key"] = "value";
            obj["nested"] = nested;
            Assert.NotNull(obj.GetJSONObject("nested"));
            Assert.Equal("value", obj.GetJSONObject("nested").GetStr("key"));
        }

        [Fact]
        public void TestGetJSONArray()
        {
            var obj = new JSONObject();
            var array = new JSONArray();
            array.Set(1).Set(2).Set(3);
            obj["array"] = array;
            Assert.NotNull(obj.GetJSONArray("array"));
            Assert.Equal(3, obj.GetJSONArray("array").Count);
        }

        #endregion

        #region 特殊方法测试

        [Fact]
        public void TestPutOnce()
        {
            var obj = new JSONObject();
            obj.PutOnce("key", "value1");
            Assert.Equal("value1", obj["key"]);

            Assert.Throws<JSONException>(() => obj.PutOnce("key", "value2"));
        }

        [Fact]
        public void TestPutOpt()
        {
            var obj = new JSONObject();
            obj.PutOpt("key", "value");
            Assert.Equal("value", obj["key"]);

            obj.PutOpt(null, "value");
            Assert.False(obj.ContainsKey("null"));

            obj.PutOpt("key2", null);
            Assert.False(obj.ContainsKey("key2"));
        }

        [Fact]
        public void TestAccumulate()
        {
            var obj = new JSONObject();
            obj.Accumulate("key", "value1");
            Assert.Equal("value1", obj["key"]);

            obj.Accumulate("key", "value2");
            var arr = obj.GetJSONArray("key");
            Assert.NotNull(arr);
            Assert.Equal(2, arr.Count);
        }

        [Fact]
        public void TestAppend()
        {
            var obj = new JSONObject();
            obj.Append("list", "value1");
            Assert.Equal("value1", obj.GetJSONArray("list")[0]);

            obj.Append("list", "value2");
            Assert.Equal(2, obj.GetJSONArray("list").Count);

            Assert.Throws<JSONException>(() => obj.Append("name", "value"));
        }

        [Fact]
        public void TestIncrement()
        {
            var obj = new JSONObject();
            obj.Increment("count");
            Assert.Equal(1, obj.GetInt("count"));

            obj.Increment("count");
            Assert.Equal(2, obj.GetInt("count"));
        }

        #endregion

        #region ToString 和 ToJSONString 测试

        [Fact]
        public void TestToString()
        {
            var obj = new JSONObject();
            obj.Set("name", "test");
            obj.Set("age", 18);
            var str = obj.ToString();
            Assert.Contains("name", str);
            Assert.Contains("test", str);
            Assert.Contains("age", str);
        }

        [Fact]
        public void TestToJSONStringWithIndent()
        {
            var obj = new JSONObject();
            obj.Set("name", "test");
            var str = obj.ToJSONString(4);
            Assert.Contains("\n", str);
        }

        #endregion

        #region ToBean 测试

        [Fact]
        public void TestToBean()
        {
            var obj = new JSONObject();
            obj["Name"] = "test";
            obj["Age"] = 18;
            var person = obj.ToBean<TestClass>();
            Assert.Equal("test", person.Name);
            Assert.Equal(18, person.Age);
        }

        [Fact]
        public void TestToBeanNull()
        {
            var obj = new JSONObject();
            var result = obj.ToBean<TestClass>();
            Assert.NotNull(result);
        }

        #endregion

        #region Clone 测试

        [Fact]
        public void TestClone()
        {
            var obj = new JSONObject();
            obj.Set("name", "test");
            var clone = obj.Clone();
            Assert.Equal("test", clone["name"]);

            clone.Set("name", "changed");
            Assert.Equal("test", obj["name"]);
        }

        #endregion

        #region GetByPath 测试

        [Fact]
        public void TestGetByPath()
        {
            var obj = new JSONObject();
            var nested = new JSONObject();
            nested.Set("name", "deep");
            obj.Set("nested", nested);

            Assert.Equal("deep", obj.GetByPath("nested.name"));
        }

        [Fact]
        public void TestPutByPath()
        {
            var obj = new JSONObject();
            obj.PutByPath("user.profile.name", "test");

            var user = obj.GetJSONObject("user");
            Assert.NotNull(user);
            var profile = user.GetJSONObject("profile");
            Assert.NotNull(profile);
            Assert.Equal("test", profile.GetStr("name"));
        }

        #endregion

        #region ToJSONArray 测试

        [Fact]
        public void TestToJSONArray()
        {
            var obj = new JSONObject();
            obj.Set("name", "test");
            obj.Set("age", 18);
            obj.Set("city", "beijing");

            var array = obj.ToJSONArray(new[] { "name", "age" });
            Assert.Equal(2, array.Count);
            Assert.Equal("test", array[0]);
            Assert.Equal(18, array[1]);
        }

        #endregion

        #region 支持类

        private class TestClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        #endregion
    }
}
