using Xunit;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue2009 测试
/// </summary>
public class Issue2009Test
{
    [Fact]
    public void ToBeanTest()
    {
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "name", "test" }
        };
        var bean = BeanUtil.ToBean<TestBean>(map);
        Assert.Equal("test", bean?.Name);
    }

    public class TestBean
    {
        public string Name { get; set; } = "";
    }
}
