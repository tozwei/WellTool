namespace WellTool.Core.IO.File.Visitor;

using System;
using System.IO;

/// <summary>
/// 文件删除访问器
/// </summary>
public class DelVisitor : SimpleFileVisitor
{
    /// <summary>
    /// 构造
    /// </summary>
    public DelVisitor()
    {
    }

    /// <summary>
    /// 访问文件
    /// </summary>
    public override bool VisitFile(string file)
    {
        File.Delete(file);
        return true;
    }

    /// <summary>
    /// 访问目录后（删除目录）
    /// </summary>
    public override void PostVisitDirectory(string dir)
    {
        Directory.Delete(dir, false);
    }
}
