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
        var crc = CRC16Util.CalcModbus(data);
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Crc16CCITTTest()
    {
        var data = "123456789"u8.ToArray();
        var crc = CRC16Util.CalcCCITT(data);
        Assert.True(crc >= 0);
    }

    [Fact]
    public void Crc16XModemTest()
    {
        var data = "123456789"u8.ToArray();
        var crc = CRC16Util.CalcXModem(data);
        Assert.True(crc >= 0);
    }
}
