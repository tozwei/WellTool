using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class SingletonTest
{
    [Fact]
    public void GetInstanceTest()
    {
        var instance1 = Singleton.GetInstance<SingletonTestClass>();
        var instance2 = Singleton.GetInstance<SingletonTestClass>();
        Xunit.Assert.Same(instance1, instance2);
    }

    [Fact]
    public void GetTest()
    {
        var instance1 = Singleton.Get<SingletonTestClass>();
        var instance2 = Singleton.Get<SingletonTestClass>();
        Xunit.Assert.Same(instance1, instance2);
    }

    [Fact]
    public void RemoveInstanceTest()
    {
        var instance1 = Singleton.GetInstance<SingletonTestClass>();
        Singleton.Remove<SingletonTestClass>();
        var instance2 = Singleton.GetInstance<SingletonTestClass>();
        Xunit.Assert.NotSame(instance1, instance2);
    }

    private class SingletonTestClass
    {
    }
}
