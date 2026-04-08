using Xunit;
using WellTool.Core.Util;
using System;

namespace WellTool.Core.Tests;

/// <summary>
/// ReferenceUtil 测试
/// </summary>
public class ReferenceUtilTest
{
    [Fact]
    public void CreateSoftReferenceTest()
    {
        var obj = new object();
        var softRef = ReferenceUtil.CreateSoftReference(obj);
        Assert.NotNull(softRef);
        Assert.Same(obj, softRef.Get());
        GC.KeepAlive(obj);
    }

    [Fact]
    public void CreateWeakReferenceTest()
    {
        var obj = new object();
        var weakRef = ReferenceUtil.CreateWeakReference(obj);
        Assert.NotNull(weakRef);
        GC.KeepAlive(obj);
    }

    [Fact]
    public void CreatePhantomReferenceTest()
    {
        var obj = new object();
        var referenceQueue = new ReferenceQueue<object>();
        var phantomRef = ReferenceUtil.CreatePhantomReference(obj, referenceQueue);
        Assert.NotNull(phantomRef);
        GC.KeepAlive(obj);
    }
}
