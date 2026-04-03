namespace WellTool.Core.util.CRC;

/// <summary>
/// CRC16 校验工具类
/// </summary>
public class CRC16Util
{
	/// <summary>
	/// CRC16-MODBUS 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcModbus(byte[] data)
	{
		return Calc(data, 0x8005, 0xFFFF, true, true);
	}

	/// <summary>
	/// CRC16-CCITT 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcCCITT(byte[] data)
	{
		return Calc(data, 0x1021, 0xFFFF, true, true);
	}

	/// <summary>
	/// CRC16-CCITT-FALSE 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcCCITTFalse(byte[] data)
	{
		return Calc(data, 0x1021, 0xFFFF, false, false);
	}

	/// <summary>
	/// CRC16-XModem 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcXModem(byte[] data)
	{
		return Calc(data, 0x1021, 0x0000, true, true);
	}

	/// <summary>
	/// CRC16-X25 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcX25(byte[] data)
	{
		return Calc(data, 0x1021, 0xFFFF, true, true);
	}

	/// <summary>
	/// CRC16-USB 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcUSB(byte[] data)
	{
		return Calc(data, 0x8005, 0xFFFF, true, true);
	}

	/// <summary>
	/// CRC16-DNP 计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC16值</returns>
	public static ushort CalcDNP(byte[] data)
	{
		return (ushort)(~Calc(data, 0x3D65, 0x0000, true, true));
	}

	/// <summary>
	/// CRC16 通用计算
	/// </summary>
	/// <param name="data">数据</param>
	/// <param name="polynomial">多项式</param>
	/// <param name="init">初始值</param>
	/// <param name="reverseIn">输入反转</param>
	/// <param name="reverseOut">输出反转</param>
	/// <returns>CRC16值</returns>
	public static ushort Calc(byte[] data, ushort polynomial, ushort init, bool reverseIn, bool reverseOut)
	{
		ushort crc = init;

		for (int i = 0; i < data.Length; i++)
		{
			byte b = reverseIn ? ReverseByte(data[i]) : data[i];
			crc ^= (ushort)(b << 8);

			for (int j = 0; j < 8; j++)
			{
				if ((crc & 0x8000) != 0)
				{
					crc = (ushort)((crc << 1) ^ polynomial);
				}
				else
				{
					crc <<= 1;
				}
			}
		}

		return reverseOut ? Reverse16(crc) : crc;
	}

	private static byte ReverseByte(byte b)
	{
		byte r = 0;
		for (int i = 0; i < 8; i++)
		{
			r <<= 1;
			r |= (byte)(b & 1);
			b >>= 1;
		}
		return r;
	}

	private static ushort Reverse16(ushort value)
	{
		ushort result = 0;
		for (int i = 0; i < 16; i++)
		{
			result <<= 1;
			result |= (ushort)(value & 1);
			value >>= 1;
		}
		return result;
	}
}
