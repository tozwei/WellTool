namespace WellTool.Extra.Tests;

using Well.Extra.Tokenizer;

public class TokenizerUtilTest
{
    [Fact]
    public void TokenizeTest()
    {
        var result = TokenizerUtil.Tokenize("Hutool是一个Java工具包");
        Assert.NotNull(result);
    }

    [Fact]
    public void TokenizeEnglishTest()
    {
        var result = TokenizerUtil.Tokenize("Hello World");
        Assert.NotNull(result);
    }

    [Fact]
    public void TokenizeNullTest()
    {
        var result = TokenizerUtil.Tokenize(null);
        Assert.Null(result);
    }

    [Fact]
    public void TokenizeEmptyTest()
    {
        var result = TokenizerUtil.Tokenize("");
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateTest()
    {
        using var tokenizer = TokenizerUtil.Create("Java编程");
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateForChineseTest()
    {
        using var tokenizer = TokenizerUtil.Create("中文分词测试");
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateForMixedTest()
    {
        using var tokenizer = TokenizerUtil.Create("Hello你好World世界");
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateWithEnglishEngineTest()
    {
        using var tokenizer = TokenizerUtil.Create("test text", TokenizerUtil.Engine.EN);
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateWithChineseEngineTest()
    {
        using var tokenizer = TokenizerUtil.Create("测试中文", TokenizerUtil.Engine.CN);
        Assert.NotNull(tokenizer);
    }
}
