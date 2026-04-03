using System;

namespace WellTool.Core.Exception;

/// <summary>
/// 依赖异常
/// </summary>
public class DependencyException : UtilException
{
	/// <summary>
	/// 构造
	/// </summary>
	public DependencyException()
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	public DependencyException(string message) : base(message)
	{
	}

	/// <summary>
	/// 构造
	/// </summary>
	/// <param name="message">消息</param>
	/// <param name="innerException">内部异常</param>
	public DependencyException(string message, System.Exception innerException) : base(message, innerException)
	{
	}
}
