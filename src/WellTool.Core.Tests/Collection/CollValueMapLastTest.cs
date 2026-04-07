using WellTool.Core.Map;
using WellTool.Core.Map.Multi;
using Xunit;

namespace WellTool.Core.Tests;

public class CollValueMapLastTest
{
    [Fact]
    public void TestListValueMapBasicOperations()
    {
        var map = new ListValueMap<string, string>();
        map.PutValue("key1", "value1");
        map.PutValue("key1", "value2");
        
        var values = map.Get("key1", 0);
        Assert.NotNull(values);
    }

    [Fact]
    public void TestSetValueMapBasicOperations()
    {
        var map = new SetValueMap<string, string>();
        map.PutValue("key1", "value1");
        map.PutValue("key1", "value1");
        
        var values = map.Get("key1", 0);
        Assert.NotNull(values);
    }
}
