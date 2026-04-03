using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2748 测试 - 测试继承类序列化
    /// </summary>
    public class Issue2748Test
    {
        [Fact]
        public void TestInheritSerialize()
        {
            var bean = new ChildBean { ParentProp = "parent", ChildProp = "child" };
            var jsonStr = JSONUtil.ToJsonStr(bean);
            Assert.Contains("ParentProp", jsonStr);
            Assert.Contains("ChildProp", jsonStr);
        }

        [Fact]
        public void TestInheritDeserialize()
        {
            var jsonStr = "{\"ParentProp\":\"p\",\"ChildProp\":\"c\"}";
            var bean = JSONUtil.ToBean<ChildBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("p", bean.ParentProp);
            Assert.Equal("c", bean.ChildProp);
        }

        public class ParentBean
        {
            public string ParentProp { get; set; }
        }

        public class ChildBean : ParentBean
        {
            public string ChildProp { get; set; }
        }
    }
}
