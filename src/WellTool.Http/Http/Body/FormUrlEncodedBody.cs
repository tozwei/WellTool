using System.Text;
using System.Web;

namespace WellTool.Http.Body;

/// <summary>
/// application/x-www-form-urlencoded 类型请求体封装
/// </summary>
public class FormUrlEncodedBody : BytesBody
{
    /// <summary>
    /// 创建 Http request body
    /// </summary>
    /// <param name="form">表单</param>
    /// <param name="charset">编码</param>
    /// <returns>FormUrlEncodedBody</returns>
    public static FormUrlEncodedBody Create(IDictionary<string, object?> form, Encoding? charset = null)
    {
        return new FormUrlEncodedBody(form, charset);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="form">表单</param>
    /// <param name="charset">编码</param>
    public FormUrlEncodedBody(IDictionary<string, object?> form, Encoding? charset = null)
        : base(ToBytes(form, charset))
    {
    }

    /// <summary>
    /// 转换为字节数组
    /// </summary>
    /// <param name="form">表单</param>
    /// <param name="charset">编码</param>
    /// <returns>字节数组</returns>
    private static byte[] ToBytes(IDictionary<string, object?> form, Encoding? charset)
    {
        if (form == null || form.Count == 0)
        {
            return Array.Empty<byte>();
        }

        var encoding = charset ?? Encoding.UTF8;
        var parts = new List<string>();

        foreach (var kvp in form)
        {
            if (string.IsNullOrEmpty(kvp.Key) || kvp.Value == null)
            {
                continue;
            }

            var encodedKey = HttpUtility.UrlEncode(kvp.Key, encoding);
            var encodedValue = HttpUtility.UrlEncode(kvp.Value.ToString(), encoding);
            parts.Add($"{encodedKey}={encodedValue}");
        }

        var queryString = string.Join("&", parts);
        return encoding.GetBytes(queryString);
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
        using var reader = new StreamReader(ms);
        return reader.ReadToEnd();
    }
}
