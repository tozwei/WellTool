using WellTool.Socket.Aio;

namespace WellTool.Socket.Protocol;

/// <summary>
/// 消息编码器
/// </summary>
/// <typeparam name="T">编码前后的数据类型</typeparam>
public interface IMsgEncoder<T>
{
	/// <summary>
	/// 编码数据用于写出
	/// </summary>
	/// <param name="session">本次需要解码的session</param>
	/// <param name="writeBuffer">待处理的写buffer</param>
	/// <param name="data">写出的数据</param>
	void Encode(AioSession session, byte[] writeBuffer, T data);
}