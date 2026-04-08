using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Base64 测试
/// </summary>
public class Base64Test
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Base64.Encode("Hello");
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var encoded = Base64.Encode("Hello");
        var decoded = Base64.Decode(encoded);
        Assert.Equal("Hello", decoded);
    }

    [Fact]
    public void EncodeBytesTest()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var encoded = Base64.Encode(bytes);
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeBytesTest()
    {
        var bytes = new byte[] { 1, 2, 3 };
        var encoded = Base64.Encode(bytes);
        var decoded = Base64.DecodeBytes(encoded);
        Assert.Equal(bytes, decoded);
    }
}
