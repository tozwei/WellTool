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
	/// 构造
	/// </summary>
	/// <param name="address">地址</param>
	/// <param name="ioAction">IO处理类</param>
	public AioClient(IPEndPoint address, IIoAction<byte[]> ioAction)
		: this(address, ioAction, new SocketConfig())
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
		var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Connect(address);
		_session = new AioSession(socket, ioAction, config);
		ioAction.Accept(_session);
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