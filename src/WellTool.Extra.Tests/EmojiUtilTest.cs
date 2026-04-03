namespace WellTool.Extra.Tests;

using Well.Extra.Emoji;

public class EmojiUtilTest
{
    [Fact]
    public void ToAliasTest()
    {
        var result = EmojiUtil.ToAlias("😀");
        Assert.Contains("smile", result);
    }

    [Fact]
    public void ToUnicodeTest()
    {
        var result = EmojiUtil.ToUnicode(":smile:");
        Assert.Contains("😀", result);
    }

    [Fact]
    public void IsEmojiTest()
    {
        Assert.True(EmojiUtil.IsEmoji("😀"));
        Assert.False(EmojiUtil.IsEmoji("abc"));
    }

    [Fact]
    public void ContainsEmojiTest()
    {
        Assert.True(EmojiUtil.ContainsEmoji("Hello 😀"));
        Assert.False(EmojiUtil.ContainsEmoji("Hello"));
    }

    [Fact]
    public void EmojiToStringTest()
    {
        var result = EmojiUtil.EmojiToString("😀😁");
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ToHtmlDecimalTest()
    {
        var result = EmojiUtil.ToHtmlDecimal("😀");
        Assert.Contains("&#", result);
    }

    [Fact]
    public void ToHtmlHexTest()
    {
        var result = EmojiUtil.ToHtmlHex("😀");
        Assert.Contains("&#x", result);
    }

    [Fact]
    public void ExtractEmojisTest()
    {
        var result = EmojiUtil.ExtractEmojis("Hello 😀 World 🌍");
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void RemoveEmojisTest()
    {
        var result = EmojiUtil.RemoveEmojis("Hello 😀 World 🌍");
        Assert.Equal("Hello  World ", result);
    }
}
