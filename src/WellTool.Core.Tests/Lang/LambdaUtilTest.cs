using WellTool.Core.Lang;
using Xunit;
using System.Reflection;

namespace WellTool.Core.Tests;

public class LambdaUtilTest
{
    [Fact]
    public void GetFieldNameTest()
    {
        var fieldName = LambdaUtil.GetFieldName<Person>(p => p.Name);
        Assert.Equal("Name", fieldName);
    }

    [Fact]
    public void GetMethodNameTest()
    {
        var methodName = LambdaUtil.GetMethodName<Person>(p => p.GetAge());
        Assert.Equal("GetAge", methodName);
    }

    [Fact]
    public void GetMemberNameTest()
    {
        var name = LambdaUtil.GetMemberName<Person>(p => p.Age);
        Assert.Equal("Age", name);
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }

        public int GetAge() => Age;
    }
}
