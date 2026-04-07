using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ClassUtilLastTest
{
    [Fact]
    public void GetClassNameTest()
    {
        var className = ClassUtil.GetClassName(typeof(ClassUtilLastTest));
        Assert.Equal("ClassUtilLastTest", className);
    }

    [Fact]
    public void GetPackageTest()
    {
        var packageName = ClassUtil.GetPackage(typeof(ClassUtilLastTest));
        Assert.Contains("WellTool", packageName);
    }

    [Fact]
    public void LoadClassTest()
    {
        var type = ClassUtil.LoadClass("System.String");
        Assert.NotNull(type);
    }

    [Fact]
    public void GetSimpleNameTest()
    {
        var simpleName = ClassUtil.GetSimpleName(typeof(ClassUtilLastTest));
        Assert.Equal("ClassUtilLastTest", simpleName);
    }

    [Fact]
    public void IsPrimitiveWrapperTest()
    {
        Assert.True(ClassUtil.IsPrimitiveWrapper(typeof(int)));
        Assert.True(ClassUtil.IsPrimitiveWrapper(typeof(bool)));
    }

    [Fact]
    public void IsPrimitiveTest()
    {
        Assert.True(ClassUtil.IsPrimitive(typeof(int)));
        Assert.True(ClassUtil.IsPrimitive(typeof(string)));
    }
}
