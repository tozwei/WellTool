using WellTool.Core.Text;
using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class StrUtilLastTest
{
    [Fact]
    public void IsEmptyTest()
    {
        Assert.True(StrUtil.IsEmpty(null));
        Assert.True(StrUtil.IsEmpty(""));
        Assert.False(StrUtil.IsEmpty("abc"));
    }

    [Fact]
    public void IsNotEmptyTest()
    {
        Assert.False(StrUtil.IsNotEmpty(null));
        Assert.False(StrUtil.IsNotEmpty(""));
        Assert.True(StrUtil.IsNotEmpty("abc"));
    }

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
    public void TrimToEmptyTest()
    {
        Assert.Equal("", StrUtil.TrimToEmpty(null));
        Assert.Equal("abc", StrUtil.TrimToEmpty(" abc "));
    }

    [Fact]
    public void TrimToNullTest()
    {
        Assert.Null(StrUtil.TrimToNull("   "));
        Assert.Equal("abc", StrUtil.TrimToNull(" abc "));
    }

    [Fact]
    public void CleanBlankTest()
    {
        Assert.Equal("abc", StrUtil.CleanBlank(" a b c "));
    }

    [Fact]
    public void CutTest()
    {
        var result = StrUtil.Cut("abcde", 2);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void SubTest()
    {
        Assert.Equal("bc", StrUtil.Sub("abcde", 1, 3));
        Assert.Equal("abc", StrUtil.Sub("abcde", 0, -1));
    }

    [Fact]
    public void SubBeforeTest()
    {
        Assert.Equal("abc", StrUtil.SubBefore("abc_def", '_'));
        Assert.Equal("abc_def", StrUtil.SubBefore("abc_def", ':'));
    }

    [Fact]
    public void SubAfterTest()
    {
        Assert.Equal("def", StrUtil.SubAfter("abc_def", '_'));
        Assert.Equal("abc_def", StrUtil.SubAfter("abc_def", ':'));
    }

    [Fact]
    public void SubBetweenTest()
    {
        Assert.Equal("middle", StrUtil.SubBetween("pre_middle_post", "_", "_"));
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.True(StrUtil.Contains("abcde", "bc"));
        Assert.False(StrUtil.Contains("abcde", "f"));
    }

    [Fact]
    public void ContainsIgnoreCaseTest()
    {
        Assert.True(StrUtil.ContainsIgnoreCase("ABCDE", "bc"));
    }

    [Fact]
    public void IndexOfTest()
    {
        Assert.Equal(1, StrUtil.IndexOf("abcde", "b"));
        Assert.Equal(-1, StrUtil.IndexOf("abcde", "f"));
    }

    [Fact]
    public void LastIndexOfTest()
    {
        Assert.Equal(4, StrUtil.LastIndexOf("abcde", "e"));
    }

    [Fact]
    public void StartsWithTest()
    {
        Assert.True(StrUtil.StartsWith("abcde", "abc"));
        Assert.False(StrUtil.StartsWith("abcde", "bcd"));
    }

    [Fact]
    public void EndsWithTest()
    {
        Assert.True(StrUtil.EndsWith("abcde", "cde"));
        Assert.False(StrUtil.EndsWith("abcde", "bcd"));
    }

    [Fact]
    public void EqualsTest()
    {
        Assert.True(StrUtil.Equals("abc", "abc"));
        Assert.False(StrUtil.Equals("abc", "ABC"));
    }

    [Fact]
    public void EqualsIgnoreCaseTest()
    {
        Assert.True(StrUtil.EqualsIgnoreCase("abc", "ABC"));
    }

    [Fact]
    public void FormatTest()
    {
        Assert.Equal("Hello World", StrUtil.Format("Hello {0}", "World"));
        Assert.Equal("1+2=3", StrUtil.Format("{0}+{1}={2}", 1, 2, 3));
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
        Assert.Equal("0abc", StrUtil.PadLeft("abc", 4, '0'));
    }

    [Fact]
    public void PadRightTest()
    {
        Assert.Equal("abc__", StrUtil.PadRight("abc", 5, '_'));
        Assert.Equal("abc0", StrUtil.PadRight("abc", 4, '0'));
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
    public void JoinWithListTest()
    {
        var list = new List<string> { "a", "b", "c" };
        Assert.Equal("a,b,c", StrUtil.Join(",", list));
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
    public void AppendIfMissingTest()
    {
        Assert.Equal("abc", StrUtil.AppendIfMissing("abc", "x"));
        Assert.Equal("abcx", StrUtil.AppendIfMissing("abc", "x", true));
    }

    [Fact]
    public void PrependIfMissingTest()
    {
        Assert.Equal("abc", StrUtil.PrependIfMissing("abc", "x"));
        Assert.Equal("xabc", StrUtil.PrependIfMissing("abc", "x", true));
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
        Assert.Equal(3, indexed.Count);
    }

    [Fact]
    public void AppendTest()
    {
        var sb = new StrBuilder();
        sb.Append("Hello").Append(" World");
        Assert.Equal("Hello World", sb.ToString());
    }

    [Fact]
    public void AppendLineTest()
    {
        var sb = new StrBuilder();
        sb.Append("Hello").AppendLine().Append("World");
        Assert.Contains("\r\n", sb.ToString());
    }

    [Fact]
    public void InsertTest()
    {
        var sb = new StrBuilder("HelloWorld");
        sb.Insert(5, " ");
        Assert.Equal("Hello World", sb.ToString());
    }

    [Fact]
    public void DeleteTest()
    {
        var sb = new StrBuilder("HelloWorld");
        sb.Delete(5, 10);
        Assert.Equal("Hello", sb.ToString());
    }

    [Fact]
    public void ReverseTest()
    {
        var sb = new StrBuilder("Hello");
        sb.Reverse();
        Assert.Equal("olleH", sb.ToString());
    }

    [Fact]
    public void LengthTest()
    {
        var sb = new StrBuilder("Hello");
        Assert.Equal(5, sb.Length);
    }

    [Fact]
    public void ClearTest()
    {
        var sb = new StrBuilder("Hello");
        sb.Clear();
        Assert.Equal("", sb.ToString());
    }
}
