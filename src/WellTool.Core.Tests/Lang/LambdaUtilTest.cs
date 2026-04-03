using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class LambdaUtilTest
{
    [Fact]
    public void GetFieldNameTest()
    {
        // Test getting field name from lambda
        var fieldName = LambdaUtil.GetFieldName<Person, string>(p => p.Name);
        Assert.Equal("Name", fieldName);
    }

    [Fact]
    public void GetFieldNameFromPropertyTest()
    {
        // Test getting field name from property
        var fieldName = LambdaUtil.GetFieldName<Person, int>(p => p.Age);
        Assert.Equal("Age", fieldName);
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
