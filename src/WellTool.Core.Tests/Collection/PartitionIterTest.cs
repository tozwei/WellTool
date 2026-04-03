using WellTool.Core.Collection;
using Xunit;

namespace WellTool.Core.Tests;

public class PartitionIterTest
{
    [Fact]
    public void IterTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7, 8, 9);
        var iter = new PartitionIter<int>(() => list.GetEnumerator(), 3);

        var partitions = new List<List<int>>();
        while (iter.MoveNext())
        {
            partitions.Add(new List<int>(iter.Current));
        }

        Assert.Equal(3, partitions.Count);
        Assert.Equal(3, partitions[0].Count);
        Assert.Equal(3, partitions[1].Count);
        Assert.Equal(3, partitions[2].Count);
    }

    [Fact]
    public void IterMaxTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 0, 12, 45, 12);
        var iter = new PartitionIter<int>(() => list.GetEnumerator(), 3);

        var max = 0;
        while (iter.MoveNext())
        {
            var partition = iter.Current;
            foreach (var num in partition)
            {
                if (num > max)
                    max = num;
            }
        }
        Assert.Equal(45, max);
    }

    [Fact]
    public void PartitionSizeTest()
    {
        var list = CollUtil.NewArrayList(1, 2, 3, 4, 5);
        var iter = new PartitionIter<int>(() => list.GetEnumerator(), 2);

        var partitions = new List<List<int>>();
        while (iter.MoveNext())
        {
            partitions.Add(new List<int>(iter.Current));
        }

        Assert.Equal(3, partitions.Count);
        Assert.Equal(2, partitions[0].Count);
        Assert.Equal(2, partitions[1].Count);
        Assert.Equal(1, partitions[2].Count);
    }
}
