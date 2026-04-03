using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3274 测试
    /// </summary>
    public class Issue3274Test
    {
        [Fact]
        public void TestDuplicateKeys()
        {
            var jsonStr = "{\"key\":\"value1\",\"key\":\"value2\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.NotNull(obj);
        }

        [Fact]
        public void TestOverwriteBehavior()
        {
            var jsonStr = "{\"name\":\"first\",\"name\":\"second\"}";
            var bean = JSONUtil.ToBean<DuplicateBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class DuplicateBean
        {
            public string Name { get; set; }
        }
    }
}
