using System.Text;

namespace WellTool.System;

/// <summary>
/// 运行时信息，包括内存总大小、已用大小、可用大小等
/// </summary>
public class RuntimeInfo
{
    private static readonly Lazy<RuntimeInfo> _instance = new(() => new RuntimeInfo());
    public static RuntimeInfo Instance => _instance.Value;

    private RuntimeInfo()
    {
    }

    /// <summary>
    /// 获得JVM最大内存
    /// </summary>
    /// <returns>最大内存</returns>
    public long MaxMemory => GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;

    /// <summary>
    /// 获得JVM已分配内存
    /// </summary>
    /// <returns>已分配内存</returns>
    public long TotalMemory => GC.GetTotalMemory(false);

    /// <summary>
    /// 获得JVM已分配内存中的剩余空间
    /// </summary>
    /// <returns>已分配内存中的剩余空间</returns>
    public long FreeMemory
    {
        get
        {
            var total = GC.GetTotalMemory(false);
            var max = GC.GetGCMemoryInfo().TotalAvailableMemoryBytes;
            return max - total;
        }
    }

    /// <summary>
    /// 获得JVM最大可用内存
    /// </summary>
    /// <returns>最大可用内存</returns>
    public long UsableMemory => MaxMemory - TotalMemory + FreeMemory;

    /// <summary>
    /// 将运行时信息转换成字符串
    /// </summary>
    /// <returns>字符串表示</returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        SystemUtil.Append(builder, "Max Memory:    ", ReadableFileSize(MaxMemory));
        SystemUtil.Append(builder, "Total Memory:     ", ReadableFileSize(TotalMemory));
        SystemUtil.Append(builder, "Free Memory:     ", ReadableFileSize(FreeMemory));
        SystemUtil.Append(builder, "Usable Memory:     ", ReadableFileSize(UsableMemory));

        return builder.ToString();
    }

    /// <summary>
    /// 将字节数转换为可读的文件大小字符串
    /// </summary>
    /// <param name="size">字节数</param>
    /// <returns>可读的文件大小</returns>
    private static string ReadableFileSize(long size)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = size;
        int order = 0;

        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }

        return $"{len:0.##} {sizes[order]}";
    }
}