using System;

namespace WellTool.Core.Exception;

/// <summary>
/// 状态异常
/// </summary>
public class StatefulException : UtilException
{
	/// <summary>
	/// 构造
	/// </summary>
	public StatefulException()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	public StatefulException(string message) : base(message)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	/// <param name="innerException">内部异常</param>
	public StatefulException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
