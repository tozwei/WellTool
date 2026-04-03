using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class FieldUtilTest
{
    [Fact]
    public void GetFieldTest()
    {
        var field = FieldUtil.GetField(typeof(TestClass), "Name");
        Assert.NotNull(field);
    }

    [Fact]
    public void GetFieldsTest()
    {
        var fields = FieldUtil.GetFields(typeof(TestClass));
        Assert.NotEmpty(fields);
    }

    [Fact]
    public void GetDeclaredFieldTest()
    {
        var field = FieldUtil.GetDeclaredField(typeof(TestClass), "Name");
        Assert.NotNull(field);
    }

    [Fact]
    public void GetDeclaredFieldsTest()
    {
        var fields = FieldUtil.GetDeclaredFields(typeof(TestClass));
        Assert.NotEmpty(fields);
    }

    [Fact]
    public void SetFieldTest()
    {
        var obj = new TestClass();
        var field = FieldUtil.GetField(typeof(TestClass), "Name");
        FieldUtil.SetField(obj, field, "John");
        Assert.Equal("John", obj.Name);
    }

    [Fact]
    public void GetFieldValueTest()
    {
        var obj = new TestClass { Name = "John" };
        var value = FieldUtil.GetFieldValue(obj, "Name");
        Assert.Equal("John", value);
    }

    private class TestClass
    {
        public string Name { get; set; } = "";
        private string _value = "";
    }
}
