using WellTool.Socket.Aio;

namespace WellTool.Socket.Protocol;

/// <summary>
/// 消息解码器
/// </summary>
/// <typeparam name="T">解码后的目标类型</typeparam>
public interface IMsgDecoder<T>
{
	/// <summary>
	/// 对于从Socket流中获取到的数据采用当前MsgDecoder的实现类协议进行解析。
	/// </summary>
	/// <param name="session">本次需要解码的session</param>
	/// <param name="readBuffer">待处理的读buffer</param>
	/// <returns>本次解码成功后封装的业务消息对象，返回null则表示解码未完成</returns>
	T? Decode(AioSession session, byte[] readBuffer);
}