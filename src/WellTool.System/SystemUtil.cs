using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System;

namespace WellTool.System;

/// <summary>
/// 系统工具类
/// </summary>
public static class SystemUtil
{
    // ----- Java运行时环境信息 -----/
    /// <summary>
    /// Java 运行时环境规范名称的KEY
    /// </summary>
    public const string SPECIFICATION_NAME = SystemPropsKeys.SPECIFICATION_NAME;

    /// <summary>
    /// Java 运行时环境版本的KEY
    /// </summary>
    public const string VERSION = SystemPropsKeys.VERSION;

    /// <summary>
    /// Java 运行时环境规范版本的KEY
    /// </summary>
    public const string SPECIFICATION_VERSION = SystemPropsKeys.SPECIFICATION_VERSION;

    /// <summary>
    /// Java 运行时环境供应商的KEY
    /// </summary>
    public const string VENDOR = SystemPropsKeys.VENDOR;

    /// <summary>
    /// Java 运行时环境规范供应商的KEY
    /// </summary>
    public const string SPECIFICATION_VENDOR = SystemPropsKeys.SPECIFICATION_VENDOR;

    /// <summary>
    /// Java 供应商的 URL的KEY
    /// </summary>
    public const string VENDOR_URL = SystemPropsKeys.VENDOR_URL;

    /// <summary>
    /// Java 安装目录的KEY
    /// </summary>
    public const string HOME = SystemPropsKeys.HOME;

    /// <summary>
    /// 加载库时搜索的路径列表的KEY
    /// </summary>
    public const string LIBRARY_PATH = SystemPropsKeys.LIBRARY_PATH;

    /// <summary>
    /// 默认的临时文件路径的KEY
    /// </summary>
    public const string TMPDIR = SystemPropsKeys.TMPDIR;

    /// <summary>
    /// 要使用的 JIT 编译器的名称的KEY
    /// </summary>
    public const string COMPILER = SystemPropsKeys.COMPILER;

    /// <summary>
    /// 一个或多个扩展目录的路径的KEY
    /// </summary>
    public const string EXT_DIRS = SystemPropsKeys.EXT_DIRS;

    // ----- Java虚拟机信息 -----/
    /// <summary>
    /// Java 虚拟机实现名称的KEY
    /// </summary>
    public const string VM_NAME = SystemPropsKeys.VM_NAME;

    /// <summary>
    /// Java 虚拟机规范名称的KEY
    /// </summary>
    public const string VM_SPECIFICATION_NAME = SystemPropsKeys.VM_SPECIFICATION_NAME;

    /// <summary>
    /// Java 虚拟机实现版本的KEY
    /// </summary>
    public const string VM_VERSION = SystemPropsKeys.VM_VERSION;

    /// <summary>
    /// Java 虚拟机规范版本的KEY
    /// </summary>
    public const string VM_SPECIFICATION_VERSION = SystemPropsKeys.VM_SPECIFICATION_VERSION;

    /// <summary>
    /// Java 虚拟机实现供应商的KEY
    /// </summary>
    public const string VM_VENDOR = SystemPropsKeys.VM_VENDOR;

    /// <summary>
    /// Java 虚拟机规范供应商的KEY
    /// </summary>
    public const string VM_SPECIFICATION_VENDOR = SystemPropsKeys.VM_SPECIFICATION_VENDOR;

    // ----- Java类信息 -----/
    /// <summary>
    /// Java 类格式版本号的KEY
    /// </summary>
    public const string CLASS_VERSION = SystemPropsKeys.CLASS_VERSION;

    /// <summary>
    /// Java 类路径的KEY
    /// </summary>
    public const string CLASS_PATH = SystemPropsKeys.CLASS_PATH;

    // ----- OS信息 -----/
    /// <summary>
    /// 操作系统的名称的KEY
    /// </summary>
    public const string OS_NAME = SystemPropsKeys.OS_NAME;

    /// <summary>
    /// 操作系统的架构的KEY
    /// </summary>
    public const string OS_ARCH = SystemPropsKeys.OS_ARCH;

    /// <summary>
    /// 操作系统的版本的KEY
    /// </summary>
    public const string OS_VERSION = SystemPropsKeys.OS_VERSION;

    /// <summary>
    /// 文件分隔符（在 UNIX 系统中是"/"）的KEY
    /// </summary>
    public const string FILE_SEPARATOR = SystemPropsKeys.FILE_SEPARATOR;

    /// <summary>
    /// 路径分隔符（在 UNIX 系统中是":"）的KEY
    /// </summary>
    public const string PATH_SEPARATOR = SystemPropsKeys.PATH_SEPARATOR;

    /// <summary>
    /// 行分隔符（在 UNIX 系统中是"\n"）的KEY
    /// </summary>
    public const string LINE_SEPARATOR = SystemPropsKeys.LINE_SEPARATOR;

    // ----- 用户信息 -----/
    /// <summary>
    /// 用户的账户名称的KEY
    /// </summary>
    public const string USER_NAME = SystemPropsKeys.USER_NAME;

    /// <summary>
    /// 用户的主目录的KEY
    /// </summary>
    public const string USER_HOME = SystemPropsKeys.USER_HOME;

    /// <summary>
    /// 用户的当前工作目录的KEY
    /// </summary>
    public const string USER_DIR = SystemPropsKeys.USER_DIR;

    /// <summary>
    /// 获取当前进程 PID
    /// </summary>
    /// <returns>当前进程 ID</returns>
    public static long GetCurrentPID()
    {
        return Process.GetCurrentProcess().Id;
    }

    /// <summary>
    /// 取得Java Virtual Machine Specification的信息
    /// </summary>
    /// <returns><see cref="JvmSpecInfo"/>对象</returns>
    public static JvmSpecInfo GetJvmSpecInfo()
    {
        return JvmSpecInfo.Instance;
    }

    /// <summary>
    /// 取得Java Virtual Machine Implementation的信息
    /// </summary>
    /// <returns><see cref="JvmInfo"/>对象</returns>
    public static JvmInfo GetJvmInfo()
    {
        return JvmInfo.Instance;
    }

    /// <summary>
    /// 取得Java Specification的信息
    /// </summary>
    /// <returns><see cref="JavaSpecInfo"/>对象</returns>
    public static JavaSpecInfo GetJavaSpecInfo()
    {
        return JavaSpecInfo.Instance;
    }

    /// <summary>
    /// 取得Java Implementation的信息
    /// </summary>
    /// <returns><see cref="JavaInfo"/>对象</returns>
    public static JavaInfo GetJavaInfo()
    {
        return JavaInfo.Instance;
    }

    /// <summary>
    /// 取得当前运行的JRE的信息
    /// </summary>
    /// <returns><see cref="JavaRuntimeInfo"/>对象</returns>
    public static JavaRuntimeInfo GetJavaRuntimeInfo()
    {
        return JavaRuntimeInfo.Instance;
    }

    /// <summary>
    /// 取得.NET Virtual Machine Specification的信息
    /// </summary>
    /// <returns><see cref="NetVmSpecInfo"/>对象</returns>
    public static NetVmSpecInfo GetNetVmSpecInfo()
    {
        return NetVmSpecInfo.Instance;
    }

    /// <summary>
    /// 取得.NET Virtual Machine Implementation的信息
    /// </summary>
    /// <returns><see cref="NetVmInfo"/>对象</returns>
    public static NetVmInfo GetNetVmInfo()
    {
        return NetVmInfo.Instance;
    }

    /// <summary>
    /// 取得.NET Specification的信息
    /// </summary>
    /// <returns><see cref="NetSpecInfo"/>对象</returns>
    public static NetSpecInfo GetNetSpecInfo()
    {
        return NetSpecInfo.Instance;
    }

    /// <summary>
    /// 取得.NET Implementation的信息
    /// </summary>
    /// <returns><see cref="NetInfo"/>对象</returns>
    public static NetInfo GetNetInfo()
    {
        return NetInfo.Instance;
    }

    /// <summary>
    /// 取得当前运行的.NET运行时的信息
    /// </summary>
    /// <returns><see cref="NetRuntimeInfo"/>对象</returns>
    public static NetRuntimeInfo GetNetRuntimeInfo()
    {
        return NetRuntimeInfo.Instance;
    }

    /// <summary>
    /// 取得OS的信息
    /// </summary>
    /// <returns><see cref="OsInfo"/>对象</returns>
    public static OsInfo GetOsInfo()
    {
        return OsInfo.Instance;
    }

    /// <summary>
    /// 取得User的信息
    /// </summary>
    /// <returns><see cref="UserInfo"/>对象</returns>
    public static UserInfo GetUserInfo()
    {
        return UserInfo.Instance;
    }

    /// <summary>
    /// 取得Host的信息
    /// </summary>
    /// <returns><see cref="HostInfo"/>对象</returns>
    public static HostInfo GetHostInfo()
    {
        return HostInfo.Instance;
    }

    /// <summary>
    /// 取得Runtime的信息
    /// </summary>
    /// <returns><see cref="RuntimeInfo"/>对象</returns>
    public static RuntimeInfo GetRuntimeInfo()
    {
        return RuntimeInfo.Instance;
    }

    /// <summary>
    /// 获取JVM中内存总大小
    /// </summary>
    /// <returns>内存总大小</returns>
    public static long GetTotalMemory()
    {
#if NET6_0_OR_GREATER
        return GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
#else
        return GC.GetTotalMemory(false);
#endif
    }

    /// <summary>
    /// 获取JVM中内存剩余大小
    /// </summary>
    /// <returns>内存剩余大小</returns>
    public static long GetFreeMemory()
    {
#if NET6_0_OR_GREATER
        var gcInfo = GC.GetGCMemoryInfo();
        return gcInfo.TotalAvailableMemoryBytes - GC.GetTotalMemory(false);
#else
        // 在.NET Standard 2.1中，我们只能返回已分配的内存，无法获取总可用内存
        return GC.GetTotalMemory(false);
#endif
    }

    /// <summary>
    /// 获取JVM可用的内存总大小
    /// </summary>
    /// <returns>JVM可用的内存总大小</returns>
    public static long GetMaxMemory()
    {
#if NET6_0_OR_GREATER
        return GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
#else
        // 在.NET Standard 2.1中，我们只能返回已分配的内存，无法获取总可用内存
        return GC.GetTotalMemory(false);
#endif
    }

    /// <summary>
    /// 获取总线程数
    /// </summary>
    /// <returns>总线程数</returns>
    public static int GetTotalThreadCount()
    {
        return Process.GetCurrentProcess().Threads.Count;
    }

    /// <summary>
    /// 将系统信息输出到控制台
    /// </summary>
    public static void DumpSystemInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine("--------------");
        sb.AppendLine(GetJvmSpecInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetJvmInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetJavaSpecInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetJavaInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetJavaRuntimeInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetNetVmSpecInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetNetVmInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetNetSpecInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetNetInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetNetRuntimeInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetOsInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetUserInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetHostInfo().ToString());
        sb.AppendLine("--------------");
        sb.AppendLine(GetRuntimeInfo().ToString());
        sb.AppendLine("--------------");
        Console.WriteLine(sb.ToString());
    }

    /// <summary>
    /// 输出到<see cref="StringBuilder"/>
    /// </summary>
    /// <param name="builder"><see cref="StringBuilder"/>对象</param>
    /// <param name="caption">标题</param>
    /// <param name="value">值</param>
    public static void Append(StringBuilder builder, string caption, object? value)
    {
        builder.Append(caption).Append(value ?? "[n/a]").AppendLine();
    }
}