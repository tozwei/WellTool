using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2377 测试 - 测试空数组和空对象
    /// </summary>
    public class Issue2377Test
    {
        [Fact]
        public void TestEmptyArray()
        {
            var jsonStr = "{\"items\":[]}";
            var bean = JSONUtil.ToBean<EmptyArrayBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.NotNull(bean.Items);
            var items = (System.Collections.IEnumerable)bean.Items;
            Assert.Empty(items);
        }

        [Fact]
        public void TestEmptyObject()
        {
            var jsonStr = "{\"data\":{}}";
            var bean = JSONUtil.ToBean<EmptyObjBean>(jsonStr);
            Assert.NotNull(bean);
        }

        [Fact]
        public void TestNullArray()
        {
            var jsonStr = "{\"items\":null}";
            var bean = JSONUtil.ToBean<EmptyArrayBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Null(bean.Items);
        }

        public class EmptyArrayBean
        {
            public object Items { get; set; }
        }

        public class EmptyObjBean
        {
            public object Data { get; set; }
        }
    }
}
