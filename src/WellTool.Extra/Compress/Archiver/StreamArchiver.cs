using System;
using System.IO;
using System.IO.Compression;
using WellTool.Extra.Compress;

namespace WellTool.Extra.Compress.Archiver
{
    /// <summary>
    /// 数据归档封装，归档即将几个文件或目录打成一个压缩包
    /// 支持的归档文件格式为：
    /// <ul>
    ///     <li>zip</li>
    /// </ul>
    /// </summary>
    public class StreamArchiver : Archiver
    {
        private readonly ZipArchive archive;
        private readonly Stream outputStream;

        /// <summary>
        /// 创建归档器
        /// </summary>
        /// <param name="archiverName">归档类型名称</param>
        /// <param name="file">归档输出的文件</param>
        /// <returns>StreamArchiver</returns>
        public static StreamArchiver Create(string archiverName, FileInfo file)
        {
            return new StreamArchiver(archiverName, file);
        }

        /// <summary>
        /// 创建归档器
        /// </summary>
        /// <param name="archiverName">归档类型名称</param>
        /// <param name="outputStream">归档输出的流</param>
        /// <returns>StreamArchiver</returns>
        public static StreamArchiver Create(string archiverName, Stream outputStream)
        {
            return new StreamArchiver(archiverName, outputStream);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="archiverName">归档类型名称</param>
        /// <param name="file">归档输出的文件</param>
        public StreamArchiver(string archiverName, FileInfo file)
        {
            this.outputStream = file.OpenWrite();
            this.archive = new ZipArchive(this.outputStream, ZipArchiveMode.Create);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="archiverName">归档类型名称</param>
        /// <param name="outputStream">归档输出的流</param>
        public StreamArchiver(string archiverName, Stream outputStream)
        {
            this.outputStream = outputStream;
            this.archive = new ZipArchive(this.outputStream, ZipArchiveMode.Create);
        }

        /// <summary>
        /// 将文件或目录加入归档，目录采取递归读取方式按照层级加入
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <returns>this</returns>
        public Archiver Add(FileInfo file)
        {
            return Add(file, null);
        }

        /// <summary>
        /// 将文件或目录加入归档，目录采取递归读取方式按照层级加入
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <param name="filter">文件过滤器，指定哪些文件或目录可以加入</param>
        /// <returns>this</returns>
        public Archiver Add(FileInfo file, Func<FileInfo, bool> filter)
        {
            return Add(file, null, filter);
        }

        /// <summary>
        /// 将文件或目录加入归档包，目录采取递归读取方式按照层级加入
        /// </summary>
        /// <param name="file">文件或目录</param>
        /// <param name="path">文件或目录的初始路径，null表示位于根路径</param>
        /// <param name="filter">文件过滤器，指定哪些文件或目录可以加入</param>
        /// <returns>this</returns>
        public Archiver Add(FileInfo file, string path, Func<FileInfo, bool> filter)
        {
            if (filter != null && !filter(file))
            {
                return this;
            }

            if (Directory.Exists(file.FullName))
            {
                // 目录遍历写入
                var directoryInfo = new DirectoryInfo(file.FullName);
                var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
                foreach (var childFile in files)
                {
                    var childFileInfo = new FileInfo(childFile.FullName);
                    if (filter != null && !filter(childFileInfo))
                    {
                        continue;
                    }
                    var relativePath = childFile.FullName.Substring(file.FullName.Length).TrimStart('\\').Replace('\\', '/');
                    var childEntryName = string.IsNullOrEmpty(path) ? relativePath : $"{path}/{relativePath}";
                    var entry = archive.CreateEntry(childEntryName);
                    using (var entryStream = entry.Open())
                    using (var fileStream = childFile.OpenRead())
                    {
                        fileStream.CopyTo(entryStream);
                    }
                }
            }
            else
            {
                // 文件直接写入
                var entryName = string.IsNullOrEmpty(path) ? file.Name : $"{path}/{file.Name}";
                var entry = archive.CreateEntry(entryName);
                using (var entryStream = entry.Open())
                using (var fileStream = file.OpenRead())
                {
                    fileStream.CopyTo(entryStream);
                }
            }

            return this;
        }

        /// <summary>
        /// 结束已经增加的文件归档，此方法不会关闭归档流，可以继续添加文件
        /// </summary>
        /// <returns>this</returns>
        public Archiver Finish()
        {
            return this;
        }

        /// <summary>
        /// 无异常关闭
        /// </summary>
        public void Dispose()
        {
            try
            {
                archive.Dispose();
            }
            catch { }
            finally
            {
                outputStream?.Dispose();
            }
        }
    }
}