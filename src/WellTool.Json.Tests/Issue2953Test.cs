using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2953 测试
    /// </summary>
    public class Issue2953Test
    {
        [Fact]
        public void TestListOfObjects()
        {
            var jsonStr = "[{\"id\":1},{\"id\":2},{\"id\":3}]";
            var list = JSONUtil.ToList<ItemBean>(jsonStr);
            Assert.NotNull(list);
            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void TestEmptyListToJson()
        {
            var bean = new ListBean { Items = new System.Collections.Generic.List<ItemBean>() };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("[]", jsonStr);
        }

        public class ItemBean
        {
            public int Id { get; set; }
        }

        public class ListBean
        {
            public System.Collections.Generic.List<ItemBean> Items { get; set; }
        }
    }
}
