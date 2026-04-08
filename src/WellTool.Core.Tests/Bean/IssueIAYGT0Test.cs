using Xunit;
using WellTool.Core;
using WellTool.Core.Bean;

namespace WellTool.Core.Tests.Bean;

/// <summary>
/// Issue IAYGT0 测试 - BeanUtil.SetProperty 是否调用 setter 方法
/// </summary>
public class IssueIAYGT0Test
{
    [Fact]
    public void TestSetProperty()
    {
        var cat = new Cat();
        BeanUtil.SetProperty(cat, "name", "Kitty");
        Assert.Equal("RedKitty", cat.Name);
    }

    public class Cat
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = "Red" + value;
        }
    }
}
