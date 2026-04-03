using System;

namespace WellDone.Core.Util;

/// <summary>
/// SystemUtil系统工具类
/// </summary>
public static class SystemUtil
{
	/// <summary>
	/// 获取当前时间戳（毫秒）
	/// </summary>
	public static long CurrentMillis() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

	/// <summary>
	/// 获取当前时间戳（秒）
	/// </summary>
	public static long CurrentSeconds() => DateTimeOffset.UtcNow.ToUnixTimeSeconds();

	/// <summary>
	/// 获取当前时间
	/// </summary>
	public static DateTime Current() => DateTime.Now;

	/// <summary>
	/// 获取当前UTC时间
	/// </summary>
	public static DateTime CurrentUTC() => DateTime.UtcNow;

	/// <summary>
	/// 获取当前进程的ID
	/// </summary>
	public static int CurrentPid() => Environment.ProcessId;

	/// <summary>
	/// 获取当前线程的ID
	/// </summary>
	public static int CurrentTid() => Environment.CurrentManagedThreadId;

	/// <summary>
	/// 获取操作系统版本
	/// </summary>
	public static string OsVersion() => Environment.OSVersion.ToString();

	/// <summary>
	/// 获取CPU核心数
	/// </summary>
	public static int CpuCount() => Environment.ProcessorCount;

	/// <summary>
	/// 获取内存使用量（字节）
	/// </summary>
	public static long MemoryUsed() => GC.GetTotalMemory(false);

	/// <summary>
	/// 获取当前工作目录
	/// </summary>
	public static string WorkDir() => Environment.CurrentDirectory;

	/// <summary>
	/// 获取临时目录
	/// </summary>
	public static string TempDir() => System.IO.Path.GetTempPath();

	/// <summary>
	/// 获取用户名
	/// </summary>
	public static string UserName() => Environment.UserName;

	/// <summary>
	/// 获取机器名
	/// </summary>
	public static string MachineName() => Environment.MachineName;
}
