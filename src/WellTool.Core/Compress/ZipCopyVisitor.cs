namespace WellTool.Core.Compress;

using WellTool.Core.IO;
using WellTool.Core.Util;
using System.IO;
using System.IO.Compression;

/// <summary>
/// Zip文件拷贝的 FileVisitor 实现，zip中追加文件，此类非线程安全
/// 此类在遍历源目录并复制过程中会自动创建目标目录中不存在的上级目录。
/// </summary>
public class ZipCopyVisitor
{
    /// <summary>
    /// 源路径，或基准路径，用于计算被拷贝文件的相对路径
    /// </summary>
    private readonly string _source;

    /// <summary>
    /// 目标 Zip 归档
    /// </summary>
    private readonly ZipArchive _zipArchive;

    /// <summary>
    /// 拷贝选项，如跳过已存在等
    /// </summary>
    private readonly bool _overwrite;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="source">源路径，或基准路径，用于计算被拷贝文件的相对路径</param>
    /// <param name="zipArchive">目标 Zip 归档</param>
    /// <param name="overwrite">是否覆盖已存在的文件</param>
    public ZipCopyVisitor(string source, ZipArchive zipArchive, bool overwrite = false)
    {
        _source = source;
        _zipArchive = zipArchive;
        _overwrite = overwrite;
    }

    /// <summary>
    /// 访问目录前
    /// </summary>
    /// <param name="dir">目录路径</param>
    /// <returns>继续访问</returns>
    public bool PreVisitDirectory(string dir)
    {
        var targetDir = ResolveTarget(dir);
        if (StrUtil.IsNotEmpty(targetDir))
        {
            try
            {
                var entry = _zipArchive.CreateEntry(targetDir + "/", CompressionLevel.Optimal);
            }
            catch
            {
                // 目录已存在，跳过
            }
        }
        return true;
    }

    /// <summary>
    /// 访问文件
    /// </summary>
    /// <param name="file">文件路径</param>
    /// <returns>继续访问</returns>
    public bool VisitFile(string file)
    {
        var targetPath = ResolveTarget(file);
        var sourceEntry = _zipArchive.CreateEntry(targetPath, CompressionLevel.Optimal);
        using var sourceStream = File.OpenRead(file);
        using var targetStream = sourceEntry.Open();
        sourceStream.CopyTo(targetStream);
        return true;
    }

    /// <summary>
    /// 根据源文件或目录路径，拼接生成目标的文件或目录路径
    /// 原理是首先截取源路径，得到相对路径，再和目标路径拼接
    /// </summary>
    /// <param name="file">需要拷贝的文件或目录路径</param>
    /// <returns>目标路径</returns>
    private string ResolveTarget(string file)
    {
        if (file.StartsWith(_source))
        {
            return file.Substring(_source.Length).TrimStart(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
        }
        return file;
    }
}
