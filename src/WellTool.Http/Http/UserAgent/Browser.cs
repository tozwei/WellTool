namespace WellTool.Http.UserAgent;

/// <summary>
/// 浏览器信息
/// </summary>
public class Browser
{
    /// <summary>
    /// 浏览器名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器厂商
    /// </summary>
    public string Vendor { get; set; } = string.Empty;

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>浏览器名称</returns>
    public override string ToString() => Name;
}
