using WellTool.Core.Lang.Func;
using Xunit;

public class LambdaUtilTest
{
    [Fact]
    public void GetFieldNameTest()
    {
        var name = LambdaUtil.GetFieldName<Person>(p => p.Name);
        Assert.Equal("Name", name);
    }

    [Fact]
    public void GetFieldNameFromPropertyTest()
    {
        var name = LambdaUtil.GetFieldName<Person>(p => p.Age);
        Assert.Equal("Age", name);
    }

    private class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
