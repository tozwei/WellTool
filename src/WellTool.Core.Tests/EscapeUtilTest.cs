using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Escape工具单元测试
    /// </summary>
    public class EscapeUtilTest
    {
        [Fact]
        public void EscapeTest()
        {
            var str = "<script>alert('xss');</script>";
            var escaped = EscapeUtil.Escape(str);
            Assert.NotEqual(str, escaped);
            Assert.DoesNotContain("<", escaped);
            Assert.DoesNotContain(">", escaped);
        }

        [Fact]
        public void UnescapeTest()
        {
            var str = "&lt;script&gt;alert(&#39;xss&#39;);&lt;/script&gt;";
            var unescaped = EscapeUtil.Unescape(str);
            Assert.Contains("<script>", unescaped);
            Assert.Contains("alert", unescaped);
        }

        [Fact]
        public void EscapeRoundTripTest()
        {
            var original = "Hello <World> & 'Test'";
            var escaped = EscapeUtil.Escape(original);
            var unescaped = EscapeUtil.Unescape(escaped);
            Assert.Equal(original, unescaped);
        }

        [Fact]
        public void EscapeChineseTest()
        {
            var str = "你好世界";
            var escaped = EscapeUtil.Escape(str);
            Assert.Equal(str, escaped); // 中文字符不应被转义
        }

        [Fact]
        public void EscapeEmptyTest()
        {
            Assert.Equal("", EscapeUtil.Escape(""));
            Assert.Equal("", EscapeUtil.Escape(null));
            Assert.Equal("", EscapeUtil.Unescape(""));
            Assert.Equal("", EscapeUtil.Unescape(null));
        }

        [Fact]
        public void EscapeSpecialCharsTest()
        {
            var str = "\"test\" & 'value'";
            var escaped = EscapeUtil.Escape(str);
            Assert.Contains("&quot;", escaped);
            Assert.Contains("&amp;", escaped);
            Assert.Contains("&#39;", escaped);
        }
    }
}
