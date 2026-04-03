using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue488 测试
    /// </summary>
    public class Issue488Test
    {
        [Fact]
        public void TestParseBoolString()
        {
            var jsonStr = "{\"flag\":\"true\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.Equal("true", obj["flag"].ToString());
        }
    }
}
