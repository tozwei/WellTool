using WellTool.Core.Collection;
using WellTool.Core.Map.Multi;
using Xunit;

namespace WellTool.Core.Tests;

public class CollValueMapTest
{
    [Fact]
    public void TestListValueMapRemove()
    {
        var entries = new ListValueMap<string, string>();
        entries.PutValue("one", "11");
        entries.PutValue("one", "22");
        entries.PutValue("one", "33");
        entries.PutValue("one", "22");

        entries.PutValue("two", "44");
        entries.PutValue("two", "55");

        entries.PutValue("three", "11");

        entries.RemoveValue("one", "22");

        Assert.Equal(WellTool.Core.Collection.CollUtil.NewArrayList("11", "33", "22"), entries["one"]);

        entries.RemoveValues("two", WellTool.Core.Collection.CollUtil.NewArrayList("44", "55"));
        Assert.Empty(entries["two"]);
    }

    [Fact]
    public void TestSetValueMapRemove()
    {
        var entries = new SetValueMap<string, string>();
        entries.PutValue("one", "11");
        entries.PutValue("one", "22");
        entries.PutValue("one", "33");
        entries.PutValue("one", "22");

        entries.PutValue("two", "44");
        entries.PutValue("two", "55");

        entries.PutValue("three", "11");

        entries.RemoveValue("one", "22");
        Assert.Equal(WellTool.Core.Collection.CollUtil.HashSetOf("11", "33"), entries["one"]);

        entries.RemoveValues("two", WellTool.Core.Collection.CollUtil.NewArrayList("44", "55"));
        Assert.Empty(entries["two"]);
    }
}
