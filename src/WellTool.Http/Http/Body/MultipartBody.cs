using System.Text;

namespace WellTool.Http.Body;

/// <summary>
/// Multipart/form-data 数据的请求体封装
/// 遵循 RFC2388 规范
/// </summary>
public class MultipartBody : IRequestBody
{
    private const string CONTENT_TYPE_MULTIPART_PREFIX = "multipart/form-data; boundary=";

    /// <summary>
    /// 存储表单数据
    /// </summary>
    private readonly Dictionary<string, object?> _form;

    /// <summary>
    /// 编码
    /// </summary>
    private readonly Encoding _charset;

    /// <summary>
    /// 边界
    /// </summary>
    private readonly string _boundary;

    /// <summary>
    /// 根据已有表单内容，构建 MultipartBody
    /// </summary>
    /// <param name="form">表单</param>
    /// <param name="charset">编码</param>
    /// <returns>MultipartBody</returns>
    public static MultipartBody Create(IDictionary<string, object?> form, Encoding? charset = null)
    {
        return new MultipartBody(form, charset);
    }

    /// <summary>
    /// 获取 Multipart 的 Content-Type 类型
    /// </summary>
    /// <returns>Multipart 的 Content-Type 类型</returns>
    public string ContentType => CONTENT_TYPE_MULTIPART_PREFIX + _boundary;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="form">表单</param>
    /// <param name="charset">编码</param>
    public MultipartBody(IDictionary<string, object?> form, Encoding? charset = null)
    {
        _form = new Dictionary<string, object?>(form);
        _charset = charset ?? Encoding.UTF8;
        _boundary = $"----WebKitFormBoundary{Guid.NewGuid():N}";
    }

    /// <summary>
    /// 写出 Multipart 数据
    /// </summary>
    /// <param name="outStream">输出流</param>
    public void Write(Stream outStream)
    {
#if NETSTANDARD2_1
                using var writer = new StreamWriter(outStream, _charset);
#else
        using var writer = new StreamWriter(outStream, _charset, leaveOpen: true);
#endif

        foreach (var kvp in _form)
        {
            writer.Write($"--{_boundary}\r\n");

            if (kvp.Value is FileInfo file)
            {
                // 文件上传
                writer.Write($"Content-Disposition: form-data; name=\"{kvp.Key}\"; filename=\"{file.Name}\"\r\n");
                writer.Write($"Content-Type: {GetMimeType(file.Extension)}\r\n\r\n");
                writer.Flush();

                // 写入文件内容
                using var fileStream = file.OpenRead();
                fileStream.CopyTo(outStream);
                writer.Write("\r\n");
            }
            else
            {
                // 普通字段
                writer.Write($"Content-Disposition: form-data; name=\"{kvp.Key}\"\r\n\r\n");
                writer.Write(kvp.Value?.ToString() ?? string.Empty);
                writer.Write("\r\n");
            }
        }

        writer.Write($"--{_boundary}--\r\n");
        writer.Flush();
    }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        using var ms = new MemoryStream();
        Write(ms);
        ms.Position = 0;
        using var reader = new StreamReader(ms, _charset);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// 根据文件扩展名获取 MIME 类型
    /// </summary>
    /// <param name="extension">扩展名</param>
    /// <returns>MIME 类型</returns>
    private static string GetMimeType(string extension)
    {
        return extension.ToLowerInvariant() switch
        {
            ".txt" => "text/plain",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            ".zip" => "application/zip",
            _ => "application/octet-stream"
        };
    }
}
