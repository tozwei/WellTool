using WellTool.Core.Convert;
using Xunit;

namespace WellTool.Core.Tests;

public class ConverterRegistryTest
{
    [Fact]
    public void RegisterTest()
    {
        ConverterRegistry.Register<string, MyType>(s => new MyType(s));
        Assert.True(ConverterRegistry.Contains<string, MyType>());
    }

    [Fact]
    public void ConvertTest()
    {
        ConverterRegistry.Register<string, MyType>(s => new MyType(s));
        var result = ConverterRegistry.Convert<string, MyType>("test");
        Assert.NotNull(result);
        Assert.Equal("test", result.Value);
    }

    [Fact]
    public void GetConverterTest()
    {
        var converter = ConverterRegistry.GetConverter<string, MyType>();
        Assert.NotNull(converter);
    }

    [Fact]
    public void ContainsTest()
    {
        Assert.False(ConverterRegistry.Contains<string, NotRegisteredType>());
    }

    private class MyType
    {
        public string Value { get; }
        public MyType(string value) => Value = value;
    }

    private class NotRegisteredType { }
}
