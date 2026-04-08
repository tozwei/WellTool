using System;
using System.Net;
using System.Net.Sockets;

namespace WellTool.Socket.Aio;

/// <summary>
/// AIO Socket客户端
/// </summary>
public class AioClient : IDisposable
{
	private readonly AioSession _session;

	/// <summary>
	/// 默认的 IO Action（空实现）
	/// </summary>
	private static readonly IIoAction<byte[]> DefaultIoAction = new DefaultSimpleIoAction();

	/// <summary>
	/// 私有默认实现类
	/// </summary>
	private class DefaultSimpleIoAction : SimpleIoAction
	{
		public override void DoAction(AioSession session, byte[] data)
		{
			// 空实现
		}
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="ioAction">IO处理类</param>
	public AioClient(IPEndPoint address, IIoAction<byte[]> ioAction)
		: this(address, ioAction, new SocketConfig())
	{
	}

	/// <summary>
	/// 构造（使用默认 IO Action）
	/// </summary>
	/// <param name="host">主机地址</param>
	/// <param name="port">端口</param>
	public AioClient(string host, int port)
		: this(new IPEndPoint(IPAddress.Parse(host), port), DefaultIoAction, new SocketConfig())
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="ioAction">IO处理类</param>
	/// <param name="config">配置项</param>
	public AioClient(IPEndPoint address, IIoAction<byte[]> ioAction, SocketConfig config)
	{
		var socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		// 延迟连接，在需要时才连接
		_session = new AioSession(socket, ioAction, config);
	}

	/// <summary>
	/// 设置Socket的Option选项
	/// </summary>
	/// <param name="optionLevel">选项级别</param>
	/// <param name="optionName">选项名</param>
	/// <param name="optionValue">选项值</param>
	/// <returns>this</returns>
	public AioClient SetOption(SocketOptionLevel optionLevel, SocketOptionName optionName, object optionValue)
	{
		_session.GetChannel().SetSocketOption(optionLevel, optionName, optionValue);
		return this;
	}

	/// <summary>
	/// 获取IO处理器
	/// </summary>
	public IIoAction<byte[]> GetIoAction() => _session.GetIoAction();

	/// <summary>
	/// 从服务端读取数据
	/// </summary>
	/// <returns>this</returns>
	public AioClient Read()
	{
		_session.Read();
		return this;
	}

	/// <summary>
	/// 写数据到服务端
	/// </summary>
	/// <param name="data">数据</param>
	/// <returns>this</returns>
	public AioClient Write(byte[] data)
	{
		_session.Write(data);
		return this;
	}

	/// <summary>
	/// 关闭客户端
	/// </summary>
	public void Close()
	{
		_session.Close();
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		Close();
	}
}