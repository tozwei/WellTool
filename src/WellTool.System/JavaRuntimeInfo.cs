using System.Text;

namespace WellTool.System;

/// <summary>
/// .NET 运行时信息（对应Java的JavaRuntimeInfo）
/// </summary>
public class JavaRuntimeInfo
{
    private static readonly Lazy<JavaRuntimeInfo> _instance = new(() => new JavaRuntimeInfo());
    public static JavaRuntimeInfo Instance => _instance.Value;

    private readonly string _runtimeName;
    private readonly string _runtimeVersion;
    private readonly string _homeDir;
    private readonly string? _classPath;
    private readonly string _libraryPath;

    private JavaRuntimeInfo()
    {
        _runtimeName = RuntimeInformation.FrameworkDescription;
        _runtimeVersion = Environment.Version.ToString();
        _homeDir = RuntimeEnvironment.GetRuntimeDirectory();
        _classPath = AppContext.GetData("APP_CONTEXT_BASE_DIRECTORY") as string ?? AppDomain.CurrentDomain.BaseDirectory;
        _libraryPath = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
    }

    /// <summary>
    /// JVM is 32M or 64M
    /// </summary>
    /// <returns>32 or 64</returns>
    public string SunArchDataModel => Environment.Is64BitProcess ? "64" : "32";

    /// <summary>
    /// 取得当前JRE的名称
    /// </summary>
    /// <returns>运行时名称</returns>
    public string Name => _runtimeName;

    /// <summary>
    /// 取得当前JRE的版本
    /// </summary>
    /// <returns>运行时版本</returns>
    public string Version => _runtimeVersion;

    /// <summary>
    /// 取得当前JRE的安装目录
    /// </summary>
    /// <returns>安装目录</returns>
    public string HomeDir => _homeDir;

    /// <summary>
    /// 取得当前JRE的classpath
    /// </summary>
    /// <returns>classpath</returns>
    public string ClassPath => _classPath;

    /// <summary>
    /// 取得当前JRE的classpath（数组形式）
    /// </summary>
    /// <returns>classpath数组</returns>
    public string[] GetClassPathArray()
    {
        return ClassPath?.Split(Path.PathSeparator) ?? Array.Empty<string>();
    }

    /// <summary>
    /// 取得当前JRE的library搜索路径
    /// </summary>
    /// <returns>library路径</returns>
    public string LibraryPath => _libraryPath;

    /// <summary>
    /// 取得当前JRE的library搜索路径（数组形式）
    /// </summary>
    /// <returns>library路径数组</returns>
    public string[] GetLibraryPathArray()
    {
        return LibraryPath?.Split(Path.PathSeparator) ?? Array.Empty<string>();
    }

    /// <summary>
    /// 将当前运行的JRE信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, "Java Runtime Name:      ", Name);
        SystemUtil.Append(builder, "Java Runtime Version:   ", Version);
        SystemUtil.Append(builder, "Java Home Dir:          ", HomeDir);
        SystemUtil.Append(builder, "Java Class Path:        ", ClassPath);
        SystemUtil.Append(builder, "Java Library Path:      ", LibraryPath);

        return builder.ToString();
    }
}