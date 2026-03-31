// Copyright (c) 2025 WellTool Team
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.IO;
using System.IO.Compression;

namespace WellTool.Extra;

/// <summary>
/// 压缩工具类
/// </summary>
public class CompressUtil
{
    /// <summary>
    /// 单例实例
    /// </summary>
    public static CompressUtil Instance { get; } = new CompressUtil();

    /// <summary>
    /// 压缩文件为ZIP
    /// </summary>
    /// <param name="sourceFilePath">源文件路径</param>
    /// <param name="targetZipPath">目标ZIP文件路径</param>
    public void ZipFile(string sourceFilePath, string targetZipPath)
    {
        try
        {
            using var zipArchive = System.IO.Compression.ZipFile.Open(targetZipPath, ZipArchiveMode.Create);
            var fileName = Path.GetFileName(sourceFilePath);
            zipArchive.CreateEntryFromFile(sourceFilePath, fileName);
        }
        catch (Exception ex)
        {
            throw new CompressException("压缩文件失败", ex);
        }
    }

    /// <summary>
    /// 压缩目录为ZIP
    /// </summary>
    /// <param name="sourceDirectoryPath">源目录路径</param>
    /// <param name="targetZipPath">目标ZIP文件路径</param>
    public void ZipDirectory(string sourceDirectoryPath, string targetZipPath)
    {
        try
        {
            using var zipArchive = System.IO.Compression.ZipFile.Open(targetZipPath, ZipArchiveMode.Create);
            var directoryInfo = new DirectoryInfo(sourceDirectoryPath);
            
            foreach (var file in directoryInfo.GetFiles("*", SearchOption.AllDirectories))
            {
                var relativePath = file.FullName.Substring(directoryInfo.FullName.Length + 1);
                zipArchive.CreateEntryFromFile(file.FullName, relativePath);
            }
        }
        catch (Exception ex)
        {
            throw new CompressException("压缩目录失败", ex);
        }
    }

    /// <summary>
    /// 解压ZIP文件
    /// </summary>
    /// <param name="zipFilePath">ZIP文件路径</param>
    /// <param name="targetDirectoryPath">目标目录路径</param>
    public void Unzip(string zipFilePath, string targetDirectoryPath)
    {
        try
        {
            using var zipArchive = System.IO.Compression.ZipFile.OpenRead(zipFilePath);
            zipArchive.ExtractToDirectory(targetDirectoryPath);
        }
        catch (Exception ex)
        {
            throw new CompressException("解压文件失败", ex);
        }
    }

    /// <summary>
    /// 压缩数据为GZIP
    /// </summary>
    /// <param name="data">要压缩的数据</param>
    /// <returns>压缩后的数据</returns>
    public byte[] Gzip(byte[] data)
    {
        try
        {
            using var memoryStream = new MemoryStream();
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress);
            gzipStream.Write(data, 0, data.Length);
            gzipStream.Flush();
            gzipStream.Close();
            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            throw new CompressException("Gzip压缩失败", ex);
        }
    }

    /// <summary>
    /// 解压GZIP数据
    /// </summary>
    /// <param name="data">要解压的数据</param>
    /// <returns>解压后的数据</returns>
    public byte[] Gunzip(byte[] data)
    {
        try
        {
            using var memoryStream = new MemoryStream(data);
            using var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress);
            using var outputStream = new MemoryStream();
            gzipStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
        catch (Exception ex)
        {
            throw new CompressException("Gzip解压失败", ex);
        }
    }
}

/// <summary>
/// 压缩异常
/// </summary>
public class CompressException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public CompressException(string message) : base(message)
    {}

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public CompressException(string message, Exception innerException) : base(message, innerException)
    {}
}