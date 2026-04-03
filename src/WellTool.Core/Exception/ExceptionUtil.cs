using System;
using System.Text;

namespace WellDone.Core.Exception;

/// <summary>
/// 异常工具类
/// </summary>
public static class ExceptionUtil
{
	/// <summary>
	/// 获取当前异常及其所有内部异常的完整消息
	/// </summary>
	/// <param name="e">异常</param>
	/// <returns>完整消息</returns>
	public static string GetMessage(Exception e)
	{
		if (e == null)
			return null;

		var sb = new StringBuilder();
		var ex = e;
		while (ex != null)
		{
			if (sb.Length > 0)
				sb.Append("; ");
			sb.Append(ex.Message);
			ex = ex.InnerException;
		}
		return sb.ToString();
	}

	/// <summary>
	/// 获取当前异常的完整堆栈信息
	/// </summary>
	/// <param name="e">异常</param>
	/// <returns>堆栈信息</returns>
	public static string GetStackTrace(Exception e)
	{
		return e?.ToString();
	}

	/// <summary>
	/// 包装异常
	/// </summary>
	/// <param name="e">异常</param>
	/// <param name="message">新消息</param>
	/// <returns>包装后的异常</returns>
	public static Exception Wrap(Exception e, string message)
	{
		return new UtilException(message, e);
	}

	/// <summary>
	/// 判断异常是否包含指定类型的异常
	/// </summary>
	/// <typeparam name="T">异常类型</typeparam>
	/// <param name="e">异常</param>
	/// <returns>是否包含</returns>
	public static bool IsExceptionOr<E>(Exception e) where E : Exception
	{
		return e is E || (e?.InnerException != null && IsExceptionOr<E>(e.InnerException));
	}
}
