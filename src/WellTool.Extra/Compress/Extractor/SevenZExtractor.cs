using System;
using System.IO;
using System.IO.Compression;

namespace WellTool.Extra.Compress.Extractor
{
    /// <summary>
    /// 7z格式数据解压器，即将归档打包的数据释放
    /// </summary>
    public class SevenZExtractor : Extractor
    {
        private readonly ZipArchive archive;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">包文件</param>
        public SevenZExtractor(FileInfo file)
        {
            this.archive = ZipFile.OpenRead(file.FullName);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="stream">包流</param>
        public SevenZExtractor(Stream stream)
        {
            this.archive = new ZipArchive(stream, ZipArchiveMode.Read);
        }

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        public void Extract(DirectoryInfo targetDir)
        {
            Extract(targetDir, 0, null);
        }

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        /// <param name="filter">解压文件过滤器，用于指定需要释放的文件，null表示不过滤</param>
        public void Extract(DirectoryInfo targetDir, Func<ZipArchiveEntry, bool> filter)
        {
            Extract(targetDir, 0, filter);
        }

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        /// <param name="stripComponents">清除(剥离)压缩包里面的 n 级文件夹名</param>
        public void Extract(DirectoryInfo targetDir, int stripComponents)
        {
            Extract(targetDir, stripComponents, null);
        }

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        /// <param name="stripComponents">清除(剥离)压缩包里面的 n 级文件夹名</param>
        /// <param name="filter">解压文件过滤器，用于指定需要释放的文件，null表示不过滤</param>
        public void Extract(DirectoryInfo targetDir, int stripComponents, Func<ZipArchiveEntry, bool> filter)
        {
            try
            {
                if (!targetDir.Exists)
                {
                    targetDir.Create();
                }

                foreach (var entry in archive.Entries)
                {
                    if (filter != null && !filter(entry))
                    {
                        continue;
                    }

                    var entryName = StripName(entry.FullName, stripComponents);
                    if (entryName == null)
                    {
                        continue;
                    }

                    var outItemPath = Path.Combine(targetDir.FullName, entryName);
                    Directory.CreateDirectory(Path.GetDirectoryName(outItemPath));
                    entry.ExtractToFile(outItemPath, true);
                }
            }
            finally
            {
                Dispose();
            }
        }

        /// <summary>
        /// 剥离名称
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="stripComponents">剥离层级</param>
        /// <returns>剥离后的文件名</returns>
        public string StripName(string name, int stripComponents)
        {
            if (stripComponents <= 0)
            {
                return name;
            }

            var parts = name.Split('/');
            if (parts.Length > stripComponents)
            {
                return string.Join('/', parts, stripComponents, parts.Length);
            }

            return null;
        }

        /// <summary>
        /// 无异常关闭
        /// </summary>
        public void Dispose()
        {
            archive.Dispose();
        }
    }
}