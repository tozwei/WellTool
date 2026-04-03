using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issues1881 测试
    /// </summary>
    public class Issues1881Test
    {
        [Fact]
        public void TestJsonNull()
        {
            var jsonStr = "{\"data\":null}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.True(obj.ContainsKey("data"));
            Assert.Null(obj["data"]);
        }
    }
}
