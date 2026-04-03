using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

namespace WellTool.Core.Tests;

public class ClassUtilTest
{
    [Fact]
    public void GetClassNameTest()
    {
        var className = ClassUtil.GetClassName<ClassUtilTest>();
        Assert.Equal("ClassUtilTest", className);
    }

    [Fact]
    public void GetPackageNameTest()
    {
        var packageName = ClassUtil.GetPackageName<ClassUtilTest>();
        Assert.Contains("WellTool", packageName);
    }

    [Fact]
    public void IsPresentTest()
    {
        Assert.True(ClassUtil.IsPresent("System.String"));
        Assert.False(ClassUtil.IsPresent("Not.Exist.Class"));
    }

    [Fact]
    public void ForNameTest()
    {
        var type = ClassUtil.ForName("System.String");
        Assert.NotNull(type);
        Assert.Equal(typeof(string), type);
    }

    [Fact]
    public void NewInstanceTest()
    {
        var instance = ClassUtil.NewInstance<ClassUtilTest>();
        Assert.NotNull(instance);
    }

    [Fact]
    public void IsPublicTest()
    {
        Assert.True(ClassUtil.IsPublic<ClassUtilTest>());
    }
}
