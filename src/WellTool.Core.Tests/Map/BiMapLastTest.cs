using WellTool.Core.Map;
using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class BiMapLastTest
{
    [Fact]
    public void BasicOperationsTest()
    {
        var biMap = new BiMap<string, int>(new Dictionary<string, int>());
        biMap.Put("a", 1);
        Assert.Equal(1, biMap.Get("a"));
        Assert.Equal("a", biMap.GetKey(1));
    }
}
