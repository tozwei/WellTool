using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssuesI44E4H 测试
    /// </summary>
    public class IssuesI44E4HTest
    {
        [Fact]
        public void TestObjContainsKey()
        {
            var jsonStr = "{\"key1\":\"v1\",\"key2\":\"v2\"}";
            var obj = JSONUtil.ParseObj(jsonStr);
            Assert.True(obj.ContainsKey("key1"));
            Assert.True(obj.ContainsKey("key2"));
            Assert.False(obj.ContainsKey("key3"));
        }
    }
}
