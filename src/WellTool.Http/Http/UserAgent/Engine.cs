namespace WellTool.Http.UserAgent;

/// <summary>
/// 渲染引擎信息
/// </summary>
public class Engine
{
    /// <summary>
    /// 引擎名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 引擎版本
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 转换为字符串
    /// </summary>
    /// <returns>引擎名称</returns>
    public override string ToString() => Name;
}
