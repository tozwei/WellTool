using System;
using System.Net.Sockets;

namespace WellTool.Socket.Aio;

/// <summary>
/// Socket流处理接口
/// </summary>
/// <typeparam name="T">经过解码器解码后的数据类型</typeparam>
public interface IIoAction<T>
{
	/// <summary>
	/// 接收客户端连接（会话建立）事件处理
	/// </summary>
	/// <param name="session">会话</param>
	void Accept(AioSession session);

	/// <summary>
	/// 执行数据处理（消息读取）
	/// </summary>
	/// <param name="session">Socket Session会话</param>
	/// <param name="data">解码后的数据</param>
	void DoAction(AioSession session, T data);

	/// <summary>
	/// 数据读取失败的回调事件处理（消息读取失败）
	/// </summary>
	/// <param name="exc">异常</param>
	/// <param name="session">Session</param>
	void Failed(Exception exc, AioSession session);
}