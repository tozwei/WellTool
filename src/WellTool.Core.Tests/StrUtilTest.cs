using Xunit;
using WellTool.Core;

namespace WellTool.Core.Tests
{
    /// <summary>
    /// Str工具单元测试
    /// </summary>
    public class StrUtilTest
    {
        [Fact]
        public void IsBlankTest()
        {
            Assert.True(StrUtil.IsBlank(""));
            Assert.True(StrUtil.IsBlank("   "));
            Assert.True(StrUtil.IsBlank("\t"));
            Assert.True(StrUtil.IsBlank("\n"));
            Assert.False(StrUtil.IsBlank("test"));
        }

        [Fact]
        public void IsNotBlankTest()
        {
            Assert.False(StrUtil.IsNotBlank(""));
            Assert.True(StrUtil.IsNotBlank("test"));
        }

        [Fact]
        public void IsEmptyTest()
        {
            Assert.True(StrUtil.IsEmpty(""));
            Assert.False(StrUtil.IsEmpty(" "));
            Assert.False(StrUtil.IsEmpty("test"));
        }

        [Fact]
        public void IsNotEmptyTest()
        {
            Assert.False(StrUtil.IsNotEmpty(""));
            Assert.True(StrUtil.IsNotEmpty("test"));
        }

        [Fact]
        public void TrimTest()
        {
            Assert.Equal("test", StrUtil.Trim("  test  "));
            Assert.Equal("test", StrUtil.Trim("\ttest\n"));
        }

        [Fact]
        public void SubTest()
        {
            Assert.Equal("test", StrUtil.Sub("testtest", 0, 4));
            Assert.Equal("test", StrUtil.Sub("testtest", 4, 4));
        }

        [Fact]
        public void SubBetweenTest()
        {
            Assert.Equal("test", StrUtil.SubBetween("【test】", "【", "】"));
        }

        [Fact]
        public void ContainsTest()
        {
            Assert.True(StrUtil.Contains("test", "es"));
            Assert.False(StrUtil.Contains("test", "xx"));
        }

        [Fact]
        public void ContainsAnyTest()
        {
            Assert.True(StrUtil.ContainsAny("test", 'e', 'x'));
            Assert.False(StrUtil.ContainsAny("test", 'x', 'y'));
        }

        [Fact]
        public void IndexOfTest()
        {
            Assert.Equal(1, StrUtil.IndexOf("test", 'e'));
            Assert.Equal(-1, StrUtil.IndexOf("test", 'x'));
        }

        [Fact]
        public void LastIndexOfTest()
        {
            Assert.Equal(3, StrUtil.LastIndexOf("test", 't'));
        }

        [Fact]
        public void StartsWithTest()
        {
            Assert.True(StrUtil.StartsWith("test", "te"));
            Assert.False(StrUtil.StartsWith("test", "es"));
        }

        [Fact]
        public void EndsWithTest()
        {
            Assert.True(StrUtil.EndsWith("test", "st"));
            Assert.False(StrUtil.EndsWith("test", "te"));
        }

        [Fact]
        public void RepeatTest()
        {
            Assert.Equal("aaa", StrUtil.Repeat('a', 3));
            Assert.Equal("testtesttest", StrUtil.Repeat("test", 3));
        }

        [Fact]
        public void PadLeftTest()
        {
            Assert.Equal("  test", StrUtil.PadLeft("test", 6));
            Assert.Equal("00test", StrUtil.PadLeft("test", 6, '0'));
        }

        [Fact]
        public void PadRightTest()
        {
            Assert.Equal("test  ", StrUtil.PadRight("test", 6));
            Assert.Equal("test00", StrUtil.PadRight("test", 6, '0'));
        }

        [Fact]
        public void UpperFirstTest()
        {
            Assert.Equal("Test", StrUtil.UpperFirst("test"));
        }

        [Fact]
        public void LowerFirstTest()
        {
            Assert.Equal("tEST", StrUtil.LowerFirst("TEST"));
        }

        [Fact]
        public void ToCamelCaseTest()
        {
            Assert.Equal("testTest", StrUtil.ToCamelCase("test_test"));
            Assert.Equal("testTest", StrUtil.ToCamelCase("test-test"));
        }

        [Fact]
        public void ToUnderlineCaseTest()
        {
            Assert.Equal("test_test", StrUtil.ToUnderlineCase("testTest"));
            Assert.Equal("test_test", StrUtil.ToUnderlineCase("testTest"));
        }

        [Fact]
        public void SplitTest()
        {
            var parts = StrUtil.Split("a,b,c", ',');
            Assert.Equal(3, parts.Length);
            Assert.Equal("a", parts[0]);
            Assert.Equal("b", parts[1]);
            Assert.Equal("c", parts[2]);
        }

        [Fact]
        public void JoinTest()
        {
            Assert.Equal("a,b,c", StrUtil.Join(",", "a", "b", "c"));
            Assert.Equal("abc", StrUtil.Join("", "a", "b", "c"));
        }

        [Fact]
        public void ReplaceTest()
        {
            Assert.Equal("bcd", StrUtil.Replace("abcd", 0, 1, ""));
            Assert.Equal("aXd", StrUtil.Replace("abcd", 1, 2, "X"));
        }

        [Fact]
        public void RemovePrefixTest()
        {
            Assert.Equal("test", StrUtil.RemovePrefix("test", "pre"));
            Assert.Equal("test", StrUtil.RemovePrefix("test", "test"));
        }

        [Fact]
        public void RemoveSuffixTest()
        {
            Assert.Equal("test", StrUtil.RemoveSuffix("test", "suffix"));
            Assert.Equal("test", StrUtil.RemoveSuffix("test", "test"));
        }

        [Fact]
        public void CutTest()
        {
            var parts = StrUtil.Cut("abcde", 2);
            Assert.Equal(3, parts.Length);
            Assert.Equal("ab", parts[0]);
            Assert.Equal("cd", parts[1]);
            Assert.Equal("e", parts[2]);
        }

        [Fact]
        public void CleanBlankTest()
        {
            Assert.Equal("test", StrUtil.CleanBlank("  t e s t  "));
        }

        [Fact]
        public void UnwrapTest()
        {
            Assert.Equal("test", StrUtil.Unwrap("\"test\"", "\""));
            Assert.Equal("test", StrUtil.Unwrap("(test)", "()"));
        }

        [Fact]
        public void WrapTest()
        {
            Assert.Equal("\"test\"", StrUtil.Wrap("test", "\""));
            Assert.Equal("(test)", StrUtil.Wrap("test", "(", ")"));
        }
    }
}
