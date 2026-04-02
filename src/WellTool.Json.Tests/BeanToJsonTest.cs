using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Bean 转 JSON 测试
    /// </summary>
    public class BeanToJsonTest
    {
        [Fact]
        public void ToJsonStrTest()
        {
            var readParam = new ReadParam
            {
                InitSpikeMac = "a",
                Mac = "b",
                SpikeMac = "c",
                Bag = "d",
                ProjectId = 123
            };

            var jsonStr = JSONUtil.ToJsonStr(readParam);
            Assert.Contains("initSpikeMac", jsonStr);
            Assert.Contains("\"a\"", jsonStr);
            Assert.Contains("123", jsonStr);
        }

        [Fact]
        public void ToBeanTest()
        {
            var json = "{\"name\":\"test\",\"age\":18}";
            var person = JSONUtil.ToBean<TestPerson>(json);
            
            Assert.NotNull(person);
        }

        [Fact]
        public void AnonymousToJsonTest()
        {
            var obj = new { name = "test", age = 18 };
            var json = JSONUtil.ToJsonStr(obj);
            
            Assert.Contains("test", json);
            Assert.Contains("18", json);
        }

        [Fact]
        public void NestedBeanToJsonTest()
        {
            var obj = new
            {
                User = new { Name = "test", Age = 18 },
                Count = 5
            };
            var json = JSONUtil.ToJsonStr(obj);
            
            Assert.Contains("User", json);
            Assert.Contains("test", json);
        }

        #region 辅助类

        private class ReadParam
        {
            public string InitSpikeMac { get; set; }
            public string Mac { get; set; }
            public string SpikeMac { get; set; }
            public string Bag { get; set; }
            public int ProjectId { get; set; }
        }

        private class TestPerson
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        #endregion
    }
}
