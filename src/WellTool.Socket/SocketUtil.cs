using System.Net;
using System.Net.Sockets;
using WellTool.Socket.Aio;
using WellTool.Socket.Nio;

namespace WellTool.Socket;

/// <summary>
/// Socket工具类
/// </summary>
public static class SocketUtil
{
	/// <summary>
	/// 创建 AIO 服务端
	/// </summary>
	/// <returns>AIO 服务端</returns>
	public static Aio.AioServer CreateServer()
	{
		return new Aio.AioServer(0);
	}

	/// <summary>
	/// 创建 AIO 服务端
	/// </summary>
	/// <param name="port">端口</param>
	/// <returns>AIO 服务端</returns>
	public static Aio.AioServer CreateServer(int port)
	{
		return new Aio.AioServer(port);
	}

	/// <summary>
	/// 创建 AIO 客户端
	/// </summary>
	/// <param name="host">主机地址</param>
	/// <param name="port">端口</param>
	/// <returns>AIO 客户端</returns>
	public static Aio.AioClient CreateClient(string host, int port)
	{
		return new Aio.AioClient(host, port);
	}

	/// <summary>
	/// 创建 NIO 客户端
	/// </summary>
	/// <param name="host">主机地址</param>
	/// <param name="port">端口</param>
	/// <returns>NIO 客户端</returns>
	public static NioClient CreateNioClient(string host, int port)
	{
		return new NioClient(host, port);
	}

	/// <summary>
	/// 创建 NIO 服务端
	/// </summary>
	/// <param name="port">端口</param>
	/// <returns>NIO 服务端</returns>
	public static NioServer CreateNioServer(int port)
	{
		return new NioServer(port);
	}

	/// <summary>
	/// 获取远程端的地址信息，包括host和端口
	/// </summary>
	/// <param name="socket">Socket</param>
	/// <returns>远程端的地址信息</returns>
	public static EndPoint? GetRemoteAddress(System.Net.Sockets.Socket? socket)
	{
		if (socket == null) return null;
		try
		{
			return socket.RemoteEndPoint;
		}
		catch (SocketException)
		{
			return null;
		}
	}

	/// <summary>
	/// 远程主机是否处于连接状态
	/// </summary>
	/// <param name="socket">Socket</param>
	/// <returns>远程主机是否处于连接状态</returns>
	public static bool IsConnected(System.Net.Sockets.Socket? socket)
	{
		return socket != null && socket.Connected;
	}

	/// <summary>
	/// 创建Socket并连接到指定地址的服务器
	/// </summary>
	/// <param name="hostname">地址</param>
	/// <param name="port">端口</param>
	/// <returns>Socket</returns>
	public static System.Net.Sockets.Socket Connect(string hostname, int port)
	{
		return Connect(hostname, port, -1);
	}

	/// <summary>
	/// 创建Socket并连接到指定地址的服务器
	/// </summary>
	/// <param name="hostname">地址</param>
	/// <param name="port">端口</param>
	/// <param name="connectionTimeout">连接超时（毫秒）</param>
	/// <returns>Socket</returns>
	public static System.Net.Sockets.Socket Connect(string hostname, int port, int connectionTimeout)
	{
		return Connect(new IPEndPoint(IPAddress.Parse(hostname), port), connectionTimeout);
	}

	/// <summary>
	/// 创建Socket并连接到指定地址的服务器
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="connectionTimeout">连接超时（毫秒）</param>
	/// <returns>Socket</returns>
	public static System.Net.Sockets.Socket Connect(IPEndPoint address, int connectionTimeout)
	{
		var socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		try
		{
			if (connectionTimeout <= 0)
			{
				socket.Connect(address);
			}
			else
			{
				var result = socket.BeginConnect(address, null, null);
				if (!result.AsyncWaitHandle.WaitOne(connectionTimeout))
				{
					socket.Close();
					throw new SocketRuntimeException("Connection timeout");
				}
				socket.EndConnect(result);
			}
		}
		catch (SocketException e)
		{
			throw new SocketRuntimeException(e);
		}
		return socket;
	}
}