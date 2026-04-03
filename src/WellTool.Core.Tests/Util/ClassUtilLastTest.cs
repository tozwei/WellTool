using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class ClassUtilLastTest
{
    [Fact]
    public void GetClassNameTest()
    {
        var className = ClassUtil.GetClassName<ClassUtilLastTest>();
        Assert.Equal("ClassUtilLastTest", className);
    }

    [Fact]
    public void GetPackageNameTest()
    {
        var packageName = ClassUtil.GetPackageName<ClassUtilLastTest>();
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
    }

    [Fact]
    public void NewInstanceTest()
    {
        var instance = ClassUtil.NewInstance<ClassUtilLastTest>();
        Assert.NotNull(instance);
    }
}
