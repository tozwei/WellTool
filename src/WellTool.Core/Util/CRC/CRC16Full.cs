namespace WellTool.Core.util.crc;

using System;

/// <summary>
/// CRC16校验算法集合
/// </summary>
public static class CRC16
{
	/// <summary>
	/// CRC16-ANSI算法
	/// </summary>
	public static ushort Ansi(byte[] data) => CRC16Util.Calc(data, 0x8005, 0x0000, true, true);

	/// <summary>
	/// CRC16-CCITT算法
	/// </summary>
	public static ushort CCITT(byte[] data) => CRC16Util.Calc(data, 0x1021, 0xFFFF, true, true);

	/// <summary>
	/// CRC16-CCITT-FALSE算法
	/// </summary>
	public static ushort CCITTFalse(byte[] data) => CRC16Util.Calc(data, 0x1021, 0xFFFF, false, false);

	/// <summary>
	/// CRC16-DNP算法
	/// </summary>
	public static ushort DNP(byte[] data) => CRC16Util.Calc(data, 0x3D65, 0x0000, true, true);

	/// <summary>
	/// CRC16-MAXIM算法
	/// </summary>
	public static ushort Maxim(byte[] data) => CRC16Util.Calc(data, 0x8005, 0x0000, true, true);

	/// <summary>
	/// CRC16-MODBUS算法
	/// </summary>
	public static ushort Modbus(byte[] data) => CRC16Util.Calc(data, 0x8005, 0xFFFF, true, true);

	/// <summary>
	/// CRC16-USB算法
	/// </summary>
	public static ushort USB(byte[] data) => CRC16Util.Calc(data, 0x8005, 0xFFFF, true, true);

	/// <summary>
	/// CRC16-X25算法
	/// </summary>
	public static ushort X25(byte[] data) => CRC16Util.Calc(data, 0x1021, 0xFFFF, true, true);

	/// <summary>
	/// CRC16-XMODEM算法
	/// </summary>
	public static ushort XModem(byte[] data) => CRC16Util.Calc(data, 0x1021, 0x0000, true, true);
}
