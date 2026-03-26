namespace WellTool.Socket.Protocol;

/// <summary>
/// 协议接口
/// 通过实现此接口完成消息的编码和解码
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
public interface IProtocol<T> : IMsgEncoder<T>, IMsgDecoder<T>
{
}