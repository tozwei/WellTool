using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Re(正则表达式)工具单元测试
    /// </summary>
    public class ReUtilTest
    {
        [Fact]
        public void IsMatchTest()
        {
            Assert.True(ReUtil.IsMatch(@"\d+", "123"));
            Assert.False(ReUtil.IsMatch(@"\d+", "abc"));
        }

        [Fact]
        public void GetMatchTest()
        {
            var match = ReUtil.GetMatch(@"\d+", "abc123def456");
            Assert.NotNull(match);
            Assert.Equal("123", match.Value);
        }

        [Fact]
        public void GetAllMatchesTest()
        {
            var matches = ReUtil.GetAllMatches(@"\d+", "abc123def456");
            Assert.Equal(2, matches.Count);
            Assert.Equal("123", matches[0].Value);
            Assert.Equal("456", matches[1].Value);
        }

        [Fact]
        public void GetFirstTest()
        {
            var result = ReUtil.GetFirst(@"\d+", "abc123def456");
            Assert.Equal("123", result);
        }

        [Fact]
        public void GetGroupCountTest()
        {
            var count = ReUtil.GetGroupCount(@"\d+");
            Assert.Equal(0, count);

            var count2 = ReUtil.GetGroupCount(@"(\d+)([a-z]+)");
            Assert.Equal(2, count2);
        }

        [Fact]
        public void ReplaceTest()
        {
            var result = ReUtil.Replace("abc123def", @"\d+", "X");
            Assert.Equal("abcXdef", result);
        }

        [Fact]
        public void ReplaceAllTest()
        {
            var result = ReUtil.ReplaceAll("abc123def456", @"\d+", "X");
            Assert.Equal("abcXdefX", result);
        }

        [Fact]
        public void ReplaceFirstTest()
        {
            var result = ReUtil.ReplaceFirst("abc123def456", @"\d+", "X");
            Assert.Equal("abcXdef456", result);
        }

        [Fact]
        public void SplitTest()
        {
            var parts = ReUtil.Split("abc123def456", @"\d+");
            Assert.Equal(3, parts.Length);
            Assert.Equal("abc", parts[0]);
            Assert.Equal("def", parts[1]);
            Assert.Equal("", parts[2]);
        }

        [Fact]
        public void FindTest()
        {
            var result = ReUtil.Find(@"\d+", "abc123def456", 0);
            Assert.Equal("123", result);
        }

        [Fact]
        public void ContainsTest()
        {
            Assert.True(ReUtil.Contains(@"\d+", "abc123"));
            Assert.False(ReUtil.Contains(@"\d+", "abc"));
        }

        [Fact]
        public void IndexOfTest()
        {
            var index = ReUtil.IndexOf(@"\d+", "abc123def", 0);
            Assert.True(index >= 0);
        }

        [Fact]
        public void EscapeTest()
        {
            var escaped = ReUtil.Escape("[a-z]");
            Assert.Equal(@"\[a\-z\]", escaped);
        }

        [Fact]
        public void UnescapeTest()
        {
            var unescaped = ReUtil.Unescape(@"\[a\-z\]");
            Assert.Equal("[a-z]", unescaped);
        }
    }
}
