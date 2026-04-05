using System;
using System.Diagnostics;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 运行时工具类
    /// </summary>
    public static class RuntimeUtil
    {
        /// <summary>
        /// 获取CPU核心数
        /// </summary>
        public static int CpuCount => Environment.ProcessorCount;

        /// <summary>
        /// 获取运行时版本
        /// </summary>
        public static string Version => Environment.Version.ToString();

        /// <summary>
        /// 获取操作系统版本
        /// </summary>
        public static string OsVersion => Environment.OSVersion.ToString();

        /// <summary>
        /// 获取系统架构
        /// </summary>
        public static string Architecture => Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");

        /// <summary>
        /// 获取运行时目录
        /// </summary>
        public static string RuntimeDirectory => Environment.GetFolderPath(Environment.SpecialFolder.System);

        /// <summary>
        /// 获取当前进程
        /// </summary>
        public static Process CurrentProcess => Process.GetCurrentProcess();

        /// <summary>
        /// 获取当前进程ID
        /// </summary>
        public static int CurrentProcessId => CurrentProcess.Id;

        /// <summary>
        /// 获取当前线程ID
        /// </summary>
        public static int CurrentThreadId => Environment.CurrentManagedThreadId;

        /// <summary>
        /// 获取当前进程的工作目录
        /// </summary>
        public static string CurrentDirectory => Environment.CurrentDirectory;

        /// <summary>
        /// 获取临时目录
        /// </summary>
        public static string TempDirectory => Path.GetTempPath();

        /// <summary>
        /// 获取程序开始时间
        /// </summary>
        public static DateTime StartTime => CurrentProcess.StartTime;

        /// <summary>
        /// 获取进程运行时间
        /// </summary>
        public static TimeSpan RunningTime => DateTime.Now - StartTime;

        /// <summary>
        /// 获取可用内存（字节）
        /// </summary>
        public static long AvailableMemory => GC.GetTotalMemory(false);

        /// <summary>
        /// 获取已用内存（字节）
        /// </summary>
        public static long UsedMemory => CurrentProcess.WorkingSet64;

        /// <summary>
        /// 获取内存使用情况
        /// </summary>
        public static (long total, long used, double usagePercent) GetMemoryInfo()
        {
            var available = AvailableMemory;
            var used = UsedMemory;
            var total = available + used;
            return (total, used, total > 0 ? (double)used / total * 100 : 0);
        }

        /// <summary>
        /// 获取CPU使用率
        /// </summary>
        public static double CpuUsage
        {
            get
            {
                try
                {
                    CurrentProcess.Refresh();
                    return CurrentProcess.TotalProcessorTime.TotalMilliseconds /
                           (DateTime.Now - StartTime).TotalMilliseconds * 100;
                }
                catch
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 获取托管线程数
        /// </summary>
        public static int ManagedThreadCount => ThreadCount;

        /// <summary>
        /// 获取线程数
        /// </summary>
        public static int ThreadCount => CurrentProcess.Threads.Count;

        /// <summary>
        /// 获取打开的句柄数
        /// </summary>
        public static int HandleCount => CurrentProcess.HandleCount;

        /// <summary>
        /// 执行垃圾回收
        /// </summary>
        public static void GCCollect(int generation = -1)
        {
            if (generation < 0)
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            }
            else
            {
                GC.Collect(generation, GCCollectionMode.Forced);
            }
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// 获取系统页大小
        /// </summary>
        public static long SystemPageSize => Environment.SystemPageSize;

        /// <summary>
        /// 判断是否为64位进程
        /// </summary>
        public static bool Is64BitProcess => IntPtr.Size == 8;

        /// <summary>
        /// 判断操作系统是否为64位
        /// </summary>
        public static bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;

        /// <summary>
        /// 获取系统用户名
        /// </summary>
        public static string UserName => Environment.UserName;

        /// <summary>
        /// 获取机器名
        /// </summary>
        public static string MachineName => Environment.MachineName;

        /// <summary>
        /// 获取系统启动以来的毫秒数
        /// </summary>
        public static long TickCount => Environment.TickCount;

        /// <summary>
        /// 获取堆内存信息
        /// </summary>
        public static (long totalMemory, long managedMemory) GetHeapInfo()
        {
            return (GC.GetTotalMemory(false), GC.GetTotalMemory(false));
        }
    }
}
