namespace WellTool.Core.IO.File.Visitor;

using System;
using System.IO;

/// <summary>
/// 文件移动访问器
/// 
/// @author looly
/// </summary>
public class MoveVisitor : SimpleFileVisitor
{
    private readonly string _targetDir;
    private readonly bool _overwrite;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="targetDir">目标目录</param>
    /// <param name="overwrite">是否覆盖</param>
    public MoveVisitor(string targetDir, bool overwrite = false)
    {
        _targetDir = targetDir ?? throw new ArgumentNullException(nameof(targetDir));
        _overwrite = overwrite;

        if (!Directory.Exists(_targetDir))
        {
            Directory.CreateDirectory(_targetDir);
        }
    }

    /// <summary>
    /// 访问目录前
    /// </summary>
    public override bool PreVisitDirectory(string dir)
    {
        string targetPath = Path.Combine(_targetDir, Path.GetFileName(dir));
        if (!Directory.Exists(targetPath))
        {
            Directory.CreateDirectory(targetPath);
        }
        return true;
    }

    /// <summary>
    /// 访问文件
    /// </summary>
    public override bool VisitFile(string file)
    {
        string targetPath = Path.Combine(_targetDir, Path.GetFileName(file));
        if (_overwrite && File.Exists(targetPath))
        {
            File.Delete(targetPath);
        }
        File.Move(file, targetPath, _overwrite);
        return true;
    }
}
