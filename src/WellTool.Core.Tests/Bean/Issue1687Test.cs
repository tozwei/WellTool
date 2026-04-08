using Xunit;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue1687 测试
/// </summary>
public class Issue1687Test
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var source = new Source { Name = "test" };
        var target = new Target();
        BeanUtil.CopyProperties(source, target);
        Assert.Equal("test", target.Name);
    }

    public class Source
    {
        public string Name { get; set; } = "";
    }

    public class Target
    {
        public string Name { get; set; } = "";
    }
}
