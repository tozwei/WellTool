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
        if (string.IsNullOrEmpty(sourceFilePath))
            throw new ArgumentNullException(nameof(sourceFilePath));
        if (string.IsNullOrEmpty(targetZipPath))
            throw new ArgumentNullException(nameof(targetZipPath));
            
        try
        {
            // 删除已存在的目标文件（带重试逻辑）
            if (File.Exists(targetZipPath))
            {
                DeleteFileWithRetry(targetZipPath);
            }
            
            // 使用 ZipFile.CreateFromDirectory 来创建 ZIP
            // 首先创建临时目录
            var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            try
            {
                Directory.CreateDirectory(tempDir);
                var tempFile = Path.Combine(tempDir, Path.GetFileName(sourceFilePath));
                File.Copy(sourceFilePath, tempFile);
                System.IO.Compression.ZipFile.CreateFromDirectory(tempDir, targetZipPath, System.IO.Compression.CompressionLevel.Optimal, false);
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }
        catch (System.Exception ex)
        {
            throw new CompressException("压缩文件失败", ex);
        }
    }
    
    /// <summary>
    /// 带重试逻辑删除文件
    /// </summary>
    private void DeleteFileWithRetry(string path, int maxRetries = 3)
    {
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                File.Delete(path);
                return;
            }
            catch (IOException)
            {
                if (i == maxRetries - 1)
                    throw;
                System.Threading.Thread.Sleep(100);
            }
        }
    }

    /// <summary>
    /// 压缩目录为ZIP
    /// </summary>
    /// <param name="sourceDirectoryPath">源目录路径</param>
    /// <param name="targetZipPath">目标ZIP文件路径</param>
    public void ZipDirectory(string sourceDirectoryPath, string targetZipPath)
    {
        if (string.IsNullOrEmpty(sourceDirectoryPath))
            throw new ArgumentNullException(nameof(sourceDirectoryPath));
        if (string.IsNullOrEmpty(targetZipPath))
            throw new ArgumentNullException(nameof(targetZipPath));
            
        try
        {
            if (!Directory.Exists(sourceDirectoryPath))
            {
                throw new DirectoryNotFoundException($"源目录不存在: {sourceDirectoryPath}");
            }
            
            // 删除已存在的目标文件（带重试逻辑）
            if (File.Exists(targetZipPath))
            {
                DeleteFileWithRetry(targetZipPath);
            }
            
            // 使用 ZipFile.CreateFromDirectory 来创建 ZIP
            System.IO.Compression.ZipFile.CreateFromDirectory(sourceDirectoryPath, targetZipPath, System.IO.Compression.CompressionLevel.Optimal, false);
        }
        catch (System.Exception ex)
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
        if (string.IsNullOrEmpty(zipFilePath))
            throw new ArgumentNullException(nameof(zipFilePath));
        if (string.IsNullOrEmpty(targetDirectoryPath))
            throw new ArgumentNullException(nameof(targetDirectoryPath));
            
        try
        {
            if (!File.Exists(zipFilePath))
            {
                throw new FileNotFoundException($"ZIP文件不存在: {zipFilePath}");
            }
            
            // 创建目标目录（如果不存在）
            if (!Directory.Exists(targetDirectoryPath))
            {
                Directory.CreateDirectory(targetDirectoryPath);
            }
            
            // 使用 ZipFile.ExtractToDirectory 来解压
            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, targetDirectoryPath, true);
        }
        catch (System.Exception ex)
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
        if (data == null)
            throw new ArgumentNullException(nameof(data));
            
        try
        {
            using var memoryStream = new MemoryStream();
            using (var gzipStream = new System.IO.Compression.GZipStream(memoryStream, System.IO.Compression.CompressionMode.Compress, leaveOpen: true))
            {
                gzipStream.Write(data, 0, data.Length);
            }
            return memoryStream.ToArray();
        }
        catch (System.Exception ex)
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
        if (data == null)
            throw new ArgumentNullException(nameof(data));
            
        try
        {
            using var memoryStream = new MemoryStream(data);
            using var gzipStream = new System.IO.Compression.GZipStream(memoryStream, System.IO.Compression.CompressionMode.Decompress);
            using var outputStream = new MemoryStream();
            gzipStream.CopyTo(outputStream);
            return outputStream.ToArray();
        }
        catch (System.Exception ex)
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
