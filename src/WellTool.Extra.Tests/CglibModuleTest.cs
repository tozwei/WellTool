using System;
using System.Collections.Generic;
using System.Reflection;
using WellTool.Extra.Cglib;

namespace WellTool.Extra.Tests;

/// <summary>
/// BeanCopierCache 缓存类测试
/// </summary>
public class BeanCopierCacheTest
{
    /// <summary>
    /// 测试获取单例实例
    /// </summary>
    [Fact]
    public void TestInstance_NotNull()
    {
        Assert.NotNull(BeanCopierCache.Instance);
    }

    /// <summary>
    /// 测试获取同一实例
    /// </summary>
    [Fact]
    public void TestInstance_SameInstance()
    {
        var instance1 = BeanCopierCache.Instance;
        var instance2 = BeanCopierCache.Instance;
        Assert.Same(instance1, instance2);
    }

    /// <summary>
    /// 测试泛型Get方法
    /// </summary>
    [Fact]
    public void TestGet_Generic()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get<SourceClass, TargetClass>();
        Assert.NotNull(copier);
    }

    /// <summary>
    /// 测试泛型Get方法 - 多次调用返回同一委托
    /// </summary>
    [Fact]
    public void TestGet_Generic_ReturnsSameDelegate()
    {
        var cache = BeanCopierCache.Instance;
        var copier1 = cache.Get<SourceClass, TargetClass>();
        var copier2 = cache.Get<SourceClass, TargetClass>();
        Assert.Same(copier1, copier2);
    }

    /// <summary>
    /// 测试非泛型Get方法
    /// </summary>
    [Fact]
    public void TestGet_NonGeneric()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get(typeof(SourceClass), typeof(TargetClass));
        Assert.NotNull(copier);
    }

    /// <summary>
    /// 测试属性拷贝功能
    /// </summary>
    [Fact]
    public void TestCopyProperties()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get<SourceClass, TargetClass>();
        
        var source = new SourceClass
        {
            Name = "Test",
            Value = 100
        };
        
        var target = new TargetClass();
        copier(source, target);
        
        Assert.Equal("Test", target.Name);
        Assert.Equal(100, target.Value);
    }

    /// <summary>
    /// 测试源对象为null
    /// </summary>
    [Fact]
    public void TestCopyProperties_NullSource()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get<SourceClass, TargetClass>();
        
        var target = new TargetClass();
        copier(null, target);
        
        // 应该不抛异常
        Assert.NotNull(target);
    }

    /// <summary>
    /// 测试目标对象为null
    /// </summary>
    [Fact]
    public void TestCopyProperties_NullTarget()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get<SourceClass, TargetClass>();
        
        var source = new SourceClass { Name = "Test" };
        
        // 应该不抛异常
        copier(source, null);
    }

    /// <summary>
    /// 测试空类拷贝
    /// </summary>
    [Fact]
    public void TestCopyEmptyClasses()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get<EmptyClass, EmptyClass>();
        
        var source = new EmptyClass();
        var target = new EmptyClass();
        
        copier(source, target);
        Assert.NotNull(target);
    }

    /// <summary>
    /// 测试不同类型属性
    /// </summary>
    [Fact]
    public void TestCopyDifferentTypes()
    {
        var cache = BeanCopierCache.Instance;
        var copier = cache.Get<SourceClass, TargetClass>();
        
        var source = new SourceClass
        {
            Name = "DifferentTypes",
            Value = 999
        };
        
        var target = new TargetClass();
        copier(source, target);
        
        Assert.Equal("DifferentTypes", target.Name);
        Assert.Equal(999, target.Value);
    }

    /// <summary>
    /// 测试类型匹配键生成
    /// </summary>
    [Fact]
    public void TestTypeMatchKey()
    {
        var cache = BeanCopierCache.Instance;
        
        // 获取相同的类型组合应该返回缓存的委托
        var copier1 = cache.Get<SourceClass, TargetClass>();
        var copier2 = cache.Get<SourceClass, TargetClass>();
        
        Assert.Same(copier1, copier2);
    }
}

/// <summary>
/// 测试用源类
/// </summary>
public class SourceClass
{
    public string Name { get; set; }
    public int Value { get; set; }
}

/// <summary>
/// 测试用目标类
/// </summary>
public class TargetClass
{
    public string Name { get; set; }
    public int Value { get; set; }
}

/// <summary>
/// 测试用空类
/// </summary>
public class EmptyClass
{
}

/// <summary>
/// 测试用复杂类
/// </summary>
public class ComplexSourceClass
{
    public string StringProperty { get; set; }
    public int IntProperty { get; set; }
    public DateTime DateProperty { get; set; }
    public bool BoolProperty { get; set; }
    public double DoubleProperty { get; set; }
}

/// <summary>
/// 测试用复杂目标类
/// </summary>
public class ComplexTargetClass
{
    public string StringProperty { get; set; }
    public int IntProperty { get; set; }
    public DateTime DateProperty { get; set; }
    public bool BoolProperty { get; set; }
    public double DoubleProperty { get; set; }
}

/// <summary>
/// CglibUtil 工具类测试
/// </summary>
public class CglibUtilCopierTest
{
    /// <summary>
    /// 测试Bean拷贝功能
    /// </summary>
    [Fact]
    public void TestCopyBean()
    {
        var source = new SourceClass { Name = "CopyTest", Value = 42 };
        var target = CglibUtil.CopyBean<SourceClass, TargetClass>(source);
        
        Assert.NotNull(target);
        Assert.Equal("CopyTest", target.Name);
        Assert.Equal(42, target.Value);
    }

    /// <summary>
    /// 测试Bean拷贝到现有对象
    /// </summary>
    [Fact]
    public void TestCopyBeanToExisting()
    {
        var source = new SourceClass { Name = "Existing", Value = 100 };
        var target = new TargetClass { Name = "Original" };
        
        CglibUtil.CopyBean(source, target);
        
        Assert.Equal("Existing", target.Name);
        Assert.Equal(100, target.Value);
    }

    /// <summary>
    /// 测试Map转换
    /// </summary>
    [Fact]
    public void TestToMap()
    {
        var source = new SourceClass { Name = "MapTest", Value = 50 };
        var map = CglibUtil.ToMap<SourceClass>(source);
        
        Assert.NotNull(map);
        Assert.Equal("MapTest", map["Name"]);
        Assert.Equal(50, map["Value"]);
    }

    /// <summary>
    /// 测试从Map转换
    /// </summary>
    [Fact]
    public void TestFromMap()
    {
        var map = new Dictionary<string, object>
        {
            { "Name", "FromMap" },
            { "Value", 75 }
        };
        
        var target = CglibUtil.FromMap<TargetClass>(map);
        
        Assert.NotNull(target);
        Assert.Equal("FromMap", target.Name);
        Assert.Equal(75, target.Value);
    }

    /// <summary>
    /// 测试复杂类型拷贝
    /// </summary>
    [Fact]
    public void TestCopyComplexBean()
    {
        var source = new ComplexSourceClass
        {
            StringProperty = "Complex",
            IntProperty = 123,
            DateProperty = new DateTime(2025, 1, 1),
            BoolProperty = true,
            DoubleProperty = 3.14
        };
        
        var target = CglibUtil.CopyBean<ComplexSourceClass, ComplexTargetClass>(source);
        
        Assert.NotNull(target);
        Assert.Equal("Complex", target.StringProperty);
        Assert.Equal(123, target.IntProperty);
        Assert.Equal(new DateTime(2025, 1, 1), target.DateProperty);
        Assert.True(target.BoolProperty);
        Assert.Equal(3.14, target.DoubleProperty);
    }
}
