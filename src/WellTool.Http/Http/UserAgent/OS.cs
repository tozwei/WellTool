namespace WellTool.Http.UserAgent;

/// <summary>
/// 操作系统信息
/// </summary>
public class OS
{
    /// <summary>
    /// 系统名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 系统版本
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 系统厂商
    /// </summary>
    public string Vendor { get; set; } = string.Empty;

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>系统名称</returns>
    public override string ToString() => Name;
}
