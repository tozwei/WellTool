using WellTool.Core.Collection;
using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class PartitionIterTest
{
    [Fact]
    public void IterTest()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        var iter = new PartitionIter<int>(list, 3);

        var partitions = new List<List<int>>();
        foreach (var partition in iter)
        {
            partitions.Add(partition);
        }

        Assert.Equal(3, partitions.Count);
        Assert.Equal(3, partitions[0].Count);
        Assert.Equal(3, partitions[1].Count);
        Assert.Equal(3, partitions[2].Count);
    }

    [Fact]
    public void IterMaxTest()
    {
        var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 9, 0, 12, 45, 12 };
        var iter = new PartitionIter<int>(list, 3);

        var max = 0;
        foreach (var partition in iter)
        {
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
        var list = new List<int> { 1, 2, 3, 4, 5 };
        var iter = new PartitionIter<int>(list, 2);

        var partitions = new List<List<int>>();
        foreach (var partition in iter)
        {
            partitions.Add(partition);
        }

        Assert.Equal(3, partitions.Count);
        Assert.Equal(2, partitions[0].Count);
        Assert.Equal(2, partitions[1].Count);
        Assert.Equal(1, partitions[2].Count);
    }
}
