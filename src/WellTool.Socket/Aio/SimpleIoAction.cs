using Microsoft.Extensions.Logging;

namespace WellTool.Socket.Aio;

/// <summary>
/// 简易IO信息处理类
/// 简单实现了Accept和Failed事件
/// </summary>
public abstract class SimpleIoAction : IIoAction<byte[]>
{
	private static readonly ILogger? Logger;

	static SimpleIoAction()
	{
		Logger = null;
	}

	/// <inheritdoc/>
	public virtual void Accept(AioSession session)
	{
	}

	/// <inheritdoc/>
	public virtual void Failed(Exception exc, AioSession session)
	{
		Logger?.LogError(exc, "AIO operation failed");
	}

	/// <inheritdoc/>
	public abstract void DoAction(AioSession session, byte[] data);
}