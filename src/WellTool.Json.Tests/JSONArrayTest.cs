using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONArray 单元测试
    /// </summary>
    public class JSONArrayTest
    {
        #region 创建和转换测试

        [Fact]
        public void CreateJSONArrayFromJSONObjectTest()
        {
            // JSONObject可转换为JSONArray
            var jsonObject = JSONUtil.CreateObj();
            var jsonArray = new JSONArray(jsonObject);

            Assert.NotNull(jsonArray);
            Assert.Equal(0, jsonArray.Count);

            jsonObject.Set("key1", "value1");
            jsonArray = new JSONArray(jsonObject);
            Assert.Equal(1, jsonArray.Count);
            Assert.Contains("key1", jsonArray.ToString());
        }

        [Fact]
        public void AddNullTest()
        {
            var list = new List<string> { "aaa", null };
            var jsonStr = JSONUtil.ToJsonStr(list);
            Assert.Contains("aaa", jsonStr);
        }

        [Fact]
        public void AddTest()
        {
            // 方法1
            var array = JSONUtil.CreateArray();
            array.Add("value1");
            array.Add("value2");
            array.Add("value3");

            Assert.Equal("value1", array.GetStr(0));
            Assert.Equal("value2", array.GetStr(1));
            Assert.Equal("value3", array.GetStr(2));
        }

        [Fact]
        public void SetTest()
        {
            var array = JSONUtil.CreateArray();
            array.Set(0, "value1");
            array.Set(1, "value2");

            Assert.Equal("value1", array.GetStr(0));
            Assert.Equal("value2", array.GetStr(1));
        }

        [Fact]
        public void ParseTest()
        {
            string jsonStr = "[\"value1\", \"value2\", \"value3\"]";
            var array = JSONUtil.ParseArray(jsonStr);
            Assert.Equal("value1", array.GetStr(0));
            Assert.Equal("value2", array.GetStr(1));
            Assert.Equal("value3", array.GetStr(2));
        }

        [Fact]
        public void ParseWithNullTest()
        {
            string jsonStr = "[{\"grep\":\"4.8\",\"result\":\"右\"},{\"grep\":\"4.8\",\"result\":null}]";
            var jsonArray = JSONUtil.ParseArray(jsonStr);
            
            // 验证可以解析
            Assert.Equal(2, jsonArray.Count);
            Assert.Equal("4.8", jsonArray.GetJSONObject(1).GetStr("grep"));
        }

        [Fact]
        public void ParseBeanListTest()
        {
            var list = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string> { { "akey", "aValue1" }, { "bkey", "bValue1" } },
                new Dictionary<string, string> { { "akey", "aValue2" }, { "bkey", "bValue2" } }
            };

            var jsonArray = JSONUtil.ParseArray(list);
            Assert.Equal("aValue1", jsonArray.GetJSONObject(0).GetStr("akey"));
            Assert.Equal("bValue2", jsonArray.GetJSONObject(1).GetStr("bkey"));
        }

        [Fact]
        public void ToListTest()
        {
            string jsonArr = "[{\"id\":111,\"name\":\"test1\"},{\"id\":112,\"name\":\"test2\"}]";

            var array = JSONUtil.ParseArray(jsonArr);
            var list = JSONUtil.ToList<TestUser>(array);

            Assert.NotNull(list);
            Assert.True(list.Count > 0);
            Assert.Equal(111, list[0].Id);
            Assert.Equal(112, list[1].Id);
            Assert.Equal("test1", list[0].Name);
            Assert.Equal("test2", list[1].Name);
        }

        [Fact]
        public void ToDictListTest()
        {
            string jsonArr = "[{\"id\":111,\"name\":\"test1\"},{\"id\":112,\"name\":\"test2\"}]";

            var array = JSONUtil.ParseArray(jsonArr);
            var list = JSONUtil.ToList<Dictionary<string, object>>(array);

            Assert.NotNull(list);
            Assert.True(list.Count > 0);
            Assert.Equal(111, Convert.ToInt32(list[0]["id"]));
            Assert.Equal(112, Convert.ToInt32(list[1]["id"]));
            Assert.Equal("test1", list[0]["name"].ToString());
            Assert.Equal("test2", list[1]["name"].ToString());
        }

        [Fact]
        public void ToArrayTest()
        {
            string jsonArr = "[{\"id\":111,\"name\":\"test1\"},{\"id\":112,\"name\":\"test2\"}]";

            var array = JSONUtil.ParseArray(jsonArr);
            var list = JSONUtil.ToList<TestUser>(array);

            Assert.NotNull(list);
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void GetByPathTest()
        {
            string jsonStr = "[{\"id\": \"1\",\"name\": \"a\"},{\"id\": \"2\",\"name\": \"b\"}]";
            var jsonArray = JSONUtil.ParseArray(jsonStr);
            
            Assert.Equal("b", jsonArray.GetByPath("[1].name"));
            Assert.Equal("b", JSONUtil.GetByPath(jsonArray, "[1].name"));
        }

        [Fact]
        public void PutNullTest()
        {
            var array = JSONUtil.CreateArray(JSONConfig.Create().SetIgnoreNullValue(false));
            array.Set(null);

            Assert.Single(array);
        }

        #endregion

        #region 辅助类

        private class TestUser
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        #endregion
    }
}
