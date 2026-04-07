using WellTool.Core.Map;
using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class BiMapLastTest
{
    [Fact]
    public void BasicOperationsTest()
    {
        var biMap = new BiMap<string, int>();
        biMap.Add("a", 1);
        Assert.Equal(1, biMap["a"]);
        Assert.Equal("a", biMap.GetKey(1));
    }
}
