using Xunit;
using WellTool.Core.Codec;

namespace WellTool.Core.Tests.Codec;

/// <summary>
/// Hashids 测试
/// </summary>
public class HashidsTest
{
    [Fact]
    public void EncodeTest()
    {
        var hashids = new Hashids("salt");
        var encoded = hashids.Encode(12345);
        Assert.NotNull(encoded);
        Assert.NotEmpty(encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var hashids = new Hashids("salt");
        var encoded = hashids.Encode(12345);
        var decoded = hashids.Decode(encoded);
        Assert.Contains(12345L, decoded);
    }

    [Fact]
    public void EncodeLongTest()
    {
        var hashids = new Hashids("salt");
        var encoded = hashids.EncodeLong(12345L);
        Assert.NotNull(encoded);
    }

    [Fact]
    public void DecodeLongTest()
    {
        var hashids = new Hashids("salt");
        var encoded = hashids.EncodeLong(12345L);
        var decoded = hashids.DecodeLong(encoded);
        Assert.Contains(12345L, decoded);
    }
}
