using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace WellTool.Http.Tests;

/// <summary>
/// HTMLFilter HTML过滤器测试
/// </summary>
public class HTMLFilterTest
{
    private readonly HTMLFilter _filter;

    public HTMLFilterTest()
    {
        _filter = new HTMLFilter();
    }

    [Fact]
    public void FilterTest_BasicTags()
    {
        var input = "<b>Bold</b><i>Italic</i>";
        var output = _filter.Filter(input);
        Assert.Contains("<b>Bold</b>", output);
        Assert.Contains("<i>Italic</i>", output);
    }

    [Fact]
    public void FilterTest_DisallowedTags()
    {
        var input = "<script>alert('xss')</script><b>Safe</b>";
        var output = _filter.Filter(input);
        Assert.DoesNotContain("<script>", output);
        Assert.Contains("<b>Safe</b>", output);
    }

    [Fact]
    public void FilterTest_AllowedAttributes()
    {
        var input = "<a href=\"http://example.com\">Link</a>";
        var output = _filter.Filter(input);
        Assert.Contains("href", output);
        Assert.Contains("http://example.com", output);
    }

    [Fact]
    public void FilterTest_DisallowedProtocol()
    {
        var input = "<a href=\"javascript:alert('xss')\">Link</a>";
        var output = _filter.Filter(input);
        Assert.DoesNotContain("javascript:", output);
    }

    [Fact]
    public void FilterTest_ImgTag()
    {
        var input = "<img src=\"test.jpg\" width=\"100\" height=\"200\" alt=\"test\" />";
        var output = _filter.Filter(input);
        Assert.Contains("<img", output);
        Assert.Contains("src", output);
    }

    [Fact]
    public void FilterTest_Comments()
    {
        var input = "<!-- comment --><b>Text</b><!-- another comment -->";
        var output = _filter.Filter(input);
        Assert.DoesNotContain("<!--", output);
        Assert.DoesNotContain("comment", output);
        Assert.Contains("<b>Text</b>", output);
    }

    [Fact]
    public void FilterTest_DivWithAlign()
    {
        var input = "<div align=\"center\">Content</div>";
        var output = _filter.Filter(input);
        Assert.Contains("<div", output);
        Assert.Contains("align", output);
    }

    [Fact]
    public void FilterTest_TableTags()
    {
        var input = "<table border=\"1\" cellpadding=\"5\"><tr><td>Cell</td></tr></table>";
        var output = _filter.Filter(input);
        Assert.Contains("<table", output);
        Assert.Contains("<tr>", output);
        Assert.Contains("<td>", output);
    }

    [Fact]
    public void FilterTest_Null()
    {
        var output = _filter.Filter(null);
        Assert.Null(output);
    }

    [Fact]
    public void FilterTest_Empty()
    {
        var output = _filter.Filter("");
        Assert.Equal("", output);
    }

    [Fact]
    public void FilterTest_PlainText()
    {
        var input = "Just plain text without any tags";
        var output = _filter.Filter(input);
        Assert.Equal(input, output);
    }

    [Fact]
    public void FilterTest_Blockquote()
    {
        var input = "<blockquote cite=\"source\">Quote</blockquote>";
        var output = _filter.Filter(input);
        Assert.Contains("<blockquote", output);
        Assert.Contains("cite", output);
    }

    [Fact]
    public void FilterTest_CodeAndPre()
    {
        var input = "<pre><code>code here</code></pre>";
        var output = _filter.Filter(input);
        Assert.Contains("<pre", output);
        Assert.Contains("code here", output);
    }

    [Fact]
    public void FilterTest_Span()
    {
        var input = "<span>Simple span</span>";
        var output = _filter.Filter(input);
        Assert.Contains("<span>", output);
        Assert.Contains("Simple span", output);
    }

    [Fact]
    public void FilterTest_Font()
    {
        var input = "<font color=\"red\" size=\"5\">Styled</font>";
        var output = _filter.Filter(input);
        Assert.Contains("<font", output);
        Assert.Contains("color", output);
        Assert.Contains("size", output);
    }

    [Fact]
    public void FilterTest_SelfClosingBr()
    {
        var input = "Line 1<br />Line 2";
        var output = _filter.Filter(input);
        Assert.Contains("Line 1", output);
        Assert.Contains("Line 2", output);
    }

    [Fact]
    public void FilterTest_DisallowedWithContent()
    {
        var input = "<div>Before<script>bad</script>After</div>";
        var output = _filter.Filter(input);
        Assert.Contains("<div>", output);
        Assert.Contains("Before", output);
        Assert.Contains("After", output);
        Assert.DoesNotContain("<script>", output);
    }

    [Fact]
    public void FilterTest_UnclosedTag()
    {
        var input = "<b>Bold text<p>New paragraph";
        var output = _filter.Filter(input);
        Assert.Contains("<b>Bold text", output);
    }

    [Fact]
    public void FilterTest_ListTags()
    {
        var input = "<ul type=\"disc\"><li>Item 1</li><li>Item 2</li></ul>";
        var output = _filter.Filter(input);
        Assert.Contains("<ul", output);
        Assert.Contains("<li>", output);
    }

    [Fact]
    public void FilterTest_DefinitionList()
    {
        var input = "<dl><dt>Term</dt><dd>Definition</dd></dl>";
        var output = _filter.Filter(input);
        Assert.Contains("<dl>", output);
        Assert.Contains("<dt>", output);
        Assert.Contains("<dd>", output);
    }
}

/// <summary>
/// Pattern 正则表达式包装类测试
/// </summary>
public class PatternTest
{
    [Fact]
    public void MatchTest()
    {
        var pattern = new PatternClass("test(\\d+)", RegexOptions.IgnoreCase);
        var result = pattern.Match("test123");
        
        Assert.True(result.Success);
    }

    [Fact]
    public void MatchTest_NoMatch()
    {
        var pattern = new PatternClass("abc", RegexOptions.None);
        var result = pattern.Match("xyz");
        
        Assert.False(result.Success);
    }

    [Fact]
    public void ReplaceAllTest()
    {
        var pattern = new PatternClass("\\d+", RegexOptions.None);
        var result = pattern.ReplaceAll("abc123def456", "X");
        
        Assert.Equal("abcXdefX", result);
    }
}

/// <summary>
/// 辅助测试类
/// </summary>
internal class PatternClass
{
    private readonly Regex _regex;

    public PatternClass(string pattern, RegexOptions options = RegexOptions.None)
    {
        _regex = new Regex(pattern, options);
    }

    public PatternMatchResult Match(string input)
    {
        var match = _regex.Match(input);
        return new PatternMatchResult(match);
    }

    public string ReplaceAll(string input, string replacement)
    {
        return _regex.Replace(input, replacement);
    }
}

internal class PatternMatchResult
{
    private readonly Match _match;

    public PatternMatchResult(Match match)
    {
        _match = match;
    }

    public bool Success => _match.Success;
}
