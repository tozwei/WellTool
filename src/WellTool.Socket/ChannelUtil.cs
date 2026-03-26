using System.Net;
using System.Net.Sockets;
using System.Collections.Concurrent;

namespace WellTool.Socket;

/// <summary>
/// Channel相关封装
/// </summary>
public static class ChannelUtil
{
	private static int _defaultPoolSize = Environment.ProcessorCount;

	/// <summary>
	/// 创建并连接到指定地址
	/// </summary>
	/// <param name="address">地址信息，包括地址和端口</param>
	/// <returns>Socket</returns>
	public static System.Net.Sockets.Socket Connect(IPEndPoint address)
	{
		return Connect(address, _defaultPoolSize);
	}

	/// <summary>
	/// 创建并连接到指定地址
	/// </summary>
	/// <param name="address">地址信息，包括地址和端口</param>
	/// <param name="poolSize">线程池大小</param>
	/// <returns>Socket</returns>
	public static System.Net.Sockets.Socket Connect(IPEndPoint address, int poolSize)
	{
		var socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			socket.Connect(address);
		}
		catch (SocketException e)
		{
			socket.Close();
			throw new SocketRuntimeException(e);
		}
		return socket;
	}

	/// <summary>
	/// 设置默认线程池大小
	/// </summary>
	/// <param name="poolSize">线程池大小</param>
	public static void SetDefaultPoolSize(int poolSize)
	{
		_defaultPoolSize = poolSize;
	}
}