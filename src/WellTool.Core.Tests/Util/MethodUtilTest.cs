using WellTool.Core.Util;
using System.Reflection;
using Xunit;

namespace WellTool.Core.Tests;

public class MethodUtilTest
{
    [Fact]
    public void GetMethodTest()
    {
        var method = ReflectUtil.GetMethod(typeof(TestClass), "DoWork");
        Assert.NotNull(method);
    }

    [Fact]
    public void GetMethodsTest()
    {
        var methods = ReflectUtil.GetMethods(typeof(TestClass));
        Assert.NotEmpty(methods);
    }

    [Fact]
    public void InvokeTest()
    {
        var obj = new TestClass();
        ReflectUtil.Invoke(obj, "DoWork");
        Assert.True(obj.WorkDone);
    }

    [Fact]
    public void GetMethodNameTest()
    {
        var method = ReflectUtil.GetMethod(typeof(TestClass), "DoWork");
        var name = method.Name;
        Assert.Equal("DoWork", name);
    }

    [Fact]
    public void GetParameterCountTest()
    {
        var method = ReflectUtil.GetMethod(typeof(TestClass), "DoWorkWithParam");
        var count = method.GetParameters().Length;
        Assert.Equal(1, count);
    }

    [Fact]
    public void IsStaticTest()
    {
        var staticMethod = ReflectUtil.GetMethod(typeof(StaticTestClass), "StaticMethod");
        Assert.True(staticMethod.IsStatic);
    }

    [Fact]
    public void IsPublicTest()
    {
        var method = ReflectUtil.GetMethod(typeof(TestClass), "DoWork");
        Assert.True(method.IsPublic);
    }

    private class TestClass
    {
        public bool WorkDone { get; private set; }
        public void DoWork() => WorkDone = true;
        public void DoWorkWithParam(int x) { }
    }

    private class StaticTestClass
    {
        public static void StaticMethod() { }
    }
}
