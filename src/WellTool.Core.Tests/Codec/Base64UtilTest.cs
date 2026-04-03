using WellTool.Core.Codec;
using Xunit;

namespace WellTool.Core.Tests;

public class Base64UtilTest
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Base64Util.Encode("Hello");
        Assert.NotNull(encoded);
        Assert.Equal("SGVsbG8=", encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var decoded = Base64Util.Decode("SGVsbG8=");
        Assert.Equal("Hello", decoded);
    }

    [Fact]
    public void EncodeBytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var encoded = Base64Util.EncodeBytes(bytes);
        Assert.Equal("SGVsbG8=", encoded);
    }

    [Fact]
    public void DecodeBytesTest()
    {
        var bytes = Base64Util.DecodeBytes("SGVsbG8=");
        var str = System.Text.Encoding.UTF8.GetString(bytes);
        Assert.Equal("Hello", str);
    }

    [Fact]
    public void UrlSafeTest()
    {
        var encoded = Base64Util.EncodeUrlSafe("Hello+/World=");
        Assert.DoesNotContain("+", encoded);
        Assert.DoesNotContain("/", encoded);
    }

    [Fact]
    public void DecodeUrlSafeTest()
    {
        var encoded = Base64Util.EncodeUrlSafe("Hello+/World=");
        var decoded = Base64Util.DecodeUrlSafe(encoded);
        Assert.Equal("Hello+/World=", decoded);
    }
}
