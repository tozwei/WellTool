using Xunit;

namespace WellTool.Core.Tests.Codec;

public class Base64UtilTest
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = WellTool.Core.Util.Base64Util.Encode("Hello");
        Assert.NotNull(encoded);
        Assert.Equal("SGVsbG8=", encoded);
    }

    [Fact]
    public void DecodeTest()
    {
        var decoded = WellTool.Core.Util.Base64Util.Decode("SGVsbG8=");
        Assert.Equal("Hello", decoded);
    }

    [Fact]
    public void EncodeBytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var encoded = WellTool.Core.Util.Base64Util.Encode(bytes);
        Assert.Equal("SGVsbG8=", encoded);
    }

    [Fact]
    public void DecodeBytesTest()
    {
        var bytes = WellTool.Core.Util.Base64Util.DecodeBytes("SGVsbG8=");
        var str = System.Text.Encoding.UTF8.GetString(bytes);
        Assert.Equal("Hello", str);
    }
}