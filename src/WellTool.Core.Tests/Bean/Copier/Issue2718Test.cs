using Xunit;
using WellTool.Core.Bean;
using WellTool.Core.Bean.Copier;

namespace WellTool.Core.Tests.Bean.Copier;

/// <summary>
/// Issue2718 测试
/// </summary>
public class Issue2718Test
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var source = new SourceClass { Name = "test" };
        var target = new TargetClass();
        
        BeanUtil.CopyProperties(source, target);
        Assert.Equal("test", target.Name);
    }

    public class SourceClass
    {
        public string Name { get; set; } = "";
    }

    public class TargetClass
    {
        public string Name { get; set; } = "";
    }
}
