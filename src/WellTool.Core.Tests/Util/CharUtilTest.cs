using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CharUtilTest
{
    [Fact]
    public void IsLetterTest()
    {
        Assert.True(CharUtil.IsLetter('a'));
        Assert.True(CharUtil.IsLetter('Z'));
        Assert.False(CharUtil.IsLetter('1'));
        Assert.False(CharUtil.IsLetter(' '));
    }

    [Fact]
    public void IsDigitTest()
    {
        Assert.True(CharUtil.IsDigit('0'));
        Assert.True(CharUtil.IsDigit('9'));
        Assert.False(CharUtil.IsDigit('a'));
    }

    [Fact]
    public void IsLetterOrDigitTest()
    {
        Assert.True(CharUtil.IsLetterOrDigit('a'));
        Assert.True(CharUtil.IsLetterOrDigit('1'));
        Assert.False(CharUtil.IsLetterOrDigit(' '));
    }

    [Fact]
    public void IsLowerCaseTest()
    {
        Assert.True(CharUtil.IsLowerCase('a'));
        Assert.False(CharUtil.IsLowerCase('A'));
    }

    [Fact]
    public void IsUpperCaseTest()
    {
        Assert.True(CharUtil.IsUpperCase('A'));
        Assert.False(CharUtil.IsUpperCase('a'));
    }

    [Fact]
    public void ToLowerCaseTest()
    {
        Assert.Equal('a', CharUtil.ToLowerCase('A'));
    }

    [Fact]
    public void ToUpperCaseTest()
    {
        Assert.Equal('A', CharUtil.ToUpperCase('a'));
    }

    [Fact]
    public void IsChineseTest()
    {
        Assert.True(CharUtil.IsChinese('你'));
        Assert.False(CharUtil.IsChinese('a'));
    }

    [Fact]
    public void IsBlankTest()
    {
        Assert.True(CharUtil.IsBlank(' '));
        Assert.True(CharUtil.IsBlank('\t'));
        Assert.False(CharUtil.IsBlank('a'));
    }

    [Fact]
    public void IsAsciiTest()
    {
        Assert.True(CharUtil.IsAscii('a'));
        Assert.True(CharUtil.IsAscii('1'));
        Assert.False(CharUtil.IsAscii('你'));
    }
}
