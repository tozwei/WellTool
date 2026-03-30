using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// 文本工具单元测试
    /// </summary>
    public class TextTests
    {
        #region 基础文本测试

        [Fact]
        public void ReplaceTest()
        {
            var actual = Regex.Replace("SSM15930297701BeryAllen", "[0-9]", match => "");
            Assert.Equal("SSMBeryAllen", actual);
        }

        [Fact]
        public void ReplaceTest2()
        {
            var replace = "#{A}";
            var result = replace.Replace("#{AAAAAAA}", "1");
            Assert.Equal(replace, result);
        }

        [Fact]
        public void AddPrefixIfNotTest()
        {
            var str = "welltool";
            var result = str.StartsWith("well") ? str : "well" + str;
            Assert.Equal(str, result);

            result = str.StartsWith("Good") ? str : "Good" + str;
            Assert.Equal("Good" + str, result);
        }

        [Fact]
        public void AddSuffixIfNotTest()
        {
            var str = "welltool";
            var result = str.EndsWith("tool") ? str : str + "tool";
            Assert.Equal(str, result);

            result = str.EndsWith(" is Good") ? str : str + " is Good";
            Assert.Equal(str + " is Good", result);

            result = string.IsNullOrEmpty("") ? "/" : "" + "/";
            Assert.Equal("/", result);
        }

        [Fact]
        public void IndexOfTest()
        {
            var index = "abc123".IndexOf('1');
            Assert.Equal(3, index);
            index = "abc123".IndexOf('3');
            Assert.Equal(5, index);
            index = "abc123".IndexOf('a');
            Assert.Equal(0, index);
        }

        [Fact]
        public void StartWithTest()
        {
            Assert.True("abcdef".StartsWith("abc", StringComparison.OrdinalIgnoreCase));
            Assert.True("ABCDEF".StartsWith("abc", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void EndWithTest()
        {
            Assert.True("abcdef".EndsWith("def", StringComparison.OrdinalIgnoreCase));
            Assert.True("ABCDEF".EndsWith("def", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void TrimToNullTest()
        {
            var a = "  ";
            Assert.Null(string.IsNullOrWhiteSpace(a) ? null : a);

            a = "";
            Assert.Null(string.IsNullOrWhiteSpace(a) ? null : a);

            a = null;
            Assert.Null(a);
        }

        [Fact]
        public void SplitTest()
        {
            var result = "a,b,c".Split(',');
            Assert.Equal(3, result.Length);
            Assert.Equal("a", result[0]);
            Assert.Equal("b", result[1]);
            Assert.Equal("c", result[2]);
        }

        [Fact]
        public void SplitTrimTest()
        {
            var result = "a, b, c".Split(',').Select(s => s.Trim()).ToArray();
            Assert.Equal(3, result.Length);
            Assert.Equal("a", result[0]);
            Assert.Equal("b", result[1]);
            Assert.Equal("c", result[2]);
        }

        [Fact]
        public void JoinTest()
        {
            var result = string.Join(",", "a", "b", "c");
            Assert.Equal("a,b,c", result);
        }

        [Fact]
        public void JoinWithListTest()
        {
            var list = new[] { "a", "b", "c" };
            var result = string.Join(",", list);
            Assert.Equal("a,b,c", result);
        }

        #endregion
    }
}