using System;
using System.Net.NetworkInformation;
using System.Text;

namespace WellTool.Core.Lang;

/// <summary>
/// MongoDB ID生成策略实现
/// </summary>
public static class ObjectId
{
	private static readonly int NEXT_INC = new Random().Next();
	private static readonly int MACHINE = GetMachinePiece() | GetProcessPiece();

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
	/// 获取一个objectId的bytes表现形式
	/// </summary>
	/// <returns>objectId字节数组</returns>
	public static byte[] NextBytes()
	{
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		var machine = MACHINE;
		var inc = NEXT_INC;
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
	/// 获取一个objectId用下划线分割
	/// </summary>
	/// <returns>objectId</returns>
	public static string Next()
	{
		return Next(false);
	}

	/// <summary>
	/// 获取一个objectId
	/// </summary>
	/// <param name="withHyphen">是否包含分隔符</param>
	/// <returns>objectId</returns>
	public static string Next(bool withHyphen)
	{
		byte[] array = NextBytes();
		var sb = new StringBuilder(withHyphen ? 26 : 24);
		for (int i = 0; i < array.Length; i++)
		{
			if (withHyphen && i % 4 == 0 && i != 0)
			{
				sb.Append('-');
			}
			int t = array[i] & 0xff;
			if (t < 16)
			{
				sb.Append('0');
			}
			sb.Append(t.ToString("x"));
		}
		return sb.ToString();
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
			return new Random().Next() << 16;
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
