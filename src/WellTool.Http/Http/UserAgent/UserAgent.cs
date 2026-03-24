namespace WellTool.Http.UserAgent;

/// <summary>
/// User-Agent 信息对象
/// </summary>
public class UserAgent
{
    /// <summary>
    /// 是否为移动平台
    /// </summary>
    public bool Mobile { get; set; }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    public Browser? Browser { get; set; }

    /// <summary>
    /// 浏览器版本
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// 平台类型
    /// </summary>
    public Platform? Platform { get; set; }

    /// <summary>
    /// 系统类型
    /// </summary>
    public OS? OS { get; set; }

    /// <summary>
    /// 系统版本
    /// </summary>
    public string? OsVersion { get; set; }

    /// <summary>
    /// 引擎类型
    /// </summary>
    public Engine? Engine { get; set; }

    /// <summary>
    /// 引擎版本
    /// </summary>
    public string? EngineVersion { get; set; }

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>User-Agent 字符串</returns>
    public override string ToString()
    {
        return $"{Browser?.Name} {Version} / {OS?.Name} {OsVersion}";
    }
}
