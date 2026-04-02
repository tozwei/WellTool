using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONObject 单元测试
    /// </summary>
    public class JSONObjectTest
    {
        #region 创建和解析测试

        [Fact]
        public void ParseStringTest()
        {
            var jsonStr = "{\"b\":\"value2\",\"c\":\"value3\",\"a\":\"value1\", \"d\": true, \"e\": null}";
            var jsonObject = JSONUtil.ParseObj(jsonStr);
            
            Assert.Equal("value1", jsonObject["a"]);
            Assert.Equal("value2", jsonObject["b"]);
            Assert.Equal("value3", jsonObject["c"]);
            Assert.True(Convert.ToBoolean(jsonObject["d"]));
        }

        [Fact]
        public void ParseStringTest2()
        {
            var jsonStr = "{\"file_name\":\"test\",\"error_code\":\"F140\",\"error_info\":\"错误信息\"}";
            var json = new JSONObject(jsonStr);
            
            Assert.Equal("F140", json.GetStr("error_code"));
            Assert.Equal("错误信息", json.GetStr("error_info"));
        }

        [Fact]
        public void ParseStringTest3()
        {
            var jsonStr = "{\"test\":\"体”、“文\"}";
            var json = new JSONObject(jsonStr);
            Assert.Equal("体”、“文", json.GetStr("test"));
        }

        [Fact]
        public void ParseStringTest4()
        {
            var jsonStr = "{'msg':'这里还没有内容','data':{'cards':[]},'ok':0}";
            var json = new JSONObject(jsonStr);
            
            Assert.Equal(0, json.GetInt("ok"));
        }

        [Fact]
        public void ParseStringWithSlashTest()
        {
            // 测试 </div> 中的 / 不会被转义
            var jsonStr = "{\"a\":\"<div>aaa</div>\"}";
            var json = new JSONObject(jsonStr);
            Assert.Equal("<div>aaa</div>", json["a"]);
            Assert.Equal(jsonStr, json.ToString());
        }

        #endregion

        #region Bean 转换测试

        [Fact]
        public void ToBeanTest()
        {
            var subJson = JSONUtil.CreateObj().Set("value1", "strValue1").Set("value2", "234");
            var json = JSONUtil.CreateObj()
                .Set("strValue", "strTest")
                .Set("intValue", 123)
                .Set("doubleValue", "")
                .Set("beanValue", subJson)
                .Set("list", JSONUtil.CreateArray().Set("a").Set("b"));

            var bean = json.ToBean<SimpleTestBean>();
            Assert.NotNull(bean);
            Assert.Equal("strTest", bean.StrValue);
            Assert.Equal(123, bean.IntValue);
        }

        [Fact]
        public void ToBeanNullStrTest()
        {
            var json = JSONUtil.CreateObj(JSONConfig.Create().SetIgnoreError(true))
                .Set("strValue", "null")
                .Set("intValue", 123);

            var bean = json.ToBean<SimpleTestBean>();
            Assert.NotNull(bean);
        }

        [Fact]
        public void ParseBeanTest()
        {
            var bean = new SimpleTestBean
            {
                StrValue = "strTest",
                IntValue = 123,
                DoubleValue = 111.1
            };

            var json = JSONUtil.ParseObj(bean, false);
            Assert.NotNull(json);
            Assert.Equal("strTest", json["strValue"]);
        }

        [Fact]
        public void BeanTransTest()
        {
            var userA = new UserA { Name = "nameTest", Age = 18 };

            var userAJson = JSONUtil.ParseObj(userA);
            var userB = JSONUtil.ToBean<UserB>(userAJson);

            Assert.Equal(userA.Name, userB.Name);
        }

        #endregion

        #region 特殊字符测试

        [Fact]
        public void SpecialCharTest()
        {
            var json = "{\"pattern\": \"[abc]\b\u2001\", \"pattern2Json\": {\"patternText\": \"[ab]\\b\"}}";
            var obj = JSONUtil.ParseObj(json);
            Assert.NotNull(obj);
        }

        [Fact]
        public void GetStrTest()
        {
            var json = "{\"name\": \"yyb\\nbbb\"}";
            var jsonObject = JSONUtil.ParseObj(json);

            Assert.Equal("yyb\nbbb", jsonObject.GetStr("name"));
        }

        #endregion

        #region 其他功能测试

        [Fact]
        public void AccumulateTest()
        {
            var jsonObject = JSONUtil.CreateObj().Accumulate("key1", "value1");
            Assert.Contains("key1", jsonObject.ToString());

            jsonObject.Accumulate("key1", "value2");
            Assert.Contains("value1", jsonObject.ToString());
            Assert.Contains("value2", jsonObject.ToString());
        }

        [Fact]
        public void PutByPathTest()
        {
            var json = new JSONObject();
            json.PutByPath("aa.bb", "BB");
            Assert.Contains("aa", json.ToString());
            Assert.Contains("BB", json.ToString());
        }

        #endregion

        #region 辅助类

        private class SimpleTestBean
        {
            public string StrValue { get; set; }
            public int IntValue { get; set; }
            public double DoubleValue { get; set; }
            public object BeanValue { get; set; }
            public List<string> List { get; set; }
        }

        private class UserA
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class UserB
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        #endregion
    }
}
