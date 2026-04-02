namespace WellTool.Extra.Tests;

/// <summary>
/// TokenizerUtil 测试类
/// </summary>
public class TokenizerUtilTest
{
    private readonly TokenizerUtil _tokenizerUtil;

    public TokenizerUtilTest()
    {
        _tokenizerUtil = new TokenizerUtil();
    }

    [Fact]
    public void TestTokenize_SimpleEnglishText_ReturnsTokens()
    {
        // 测试简单英文分词
        var text = "Hello World";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Contains("Hello", result);
        Assert.Contains("World", result);
    }

    [Fact]
    public void TestTokenize_SimpleChineseText_ReturnsTokens()
    {
        // 测试简单中文分词
        var text = "你好世界";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.NotEmpty(result);
    }

    [Fact]
    public void TestTokenize_MixedText_ReturnsAllTokens()
    {
        // 测试混合文本分词
        var text = "Hello 你好 World 世界";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Contains("Hello", result);
        Assert.Contains("World", result);
        Assert.Contains("你好", result);
        Assert.Contains("世界", result);
    }

    [Fact]
    public void TestTokenize_NumbersOnly_ReturnsNumberTokens()
    {
        // 测试纯数字分词
        var text = "123 456 789";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Contains("123", result);
        Assert.Contains("456", result);
        Assert.Contains("789", result);
    }

    [Fact]
    public void TestTokenize_EmptyString_ReturnsEmptyList()
    {
        // 测试空字符串
        var text = "";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Empty(result);
    }

    [Fact]
    public void TestTokenize_SpecialCharactersOnly_ReturnsEmptyList()
    {
        // 测试仅特殊字符
        var text = "!@#$%^&*()";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Empty(result);
    }

    [Fact]
    public void TestTokenize_SentenceWithPunctuation_ReturnsTokensOnly()
    {
        // 测试带标点符号的句子
        var text = "Hello, World! How are you?";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Contains("Hello", result);
        Assert.Contains("World", result);
        Assert.Contains("How", result);
        Assert.Contains("are", result);
        Assert.Contains("you", result);
    }

    [Fact]
    public void TestTokenizeWithFrequency_SimpleText_ReturnsCorrectFrequency()
    {
        // 测试词频统计
        var text = "Hello World Hello";
        var result = _tokenizerUtil.TokenizeWithFrequency(text);
        
        Assert.Equal(2, result["Hello"]);
        Assert.Equal(1, result["World"]);
    }

    [Fact]
    public void TestTokenizeWithFrequency_EmptyString_ReturnsEmptyDictionary()
    {
        // 测试空字符串的词频统计
        var text = "";
        var result = _tokenizerUtil.TokenizeWithFrequency(text);
        
        Assert.Empty(result);
    }

    [Fact]
    public void TestTokenizeWithFrequency_NoRepetition_ReturnsCorrectCounts()
    {
        // 测试无重复的词频统计
        var text = "Apple Banana Cherry";
        var result = _tokenizerUtil.TokenizeWithFrequency(text);
        
        Assert.Equal(1, result.Count);
        Assert.Equal(1, result["Apple"]);
        Assert.Equal(1, result["Banana"]);
        Assert.Equal(1, result["Cherry"]);
    }

    [Fact]
    public void TestTokenizeWithFrequency_MixedContent_ReturnsCorrectFrequency()
    {
        // 测试混合内容的词频统计
        var text = "123 123 123 abc abc def";
        var result = _tokenizerUtil.TokenizeWithFrequency(text);
        
        Assert.Equal(3, result["123"]);
        Assert.Equal(2, result["abc"]);
        Assert.Equal(1, result["def"]);
    }

    [Fact]
    public void TestTokenize_LongText_ReturnsExpectedTokens()
    {
        // 测试长文本分词
        var text = "This is a long text that contains multiple words and numbers 12345 for testing purposes.";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Contains("This", result);
        Assert.Contains("is", result);
        Assert.Contains("a", result);
        Assert.Contains("long", result);
        Assert.Contains("text", result);
        Assert.Contains("12345", result);
    }

    [Fact]
    public void TestTokenize_ChineseWithNumbers_ReturnsAllTokens()
    {
        // 测试中文与数字混合
        var text = "订单号12345用户张三";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Contains("订单号12345用户张三", result);
        Assert.Contains("12345", result);
    }

    [Fact]
    public void TestTokenizeWithFrequency_CaseSensitive_HandlesCorrectly()
    {
        // 测试大小写敏感性
        var text = "Hello hello HELLO";
        var result = _tokenizerUtil.TokenizeWithFrequency(text);
        
        // 假设分词是大小写敏感的
        Assert.Contains("Hello", result.Keys);
        Assert.Contains("hello", result.Keys);
        Assert.Contains("HELLO", result.Keys);
    }

    [Fact]
    public void TestTokenize_WhitespaceOnly_ReturnsEmptyList()
    {
        // 测试仅空白字符
        var text = "   \t\n  ";
        var result = _tokenizerUtil.Tokenize(text);
        
        Assert.Empty(result);
    }
}
