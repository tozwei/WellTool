namespace WellTool.Http.Body;

/// <summary>
/// 定义请求体接口
/// </summary>
public interface IRequestBody
{
    /// <summary>
    /// 写出数据，不关闭流
    /// </summary>
    /// <param name="out">输出流</param>
    void Write(Stream outStream);

    /// <summary>
    /// 写出并关闭输出流
    /// </summary>
    /// <param name="outStream">输出流</param>
    void WriteClose(Stream outStream)
    {
        try
        {
            Write(outStream);
        }
        finally
        {
            outStream?.Dispose();
        }
    }
}
