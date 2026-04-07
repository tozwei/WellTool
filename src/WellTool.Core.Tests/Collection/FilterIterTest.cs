using WellTool.Core.Collection;
using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests.Collection;

public class FilterIterTest
{
    [Fact]
    public void CheckFilterIterTest()
    {
        var list = new List<string> { "1", "2" };
        var it = list.GetEnumerator();

        var filterIter = new FilterIter<string>(it, null);

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
        filterIter = new FilterIter<string>(it, key => key.Equals("1"));
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
        var list = new List<string> { "a", "b", "c", "d" };
        var it = list.GetEnumerator();
        var filterIter = new FilterIter<string>(it, s => s.CompareTo("b") > 0);

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