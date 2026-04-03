using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI4XFMW 测试
    /// </summary>
    public class IssueI4XFMWTest
    {
        [Fact]
        public void TestMapToBean()
        {
            var jsonStr = "{\"data\":{\"key1\":\"v1\",\"key2\":\"v2\"}}";
            var bean = JSONUtil.ToBean<MapBean>(jsonStr);
            Assert.NotNull(bean);
        }

        public class MapBean
        {
            public System.Collections.Generic.Dictionary<string, string> Data { get; set; }
        }
    }
}
