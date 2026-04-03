namespace WellTool.Core.IO.File.Visitor;

using System;
using System.IO;

/// <summary>
/// 文件复制访问器
/// 
/// @author looly
/// </summary>
public class CopyVisitor : SimpleFileVisitor
{
    private readonly string _targetDir;
    private readonly bool _overwrite;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="targetDir">目标目录</param>
    /// <param name="overwrite">是否覆盖</param>
    public CopyVisitor(string targetDir, bool overwrite = false)
    {
        _targetDir = targetDir ?? throw new ArgumentNullException(nameof(targetDir));
        _overwrite = overwrite;
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
        File.Copy(file, targetPath, _overwrite);
        return true;
    }
}

/// <summary>
/// 简单文件访问器基类
/// </summary>
public class SimpleFileVisitor
{
    /// <summary>
    /// 访问目录前
    /// </summary>
    public virtual bool PreVisitDirectory(string dir) => true;

    /// <summary>
    /// 访问文件
    /// </summary>
    public virtual bool VisitFile(string file) => true;

    /// <summary>
    /// 访问目录后
    /// </summary>
    public virtual void PostVisitDirectory(string dir) { }
}
