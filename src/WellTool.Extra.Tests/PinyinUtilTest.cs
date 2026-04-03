namespace WellTool.Extra.Tests;

using Well.Extra.Pinyin;

public class PinyinUtilTest
{
    [Fact]
    public void GetPinyinTest()
    {
        var pinyin = PinyinUtil.GetPinyin("你好怡", " ");
        Assert.Equal("ni hao yi", pinyin);
    }

    [Fact]
    public void GetFirstLetterTest()
    {
        var result = PinyinUtil.GetFirstLetter("H是第一个", ", ");
        Assert.Equal("h, s, d, y, g", result);
    }

    [Fact]
    public void GetFirstLetterTest2()
    {
        var result = PinyinUtil.GetFirstLetter("崞阳", ", ");
        Assert.Equal("g, y", result);
    }

    [Fact]
    public void GetFirstLetterTest3()
    {
        var result = PinyinUtil.GetFirstLetter(null, ", ");
        Assert.Null(result);
    }

    [Fact]
    public void GetPinyinNullTest()
    {
        var result = PinyinUtil.GetPinyin(null, " ");
        Assert.Null(result);
    }
}
