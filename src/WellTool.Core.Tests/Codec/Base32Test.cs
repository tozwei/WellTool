using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Base32 测试
/// </summary>
public class Base32Test
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Base32.Encode("Hello");
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var encoded = Base32.Encode("Hello");
        var decoded = Base32.Decode(encoded);
        Assert.Equal("Hello", decoded);
    }
}
