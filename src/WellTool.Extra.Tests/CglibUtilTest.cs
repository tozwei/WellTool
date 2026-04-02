using WellTool.Extra.Cglib;

namespace WellTool.Extra.Tests;

/// <summary>
/// CglibUtil 测试类
/// </summary>
public class CglibUtilTest
{
    [Fact]
    public void TestCopy_SameType_CopiesProperties()
    {
        // 测试同类型对象拷贝
        var source = new SourceClass { Id = 1, Name = "Test", Value = 100.5 };
        var target = new SourceClass();
        
        CglibUtil.Copy(source, target);
        
        Assert.Equal(source.Id, target.Id);
        Assert.Equal(source.Name, target.Name);
        Assert.Equal(source.Value, target.Value);
    }

    [Fact]
    public void TestCopy_DifferentType_CopiesMatchingProperties()
    {
        // 测试不同类型对象拷贝
        var source = new SourceClass { Id = 1, Name = "Test", Value = 100.5 };
        var target = new TargetClass();
        
        CglibUtil.Copy(source, target);
        
        Assert.Equal(source.Id, target.Id);
        Assert.Equal(source.Name, target.Name);
    }

    [Fact]
    public void TestCopy_NullSource_ThrowsException()
    {
        // 测试空源对象
        SourceClass source = null;
        var target = new SourceClass();
        
        Assert.Throws<ArgumentNullException>(() => CglibUtil.Copy(source, target));
    }

    [Fact]
    public void TestCopy_NullTarget_ThrowsException()
    {
        // 测试空目标对象
        var source = new SourceClass { Id = 1, Name = "Test", Value = 100.5 };
        SourceClass target = null;
        
        Assert.Throws<ArgumentNullException>(() => CglibUtil.Copy(source, target));
    }

    [Fact]
    public void TestCopyGeneric_CreatesNewInstance()
    {
        // 测试泛型拷贝创建新实例
        var source = new SourceClass { Id = 1, Name = "Test", Value = 100.5 };
        
        var target = CglibUtil.Copy<SourceClass>(source);
        
        Assert.NotNull(target);
        Assert.Equal(source.Id, target.Id);
        Assert.Equal(source.Name, target.Name);
        Assert.Equal(source.Value, target.Value);
    }

    [Fact]
    public void TestCopyGeneric_DifferentType_CreatesNewInstance()
    {
        // 测试不同类型泛型拷贝
        var source = new SourceClass { Id = 1, Name = "Test", Value = 100.5 };
        
        var target = CglibUtil.Copy<TargetClass>(source, typeof(TargetClass));
        
        Assert.NotNull(target);
        Assert.Equal(source.Id, target.Id);
        Assert.Equal(source.Name, target.Name);
    }

    [Fact]
    public void TestCopyList_ValidList_CopiesAllItems()
    {
        // 测试列表拷贝
        var sourceList = new List<SourceClass>
        {
            new SourceClass { Id = 1, Name = "One", Value = 1.0 },
            new SourceClass { Id = 2, Name = "Two", Value = 2.0 },
            new SourceClass { Id = 3, Name = "Three", Value = 3.0 }
        };
        
        var targetList = CglibUtil.CopyList<SourceClass, SourceClass>(sourceList);
        
        Assert.Equal(sourceList.Count, targetList.Count);
        for (int i = 0; i < sourceList.Count; i++)
        {
            Assert.Equal(sourceList[i].Id, targetList[i].Id);
            Assert.Equal(sourceList[i].Name, targetList[i].Name);
        }
    }

    [Fact]
    public void TestCopyList_DifferentType_CopiesAllItems()
    {
        // 测试不同类型列表拷贝
        var sourceList = new List<SourceClass>
        {
            new SourceClass { Id = 1, Name = "One", Value = 1.0 },
            new SourceClass { Id = 2, Name = "Two", Value = 2.0 }
        };
        
        var targetList = CglibUtil.CopyList<SourceClass, TargetClass>(sourceList);
        
        Assert.Equal(sourceList.Count, targetList.Count);
        foreach (var target in targetList)
        {
            Assert.IsType<TargetClass>(target);
        }
    }

    [Fact]
    public void TestCopyList_NullList_ReturnsEmptyList()
    {
        // 测试空列表拷贝
        List<SourceClass> sourceList = null;
        
        var targetList = CglibUtil.CopyList<SourceClass, SourceClass>(sourceList);
        
        Assert.NotNull(targetList);
        Assert.Empty(targetList);
    }

    [Fact]
    public void TestCopyList_EmptyList_ReturnsEmptyList()
    {
        // 测试空列表拷贝
        var sourceList = new List<SourceClass>();
        
        var targetList = CglibUtil.CopyList<SourceClass, SourceClass>(sourceList);
        
        Assert.NotNull(targetList);
        Assert.Empty(targetList);
    }

    [Fact]
    public void TestCopyList_WithCallback_ExecutesCallback()
    {
        // 测试带回调的列表拷贝
        var sourceList = new List<SourceClass>
        {
            new SourceClass { Id = 1, Name = "One", Value = 1.0 }
        };
        
        var callbackExecuted = false;
        
        var targetList = CglibUtil.CopyList<SourceClass, SourceClass>(
            sourceList, 
            () => new SourceClass(),
            (s, t) => callbackExecuted = true);
        
        Assert.True(callbackExecuted);
    }

    [Fact]
    public void TestToMap_ValidBean_ConvertsToMap()
    {
        // 测试 Bean 转 Map
        var bean = new SourceClass { Id = 1, Name = "Test", Value = 100.5 };
        
        var map = CglibUtil.ToMap(bean);
        
        Assert.NotNull(map);
        Assert.True(map.ContainsKey("Id"));
        Assert.True(map.ContainsKey("Name"));
        Assert.True(map.ContainsKey("Value"));
        Assert.Equal(1, map["Id"]);
        Assert.Equal("Test", map["Name"]);
    }

    [Fact]
    public void TestToMap_NullBean_ReturnsEmptyMap()
    {
        // 测试空 Bean 转 Map
        object bean = null;
        
        var map = CglibUtil.ToMap(bean);
        
        Assert.NotNull(map);
        Assert.Empty(map);
    }

    [Fact]
    public void TestFillBean_ValidMap_FillsBean()
    {
        // 测试 Map 填充 Bean
        var map = new Dictionary<string, object>
        {
            { "Id", 1 },
            { "Name", "Test" },
            { "Value", 100.5 }
        };
        var bean = new SourceClass();
        
        var result = CglibUtil.FillBean(map, bean);
        
        Assert.Equal(1, result.Id);
        Assert.Equal("Test", result.Name);
    }

    [Fact]
    public void TestFillBean_NullMap_ReturnsOriginalBean()
    {
        // 测试空 Map 填充
        Dictionary<string, object> map = null;
        var bean = new SourceClass { Id = 1 };
        
        var result = CglibUtil.FillBean(map, bean);
        
        Assert.Equal(bean, result);
    }

    [Fact]
    public void TestToBean_ValidMap_CreatesBean()
    {
        // 测试 Map 转 Bean
        var map = new Dictionary<string, object>
        {
            { "Id", 1 },
            { "Name", "Test" },
            { "Value", 100.5 }
        };
        
        var bean = CglibUtil.ToBean<SourceClass>(map);
        
        Assert.NotNull(bean);
        Assert.Equal(1, bean.Id);
        Assert.Equal("Test", bean.Name);
    }

    /// <summary>
    /// 源类
    /// </summary>
    private class SourceClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
    }

    /// <summary>
    /// 目标类
    /// </summary>
    private class TargetClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Extra { get; set; }
    }
}
