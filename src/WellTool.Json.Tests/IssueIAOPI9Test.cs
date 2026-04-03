using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueIAOPI9 测试
    /// </summary>
    public class IssueIAOPI9Test
    {
        [Fact]
        public void TestEscapedSlash()
        {
            var jsonStr = "{\"url\":\"http:\\/\\/example.com\"}";
            var bean = JSONUtil.ToBean<UrlBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Contains("http://", bean.Url);
        }

        public class UrlBean
        {
            public string Url { get; set; }
        }
    }
}
