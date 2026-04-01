using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace WellTool.Extra.Compress.Extractor
{
    /// <summary>
    /// 归档数据解包封装，用于将zip等包解包为文件
    /// </summary>
    public interface Extractor : IDisposable
    {
        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        void Extract(DirectoryInfo targetDir);

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        /// <param name="filter">解压文件过滤器，用于指定需要释放的文件，null表示不过滤</param>
        void Extract(DirectoryInfo targetDir, Func<ZipArchiveEntry, bool> filter);

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        /// <param name="stripComponents">清除(剥离)压缩包里面的 n 级文件夹名</param>
        void Extract(DirectoryInfo targetDir, int stripComponents);

        /// <summary>
        /// 释放（解压）到指定目录，结束后自动关闭流，此方法只能调用一次
        /// </summary>
        /// <param name="targetDir">目标目录</param>
        /// <param name="stripComponents">清除(剥离)压缩包里面的 n 级文件夹名</param>
        /// <param name="filter">解压文件过滤器，用于指定需要释放的文件，null表示不过滤</param>
        void Extract(DirectoryInfo targetDir, int stripComponents, Func<ZipArchiveEntry, bool> filter);

        /// <summary>
        /// 剥离名称
        /// </summary>
        /// <param name="name">文件名</param>
        /// <param name="stripComponents">剥离层级</param>
        /// <returns>剥离后的文件名</returns>
        string StripName(string name, int stripComponents);

        /// <summary>
        /// 无异常关闭
        /// </summary>
        void Dispose();
    }
}