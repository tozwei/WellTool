namespace WellTool.Core.util.CRC;

/// <summary>
/// CRC16算法类
/// </summary>
public class CRC16Ansi
{
	/// <summary>
	/// 计算CRC16-ANSI
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x8005, 0x0000, true, true);
}

/// <summary>
/// CRC16-CCITT算法类
/// </summary>
public class CRC16CCITT
{
	/// <summary>
	/// 计算CRC16-CCITT
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x1021, 0xFFFF, true, true);
}

/// <summary>
/// CRC16-CCITT-FALSE算法类
/// </summary>
public class CRC16CCITTFalse
{
	/// <summary>
	/// 计算CRC16-CCITT-FALSE
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x1021, 0xFFFF, false, false);
}

/// <summary>
/// CRC16-DNP算法类
/// </summary>
public class CRC16DNP
{
	/// <summary>
	/// 计算CRC16-DNP
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x3D65, 0x0000, true, true);
}

/// <summary>
/// CRC16-MAXIM算法类
/// </summary>
public class CRC16Maxim
{
	/// <summary>
	/// 计算CRC16-MAXIM
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x8005, 0x0000, true, true);
}

/// <summary>
/// CRC16-MODBUS算法类
/// </summary>
public class CRC16Modbus
{
	/// <summary>
	/// 计算CRC16-MODBUS
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x8005, 0xFFFF, true, true);
}

/// <summary>
/// CRC16-USB算法类
/// </summary>
public class CRC16USB
{
	/// <summary>
	/// 计算CRC16-USB
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x8005, 0xFFFF, true, true);
}

/// <summary>
/// CRC16-X25算法类
/// </summary>
public class CRC16X25
{
	/// <summary>
	/// 计算CRC16-X25
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x1021, 0xFFFF, true, true);
}

/// <summary>
/// CRC16-XMODEM算法类
/// </summary>
public class CRC16XModem
{
	/// <summary>
	/// 计算CRC16-XMODEM
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>CRC值</returns>
	public static ushort Calc(byte[] data) => CRC16Util.Calc(data, 0x1021, 0x0000, true, true);
}
