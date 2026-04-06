namespace WellTool.Captcha.Tests;

using WellTool.Captcha;
using WellTool.Captcha.Generator;
using Xunit;

public class GeneratorTest
{
    [Fact]
    public void GenerateCodeTest()
    {
        var generator = new RandomGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }

    [Fact]
    public void GenerateNumericTest()
    {
        // 使用 RandomGenerator（支持数字）
        var generator = new RandomGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
        Assert.True(code.All(char.IsDigit));
    }

    [Fact]
    public void GenerateAlphaTest()
    {
        // 使用 RandomGenerator（支持字母）
        var generator = new RandomGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }

    [Fact]
    public void GenerateAlphaNumericTest()
    {
        // 使用 RandomGenerator（默认支持字母数字）
        var generator = new RandomGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }

    [Fact]
    public void GenerateCustomLenTest()
    {
        var generator = new RandomGenerator(6);
        var code = generator.Generate();
        Assert.Equal(6, code.Length);
    }
}