using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WellTool.Socket.Aio;

/// <summary>
/// 基于AIO的Socket服务端实现
/// </summary>
public class AioServer : IDisposable
{
	private readonly ILogger<AioServer>? _logger;
	private System.Net.Sockets.Socket? _serverSocket;
	protected IIoAction<byte[]>? IoAction;
	public readonly SocketConfig Config;
	private readonly CancellationTokenSource _cts = new();

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="port">端口</param>
	public AioServer(int port) : this(new IPEndPoint(IPAddress.Any, port), new SocketConfig(), null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="config">配置项</param>
	public AioServer(IPEndPoint address, SocketConfig config) : this(address, config, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="config">配置项</param>
	/// <param name="logger">日志</param>
	public AioServer(IPEndPoint address, SocketConfig config, ILogger<AioServer>? logger)
	{
		_logger = logger;
		Config = config;
		Init(address);
	}

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="address">地址和端口</param>
	/// <returns>this</returns>
	public AioServer Init(IPEndPoint address)
	{
		_serverSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		_serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
		_serverSocket.Bind(address);
		_serverSocket.Listen(128);

		_logger?.LogDebug("AIO Server started, waiting for accept.");

		return this;
	}

	/// <summary>
	/// 开始监听
	/// </summary>
	/// <param name="sync">是否阻塞</param>
	public void Start(bool sync)
	{
		DoStart(sync);
	}

	/// <summary>
	/// 获取IO处理器
	/// </summary>
	public IIoAction<byte[]>? GetIoAction() => IoAction;

	/// <summary>
	/// 设置IO处理器
	/// </summary>
	/// <param name="ioAction">IoAction</param>
	/// <returns>this</returns>
	public AioServer SetIoAction(IIoAction<byte[]> ioAction)
	{
		IoAction = ioAction;
		return this;
	}

	/// <summary>
	/// 获取ServerSocket
	/// </summary>
	public System.Net.Sockets.Socket? GetChannel() => _serverSocket;

	/// <summary>
	/// 处理接入的客户端
	/// </summary>
	/// <returns>this</returns>
	public AioServer Accept()
	{
		Task.Run(() =>
		{
			while (!_cts.Token.IsCancellationRequested && _serverSocket != null)
			{
				try
				{
					var clientSocket = _serverSocket.Accept();
					_logger?.LogDebug("Client [{0}] accepted.", clientSocket.RemoteEndPoint);

					// 创建Session会话
					var session = new AioSession(clientSocket, IoAction!, Config);

					// 处理请求接入（同步）
					IoAction?.Accept(session);

					// 处理读（异步）
					session.Read();
				}
				catch (SocketException)
				{
					// Server closed
					break;
				}
				catch (Exception ex)
				{
					_logger?.LogError(ex, "Accept error");
				}
			}
		}, _cts.Token);

		return this;
	}

	/// <summary>
	/// 服务是否开启状态
	/// </summary>
	public bool IsOpen() => _serverSocket?.IsBound ?? false;

	/// <summary>
	/// 关闭服务
	/// </summary>
	public void Close()
	{
		_cts.Cancel();
		_serverSocket?.Close();
	}

	/// <summary>
	/// 开始监听
	/// </summary>
	/// <param name="sync">是否阻塞</param>
	private void DoStart(bool sync)
	{
		_logger?.LogDebug("AIO Server started, waiting for accept.");

		// 接收客户端连接
		Accept();

		if (sync)
		{
			// 阻塞等待
			while (!_cts.Token.IsCancellationRequested)
			{
				Thread.Sleep(100);
			}
		}
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Close();
		_cts.Dispose();
	}
}