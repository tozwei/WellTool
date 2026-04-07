using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class WordUtilsTest
{
    [Fact]
    public void ToCamelCaseTest()
    {
        Assert.Equal("helloWorld", NamingCase.ToCamelCase("hello_world"));
        Assert.Equal("testAbc", NamingCase.ToCamelCase("test_abc"));
    }

    [Fact]
    public void ToPascalCaseTest()
    {
        Assert.Equal("HelloWorld", NamingCase.ToPascalCase("hello_world"));
        Assert.Equal("TestAbc", NamingCase.ToPascalCase("test_abc"));
    }

    [Fact]
    public void ToUnderScoreCaseTest()
    {
        Assert.Equal("hello_world", NamingCase.ToUnderlineCase("helloWorld"));
        Assert.Equal("test_abc", NamingCase.ToUnderlineCase("testAbc"));
    }

    [Fact]
    public void ToHyphenCaseTest()
    {
        Assert.Equal("hello-world", NamingCase.ToKebabCase("helloWorld"));
        Assert.Equal("test-abc", NamingCase.ToKebabCase("testAbc"));
    }

    [Fact]
    public void ToSnakeCaseTest()
    {
        Assert.Equal("hello_world", NamingCase.ToUnderlineCase("helloWorld"));
    }

    [Fact]
    public void CapitalizeTest()
    {
        Assert.Equal("Hello", NamingCase.ToPascalCase("hello"));
    }

    [Fact]
    public void UncapitalizeTest()
    {
        Assert.Equal("hello", NamingCase.ToCamelCase("Hello"));
    }

    [Fact]
    public void WrapTest()
    {
        Assert.Equal("\"hello\"", "hello");
    }
}
