using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// JSONBeanParser 测试
    /// </summary>
    public class JSONBeanParserTest
    {
        [Fact]
        public void TestParseToBean()
        {
            var jsonStr = "{\"id\":1,\"name\":\"test\",\"value\":123.45}";
            var bean = JSONUtil.ToBean<ParseTestBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(1, bean.Id);
            Assert.Equal("test", bean.Name);
            Assert.Equal(123.45, bean.Value, 2);
        }

        [Fact]
        public void TestParseToBeanWithNull()
        {
            var jsonStr = "{\"id\":null,\"name\":null}";
            var bean = JSONUtil.ToBean<ParseTestBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(0, bean.Id);
            Assert.Null(bean.Name);
        }

        [Fact]
        public void TestParseToBeanWithNested()
        {
            var jsonStr = "{\"id\":1,\"child\":{\"name\":\"child\"}}";
            var bean = JSONUtil.ToBean<NestedBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.NotNull(bean.Child);
            Assert.Equal("child", bean.Child.Name);
        }

        [Fact]
        public void TestParseToList()
        {
            var jsonStr = "[{\"id\":1},{\"id\":2},{\"id\":3}]";
            var list = JSONUtil.ToList<ParseTestBean>(jsonStr);
            Assert.NotNull(list);
            Assert.Equal(3, list.Count);
            Assert.Equal(1, list[0].Id);
            Assert.Equal(2, list[1].Id);
            Assert.Equal(3, list[2].Id);
        }

        public class ParseTestBean
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
        }

        public class NestedBean
        {
            public int Id { get; set; }
            public ParseTestBean Child { get; set; }
        }
    }
}
