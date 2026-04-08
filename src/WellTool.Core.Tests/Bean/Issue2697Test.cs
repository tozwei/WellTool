using Xunit;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue2697 测试
/// </summary>
public class Issue2697Test
{
    [Fact]
    public void BeanToMapTest()
    {
        var bean = new TestBean { Name = "test", Age = 20 };
        var map = BeanUtil.BeanToMap(bean);
        // BeanToMap 默认将字段名转换为小写
        Assert.Equal("test", map["name"]);
        Assert.Equal(20, map["age"]);
    }

    public class TestBean
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
