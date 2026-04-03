using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2746 测试
    /// </summary>
    public class Issue2746Test
    {
        [Fact]
        public void TestMixedArray()
        {
            var jsonStr = "[1,\"str\",true,null,{},[]]";
            var arr = JSONUtil.ParseArray(jsonStr);
            Assert.NotNull(arr);
            Assert.Equal(6, arr.Count);
        }

        [Fact]
        public void TestNestedMixed()
        {
            var jsonStr = "{\"data\":[1,\"str\",{\"key\":\"value\"}]}";
            var bean = JSONUtil.ToBean<NestedMixedBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class NestedMixedBean
        {
            public object Data { get; set; }
        }
    }
}
