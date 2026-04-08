using Xunit;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue2202 测试
/// </summary>
public class Issue2202Test
{
    [Fact]
    public void MapToBeanTest()
    {
        var map = new System.Collections.Generic.Dictionary<string, object>
        {
            { "name", "test" },
            { "age", 20 }
        };
        var bean = BeanUtil.MapToBean<MapBean>(map);
        Assert.Equal("test", bean.Name);
        Assert.Equal(20, bean.Age);
    }

    public class MapBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
