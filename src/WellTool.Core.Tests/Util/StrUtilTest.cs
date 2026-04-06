using WellTool.Core.Text;
using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class StrUtilTest
{
    [Fact]
    public void IsBlankTest()
    {
        Assert.True(StrUtil.IsBlank(null));
        Assert.True(StrUtil.IsBlank(""));
        Assert.True(StrUtil.IsBlank("   "));
        Assert.False(StrUtil.IsBlank("abc"));
    }

    [Fact]
    public void IsNotBlankTest()
    {
        Assert.False(StrUtil.IsNotBlank(null));
        Assert.False(StrUtil.IsNotBlank(""));
        Assert.True(StrUtil.IsNotBlank("abc"));
    }

    [Fact]
    public void TrimTest()
    {
        Assert.Equal("abc", StrUtil.Trim("  abc  "));
        Assert.Equal("abc", StrUtil.Trim("\tabc\n"));
    }

    [Fact]
    public void CutTest()
    {
        var result = StrUtil.Cut("abcde", 2);
        Assert.Equal(3, result.Length);
        Assert.Equal("ab", result[0]);
        Assert.Equal("cd", result[1]);
        Assert.Equal("e", result[2]);
    }

    [Fact]
    public void SubTest()
    {
        Assert.Equal("bcd", StrUtil.Sub("abcde", 1, 4));
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True(StrUtil.Contains("abcde", "bc"));
        Assert.False(StrUtil.Contains("abcde", "f"));
    }

    [Fact]
    public void FormatTest()
    {
        var result = StrUtil.Format("Hello {0}", "World");
        Assert.Equal("Hello World", result);
    }

    [Fact]
    public void RepeatTest()
    {
        Assert.Equal("aaa", StrUtil.Repeat('a', 3));
        Assert.Equal("abcabc", StrUtil.Repeat("abc", 2));
    }

    [Fact]
    public void PadLeftTest()
    {
        Assert.Equal("__abc", StrUtil.PadLeft("abc", 5, '_'));
    }

    [Fact]
    public void PadRightTest()
    {
        Assert.Equal("abc__", StrUtil.PadRight("abc", 5, '_'));
    }

    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(StrUtil.IsEmpty(null));
        Assert.True(StrUtil.IsEmpty(""));
        Assert.False(StrUtil.IsEmpty("abc"));
    }

    [Fact]
    public void CleanBlankTest()
    {
        Assert.Equal("abc", StrUtil.CleanBlank(" a b c "));
    }

    [Fact]
    public void ToCamelCaseTest()
    {
        Assert.Equal("helloWorld", StrUtil.ToCamelCase("hello_world"));
        Assert.Equal("helloWorld", StrUtil.ToCamelCase("helloWorld"));
    }

    [Fact]
    public void ToUnderScoreCaseTest()
    {
        Assert.Equal("hello_world", StrUtil.ToUnderScoreCase("helloWorld"));
        Assert.Equal("hello_world", StrUtil.ToUnderScoreCase("HelloWorld"));
    }

    [Fact]
    public void UpperFirstTest()
    {
        Assert.Equal("Hello", StrUtil.UpperFirst("hello"));
    }

    [Fact]
    public void LowerFirstTest()
    {
        Assert.Equal("hello", StrUtil.LowerFirst("Hello"));
    }

    [Fact]
    public void IsNumericTest()
    {
        Assert.True(StrUtil.IsNumeric("123"));
        Assert.False(StrUtil.IsNumeric("123a"));
    }

    [Fact]
    public void IsAlphaTest()
    {
        Assert.True(StrUtil.IsAlpha("abc"));
        Assert.False(StrUtil.IsAlpha("123"));
    }

    [Fact]
    public void IsAlphanumericTest()
    {
        Assert.True(StrUtil.IsAlphanumeric("abc123"));
        Assert.False(StrUtil.IsAlphanumeric("abc-123"));
    }

    [Fact]
    public void IsNumberTest()
    {
        Assert.True(StrUtil.IsNumber("123.45"));
        Assert.True(StrUtil.IsNumber("-123.45"));
        Assert.False(StrUtil.IsNumber("abc"));
    }

    [Fact]
    public void StripTest()
    {
        Assert.Equal("abc", StrUtil.Strip("abc", "a", "c"));
    }

    [Fact]
    public void SubBetweenTest()
    {
        Assert.Equal("middle", StrUtil.SubBetween("pre_middle_post", "_", "_"));
    }

    [Fact]
    public void SubBeforeTest()
    {
        Assert.Equal("pre", StrUtil.SubBefore("pre_middle", "_"));
    }

    [Fact]
    public void SubAfterTest()
    {
        Assert.Equal("post", StrUtil.SubAfter("middle_post", "_"));
    }

    [Fact]
    public void RemovePrefixTest()
    {
        Assert.Equal("world", StrUtil.RemovePrefix("hello_world", "hello_"));
    }

    [Fact]
    public void RemoveSuffixTest()
    {
        Assert.Equal("hello", StrUtil.RemoveSuffix("hello_world", "_world"));
    }

    [Fact]
    public void ReplaceTest()
    {
        Assert.Equal("aXcXd", StrUtil.Replace("a_b_c_d", 1, 5, "X"));
    }

    [Fact]
    public void ReplaceCharsTest()
    {
        Assert.Equal("aXcXd", StrUtil.ReplaceChars("a_b_c_d", '_', 'X'));
    }

    [Fact]
    public void IndexedTest()
    {
        var indexed = StrUtil.Indexed("abc");
        Assert.Equal("[0]=[1]=[2]", string.Join("", indexed.Select(kv => $"{kv.Key}={kv.Value}")));
    }

    [Fact]
    public void SplitTest()
    {
        var parts = StrUtil.Split("a,b,c", ',');
        Assert.Equal(3, parts.Length);
    }

    [Fact]
    public void SplitToArrayTest()
    {
        var parts = StrUtil.SplitToArray("a,b,c", ',');
        Assert.Equal(3, parts.Length);
    }

    [Fact]
    public void JoinTest()
    {
        Assert.Equal("a,b,c", StrUtil.Join(",", "a", "b", "c"));
    }

    [Fact]
    public void AppendIfMissingTest()
    {
        Assert.Equal("abc", StrUtil.AppendIfMissing("abc", "x"));
        Assert.Equal("abcx", StrUtil.AppendIfMissing("abc", "x"));
    }

    [Fact]
    public void PrependIfMissingTest()
    {
        Assert.Equal("abc", StrUtil.PrependIfMissing("abc", "x"));
        Assert.Equal("xabc", StrUtil.PrependIfMissing("abc", "x"));
    }

    [Fact]
    public void WrapTest()
    {
        Assert.Equal("\"abc\"", StrUtil.Wrap("abc", '"'));
    }

    [Fact]
    public void UnwrapTest()
    {
        Assert.Equal("abc", StrUtil.Unwrap("\"abc\"", '"'));
    }
}
