using Xunit;
using System.Collections.Generic;

namespace WellTool.Core.Tests;

public class DictTest
{
    [Fact]
    public void CreateTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 25 }
        };

        Xunit.Assert.Equal("John", dict["name"]);
        Xunit.Assert.Equal(25, dict["age"]);
    }

    [Fact]
    public void GetStrTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "name", "John" }
        };

        Xunit.Assert.Equal("John", dict["name"]);
        Xunit.Assert.True(dict.TryGetValue("notExist", out _) == false);
    }

    [Fact]
    public void GetIntTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "age", 25 }
        };

        Xunit.Assert.Equal(25, dict["age"]);
        Xunit.Assert.True(dict.TryGetValue("notExist", out _) == false);
    }

    [Fact]
    public void GetLongTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "num", 1000000L }
        };

        Xunit.Assert.Equal(1000000L, dict["num"]);
    }

    [Fact]
    public void GetBoolTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "enabled", true }
        };

        Xunit.Assert.True((bool)dict["enabled"]);
        Xunit.Assert.True(dict.TryGetValue("notExist", out _) == false);
    }

    [Fact]
    public void GetDoubleTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "price", 99.99 }
        };

        Xunit.Assert.Equal(99.99, (double)dict["price"]);
    }

    [Fact]
    public void ContainsKeyTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "name", "John" }
        };

        Xunit.Assert.True(dict.ContainsKey("name"));
        Xunit.Assert.False(dict.ContainsKey("notExist"));
    }

    [Fact]
    public void RemoveTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 25 }
        };

        dict.Remove("age");

        Xunit.Assert.True(dict.ContainsKey("name"));
        Xunit.Assert.False(dict.ContainsKey("age"));
    }

    [Fact]
    public void ToMapTest()
    {
        var dict = new Dictionary<string, object>
        {
            { "name", "John" },
            { "age", 25 }
        };

        Xunit.Assert.Equal(2, dict.Count);
        Xunit.Assert.Equal("John", dict["name"]);
    }
}
