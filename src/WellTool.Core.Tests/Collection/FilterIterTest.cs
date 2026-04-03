using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class FilterIterTest
{
    [Fact]
    public void CheckFilterIterTest()
    {
        var list = CollUtil.NewArrayList("1", "2");
        var it = list.GetEnumerator();

        // filter 为 null
        var filterIter = new FilterIter<string>(() => it, null);

        int count = 0;
        while (filterIter.MoveNext())
        {
            if (filterIter.Current != null)
            {
                count++;
            }
        }
        Assert.Equal(2, count);

        it = list.GetEnumerator();
        // filter 不为空
        filterIter = new FilterIter<string>(() => it, key => key.Equals("1"));
        count = 0;
        while (filterIter.MoveNext())
        {
            if (filterIter.Current != null)
            {
                count++;
            }
        }
        Assert.Equal(1, count);
    }

    [Fact]
    public void FilterIterWithPredicateTest()
    {
        var list = CollUtil.NewArrayList("a", "b", "c", "d");
        var it = list.GetEnumerator();
        var filterIter = new FilterIter<string>(() => it, s => s.CompareTo("b") > 0);

        var result = new List<string>();
        while (filterIter.MoveNext())
        {
            result.Add(filterIter.Current);
        }

        Assert.Equal(2, result.Count);
        Assert.Contains("c", result);
        Assert.Contains("d", result);
    }
}
