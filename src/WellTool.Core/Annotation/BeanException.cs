namespace WellTool.Core.Bean;

/// <summary>
/// Bean异常
/// </summary>
public class BeanException : Exception
{
	/// <summary>
	/// 构造函数
	/// </summary>
	public BeanException() : base()
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="message">异常消息</param>
	public BeanException(string message) : base(message)
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="message">异常消息</param>
	/// <param name="innerException">内部异常</param>
	public BeanException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="format">格式化字符串</param>
	/// <param name="args">参数</param>
	public BeanException(string format, params object[] args) : base(string.Format(format, args))
	{
	}

	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="innerException">内部异常</param>
	/// <param name="format">格式化字符串</param>
	/// <param name="args">参数</param>
	public BeanException(Exception innerException, string format, params object[] args) : base(string.Format(format, args), innerException)
	{
	}
}
