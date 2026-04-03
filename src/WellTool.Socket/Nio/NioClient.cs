using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WellTool.Socket.Nio;

/// <summary>
/// NIO客户端
/// </summary>
public class NioClient : IDisposable
{
	private readonly ILogger<NioClient>? _logger;
	private System.Net.Sockets.Socket? _channel;
	private ChannelHandlerDelegate? _handler;
	private readonly CancellationTokenSource _cts = new();

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="host">服务器地址</param>
	/// <param name="port">端口</param>
	public NioClient(string host, int port) : this(new IPEndPoint(IPAddress.Parse(host), port), null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="address">服务器地址</param>
	public NioClient(IPEndPoint address) : this(address, null)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="address">服务器地址</param>
	/// <param name="logger">日志</param>
	public NioClient(IPEndPoint address, ILogger<NioClient>? logger)
	{
		_logger = logger;
		Init(address);
	}

	/// <summary>
	/// 初始化
	/// </summary>
	/// <param name="address">地址和端口</param>
	/// <returns>this</returns>
	public NioClient Init(IPEndPoint address)
	{
		try
		{
			_channel = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_channel.Blocking = false;
			
			try
			{
				_channel.Connect(address);
			}
			catch (SocketException e) when (e.SocketErrorCode == SocketError.WouldBlock || e.SocketErrorCode == SocketError.InProgress)
			{
				// 非阻塞套接字，连接正在进行中
				while (!_channel.Connected)
				{
					Thread.Sleep(10);
				}
			}
		}
		catch (SocketException e)
		{
			Close();
			throw new SocketRuntimeException(e);
		}
		return this;
	}

	/// <summary>
	/// 设置NIO数据处理器
	/// </summary>
	/// <param name="handler">ChannelHandlerDelegate</param>
	/// <returns>this</returns>
	public NioClient SetChannelHandler(ChannelHandlerDelegate handler)
	{
		_handler = handler;
		return this;
	}

	/// <summary>
	/// 开始监听
	/// </summary>
	public void Listen()
	{
		Task.Run(() =>
		{
			var buffer = new byte[8192];
			while (!_cts.Token.IsCancellationRequested)
			{
				try
				{
					if (_channel != null && _channel.Connected)
					{
						if (_channel.Available > 0)
						{
							var length = _channel.Receive(buffer);
							if (length > 0)
							{
								_handler?.Invoke(_channel);
							}
						}
						else
						{
							Thread.Sleep(10);
						}
					}
				}
				catch (SocketException)
				{
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
	/// 写数据到服务端
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>this</returns>
	public NioClient Write(params byte[][] data)
	{
		try
		{
			foreach (var buffer in data)
			{
				_channel?.Send(buffer);
			}
		}
		catch (SocketException e)
		{
			throw new SocketRuntimeException(e);
		}
		return this;
	}

	/// <summary>
	/// 获取SocketChannel
	/// </summary>
	public System.Net.Sockets.Socket? GetChannel() => _channel;

	/// <summary>
	/// 关闭客户端
	/// </summary>
	public void Close()
	{
		_cts.Cancel();
		_channel?.Close();
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Close();
		_cts.Dispose();
	}
}