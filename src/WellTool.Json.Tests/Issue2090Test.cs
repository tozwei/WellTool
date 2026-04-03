using System;
using Xunit;

namespace WellTool.Json.Tests
{
    /// <summary>
    /// Issue2090 测试 - 测试 JSON 中特殊字符处理
    /// </summary>
    public class Issue2090Test
    {
        [Fact]
        public void TestEscapeChars()
        {
            var jsonStr = "{\"text\":\"hello\\nworld\"}";
            var bean = JSONUtil.ToBean<EscapeBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("hello\nworld", bean.Text);
        }

        [Fact]
        public void TestUnicode()
        {
            var jsonStr = "{\"text\":\"\\u4e2d\\u6587\"}";
            var bean = JSONUtil.ToBean<EscapeBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("中文", bean.Text);
        }

        [Fact]
        public void TestBackslash()
        {
            var jsonStr = "{\"text\":\"path\\\\to\\\\file\"}";
            var bean = JSONUtil.ToBean<EscapeBean>(jsonStr);
            Assert.NotNull(bean);
            Assert.Equal("path\\to\\file", bean.Text);
        }

        public class EscapeBean
        {
            public string Text { get; set; }
        }
    }
}
