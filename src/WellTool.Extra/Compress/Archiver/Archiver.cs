using System;
using System.IO;

namespace WellTool.Extra.Compress.Archiver
{
    /// <summary>
    /// 数据归档封装，归档即将几个文件或目录打成一个压缩包
    /// </summary>
    public interface Archiver : IDisposable
    {
        /// <summary>
        /// 将文件或目录加入归档，目录采取递归读取方式按照层级加入
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <returns>this</returns>
        Archiver Add(FileInfo file);

        /// <summary>
        /// 将文件或目录加入归档，目录采取递归读取方式按照层级加入
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <param name="filter">文件过滤器，指定哪些文件或目录可以加入</param>
        /// <returns>this</returns>
        Archiver Add(FileInfo file, Func<FileInfo, bool> filter);

        /// <summary>
        /// 将文件或目录加入归档包，目录采取递归读取方式按照层级加入
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <param name="path">文件或目录的初始路径，null表示位于根路径</param>
        /// <param name="filter">文件过滤器，指定哪些文件或目录可以加入</param>
        /// <returns>this</returns>
        Archiver Add(FileInfo file, string path, Func<FileInfo, bool> filter);

        /// <summary>
        /// 结束已经增加的文件归档，此方法不会关闭归档流，可以继续添加文件
        /// </summary>
        /// <returns>this</returns>
        Archiver Finish();

        /// <summary>
        /// 无异常关闭
        /// </summary>
        void Dispose();
    }
}