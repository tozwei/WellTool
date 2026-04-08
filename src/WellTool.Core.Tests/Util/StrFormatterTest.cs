using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests.Util;

public class StrFormatterTest
{
    [Fact]
    public void FormatTest()
    {
        // 直接测试 string.Format
        var stringFormatResult = string.Format("Hello {0}", "World");
        Console.WriteLine($"string.Format result: {stringFormatResult}");
        
        // 测试 StrFormatter.Format
        var result = StrFormatter.Format("Hello {0}", "World");
        Console.WriteLine($"StrFormatter.Format result: {result}");
        
        Assert.Equal("Hello World", result);
    }

    [Fact]
    public void FormatMultipleTest()
    {
        var result = StrFormatter.Format("{0} + {1} = {2}", 1, 2, 3);
        Assert.Equal("1 + 2 = 3", result);
    }

    [Fact]
    public void FormatNamedTest()
    {
        var result = StrFormatter.FormatWithObject("{name} is {age} years old", 
            new { name = "John", age = 25 });
        Assert.Equal("John is 25 years old", result);
    }

    [Fact]
    public void FormatDictionaryTest()
    {
        var dict = new System.Collections.Generic.Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 25 }
        };
        var result = StrFormatter.Format("{name} is {age} years old", dict);
        Assert.Equal("John is 25 years old", result);
    }

    [Fact]
    public void FormatWithBracesTest()
    {
        // 直接测试 string.Format
        var stringFormatResult = string.Format("{{0}}", 123);
        Console.WriteLine($"string.Format result: {stringFormatResult}");
        
        // 测试 StrFormatter.Format
        var result = StrFormatter.Format("{{0}}", 123);
        Console.WriteLine($"StrFormatter.Format result: {result}");
        
        Assert.Equal("{0}", result);
    }

    [Fact]
    public void FormatEmptyTest()
    {
        var result = StrFormatter.Format("Hello", "World");
        Assert.Equal("Hello", result);
    }
}
