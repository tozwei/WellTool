using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class MethodUtilTest
{
    [Fact]
    public void GetMethodTest()
    {
        var method = MethodUtil.GetMethod(typeof(TestClass), "DoWork");
        Assert.NotNull(method);
    }

    [Fact]
    public void GetMethodsTest()
    {
        var methods = MethodUtil.GetMethods(typeof(TestClass));
        Assert.NotEmpty(methods);
    }

    [Fact]
    public void InvokeTest()
    {
        var obj = new TestClass();
        MethodUtil.Invoke(obj, "DoWork");
        Assert.True(obj.WorkDone);
    }

    [Fact]
    public void GetMethodNameTest()
    {
        var method = MethodUtil.GetMethod(typeof(TestClass), "DoWork");
        var name = MethodUtil.GetMethodName(method);
        Assert.Equal("DoWork", name);
    }

    [Fact]
    public void GetParameterCountTest()
    {
        var method = MethodUtil.GetMethod(typeof(TestClass), "DoWorkWithParam");
        var count = MethodUtil.GetParameterCount(method);
        Assert.Equal(1, count);
    }

    [Fact]
    public void IsStaticTest()
    {
        var staticMethod = MethodUtil.GetMethod(typeof(StaticTestClass), "StaticMethod");
        Assert.True(MethodUtil.IsStatic(staticMethod));
    }

    [Fact]
    public void IsPublicTest()
    {
        var method = MethodUtil.GetMethod(typeof(TestClass), "DoWork");
        Assert.True(MethodUtil.IsPublic(method));
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
