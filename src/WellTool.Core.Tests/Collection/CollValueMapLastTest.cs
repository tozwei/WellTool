using WellTool.Core.Map;
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
        
        var values = map.Get("key1");
        Assert.NotNull(values);
        Assert.Equal(2, values.Count);
    }

    [Fact]
    public void TestSetValueMapBasicOperations()
    {
        var map = new SetValueMap<string, string>();
        map.PutValue("key1", "value1");
        map.PutValue("key1", "value1");
        
        var values = map.Get("key1");
        Assert.NotNull(values);
        Assert.Single(values);
    }
}
