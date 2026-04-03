using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2223 测试 - 测试深层嵌套对象
    /// </summary>
    public class Issue2223Test
    {
        [Fact]
        public void TestDeepNesting()
        {
            var jsonStr = "{\"level1\":{\"level2\":{\"level3\":{\"level4\":{\"value\":\"deep\"}}}}}";
            var bean = JSONUtil.ToBean<DeepBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("deep", bean.Level1?.Level2?.Level3?.Level4?.Value);
        }

        [Fact]
        public void TestArrayNesting()
        {
            var jsonStr = "{\"data\":[[[{\"value\":1}]]]}";
            var bean = JSONUtil.ToBean<ArrayNestingBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class Level4Bean
        {
            public string Value { get; set; }
        }

        public class Level3Bean
        {
            public Level4Bean Level4 { get; set; }
        }

        public class Level2Bean
        {
            public Level3Bean Level3 { get; set; }
        }

        public class Level1Bean
        {
            public Level2Bean Level2 { get; set; }
        }

        public class DeepBean
        {
            public Level1Bean Level1 { get; set; }
        }

        public class ArrayNestingBean
        {
            public object Data { get; set; }
        }
    }
}
