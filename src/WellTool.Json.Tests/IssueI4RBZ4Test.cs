using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI4RBZ4 测试
    /// </summary>
    public class IssueI4RBZ4Test
    {
        [Fact]
        public void TestListToBean()
        {
            var jsonStr = "{\"items\":[{\"id\":1},{\"id\":2}]}";
            var bean = JSONUtil.ToBean<ListItemBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal(2, bean.Items.Count);
        }

        public class ItemBean
        {
            public int Id { get; set; }
        }

        public class ListItemBean
        {
            public System.Collections.Generic.List<ItemBean> Items { get; set; }
        }
    }
}
