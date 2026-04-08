using Xunit;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue2082 测试
/// </summary>
public class Issue2082Test
{
    [Fact]
    public void CopyPropertiesTest()
    {
        var source = new Source { Value = "test" };
        var target = new Target();
        BeanUtil.CopyProperties(source, target);
        Assert.Equal("test", target.Value);
    }

    public class Source
    {
        public string Value { get; set; } = "";
    }

    public class Target
    {
        public string Value { get; set; } = "";
    }
}
