using System.Net.NetworkInformation;
using System.Text;
using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace WellTool.Core.Lang;

/// <summary>
/// MongoDB风格的ObjectId生成策略实现
/// ObjectId由以下几部分组成：
/// 1. Time 时间戳
/// 2. Machine 所在主机的唯一标识符，一般是机器主机名的散列值
/// 3. PID 进程ID。确保同一机器中不冲突
/// 4. INC 自增计数器。确保同一秒内产生objectId的唯一性
/// </summary>
public static class ObjectId
{
	/// <summary>
	/// 线程安全的随机数生成器
	/// </summary>
	private static readonly Random Random = new();
	/// <summary>
	/// 线程安全的下一个随机数，每次生成自增+1
	/// </summary>
	private static int _nextInc = Random.Next();
	private static readonly object LockObj = new();

	/// <summary>
	/// 机器信息
	/// </summary>
	private static readonly int Machine;

	static ObjectId()
	{
		Machine = GetMachinePiece() | GetProcessPiece();
	}

	/// <summary>
	/// 给定的字符串是否为有效的ObjectId
	/// </summary>
	/// <param name="s">字符串</param>
	/// <returns>是否为有效的ObjectId</returns>
	public static bool IsValid(string? s)
	{
		if (s == null)
			return false;

		s = s.Replace("-", "");
		var len = s.Length;
		if (len != 24)
			return false;

		foreach (var c in s)
		{
			if (c >= '0' && c <= '9')
				continue;
			if (c >= 'a' && c <= 'f')
				continue;
			if (c >= 'A' && c <= 'F')
				continue;
			return false;
		}
		return true;
	}

	/// <summary>
	/// 获取一个objectId的bytes表现形式
	/// </summary>
	/// <returns>字节数组</returns>
	public static byte[] NextBytes()
	{
		var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		lock (LockObj)
		{
			var inc = _nextInc++;
			return new byte[]
			{
				(byte)(timestamp >> 24),
				(byte)(timestamp >> 16),
				(byte)(timestamp >> 8),
				(byte)timestamp,
				(byte)(Machine >> 24),
				(byte)(Machine >> 16),
				(byte)(Machine >> 8),
				(byte)Machine,
				(byte)(inc >> 24),
				(byte)(inc >> 16),
				(byte)(inc >> 8),
				(byte)inc
			};
		}
	}

	/// <summary>
	/// 获取一个objectId用下划线分割
	/// </summary>
	/// <returns>ObjectId字符串</returns>
	public static string Next()
	{
		return Next(false);
	}

	/// <summary>
	/// 获取一个objectId
	/// </summary>
	/// <param name="withHyphen">是否包含分隔符</param>
	/// <returns>ObjectId字符串</returns>
	public static string Next(bool withHyphen)
	{
		var array = NextBytes();
		var buf = new StringBuilder(withHyphen ? 26 : 24);
		for (var i = 0; i < array.Length; i++)
		{
			if (withHyphen && i > 0 && i % 4 == 0)
			{
				buf.Append('-');
			}
			var t = array[i] & 0xff;
			if (t < 16)
				buf.Append('0');
			buf.Append(t.ToString("x"));
		}
		return buf.ToString();
	}

	/// <summary>
	/// 获取机器码片段
	/// </summary>
	/// <returns>机器码片段</returns>
	private static int GetMachinePiece()
	{
		var netSb = new StringBuilder();
		try
		{
			var interfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (var ni in interfaces)
			{
				netSb.Append(ni.ToString());
			}
			return netSb.ToString().GetHashCode() << 16;
		}
		catch
		{
			return Random.Next() << 16;
		}
	}

	/// <summary>
	/// 获取进程码片段
	/// </summary>
	/// <returns>进程码片段</returns>
	private static int GetProcessPiece()
	{
		var processId = Process.GetCurrentProcess().Id;
		if (processId < 0)
			processId = Random.Next();

		var loaderId = RuntimeHelpers.GetHashCode(null);

		var processSb = $"{processId:x}{loaderId:x}";
		return processSb.GetHashCode() & 0xFFFF;
	}
}
