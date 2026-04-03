namespace WellTool.Core.lang.net;

using System;
using System.Net;
using System.Net.NetworkInformation;

/// <summary>
/// 本地端口生成器
/// </summary>
public class LocalPortGenerater
{
	private static readonly Random Random = new Random();
	private const int MinPort = 1024;
	private const int MaxPort = 65535;

	/// <summary>
	/// 生成一个可用的本地端口
	/// </summary>
	/// <returns>端口号</returns>
	public static int Generate()
	{
		for (int i = 0; i < 100; i++)
		{
			var port = Random.Next(MinPort, MaxPort + 1);
			if (IsPortAvailable(port))
				return port;
		}
		throw new InvalidOperationException("Unable to find an available port");
	}

	/// <summary>
	/// 检查端口是否可用
	/// </summary>
	/// <param name="port">端口号</param>
	/// <returns>是否可用</returns>
	public static bool IsPortAvailable(int port)
	{
		try
		{
			var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			var tcpListeners = ipGlobalProperties.GetActiveTcpListeners();
			var tcpConnections = ipGlobalProperties.GetActiveTcpConnectionInformation();

			foreach (var listener in tcpListeners)
			{
				if (listener.Port == port)
					return false;
			}

			foreach (var conn in tcpConnections)
			{
				if (conn.LocalEndPoint.Port == port)
					return false;
			}

			return true;
		}
		catch
		{
			return true;
		}
	}

	/// <summary>
	/// 获取已使用的端口列表
	/// </summary>
	/// <returns>端口列表</returns>
	public static int[] GetUsedPorts()
	{
		var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
		var listeners = ipGlobalProperties.GetActiveTcpListeners();
		var ports = new int[listeners.Length];
		for (int i = 0; i < listeners.Length; i++)
			ports[i] = listeners[i].Port;
		return ports;
	}
}
