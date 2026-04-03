using WellTool.Core.Codec;
using Xunit;

namespace WellTool.Core.Tests;

public class CRC16Test
{
    [Fact]
    public void CalcTest()
    {
        var crc = CRC16.Calc("Hello");
        Assert.True(crc >= 0);
    }

    [Fact]
    public void CalcBytesTest()
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes("Hello");
        var crc = CRC16.Calc(bytes);
        Assert.True(crc >= 0);
    }

    [Fact]
    public void CheckTest()
    {
        var crc = CRC16.Calc("Hello");
        Assert.True(CRC16.Check("Hello", crc));
        Assert.False(CRC16.Check("World", crc));
    }

    [Fact]
    public void XmodemTest()
    {
        var crc = CRC16.Xmodem("Hello");
        Assert.True(crc >= 0);
    }

    [Fact]
    public void ModbusTest()
    {
        var crc = CRC16.Modbus("Hello");
        Assert.True(crc >= 0);
    }

    [Fact]
    public void UsbTest()
    {
        var crc = CRC16.Usb("Hello");
        Assert.True(crc >= 0);
    }
}
