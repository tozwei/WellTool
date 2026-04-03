namespace WellTool.Core.IO.File;

using WellTool.Core.IO;
using System;
using System.IO;

/// <summary>
/// 文件移动封装
/// 
/// @author looly
/// @since 5.8.14
/// </summary>
public class PathMover
{
    private readonly string _src;
    private readonly string _target;
    private readonly bool _overwrite;

    /// <summary>
    /// 创建文件或目录移动器
    /// </summary>
    /// <param name="src">源文件或目录</param>
    /// <param name="target">目标文件或目录</param>
    /// <param name="overwrite">是否覆盖目标文件</param>
    /// <returns>PathMover</returns>
    public static PathMover Of(string src, string target, bool overwrite)
    {
        return new PathMover(src, target, overwrite);
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="src">源文件或目录</param>
    /// <param name="target">目标文件或目录</param>
    /// <param name="overwrite">是否覆盖</param>
    public PathMover(string src, string target, bool overwrite)
    {
        if (string.IsNullOrEmpty(src))
        {
            throw new ArgumentException("Src path must be not null!");
        }
        if (!File.Exists(src) && !Directory.Exists(src))
        {
            throw new ArgumentException("Src path is not exist!");
        }
        
        _src = src;
        _target = target ?? throw new ArgumentException("Target path must be not null!");
        _overwrite = overwrite;
    }

    /// <summary>
    /// 移动文件或目录到目标中
    /// </summary>
    /// <returns>目标路径</returns>
    public string Move()
    {
        // 如果源和目标是同一路径，直接返回
        if (Path.GetFullPath(_src) == Path.GetFullPath(_target))
        {
            return _target;
        }

        var srcInfo = new FileInfo(_src);
        var targetInfo = new FileInfo(_target);
        
        // 如果目标是已存在的目录
        if (Directory.Exists(_target) || targetInfo.Exists == false && _target.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            string targetPath;
            if (Directory.Exists(_target))
            {
                targetPath = Path.Combine(_target, Path.GetFileName(_src));
            }
            else
            {
                targetPath = _target;
            }

            if (srcInfo.Exists)
            {
                // 源是文件
                MoveFile(_src, targetPath);
            }
            else
            {
                // 源是目录
                MoveDirectory(_src, targetPath);
            }
            return targetPath;
        }

        // 目标是文件
        if (srcInfo.Exists)
        {
            MoveFile(_src, _target);
        }
        else
        {
            MoveDirectory(_src, _target);
        }

        return _target;
    }

    private void MoveFile(string src, string target)
    {
        string targetDir = Path.GetDirectoryName(target) ?? string.Empty;
        if (!string.IsNullOrEmpty(targetDir) && !Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }

        if (_overwrite && File.Exists(target))
        {
            File.Delete(target);
        }
        File.Move(src, target);
    }

    private void MoveDirectory(string src, string target)
    {
        string targetDir = target;
        if (!target.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            // 如果目标不存在，相当于重命名
            if (!Directory.Exists(target))
            {
                Directory.Move(src, target);
                return;
            }
            targetDir = Path.Combine(target, Path.GetFileName(src));
        }

        if (!Directory.Exists(targetDir))
        {
            Directory.CreateDirectory(targetDir);
        }

        // 移动目录内容
        foreach (string file in Directory.GetFiles(src))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(targetDir, fileName);
            if (_overwrite && File.Exists(destFile))
            {
                File.Delete(destFile);
            }
            File.Move(file, destFile);
        }

        foreach (string dir in Directory.GetDirectories(src))
        {
            string dirName = Path.GetFileName(dir);
            string destDir = Path.Combine(targetDir, dirName);
            MoveDirectory(dir, destDir);
        }

        // 删除源目录
        Directory.Delete(src, true);
    }
}
