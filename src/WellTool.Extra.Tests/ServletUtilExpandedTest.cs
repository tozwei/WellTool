using System;
using System.IO;
using System.Text;
using WellTool.Extra.Servlet;

namespace WellTool.Extra.Tests;

/// <summary>
/// ServletUtil Servlet工具测试类
/// </summary>
public class ServletUtilExpandedTest
{
    /// <summary>
    /// 测试ServletUtil类型存在
    /// </summary>
    [Fact]
    public void TestServletUtilClassExists()
    {
        var type = typeof(ServletUtil);
        Assert.NotNull(type);
    }

    /// <summary>
    /// 测试ServletUtil是静态类
    /// </summary>
    [Fact]
    public void TestServletUtilIsStaticClass()
    {
        var type = typeof(ServletUtil);
        Assert.True(type.IsAbstract);
        Assert.True(type.IsSealed);
    }
}

/// <summary>
/// ExtraUtil 扩展测试类
/// </summary>
public class ExtraUtilExpandedTest
{
    /// <summary>
    /// 测试获取单例实例
    /// </summary>
    [Fact]
    public void TestGetInstance()
    {
        var instance = WellTool.Extra.ExtraUtil.Instance;
        Assert.NotNull(instance);
    }

    /// <summary>
    /// 测试实例是单例
    /// </summary>
    [Fact]
    public void TestIsSingleton()
    {
        var instance1 = WellTool.Extra.ExtraUtil.Instance;
        var instance2 = WellTool.Extra.ExtraUtil.Instance;
        Assert.Same(instance1, instance2);
    }
}
