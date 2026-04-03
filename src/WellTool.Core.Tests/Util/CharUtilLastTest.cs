using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class CharUtilLastTest
{
    [Fact]
    public void IsLetterTest()
    {
        Assert.True(CharUtil.IsLetter('a'));
        Assert.False(CharUtil.IsLetter('1'));
    }

    [Fact]
    public void IsDigitTest()
    {
        Assert.True(CharUtil.IsDigit('0'));
        Assert.False(CharUtil.IsDigit('a'));
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
    public void IsChineseTest()
    {
        Assert.True(CharUtil.IsChinese('你'));
        Assert.False(CharUtil.IsChinese('a'));
    }
}
