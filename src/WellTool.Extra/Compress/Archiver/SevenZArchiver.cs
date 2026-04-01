using System;
using System.IO;
using System.IO.Compression;

namespace WellTool.Extra.Compress.Archiver
{
    /// <summary>
    /// 7zip格式的归档封装
    /// </summary>
    public class SevenZArchiver : Archiver
    {
        private readonly ZipArchive archive;
        private readonly MemoryStream memoryStream;
        private readonly Stream outputStream;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">归档输出的文件</param>
        public SevenZArchiver(FileInfo file)
        {
            this.outputStream = file.OpenWrite();
            this.archive = new ZipArchive(this.outputStream, ZipArchiveMode.Create);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="outputStream">归档输出的流</param>
        public SevenZArchiver(Stream outputStream)
        {
            this.outputStream = outputStream;
            this.archive = new ZipArchive(this.outputStream, ZipArchiveMode.Create);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="memoryStream">内存流</param>
        public SevenZArchiver(MemoryStream memoryStream)
        {
            this.memoryStream = memoryStream;
            this.outputStream = memoryStream;
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