using System;

namespace WellTool.Socket;

/// <summary>
/// Socket异常
/// </summary>
public class SocketRuntimeException : Exception
{
	public SocketRuntimeException(Exception e) : base(e.Message, e)
	{
	}

	public SocketRuntimeException(string message) : base(message)
	{
	}

	public SocketRuntimeException(string messageTemplate, params object[] args) : base(string.Format(messageTemplate, args))
	{
	}

	public SocketRuntimeException(string message, Exception throwable) : base(message, throwable)
	{
	}

	public SocketRuntimeException(Exception throwable, string messageTemplate, params object[] args)
		: base(string.Format(messageTemplate, args), throwable)
	{
	}
}