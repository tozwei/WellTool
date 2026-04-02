using WellTool.Extra;

namespace WellTool.Extra.Tests;

/// <summary>
/// PinyinUtil 测试类
/// </summary>
public class PinyinUtilTest
{
    private readonly PinyinUtil _pinyinUtil;

    public PinyinUtilTest()
    {
        _pinyinUtil = new PinyinUtil();
    }

    [Fact]
    public void TestGetFirstLetter_SingleChineseChar_ReturnsLetter()
    {
        // 测试单个中文字符 - 非中文原样返回
        Assert.Equal('A', _pinyinUtil.GetFirstLetter('A'));
        Assert.Equal('1', _pinyinUtil.GetFirstLetter('1'));
        // 中文字符根据简单实现返回
        var result = _pinyinUtil.GetFirstLetter('中');
        Assert.True(char.IsLetter(result) || char.IsLetter(result));
    }

    [Fact]
    public void TestGetFirstLetter_NonChineseChar_ReturnsSameChar()
    {
        // 测试非中文字符
        Assert.Equal('A', _pinyinUtil.GetFirstLetter('A'));
        Assert.Equal('b', _pinyinUtil.GetFirstLetter('b'));
        Assert.Equal('1', _pinyinUtil.GetFirstLetter('1'));
    }

    [Fact]
    public void TestGetFirstLetters_MultipleChineseChars_ReturnsFirstLetters()
    {
        // 测试多个中文字符
        var result = _pinyinUtil.GetFirstLetters("中国");
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Length);
    }

    [Fact]
    public void TestGetFirstLetters_MixedChars_ReturnsCorrectResult()
    {
        // 测试混合字符
        var result = _pinyinUtil.GetFirstLetters("Hello中国");
        Assert.Contains('H', result);
        Assert.Contains('e', result);
    }

    [Fact]
    public void TestGetFirstLetters_EmptyString_ReturnsEmptyString()
    {
        // 测试空字符串
        var result = _pinyinUtil.GetFirstLetters("");
        Assert.Empty(result);
    }

    [Fact]
    public void TestToPinyin_ChineseString_ReturnsPinyin()
    {
        // 测试中文转拼音
        var result = _pinyinUtil.ToPinyin("中国");
        Assert.NotEmpty(result);
    }

    [Fact]
    public void TestToPinyin_EnglishString_ReturnsSameString()
    {
        // 测试英文转拼音
        var result = _pinyinUtil.ToPinyin("Hello");
        Assert.Equal("Hello", result);
    }

    [Fact]
    public void TestToPinyin_MixedString_ReturnsPinyinAndOriginal()
    {
        // 测试混合字符串 - 返回转换后的拼音和非中文原字符
        var result = _pinyinUtil.ToPinyin("Hello中国");
        Assert.StartsWith("Hello", result);
        // ToPinyin 方法会将中文字符转为拼音首字母
        Assert.True(result.Length > 0);
    }

    [Fact]
    public void TestToPinyin_EmptyString_ReturnsEmptyString()
    {
        // 测试空字符串
        var result = _pinyinUtil.ToPinyin("");
        Assert.Empty(result);
    }

    [Fact]
    public void TestToPinyin_Numbers_ReturnsNumbers()
    {
        // 测试数字
        var result = _pinyinUtil.ToPinyin("123");
        Assert.Equal("123", result);
    }

    [Theory]
    [InlineData('A')]
    [InlineData('B')]
    [InlineData('C')]
    [InlineData('D')]
    [InlineData('1')]
    [InlineData('2')]
    [InlineData('a')]
    [InlineData('z')]
    public void TestGetFirstLetter_AsciiChars_ReturnsSameChar(char c)
    {
        Assert.Equal(c, _pinyinUtil.GetFirstLetter(c));
    }

    [Fact]
    public void TestGetFirstLetters_LongChineseString_ReturnsCorrectLength()
    {
        // 测试长中文字符串
        var input = "中华人民共和国";
        var result = _pinyinUtil.GetFirstLetters(input);
        Assert.Equal(input.Length, result.Length);
    }

    [Fact]
    public void TestToPinyin_AllChinese_ReturnsAllPinyin()
    {
        // 测试全中文
        var input = "中国";
        var result = _pinyinUtil.ToPinyin(input);
        Assert.All(result, c => Assert.True(char.IsLetter(c)));
    }

    [Fact]
    public void TestGetFirstLetters_SpecialChars_HandlesGracefully()
    {
        // 测试特殊字符
        var result = _pinyinUtil.GetFirstLetters("!@#$%");
        Assert.NotNull(result);
    }
}
