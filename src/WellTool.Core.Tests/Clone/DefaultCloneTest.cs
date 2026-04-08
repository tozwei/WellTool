using Xunit;
using WellTool.Core.Clone;

namespace WellTool.Core.Tests.Clone;

/// <summary>
/// DefaultClone 测试
/// </summary>
public class DefaultCloneTest
{
    [Fact]
    public void CloneTest()
    {
        var original = new TestClass { Name = "test", Age = 20 };
        var cloned = DefaultClone.Instance.Clone(original);
        Assert.NotNull(cloned);
        Assert.Equal("test", cloned.Name);
        Assert.Equal(20, cloned.Age);
    }

    public class TestClass
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
