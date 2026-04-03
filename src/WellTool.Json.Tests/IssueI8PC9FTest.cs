using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// IssueI8PC9F 测试
    /// </summary>
    public class IssueI8PC9FTest
    {
        [Fact]
        public void TestChineseChars()
        {
            var jsonStr = "{\"text\":\"中文测试\\u4e2d\\u6587\"}";
            var bean = JSONUtil.ToBean<ChineseBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Contains("中文", bean.Text);
        }

        public class ChineseBean
        {
            public string Text { get; set; }
        }
    }
}
