using System.Text;
using System.Runtime.InteropServices;

namespace WellTool.System;

/// <summary>
/// 操作系统信息（对应Java的OsInfo）
/// </summary>
public class OsInfo
{
    private static readonly Lazy<OsInfo> _instance = new(() => new OsInfo());
    public static OsInfo Instance => _instance.Value;

    private readonly string _osVersion;
    private readonly string _osArch;
    private readonly string _osName;
    private readonly bool _isOsAix;
    private readonly bool _isOsHpUx;
    private readonly bool _isOsIrix;
    private readonly bool _isOsLinux;
    private readonly bool _isOsMac;
    private readonly bool _isOsMacOsx;
    private readonly bool _isOsOs2;
    private readonly bool _isOsSolaris;
    private readonly bool _isOsSunOs;
    private readonly bool _isOsWindows;
    private readonly bool _isOsWindows2000;
    private readonly bool _isOsWindows95;
    private readonly bool _isOsWindows98;
    private readonly bool _isOsWindowsMe;
    private readonly bool _isOsWindowsNt;
    private readonly bool _isOsWindowsXp;
    private readonly bool _isOsWindows7;
    private readonly bool _isOsWindows8;
    private readonly bool _isOsWindows8_1;
    private readonly bool _isOsWindows10;
    private readonly bool _isOsWindows11;

    private readonly string _fileSeparator;
    private readonly string _lineSeparator;
    private readonly string _pathSeparator;

    private OsInfo()
    {
        _osVersion = Environment.OSVersion.VersionString;
        _osArch = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE") ?? RuntimeInformation.OSArchitecture.ToString();
        _osName = RuntimeInformation.OSDescription;

        _isOsAix = GetOSMatches("AIX");
        _isOsHpUx = GetOSMatches("HP-UX");
        _isOsIrix = GetOSMatches("Irix");
        _isOsLinux = GetOSMatches("Linux") || GetOSMatches("LINUX");
        _isOsMac = GetOSMatches("Mac");
        _isOsMacOsx = GetOSMatches("Mac OS X");
        _isOsOs2 = GetOSMatches("OS/2");
        _isOsSolaris = GetOSMatches("Solaris");
        _isOsSunOs = GetOSMatches("SunOS");
        _isOsWindows = GetOSMatches("Windows");
        _isOsWindows2000 = GetOSMatches("Windows", "5.0");
        _isOsWindows95 = GetOSMatches("Windows 9", "4.0");
        _isOsWindows98 = GetOSMatches("Windows 9", "4.1");
        _isOsWindowsMe = GetOSMatches("Windows", "4.9");
        _isOsWindowsNt = GetOSMatches("Windows NT");
        _isOsWindowsXp = GetOSMatches("Windows", "5.1");
        _isOsWindows7 = GetOSMatches("Windows", "6.1");
        _isOsWindows8 = GetOSMatches("Windows", "6.2");
        _isOsWindows8_1 = GetOSMatches("Windows", "6.3");
        _isOsWindows10 = GetOSMatches("Windows", "10.0");
        _isOsWindows11 = GetOSMatches("Windows 11");

        _fileSeparator = Path.DirectorySeparatorChar.ToString();
        _lineSeparator = Environment.NewLine;
        _pathSeparator = Path.PathSeparator.ToString();
    }

    /// <summary>
    /// 取得当前OS的架构
    /// </summary>
    /// <returns>OS架构</returns>
    public string Arch => _osArch;

    /// <summary>
    /// 取得当前OS的名称
    /// </summary>
    /// <returns>OS名称</returns>
    public string Name => _osName;

    /// <summary>
    /// 取得当前OS的版本
    /// </summary>
    /// <returns>OS版本</returns>
    public string Version => _osVersion;

    /// <summary>
    /// 判断当前OS的类型是否为AIX
    /// </summary>
    public bool IsAix => _isOsAix;

    /// <summary>
    /// 判断当前OS的类型是否为HP-UX
    /// </summary>
    public bool IsHpUx => _isOsHpUx;

    /// <summary>
    /// 判断当前OS的类型是否为IRIX
    /// </summary>
    public bool IsIrix => _isOsIrix;

    /// <summary>
    /// 判断当前OS的类型是否为Linux
    /// </summary>
    public bool IsLinux => _isOsLinux;

    /// <summary>
    /// 判断当前OS的类型是否为Mac
    /// </summary>
    public bool IsMac => _isOsMac;

    /// <summary>
    /// 判断当前OS的类型是否为MacOS X
    /// </summary>
    public bool IsMacOsx => _isOsMacOsx;

    /// <summary>
    /// 判断当前OS的类型是否为OS2
    /// </summary>
    public bool IsOs2 => _isOsOs2;

    /// <summary>
    /// 判断当前OS的类型是否为Solaris
    /// </summary>
    public bool IsSolaris => _isOsSolaris;

    /// <summary>
    /// 判断当前OS的类型是否为Sun OS
    /// </summary>
    public bool IsSunOS => _isOsSunOs;

    /// <summary>
    /// 判断当前OS的类型是否为Windows
    /// </summary>
    public bool IsWindows => _isOsWindows;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 2000
    /// </summary>
    public bool IsWindows2000 => _isOsWindows2000;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 95
    /// </summary>
    public bool IsWindows95 => _isOsWindows95;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 98
    /// </summary>
    public bool IsWindows98 => _isOsWindows98;

    /// <summary>
    /// 判断当前OS的类型是否为Windows ME
    /// </summary>
    public bool IsWindowsMe => _isOsWindowsMe;

    /// <summary>
    /// 判断当前OS的类型是否为Windows NT
    /// </summary>
    public bool IsWindowsNt => _isOsWindowsNt;

    /// <summary>
    /// 判断当前OS的类型是否为Windows XP
    /// </summary>
    public bool IsWindowsXp => _isOsWindowsXp;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 7
    /// </summary>
    public bool IsWindows7 => _isOsWindows7;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 8
    /// </summary>
    public bool IsWindows8 => _isOsWindows8;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 8.1
    /// </summary>
    public bool IsWindows8_1 => _isOsWindows8_1;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 10
    /// </summary>
    public bool IsWindows10 => _isOsWindows10 && !_isOsWindows11;

    /// <summary>
    /// 判断当前OS的类型是否为Windows 11
    /// </summary>
    public bool IsWindows11 => _isOsWindows11;

    /// <summary>
    /// 取得OS的文件路径的分隔符
    /// </summary>
    public string FileSeparator => _fileSeparator;

    /// <summary>
    /// 取得OS的文本文件换行符
    /// </summary>
    public string LineSeparator => _lineSeparator;

    /// <summary>
    /// 取得OS的搜索路径分隔符
    /// </summary>
    public string PathSeparator => _pathSeparator;

    private bool GetOSMatches(string osNamePrefix)
    {
        if (string.IsNullOrEmpty(_osName))
        {
            return false;
        }

        return _osName.StartsWith(osNamePrefix, StringComparison.OrdinalIgnoreCase);
    }

    private bool GetOSMatches(string osNamePrefix, string osVersionPrefix)
    {
        if (string.IsNullOrEmpty(_osName) || string.IsNullOrEmpty(_osVersion))
        {
            return false;
        }

        return _osName.StartsWith(osNamePrefix, StringComparison.OrdinalIgnoreCase) &&
               _osVersion.StartsWith(osVersionPrefix, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// 将OS的信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, "OS Arch:        ", Arch);
        SystemUtil.Append(builder, "OS Name:        ", Name);
        SystemUtil.Append(builder, "OS Version:     ", Version);
        SystemUtil.Append(builder, "File Separator: ", FileSeparator);
        SystemUtil.Append(builder, "Line Separator: ", LineSeparator.Replace("\r", "\\r").Replace("\n", "\\n"));
        SystemUtil.Append(builder, "Path Separator: ", PathSeparator);

        return builder.ToString();
    }
}