namespace WellTool.Http;

/// <summary>
/// HTTP 异常
/// </summary>
public class HttpException : Exception
{
    /// <summary>
    /// 构造
    /// </summary>
    public HttpException()
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="message">错误消息</param>
    public HttpException(string? message) : base(message)
    {
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public HttpException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
