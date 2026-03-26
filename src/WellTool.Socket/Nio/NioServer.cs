using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;

namespace WellTool.Socket.Nio;

/// <summary>
/// 基于NIO的Socket服务端实现
/// </summary>
public class NioServer : IDisposable
{
	private readonly ILogger<NioServer>? _logger;
	private Socket? _serverSocket;
	private ChannelHandlerDelegate? _handler;
	private readonly CancellationTokenSource _cts = new();
	private bool _isRunning;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="port">端口</param>
	public NioServer(int port) : this(port, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="port">端口</param>
	/// <param name="logger">日志</param>
	public NioServer(int port, ILogger<NioServer>? logger)
	{
		_logger = logger;
		Init(new IPEndPoint(IPAddress.Any, port));
	}

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="address">地址和端口</param>
	/// <returns>this</returns>
	public NioServer Init(IPEndPoint address)
	{
		// 创建ServerSocketChannel
		_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		_serverSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
		_serverSocket.Bind(address);
		_serverSocket.Listen(128);

		_logger?.LogDebug("Server listen on: [{0}]...", address);

		return this;
	}

	/// <summary>
	/// 设置NIO数据处理器
	/// </summary>
	/// <param name="handler">ChannelHandlerDelegate</param>
	/// <returns>this</returns>
	public NioServer SetChannelHandler(ChannelHandlerDelegate handler)
	{
		_handler = handler;
		return this;
	}

	/// <summary>
	/// 启动NIO服务端，即开始监听
	/// </summary>
	public void Start()
	{
		if (_isRunning) return;
		_isRunning = true;
		Listen();
	}

	/// <summary>
	/// 开始监听
	/// </summary>
	public void Listen()
	{
		if (_serverSocket == null) throw new InvalidOperationException("Server not initialized");

		Task.Run(() =>
		{
			while (!_cts.Token.IsCancellationRequested)
			{
				try
				{
					DoListen();
				}
				catch (SocketException)
				{
					// Server closed
					break;
				}
				catch (Exception e)
				{
					_logger?.LogError(e, "Listen error");
				}
			}
		}, _cts.Token);
	}

	/// <summary>
	/// 开始监听
	/// </summary>
	private void DoListen()
	{
		var readList = new List<Socket> { _serverSocket! };

		while (!_cts.Token.IsCancellationRequested)
		{
			try
			{
				Socket.Select(readList, null, null, 1000);

				foreach (var socket in readList.ToArray())
				{
					if (socket == _serverSocket)
					{
						// 有客户端接入
						var clientSocket = _serverSocket!.Accept();
						_logger?.LogDebug("Client [{0}] accepted.", clientSocket.RemoteEndPoint);
						_handler?.Invoke(clientSocket);
					}
					else
					{
						// 读事件就绪
						try
						{
							_handler?.Invoke(socket);
						}
						catch (Exception e)
						{
							socket.Close();
							_logger?.LogError(e, "Handle error");
						}
					}
				}
			}
			catch (SocketException)
			{
				break;
			}
		}
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
		_isRunning = false;
		_serverSocket?.Close();
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Close();
		_cts.Dispose();
	}
}

/// <summary>
/// ChannelHandler 委托
/// </summary>
public delegate void ChannelHandlerDelegate(Socket socket);