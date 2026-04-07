using System;
using Xunit;

namespace WellTool.Core.Tests
{
    public class StringUtilTests
    {
        [Fact]
        public void TestIsBlank()
        {
            var blank = "\t \u00A0\u3000";
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank(blank));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank(""));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank("   "));
            Assert.False(WellTool.Core.Lang.StringUtil.IsBlank("test"));
        }

        [Fact]
        public void TestTrim()
        {
            var blank = "\t 哈哈\u00A0\u3000";
            var trim = WellTool.Core.Lang.StringUtil.Trim(blank);
            Assert.Equal("哈哈", trim);
        }

        [Fact]
        public void TestTrimNewLine()
        {
            string str = "\r\naaa";
            Assert.Equal("aaa", WellTool.Core.Lang.StringUtil.Trim(str));
            str = "\raaa";
            Assert.Equal("aaa", WellTool.Core.Lang.StringUtil.Trim(str));
            str = "\naaa";
            Assert.Equal("aaa", WellTool.Core.Lang.StringUtil.Trim(str));
            str = "\r\n\r\naaa";
            Assert.Equal("aaa", WellTool.Core.Lang.StringUtil.Trim(str));
        }

        [Fact]
        public void TestTrimTab()
        {
            var str = "\taaa";
            Assert.Equal("aaa", WellTool.Core.Lang.StringUtil.Trim(str));
        }

        [Fact]
        public void TestCleanBlank()
        {
            // 包含：制表符、英文空格、不间断空白符、全角空格
            var str = "\t 你\u00A0好\u3000";
            var cleanBlank = WellTool.Core.Lang.StringUtil.CleanBlank(str);
            Assert.Equal("你好", cleanBlank);
        }

        [Fact]
        public void TestSplit()
        {
            var str = "a,b ,c,d,,e";
            var split = WellTool.Core.Lang.StringUtil.Split(str, ',');
            // 测试分割结果
            Assert.NotEmpty(split);
        }

        [Fact]
        public void TestSubstring()
        {
            var a = "abcderghigh";
            var pre = WellTool.Core.Lang.StringUtil.Sub(a, 0, 3);
            Assert.Equal("abc", pre);
        }

        [Fact]
        public void TestStartsWith()
        {
            var a = "123";
            var b = "12";
            Assert.True(WellTool.Core.Lang.StringUtil.StartsWith(a, b));
            Assert.False(WellTool.Core.Lang.StringUtil.StartsWith(a, "45"));
        }

        [Fact]
        public void TestEndsWith()
        {
            var a = "123";
            var b = "23";
            Assert.True(WellTool.Core.Lang.StringUtil.EndsWith(a, b));
            Assert.False(WellTool.Core.Lang.StringUtil.EndsWith(a, "45"));
        }

        [Fact]
        public void TestContains()
        {
            var a = "abcdef";
            Assert.True(WellTool.Core.Lang.StringUtil.Contains(a, "cde"));
            Assert.False(WellTool.Core.Lang.StringUtil.Contains(a, "xyz"));
        }

        [Fact]
        public void TestEqualsIgnoreCase()
        {
            Assert.True(WellTool.Core.Lang.StringUtil.EqualsIgnoreCase("ABC", "abc"));
            Assert.False(WellTool.Core.Lang.StringUtil.EqualsIgnoreCase("ABC", "def"));
        }

        [Fact]
        public void TestLength()
        {
            var str = "Hello World";
            Assert.Equal(11, WellTool.Core.Lang.StringUtil.Length(str));
            Assert.Equal(0, WellTool.Core.Lang.StringUtil.Length(""));
        }

        [Fact]
        public void TestIsEmpty()
        {
            Assert.True(WellTool.Core.Lang.StringUtil.IsEmpty(""));
            Assert.False(WellTool.Core.Lang.StringUtil.IsEmpty("test"));
        }

        [Fact]
        public void TestIsNotEmpty()
        {
            Assert.False(WellTool.Core.Lang.StringUtil.IsNotEmpty(""));
            Assert.True(WellTool.Core.Lang.StringUtil.IsNotEmpty("test"));
        }

        [Fact]
        public void TestToUpper()
        {
            var str = "hello";
            Assert.Equal("HELLO", WellTool.Core.Lang.StringUtil.ToUpper(str));
        }

        [Fact]
        public void TestToLower()
        {
            var str = "HELLO";
            Assert.Equal("hello", WellTool.Core.Lang.StringUtil.ToLower(str));
        }

        [Fact]
        public void TestRemovePrefix()
        {
            var str = "prefix_test";
            var result = WellTool.Core.Lang.StringUtil.RemovePrefix(str, "prefix_");
            Assert.Equal("test", result);
        }

        [Fact]
        public void TestRemoveSuffix()
        {
            var str = "test_suffix";
            var result = WellTool.Core.Lang.StringUtil.RemoveSuffix(str, "_suffix");
            Assert.Equal("test", result);
        }
    }

    public class StrUtilTests
    {
        [Fact]
        public void TestIsBlankIfStr()
        {
            Assert.False(WellTool.Core.Lang.StringUtil.IsBlank("test"));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank(null));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank(""));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank("   "));
        }

        [Fact]
        public void TestIsEmptyIfStr()
        {
            Assert.False(WellTool.Core.Lang.StringUtil.IsEmpty("test"));
            Assert.True(WellTool.Core.Lang.StringUtil.IsEmpty(null));
            Assert.True(WellTool.Core.Lang.StringUtil.IsEmpty(""));
            Assert.False(WellTool.Core.Lang.StringUtil.IsEmpty("   ")); // 空格不是空
        }

        [Fact]
        public void TestIsBlank()
        {
            Assert.False(WellTool.Core.Lang.StringUtil.IsBlank("test"));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank(null));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank(""));
            Assert.True(WellTool.Core.Lang.StringUtil.IsBlank("   "));
        }

        [Fact]
        public void TestIsEmpty()
        {
            Assert.False(WellTool.Core.Lang.StringUtil.IsEmpty("test"));
            Assert.True(WellTool.Core.Lang.StringUtil.IsEmpty(null));
            Assert.True(WellTool.Core.Lang.StringUtil.IsEmpty(""));
            Assert.False(WellTool.Core.Lang.StringUtil.IsEmpty("   ")); // 空格不是空
        }

        [Fact]
        public void TestIsNotBlank()
        {
            Assert.True(WellTool.Core.Lang.StringUtil.IsNotBlank("test"));
            Assert.False(WellTool.Core.Lang.StringUtil.IsNotBlank(null));
            Assert.False(WellTool.Core.Lang.StringUtil.IsNotBlank(""));
            Assert.False(WellTool.Core.Lang.StringUtil.IsNotBlank("   "));
        }

        [Fact]
        public void TestIsNotEmpty()
        {
            Assert.True(WellTool.Core.Lang.StringUtil.IsNotEmpty("test"));
            Assert.False(WellTool.Core.Lang.StringUtil.IsNotEmpty(null));
            Assert.False(WellTool.Core.Lang.StringUtil.IsNotEmpty(""));
            Assert.True(WellTool.Core.Lang.StringUtil.IsNotEmpty("   "));
        }
    }
}
