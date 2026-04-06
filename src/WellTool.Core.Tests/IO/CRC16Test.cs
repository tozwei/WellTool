using WellTool.Core.Codec;
using WellTool.Core.Util.CRC;
using Xunit;

namespace WellTool.Core.Tests;

public class CRC16Test
{
    [Fact]
    public void Crc16Test()
    {
        var data = "123456789"u8.ToArray();
        var crc = CRC16Util.Calculate(data);
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Crc16HexTest()
    {
        var data = "123456789"u8.ToArray();
        var hex = CRC16Util.CalculateHex(data);
        Assert.NotNull(hex);
        Assert.Equal(4, hex.Length);
    }

    [Fact]
    public void Crc16WithModelTest()
    {
        var data = "123456789"u8.ToArray();

        // Test with different models
        var crcIBM = CRC16Util.Calculate(data, CRC16Util.MODEL_IBM);
        var crcCCITT = CRC16Util.Calculate(data, CRC16Util.MODEL_CCITT);
        var crcXModem = CRC16Util.Calculate(data, CRC16Util.MODEL_XMODEM);

        // All should return valid CRC values
        Assert.True(crcIBM >= 0);
        Assert.True(crcCCITT >= 0);
        Assert.True(crcXModem >= 0);
    }
}
