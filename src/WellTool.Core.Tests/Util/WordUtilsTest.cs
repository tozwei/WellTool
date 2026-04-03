using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class WordUtilsTest
{
    [Fact]
    public void ToCamelCaseTest()
    {
        Assert.Equal("helloWorld", WordUtils.ToCamelCase("hello_world"));
        Assert.Equal("helloWorld", WordUtils.ToCamelCase("helloWorld"));
    }

    [Fact]
    public void ToPascalCaseTest()
    {
        Assert.Equal("HelloWorld", WordUtils.ToPascalCase("hello_world"));
        Assert.Equal("HelloWorld", WordUtils.ToPascalCase("helloWorld"));
    }

    [Fact]
    public void ToUnderScoreCaseTest()
    {
        Assert.Equal("hello_world", WordUtils.ToUnderScoreCase("helloWorld"));
        Assert.Equal("hello_world", WordUtils.ToUnderScoreCase("HelloWorld"));
    }

    [Fact]
    public void ToHyphenCaseTest()
    {
        Assert.Equal("hello-world", WordUtils.ToHyphenCase("helloWorld"));
        Assert.Equal("hello-world", WordUtils.ToHyphenCase("HelloWorld"));
    }

    [Fact]
    public void ToSnakeCaseTest()
    {
        Assert.Equal("hello_world", WordUtils.ToSnakeCase("HelloWorld"));
    }

    [Fact]
    public void CapitalizeTest()
    {
        Assert.Equal("Hello World", WordUtils.Capitalize("hello world"));
    }

    [Fact]
    public void UncapitalizeTest()
    {
        Assert.Equal("hello World", WordUtils.Uncapitalize("Hello World"));
    }

    [Fact]
    public void WrapTest()
    {
        var wrapped = WordUtils.Wrap("Hello World", 5);
        Assert.Contains("\n", wrapped);
    }
}
