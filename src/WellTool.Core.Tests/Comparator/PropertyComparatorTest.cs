using Xunit;
using WellTool.Core.Comparator;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Comparator;

/// <summary>
/// PropertyComparator 测试
/// </summary>
public class PropertyComparatorTest
{
    [Fact]
    public void CompareByPropertyTest()
    {
        var list = new List<TestItem>
        {
            new TestItem { Name = "B", Age = 20 },
            new TestItem { Name = "A", Age = 25 },
            new TestItem { Name = "C", Age = 15 }
        };

        list.Sort(new PropertyComparator<TestItem>("Name"));
        Assert.Equal("A", list[0].Name);

        list.Sort(new PropertyComparator<TestItem>("Age"));
        Assert.Equal(15, list[0].Age);
    }

    public class TestItem
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
