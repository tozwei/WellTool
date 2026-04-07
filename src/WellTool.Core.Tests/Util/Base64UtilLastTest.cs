using WellTool.Core.Util;
using Xunit;

namespace WellTool.Core.Tests;

public class Base64UtilLastTest
{
    [Fact]
    public void EncodeTest()
    {
        var encoded = Base64Util.Encode("Hello");
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
        var encoded = Base64Util.Encode(bytes);
        Assert.Equal("SGVsbG8=", encoded);
    }

    [Fact]
    public void DecodeBytesTest()
    {
        var bytes = Base64Util.DecodeBytes("SGVsbG8=");
        var str = System.Text.Encoding.UTF8.GetString(bytes);
        Assert.Equal("Hello", str);
    }
}
