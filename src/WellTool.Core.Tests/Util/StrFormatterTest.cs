using WellTool.Core.Text;
using Xunit;

namespace WellTool.Core.Tests;

public class StrFormatterTest
{
    [Fact]
    public void FormatTest()
    {
        var result = StrFormatter.Format("Hello {0}", "World");
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
        var result = StrFormatter.Format("{name} is {age} years old", 
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
        var result = StrFormatter.Format("{{0}}", 123);
        Assert.Equal("{0}", result);
    }

    [Fact]
    public void FormatEmptyTest()
    {
        var result = StrFormatter.Format("Hello", "World");
        Assert.Equal("Hello", result);
    }
}
