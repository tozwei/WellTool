using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3588 测试
    /// </summary>
    public class Issue3588Test
    {
        [Fact]
        public void TestListOfInt()
        {
            var jsonStr = "{\"ints\":[1,2,3,4,5]}";
            var bean = JSONUtil.ToBean<ListIntBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(5, bean.Ints.Count);
        }

        [Fact]
        public void TestListOfString()
        {
            var jsonStr = "{\"strs\":[\"a\",\"b\",\"c\"]}";
            var bean = JSONUtil.ToBean<ListStrBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(3, bean.Strs.Count);
        }

        public class ListIntBean
        {
            public System.Collections.Generic.List<int> Ints { get; set; }
        }

        public class ListStrBean
        {
            public System.Collections.Generic.List<string> Strs { get; set; }
        }
    }
}
