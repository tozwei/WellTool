namespace WellTool.System;

/// <summary>
/// 系统属性名称常量池
/// </summary>
public static class SystemPropsKeys
{
    // ----- .NET运行时环境信息 -----
    /// <summary>
    /// .NET 运行时环境规范名称
    /// </summary>
    public const string SPECIFICATION_NAME = "dotnet.specification.name";

    /// <summary>
    /// .NET 运行时环境版本
    /// </summary>
    public const string VERSION = "dotnet.version";

    /// <summary>
    /// .NET 运行时环境规范版本
    /// </summary>
    public const string SPECIFICATION_VERSION = "dotnet.specification.version";

    /// <summary>
    /// .NET 运行时环境供应商
    /// </summary>
    public const string VENDOR = "dotnet.vendor";

    /// <summary>
    /// .NET 运行时环境规范供应商
    /// </summary>
    public const string SPECIFICATION_VENDOR = "dotnet.specification.vendor";

    /// <summary>
    /// .NET 供应商的 URL
    /// </summary>
    public const string VENDOR_URL = "dotnet.vendor.url";

    /// <summary>
    /// .NET 安装目录
    /// </summary>
    public const string HOME = "dotnet.home";

    /// <summary>
    /// 加载库时搜索的路径列表
    /// </summary>
    public const string LIBRARY_PATH = "dotnet.library.path";

    /// <summary>
    /// 默认的临时文件路径
    /// </summary>
    public const string TMPDIR = "dotnet.io.tmpdir";

    /// <summary>
    /// 要使用的 JIT 编译器的名称
    /// </summary>
    public const string COMPILER = "dotnet.compiler";

    /// <summary>
    /// 一个或多个扩展目录的路径
    /// </summary>
    public const string EXT_DIRS = "dotnet.ext.dirs";

    // ----- .NET虚拟机信息 -----
    /// <summary>
    /// .NET 虚拟机实现名称
    /// </summary>
    public const string VM_NAME = "dotnet.vm.name";

    /// <summary>
    /// .NET 虚拟机规范名称
    /// </summary>
    public const string VM_SPECIFICATION_NAME = "dotnet.vm.specification.name";

    /// <summary>
    /// .NET 虚拟机实现版本
    /// </summary>
    public const string VM_VERSION = "dotnet.vm.version";

    /// <summary>
    /// .NET 虚拟机规范版本
    /// </summary>
    public const string VM_SPECIFICATION_VERSION = "dotnet.vm.specification.version";

    /// <summary>
    /// .NET 虚拟机实现供应商
    /// </summary>
    public const string VM_VENDOR = "dotnet.vm.vendor";

    /// <summary>
    /// .NET 虚拟机规范供应商
    /// </summary>
    public const string VM_SPECIFICATION_VENDOR = "dotnet.vm.specification.vendor";

    // ----- .NET类信息 -----
    /// <summary>
    /// .NET 类格式版本号
    /// </summary>
    public const string CLASS_VERSION = "dotnet.class.version";

    /// <summary>
    /// .NET 类路径
    /// </summary>
    public const string CLASS_PATH = "dotnet.class.path";

    // ----- OS信息 -----/
    /// <summary>
    /// 操作系统的名称
    /// </summary>
    public const string OS_NAME = "os.name";

    /// <summary>
    /// 操作系统的架构
    /// </summary>
    public const string OS_ARCH = "os.arch";

    /// <summary>
    /// 操作系统的版本
    /// </summary>
    public const string OS_VERSION = "os.version";

    /// <summary>
    /// 文件分隔符（在 UNIX 系统中是"/"）
    /// </summary>
    public const string FILE_SEPARATOR = "file.separator";

    /// <summary>
    /// 路径分隔符（在 UNIX 系统中是":"）
    /// </summary>
    public const string PATH_SEPARATOR = "path.separator";

    /// <summary>
    /// 行分隔符（在 UNIX 系统中是"\n"）
    /// </summary>
    public const string LINE_SEPARATOR = "line.separator";

    // ----- 用户信息 -----/
    /// <summary>
    /// 用户的账户名称
    /// </summary>
    public const string USER_NAME = "user.name";

    /// <summary>
    /// 用户的主目录
    /// </summary>
    public const string USER_HOME = "user.home";

    /// <summary>
    /// 用户的当前工作目录
    /// </summary>
    public const string USER_DIR = "user.dir";
}