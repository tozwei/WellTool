using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace WellTool.Core.Compress
{
    public class ZipWriter : IDisposable
    {
        private string zipPath;
        private Stream zipStream;
        private ZipArchive zipArchive;

        public static ZipWriter Of(string zipPath, Encoding charset = null)
        {
            return new ZipWriter(zipPath, charset);
        }

        public static ZipWriter Of(Stream stream, Encoding charset = null)
        {
            return new ZipWriter(stream, charset);
        }

        public ZipWriter(string zipPath, Encoding charset = null)
        {
            this.zipPath = zipPath;
            zipStream = File.Create(zipPath);
            zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create);
        }

        public ZipWriter(Stream stream, Encoding charset = null)
        {
            zipStream = stream;
            zipArchive = new ZipArchive(stream, ZipArchiveMode.Create);
        }

        public ZipWriter SetComment(string comment)
        {
            zipArchive.CreateEntry("_comment").Delete();
            return this;
        }

        public ZipWriter Add(bool withSrcDir, Func<FileInfo, bool> filter, params string[] files)
        {
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                string srcRootDir = fileInfo.FullName;
                if (!fileInfo.Attributes.HasFlag(FileAttributes.Directory) || withSrcDir)
                {
                    srcRootDir = fileInfo.Directory.FullName;
                }

                _Add(fileInfo, srcRootDir, filter);
            }
            return this;
        }

        public ZipWriter Add(string path, Stream stream)
        {
            path = path ?? string.Empty;
            if (stream == null)
            {
                if (!path.EndsWith("/"))
                {
                    path += "/";
                }
                if (string.IsNullOrWhiteSpace(path))
                {
                    return this;
                }
            }

            return PutEntry(path, stream);
        }

        public ZipWriter Add(string[] paths, Stream[] streams)
        {
            if (paths == null || streams == null)
            {
                throw new ArgumentException("Paths or streams is empty!");
            }
            if (paths.Length != streams.Length)
            {
                throw new ArgumentException("Paths length is not equals to streams length!");
            }

            for (int i = 0; i < paths.Length; i++)
            {
                Add(paths[i], streams[i]);
            }

            return this;
        }

        public void Dispose()
        {
            zipArchive?.Dispose();
            zipStream?.Dispose();
        }

        private ZipWriter _Add(FileInfo fileInfo, string srcRootDir, Func<FileInfo, bool> filter)
        {
            if (fileInfo == null || (filter != null && !filter(fileInfo)))
            {
                return this;
            }

            string subPath = GetSubPath(srcRootDir, fileInfo.FullName);
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory))
            {
                var directoryInfo = new DirectoryInfo(fileInfo.FullName);
                var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);

                if (files.Length == 0)
                {
                    Add(subPath, null);
                }
                else
                {
                    foreach (var childFile in files)
                    {
                        _Add(childFile, srcRootDir, filter);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(zipPath) && fileInfo.FullName.Equals(zipPath, StringComparison.OrdinalIgnoreCase))
                {
                    return this;
                }

                using (var stream = fileInfo.OpenRead())
                {
                    PutEntry(subPath, stream);
                }
            }
            return this;
        }

        private ZipWriter PutEntry(string path, Stream stream)
        {
            var entry = zipArchive.CreateEntry(path);
            if (stream != null)
            {
                using (var entryStream = entry.Open())
                {
                    stream.CopyTo(entryStream);
                }
            }
            return this;
        }

        private string GetSubPath(string srcRootDir, string fullPath)
        {
            if (fullPath.StartsWith(srcRootDir, StringComparison.OrdinalIgnoreCase))
            {
                string subPath = fullPath.Substring(srcRootDir.Length);
                if (subPath.StartsWith(Path.DirectorySeparatorChar.ToString()) || subPath.StartsWith(Path.AltDirectorySeparatorChar.ToString()))
                {
                    subPath = subPath.Substring(1);
                }
                return subPath.Replace(Path.DirectorySeparatorChar, '/');
            }
            return fullPath;
        }
    }
}