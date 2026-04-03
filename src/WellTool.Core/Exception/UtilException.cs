using System;

namespace WellDone.Core.Exception;

/// <summary>
/// UtilException工具异常
/// </summary>
public class UtilException : System.Exception
{
	/// <summary>
	/// 构造
	/// </summary>
	public UtilException()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	public UtilException(string message) : base(message)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	/// <param name="innerException">内部异常</param>
	public UtilException(string message, System.Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="innerException">内部异常</param>
	public UtilException(System.Exception innerException) : base(innerException.Message, innerException)
	{
	}
}
