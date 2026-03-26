using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Socket.Aio;

/// <summary>
/// AIO会话
/// 每个客户端对应一个会话对象
/// </summary>
public class AioSession : IDisposable
{
	private readonly System.Net.Sockets.Socket _channel;
	private readonly IIoAction<byte[]> _ioAction;
	private readonly byte[] _readBuffer;
	private readonly byte[] _writeBuffer;
	private readonly long _readTimeout;
	private readonly long _writeTimeout;

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="channel">Socket</param>
	/// <param name="ioAction">IO消息处理类</param>
	/// <param name="config">配置项</param>
	public AioSession(System.Net.Sockets.Socket channel, IIoAction<byte[]> ioAction, SocketConfig config)
	{
		_channel = channel;
		_ioAction = ioAction;
		_readBuffer = new byte[config.ReadBufferSize];
		_writeBuffer = new byte[config.WriteBufferSize];
		_readTimeout = config.ReadTimeout;
		_writeTimeout = config.WriteTimeout;
	}

	/// <summary>
	/// 获取Socket
	/// </summary>
	public System.Net.Sockets.Socket GetChannel() => _channel;

	/// <summary>
	/// 获取读取Buffer
	/// </summary>
	public byte[] GetReadBuffer() => _readBuffer;

	/// <summary>
	/// 获取写Buffer
	/// </summary>
	public byte[] GetWriteBuffer() => _writeBuffer;

	/// <summary>
	/// 获取消息处理器
	/// </summary>
	public IIoAction<byte[]> GetIoAction() => _ioAction;

	/// <summary>
	/// 获取远程主机（客户端）地址和端口
	/// </summary>
	public EndPoint? GetRemoteAddress()
	{
		try
		{
			return _channel.RemoteEndPoint;
		}
		catch
		{
			return null;
		}
	}

	/// <summary>
	/// 读取数据
	/// </summary>
	/// <returns>this</returns>
	public AioSession Read()
	{
		if (IsOpen())
		{
			try
			{
				Task.Run(() =>
				{
					while (IsOpen())
					{
						if (_channel.Available > 0)
						{
							var length = _channel.Receive(_readBuffer);
							if (length > 0)
							{
								var data = new byte[length];
								Array.Copy(_readBuffer, data, length);
								_ioAction.DoAction(this, data);
							}
						}
						Thread.Sleep(10);
					}
				});
			}
			catch (Exception ex)
			{
				_ioAction.Failed(ex, this);
			}
		}
		return this;
	}

	/// <summary>
	/// 写数据到目标端
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>写入的字节数</returns>
	public int Write(byte[] data)
	{
		return _channel.Send(data);
	}

	/// <summary>
	/// 写数据到目标端，并关闭输出
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>this</returns>
	public AioSession WriteAndClose(byte[] data)
	{
		Write(data);
		return CloseOut();
	}

	/// <summary>
	/// 会话是否打开状态
	/// </summary>
	public bool IsOpen() => _channel != null && _channel.Connected;

	/// <summary>
	/// 关闭输入
	/// </summary>
	/// <returns>this</returns>
	public AioSession CloseIn()
	{
		if (_channel != null)
		{
			try
			{
				_channel.Shutdown(SocketShutdown.Receive);
			}
			catch (SocketException e)
			{
				throw new SocketRuntimeException(e);
			}
		}
		return this;
	}

	/// <summary>
	/// 关闭输出
	/// </summary>
	/// <returns>this</returns>
	public AioSession CloseOut()
	{
		if (_channel != null)
		{
			try
			{
				_channel.Shutdown(SocketShutdown.Send);
			}
			catch (SocketException e)
			{
				throw new SocketRuntimeException(e);
			}
		}
		return this;
	}

	/// <summary>
	/// 关闭会话
	/// </summary>
	public void Close()
	{
		_channel.Close();
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Close();
	}
}