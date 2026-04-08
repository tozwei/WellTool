using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Base58 测试
/// </summary>
public class Base58Test
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Base58.Encode("Hello");
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var encoded = Base58.Encode("Hello");
        var decoded = Base58.Decode(encoded);
        Assert.Equal("Hello", decoded);
    }

    [Fact]
    public void EncodeBytesTest()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var encoded = Base58.Encode(bytes);
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeBytesTest()
    {
        var bytes = new byte[] { 1, 2, 3, 4, 5 };
        var encoded = Base58.Encode(bytes);
        var decoded = Base58.DecodeBytes(encoded);
        Assert.Equal(bytes, decoded);
    }
}
