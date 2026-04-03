using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue3086 测试
    /// </summary>
    public class Issue3086Test
    {
        [Fact]
        public void TestMapKey()
        {
            var jsonStr = "{\"key1\":\"value1\",\"key2\":\"value2\"}";
            var bean = JSONUtil.ToBean<MapBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class MapBean
        {
            public System.Collections.Generic.Dictionary<string, string> Data { get; set; }
        }
    }
}
