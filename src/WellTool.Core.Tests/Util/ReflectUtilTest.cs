using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ReflectUtilTest
{
    [Fact]
    public void GetFieldValueTest()
    {
        var obj = new TestClass { Name = "John" };
        var value = ReflectUtil.GetFieldValue(obj, "Name");
        Assert.Equal("John", value);
    }

    [Fact]
    public void SetFieldValueTest()
    {
        var obj = new TestClass();
        ReflectUtil.SetFieldValue(obj, "Name", "Jane");
        Assert.Equal("Jane", obj.Name);
    }

    [Fact]
    public void GetMethodTest()
    {
        var method = ReflectUtil.GetMethod(typeof(TestClass), "GetName");
        Assert.NotNull(method);
    }

    [Fact]
    public void InvokeMethodTest()
    {
        var obj = new TestClass { Name = "John" };
        var result = ReflectUtil.InvokeMethod(obj, "GetName");
        Assert.Equal("John", result);
    }

    [Fact]
    public void GetFieldTest()
    {
        var field = ReflectUtil.GetField(typeof(TestClass), "Name");
        Assert.NotNull(field);
    }

    [Fact]
    public void GetFieldsTest()
    {
        var fields = ReflectUtil.GetFields(typeof(TestClass));
        Assert.NotEmpty(fields);
    }

    [Fact]
    public void GetMethodsTest()
    {
        var methods = ReflectUtil.GetMethods(typeof(TestClass));
        Assert.NotEmpty(methods);
    }

    [Fact]
    public void NewInstanceTest()
    {
        var obj = ReflectUtil.CreateInstance<TestClass>();
        Assert.NotNull(obj);
    }

    [Fact]
    public void IsAssignableFromTest()
    {
        Assert.True(ReflectUtil.IsAssignableFrom(typeof(object), typeof(TestClass)));
        Assert.False(ReflectUtil.IsAssignableFrom(typeof(string), typeof(TestClass)));
    }

    [Fact]
    public void GetTypeNameTest()
    {
        var name = ReflectUtil.GetTypeName(typeof(TestClass));
        Assert.Contains("TestClass", name);
    }

    private class TestClass
    {
        public string Name = "";
        public string GetName() => Name;
    }
}
