using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace WellTool.Core.Compress
{
    public class ZipReader : IDisposable
    {
        private const int DEFAULT_MAX_SIZE_DIFF = 100;

        private ZipArchive zipArchive;
        private Stream zipStream;
        private int maxSizeDiff = DEFAULT_MAX_SIZE_DIFF;

        public static ZipReader Of(string zipPath, Encoding charset = null)
        {
            return new ZipReader(zipPath, charset);
        }

        public static ZipReader Of(Stream stream, Encoding charset = null)
        {
            return new ZipReader(stream, charset);
        }

        public ZipReader(string zipPath, Encoding charset = null)
        {
            zipStream = File.OpenRead(zipPath);
            zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Read, charset != null);
        }

        public ZipReader(Stream stream, Encoding charset = null)
        {
            zipStream = stream;
            zipArchive = new ZipArchive(stream, ZipArchiveMode.Read, charset != null);
        }

        public ZipReader SetMaxSizeDiff(int maxSizeDiff)
        {
            this.maxSizeDiff = maxSizeDiff;
            return this;
        }

        public Stream Get(string path)
        {
            var entry = zipArchive.GetEntry(path);
            return entry?.Open();
        }

        public string ReadTo(string outDir)
        {
            return ReadTo(outDir, null);
        }

        public string ReadTo(string outDir, Func<ZipArchiveEntry, bool> entryFilter)
        {
            if (!Directory.Exists(outDir))
            {
                Directory.CreateDirectory(outDir);
            }

            Read(entry =>
            {
                if (entryFilter == null || entryFilter(entry))
                {
                    string path = entry.FullName;
                    if (Path.DirectorySeparatorChar == '\\')
                    {
                        path = path.Replace('*', '_');
                    }

                    string outItemPath = Path.Combine(outDir, path);
                    if (entry.FullName.EndsWith("/"))
                    {
                        Directory.CreateDirectory(outItemPath);
                    }
                    else
                    {
                        string directory = Path.GetDirectoryName(outItemPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        using (var entryStream = entry.Open())
                        using (var fileStream = File.Create(outItemPath))
                        {
                            entryStream.CopyTo(fileStream);
                        }
                    }
                }
            });

            return outDir;
        }

        public ZipReader Read(Action<ZipArchiveEntry> action)
        {
            foreach (var entry in zipArchive.Entries)
            {
                CheckZipBomb(entry);
                action(entry);
            }
            return this;
        }

        public void Dispose()
        {
            zipArchive?.Dispose();
            zipStream?.Dispose();
        }

        private void CheckZipBomb(ZipArchiveEntry entry)
        {
            if (entry == null)
            {
                return;
            }

            if (maxSizeDiff < 0 || entry.FullName.EndsWith("/"))
            {
                return;
            }

            long compressedSize = entry.CompressedLength;
            long uncompressedSize = entry.Length;

            if (compressedSize < 0 || uncompressedSize < 0 ||
                compressedSize * maxSizeDiff < uncompressedSize)
            {
                throw new Exception($"Zip bomb attack detected, invalid sizes: compressed {compressedSize}, uncompressed {uncompressedSize}, name {entry.FullName}");
            }
        }
    }
}