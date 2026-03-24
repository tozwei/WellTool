using System.Text;

namespace WellTool.Http.Body;

/// <summary>
/// 字节数组形式的请求体
/// </summary>
public class BytesBody : IRequestBody
{
    private readonly byte[] _bytes;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="bytes">字节数组</param>
    public BytesBody(byte[] bytes)
    {
        _bytes = bytes ?? Array.Empty<byte>();
    }

    /// <summary>
    /// 获取字节数组
    /// </summary>
    /// <returns>字节数组</returns>
    public byte[] GetBytes() => _bytes;

    /// <summary>
    /// 写出数据
    /// </summary>
    /// <param name="outStream">输出流</param>
    public virtual void Write(Stream outStream)
    {
        outStream.Write(_bytes, 0, _bytes.Length);
    }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        return Encoding.UTF8.GetString(_bytes);
    }
}
