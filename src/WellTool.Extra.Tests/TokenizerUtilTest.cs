namespace WellTool.Extra.Tests;

using WellTool.Extra.Tokenizer;

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
        var tokenizer = TokenizerUtil.Create("Java编程");
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateForChineseTest()
    {
        var tokenizer = TokenizerUtil.Create("中文分词测试");
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateForMixedTest()
    {
        var tokenizer = TokenizerUtil.Create("Hello你好World世界");
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateWithEnglishEngineTest()
    {
        var tokenizer = TokenizerUtil.Create("test text", TokenizerUtil.EngineType.EN);
        Assert.NotNull(tokenizer);
    }

    [Fact]
    public void CreateWithChineseEngineTest()
    {
        var tokenizer = TokenizerUtil.Create("测试中文", TokenizerUtil.EngineType.CN);
        Assert.NotNull(tokenizer);
    }
}
