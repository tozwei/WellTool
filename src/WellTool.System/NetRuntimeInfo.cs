using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Runtime.InteropServices;

namespace WellTool.System;

/// <summary>
/// .NET 运行时信息
/// </summary>
public class NetRuntimeInfo
{
    private static readonly Lazy<NetRuntimeInfo> _instance = new(() => new NetRuntimeInfo());
    public static NetRuntimeInfo Instance => _instance.Value;

    private readonly string _runtimeName;
    private readonly string _runtimeVersion;
    private readonly string _homeDir;
    private readonly string? _baseDirectory;
    private readonly string _libraryPath;

    private NetRuntimeInfo()
    {
        _runtimeName = RuntimeInformation.FrameworkDescription;
        _runtimeVersion = Environment.Version.ToString();
#if NET6_0_OR_GREATER
        _homeDir = RuntimeEnvironment.GetRuntimeDirectory();
        _baseDirectory = AppContext.GetData("APP_CONTEXT_BASE_DIRECTORY") as string ?? AppDomain.CurrentDomain.BaseDirectory;
#else
        // 在.NET Standard 2.1中，我们使用替代方案
        _homeDir = AppDomain.CurrentDomain.BaseDirectory;
        _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
#endif
        _libraryPath = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
    }

    /// <summary>
    /// CLR is 32-bit or 64-bit
    /// </summary>
    /// <returns>32 or 64</returns>
    public string ArchDataModel => Environment.Is64BitProcess ? "64" : "32";

    /// <summary>
    /// 取得当前.NET运行时的名称
    /// </summary>
    /// <returns>运行时名称</returns>
    public string Name => _runtimeName;

    /// <summary>
    /// 取得当前.NET运行时的版本
    /// </summary>
    /// <returns>运行时版本</returns>
    public string Version => _runtimeVersion;

    /// <summary>
    /// 取得当前.NET运行时的安装目录
    /// </summary>
    /// <returns>安装目录</returns>
    public string HomeDir => _homeDir;

    /// <summary>
    /// 取得当前应用程序的基础目录
    /// </summary>
    /// <returns>基础目录</returns>
    public string BaseDirectory => _baseDirectory;

    /// <summary>
    /// 取得当前应用程序的基础目录（数组形式）
    /// </summary>
    /// <returns>基础目录数组</returns>
    public string[] GetBaseDirectoryArray()
    {
        return BaseDirectory?.Split(Path.PathSeparator) ?? Array.Empty<string>();
    }

    /// <summary>
    /// 取得当前系统的library搜索路径
    /// </summary>
    /// <returns>library路径</returns>
    public string LibraryPath => _libraryPath;

    /// <summary>
    /// 取得当前系统的library搜索路径（数组形式）
    /// </summary>
    /// <returns>library路径数组</returns>
    public string[] GetLibraryPathArray()
    {
        return LibraryPath?.Split(Path.PathSeparator) ?? Array.Empty<string>();
    }

    /// <summary>
    /// 将当前运行的.NET运行时信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, ".NET Runtime Name:      ", Name);
        SystemUtil.Append(builder, ".NET Runtime Version:   ", Version);
        SystemUtil.Append(builder, ".NET Home Dir:          ", HomeDir);
        SystemUtil.Append(builder, ".NET Base Directory:    ", BaseDirectory);
        SystemUtil.Append(builder, ".NET Library Path:      ", LibraryPath);

        return builder.ToString();
    }
}