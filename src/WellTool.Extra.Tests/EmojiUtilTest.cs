namespace WellTool.Extra.Tests;

/// <summary>
/// EmojiUtil 测试类
/// </summary>
public class EmojiUtilTest
{
    private readonly EmojiUtil _emojiUtil;

    public EmojiUtilTest()
    {
        _emojiUtil = new EmojiUtil();
    }

    [Fact]
    public void TestContainsEmoji_WithEmoji_ReturnsTrue()
    {
        // 测试包含表情符号的字符串
        Assert.True(_emojiUtil.ContainsEmoji("Hello 😊 World"));
        Assert.True(_emojiUtil.ContainsEmoji("🎉🎊🎈"));
        Assert.True(_emojiUtil.ContainsEmoji("Test 😀"));
    }

    [Fact]
    public void TestContainsEmoji_WithoutEmoji_ReturnsFalse()
    {
        // 测试不包含表情符号的字符串
        Assert.False(_emojiUtil.ContainsEmoji("Hello World"));
        Assert.False(_emojiUtil.ContainsEmoji("普通文本测试"));
        Assert.False(_emojiUtil.ContainsEmoji("12345"));
    }

    [Fact]
    public void TestRemoveEmoji_WithEmoji_RemovesEmojis()
    {
        // 测试移除表情符号
        var result = _emojiUtil.RemoveEmoji("Hello 😊 World 🎉");
        Assert.DoesNotContain("😊", result);
        Assert.DoesNotContain("🎉", result);
        Assert.Contains("Hello", result);
        Assert.Contains("World", result);
    }

    [Fact]
    public void TestRemoveEmoji_WithoutEmoji_ReturnsOriginal()
    {
        // 测试没有表情符号时返回原字符串
        var original = "Hello World";
        var result = _emojiUtil.RemoveEmoji(original);
        Assert.Equal(original, result);
    }

    [Fact]
    public void TestExtractEmoji_WithEmojis_ExtractsAllEmojis()
    {
        // 测试提取表情符号
        var result = _emojiUtil.ExtractEmoji("Hello 😊 World 🎉");
        Assert.NotEmpty(result);
        Assert.Contains("😊", result);
        Assert.Contains("🎉", result);
    }

    [Fact]
    public void TestExtractEmoji_WithoutEmoji_ReturnsEmptyList()
    {
        // 测试没有表情符号时返回空列表
        var result = _emojiUtil.ExtractEmoji("Hello World");
        Assert.Empty(result);
    }

    [Fact]
    public void TestExtractEmoji_WithMultipleSameEmojis_ExtractsAll()
    {
        // 测试提取多个相同表情符号
        var result = _emojiUtil.ExtractEmoji("😊🎉😊🎉😊");
        Assert.Equal(5, result.Count);
    }

    [Theory]
    [InlineData("😀", true)]
    [InlineData("😁", true)]
    [InlineData("😂", true)]
    [InlineData("😃", true)]
    [InlineData("😄", true)]
    [InlineData("Hello", false)]
    [InlineData("World", false)]
    [InlineData("123", false)]
    public void TestContainsEmoji_VariousInputs_ReturnsExpected(string input, bool expected)
    {
        Assert.Equal(expected, _emojiUtil.ContainsEmoji(input));
    }

    [Fact]
    public void TestRemoveEmoji_WithVariousEmojis_RemovesAll()
    {
        // 测试移除各种表情符号
        var emojis = new[] { "😀", "😁", "😂", "😃", "😄", "😆", "😅", "🤣", "😊", "😇" };
        var input = string.Join("", emojis);
        var result = _emojiUtil.RemoveEmoji(input);
        Assert.Empty(result);
    }

    [Fact]
    public void TestExtractEmoji_WithChineseText_ExtractsOnlyEmojis()
    {
        // 测试从中文文本中提取表情符号
        var result = _emojiUtil.ExtractEmoji("你好😊今天天气不错🎉");
        Assert.Equal(2, result.Count);
    }
}
