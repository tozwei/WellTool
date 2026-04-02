using System;
using System.Collections.Generic;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// ParseBean 单元测试
    /// </summary>
    public class ParseBeanTest
    {
        [Fact]
        public void ParseSimpleBeanTest()
        {
            var json = "{\"name\":\"test\",\"age\":18}";
            var bean = JSONUtil.ToBean<SimpleBean>(json);
            
            Assert.NotNull(bean);
            Assert.Equal("test", bean.Name);
            Assert.Equal(18, bean.Age);
        }

        [Fact]
        public void ParseNestedBeanTest()
        {
            var json = "{\"user\":{\"name\":\"test\",\"age\":18},\"count\":5}";
            var bean = JSONUtil.ToBean<NestedBean>(json);
            
            Assert.NotNull(bean);
            Assert.NotNull(bean.User);
            Assert.Equal("test", bean.User.Name);
            Assert.Equal(18, bean.User.Age);
            Assert.Equal(5, bean.Count);
        }

        [Fact]
        public void ParseListBeanTest()
        {
            var json = "{\"items\":[{\"id\":1},{\"id\":2}],\"total\":2}";
            var bean = JSONUtil.ToBean<ListBean>(json);
            
            Assert.NotNull(bean);
            Assert.NotNull(bean.Items);
            Assert.Equal(2, bean.Items.Count);
            Assert.Equal(1, bean.Items[0].Id);
            Assert.Equal(2, bean.Items[1].Id);
        }

        [Fact]
        public void ParseMapBeanTest()
        {
            var json = "{\"data\":{\"key1\":\"value1\",\"key2\":\"value2\"}}";
            var bean = JSONUtil.ToBean<MapBean>(json);
            
            Assert.NotNull(bean);
            Assert.NotNull(bean.Data);
            Assert.Equal("value1", bean.Data["key1"]);
            Assert.Equal("value2", bean.Data["key2"]);
        }

        [Fact]
        public void ParseWithCaseInsensitiveTest()
        {
            var json = "{\"Name\":\"test\",\"Age\":18}";
            var bean = JSONUtil.ToBean<SimpleBean>(json);
            
            Assert.NotNull(bean);
        }

        [Fact]
        public void ParseWithMissingFieldTest()
        {
            var json = "{\"name\":\"test\"}";
            var bean = JSONUtil.ToBean<SimpleBean>(json);
            
            Assert.NotNull(bean);
            Assert.Equal("test", bean.Name);
            Assert.Equal(0, bean.Age); // 默认值
        }

        [Fact]
        public void ParseWithExtraFieldTest()
        {
            var json = "{\"name\":\"test\",\"extra\":\"data\",\"age\":18}";
            var bean = JSONUtil.ToBean<SimpleBean>(json);
            
            Assert.NotNull(bean);
            Assert.Equal("test", bean.Name);
            Assert.Equal(18, bean.Age);
        }

        [Fact]
        public void ParseBooleanFieldTest()
        {
            var json = "{\"active\":true,\"deleted\":false}";
            var bean = JSONUtil.ToBean<BooleanBean>(json);
            
            Assert.NotNull(bean);
            Assert.True(bean.Active);
            Assert.False(bean.Deleted);
        }

        [Fact]
        public void ParseNumberFieldTest()
        {
            var json = "{\"intValue\":10,\"longValue\":10000000000,\"doubleValue\":3.14}";
            var bean = JSONUtil.ToBean<NumberBean>(json);
            
            Assert.NotNull(bean);
            Assert.Equal(10, bean.IntValue);
            Assert.Equal(10000000000L, bean.LongValue);
            Assert.Equal(3.14, bean.DoubleValue, 2);
        }

        [Fact]
        public void ParseDateFieldTest()
        {
            var json = "{\"createTime\":\"2024-01-01 12:00:00\"}";
            var bean = JSONUtil.ToBean<DateBean>(json);
            
            Assert.NotNull(bean);
        }

        #region 辅助类

        private class SimpleBean
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        private class NestedBean
        {
            public SimpleBean User { get; set; }
            public int Count { get; set; }
        }

        private class ItemBean
        {
            public int Id { get; set; }
        }

        private class ListBean
        {
            public List<ItemBean> Items { get; set; }
            public int Total { get; set; }
        }

        private class MapBean
        {
            public Dictionary<string, string> Data { get; set; }
        }

        private class BooleanBean
        {
            public bool Active { get; set; }
            public bool Deleted { get; set; }
        }

        private class NumberBean
        {
            public int IntValue { get; set; }
            public long LongValue { get; set; }
            public double DoubleValue { get; set; }
        }

        private class DateBean
        {
            public string CreateTime { get; set; }
        }

        #endregion
    }
}
