using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace WellTool.Core.Lang;

/// <summary>
/// MongoDB ID生成策略实现
/// </summary>
public class ObjectId : IComparable<ObjectId>
{
	private static readonly Random Random = new Random();
	private static readonly int MACHINE = GetMachinePiece() | GetProcessPiece();
	private static int _nextInc = Random.Next();

	private readonly string _value;
	private readonly byte[] _bytes;

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="value">ObjectId字符串</param>
	private ObjectId(string value)
	{
		_value = value;
		_bytes = HexToBytes(value);
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="bytes">ObjectId字节数组</param>
	private ObjectId(byte[] bytes)
	{
		_bytes = bytes;
		_value = BytesToHex(bytes);
	}

	/// <summary>
	/// 获取新的ObjectId
	/// </summary>
	/// <returns>ObjectId实例</returns>
	public static ObjectId Get()
	{
		return new ObjectId(NextBytes());
	}

	/// <summary>
	/// 解析ObjectId字符串
	/// </summary>
	/// <param name="s">ObjectId字符串</param>
	/// <returns>ObjectId实例</returns>
	public static ObjectId Parse(string s)
	{
		if (!IsValid(s))
		{
			throw new ArgumentException("Invalid ObjectId string", nameof(s));
		}
		s = s.Replace("-", "");
		return new ObjectId(s);
	}

	/// <summary>
	/// 给定的字符串是否为有效的ObjectId
	/// </summary>
	/// <param name="s">字符串</param>
	/// <returns>是否为有效的ObjectId</returns>
	public static bool IsValid(string s)
	{
		if (s == null)
		{
			return false;
		}
		s = s.Replace("-", "");
		if (s.Length != 24)
		{
			return false;
		}

		foreach (char c in s)
		{
			if (c >= '0' && c <= '9') continue;
			if (c >= 'a' && c <= 'f') continue;
			if (c >= 'A' && c <= 'F') continue;
			return false;
		}
		return true;
	}

	/// <summary>
	/// 获取时间戳
	/// </summary>
	/// <returns>时间戳</returns>
	public long GetTimestamp()
	{
		return (_bytes[0] << 24) | (_bytes[1] << 16) | (_bytes[2] << 8) | _bytes[3];
	}

	/// <summary>
	/// 获取机器ID
	/// </summary>
	/// <returns>机器ID</returns>
	public int GetMachineId()
	{
		return (_bytes[4] << 24) | (_bytes[5] << 16) | (_bytes[6] << 8) | _bytes[7];
	}

	/// <summary>
	/// 比较两个ObjectId
	/// </summary>
	/// <param name="other">另一个ObjectId</param>
	/// <returns>比较结果</returns>
	public int CompareTo(ObjectId other)
	{
		if (other == null)
		{
			return 1;
		}
		return string.Compare(_value, other._value, StringComparison.Ordinal);
	}

	/// <summary>
	/// 比较两个ObjectId是否相等
	/// </summary>
	/// <param name="obj">另一个对象</param>
	/// <returns>是否相等</returns>
	public override bool Equals(object obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		return _value == ((ObjectId)obj)._value;
	}

	/// <summary>
	/// 获取哈希码
	/// </summary>
	/// <returns>哈希码</returns>
	public override int GetHashCode()
	{
		return _value.GetHashCode();
	}

	/// <summary>
	/// 转换为字符串
	/// </summary>
	/// <returns>ObjectId字符串</returns>
	public override string ToString()
	{
		return _value;
	}

	/// <summary>
	/// 获取一个objectId的bytes表现形式
	/// </summary>
	/// <returns>objectId字节数组</returns>
	private static byte[] NextBytes()
	{
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var machine = MACHINE;
		var inc = Interlocked.Increment(ref _nextInc);
		return new byte[]
		{
			(byte)((timestamp >> 24) & 0xFF),
			(byte)((timestamp >> 16) & 0xFF),
			(byte)((timestamp >> 8) & 0xFF),
			(byte)(timestamp & 0xFF),
			(byte)((machine >> 24) & 0xFF),
			(byte)((machine >> 16) & 0xFF),
			(byte)((machine >> 8) & 0xFF),
			(byte)(machine & 0xFF),
			(byte)((inc >> 24) & 0xFF),
			(byte)((inc >> 16) & 0xFF),
			(byte)((inc >> 8) & 0xFF),
			(byte)(inc & 0xFF)
		};
	}

	/// <summary>
	/// 字节数组转十六进制字符串
	/// </summary>
	/// <param name="bytes">字节数组</param>
	/// <returns>十六进制字符串</returns>
	private static string BytesToHex(byte[] bytes)
	{
		var sb = new StringBuilder(24);
		foreach (var b in bytes)
		{
			sb.Append(b.ToString("x2"));
		}
		return sb.ToString();
	}

	/// <summary>
	/// 十六进制字符串转字节数组
	/// </summary>
	/// <param name="hex">十六进制字符串</param>
	/// <returns>字节数组</returns>
	private static byte[] HexToBytes(string hex)
	{
		var bytes = new byte[12];
		for (int i = 0; i < 12; i++)
		{
			bytes[i] = System.Convert.ToByte(hex.Substring(i * 2, 2), 16);
		}
		return bytes;
	}

	/// <summary>
	/// 获取机器码片段
	/// </summary>
	private static int GetMachinePiece()
	{
		try
		{
			var sb = new StringBuilder();
			var interfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (var ni in interfaces)
			{
				sb.Append(ni.ToString());
			}
			return (sb.ToString().GetHashCode() << 16);
		}
		catch
		{
			return Random.Next() << 16;
		}
	}

	/// <summary>
	/// 获取进程码片段
	/// </summary>
	private static int GetProcessPiece()
	{
		int processId = Environment.CurrentManagedThreadId;
		int loaderId = Environment.CurrentManagedThreadId;

		string processSb = processId.ToString("x") + loaderId.ToString("x");
		return processSb.GetHashCode() & 0xFFFF;
	}
}