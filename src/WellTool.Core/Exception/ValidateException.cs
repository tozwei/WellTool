using System;

namespace WellDone.Core.Exception;

/// <summary>
/// ValidateException验证异常
/// </summary>
public class ValidateException : UtilException
{
	/// <summary>
	/// 构造
	/// </summary>
	public ValidateException()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	public ValidateException(string message) : base(message)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	/// <param name="innerException">内部异常</param>
	public ValidateException(string message, Exception innerException) : base(message, innerException)
	{
	}
}
