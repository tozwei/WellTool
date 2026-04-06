using WellTool.Core.Lang;
using Xunit;

namespace WellTool.Core.Tests;

public class SingletonLastTest
{
    [Fact]
    public void GetInstanceTest()
    {
        var instance1 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        var instance2 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Xunit.Assert.Same(instance1, instance2);
    }

    [Fact]
    public void GetTest()
    {
        var instance1 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        var instance2 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Xunit.Assert.Same(instance1, instance2);
    }

    [Fact]
    public void RemoveInstanceTest()
    {
        var instance1 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Singleton.Remove(typeof(SingletonTestClass));
        var instance2 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Xunit.Assert.NotSame(instance1, instance2);
    }

    private class SingletonTestClass { }
}
