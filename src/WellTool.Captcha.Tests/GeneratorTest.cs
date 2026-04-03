namespace WellTool.Captcha.Tests;

using Well.Captcha;

public class GeneratorTest
{
    [Fact]
    public void GenerateCodeTest()
    {
        var generator = new Generator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }

    [Fact]
    public void GenerateNumericTest()
    {
        var generator = new NumericGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
        Assert.True(code.All(char.IsDigit));
    }

    [Fact]
    public void GenerateAlphaTest()
    {
        var generator = new AlphaGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }

    [Fact]
    public void GenerateAlphaNumericTest()
    {
        var generator = new AlphaNumericGenerator(4);
        var code = generator.Generate();
        Assert.NotNull(code);
        Assert.Equal(4, code.Length);
    }

    [Fact]
    public void GenerateCustomLenTest()
    {
        var generator = new Generator(6);
        var code = generator.Generate();
        Assert.Equal(6, code.Length);
    }
}
