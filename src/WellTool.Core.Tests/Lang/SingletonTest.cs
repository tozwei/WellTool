using WellTool.Core.Lang;
using Xunit;
using Assert = Xunit.Assert;

namespace WellTool.Core.Tests;

public class SingletonTest
{
    [Fact]
    public void GetTest()
    {
        var instance1 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        var instance2 = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Assert.Same(instance1, instance2);
    }

    [Fact]
    public void PutAndGetTest()
    {
        var instance = new SingletonTestClass();
        Singleton.Put(instance);
        var result = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Assert.Same(instance, result);
    }

    [Fact]
    public void RemoveTest()
    {
        var instance = new SingletonTestClass();
        Singleton.Put(instance);
        Singleton.Remove(typeof(SingletonTestClass));
        var result = Singleton.Get<SingletonTestClass>(typeof(SingletonTestClass));
        Assert.NotSame(instance, result);
    }

    private class SingletonTestClass
    {
    }
}
