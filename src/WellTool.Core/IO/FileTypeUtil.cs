using System;
using System.Collections.Generic;
using System.IO;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 文件类型工具类
    /// </summary>
    public class FileTypeUtil
    {
        private static readonly Dictionary<string, byte[]> _fileSignatures = new Dictionary<string, byte[]>
        {
            { ".jpg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".jpeg", new byte[] { 0xFF, 0xD8, 0xFF } },
            { ".png", new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } },
            { ".gif", new byte[] { 0x47, 0x49, 0x46, 0x38 } },
            { ".bmp", new byte[] { 0x42, 0x4D } },
            { ".pdf", new byte[] { 0x25, 0x50, 0x44, 0x46 } },
            { ".doc", new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } },
            { ".docx", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
            { ".xls", new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } },
            { ".xlsx", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
            { ".ppt", new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } },
            { ".pptx", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
            { ".zip", new byte[] { 0x50, 0x4B, 0x03, 0x04 } },
            { ".rar", new byte[] { 0x52, 0x61, 0x72, 0x21 } },
            { ".7z", new byte[] { 0x37, 0x7A, 0xBC, 0xAF, 0x27, 0x1C } },
            { ".exe", new byte[] { 0x4D, 0x5A } },
            { ".dll", new byte[] { 0x4D, 0x5A } },
            { ".mp3", new byte[] { 0x49, 0x44, 0x33 } },
            { ".mp4", new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70 } },
            { ".avi", new byte[] { 0x52, 0x49, 0x46, 0x46 } },
            { ".wmv", new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 } },
            { ".flv", new byte[] { 0x46, 0x4C, 0x56 } },
            { ".txt", new byte[] { 0xEF, 0xBB, 0xBF } }, // UTF-8 BOM
            { ".xml", new byte[] { 0x3C, 0x3F, 0x78, 0x6D, 0x6C } },
            { ".html", new byte[] { 0x3C, 0x21, 0x44, 0x4F, 0x43, 0x54, 0x59, 0x50, 0x45 } },
            { ".css", new byte[] { 0x2F, 0x2A, 0x2A, 0x2F } },
            { ".js", new byte[] { 0x2F, 0x2A, 0x2A, 0x2F } },
            { ".json", new byte[] { 0x7B } },
            { ".sql", new byte[] { 0x2D, 0x2D } },
            { ".java", new byte[] { 0x70, 0x61, 0x63, 0x6B, 0x61, 0x67, 0x65 } },
            { ".cs", new byte[] { 0x75, 0x73, 0x69, 0x6E, 0x67, 0x20, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D } },
            { ".c", new byte[] { 0x23, 0x69, 0x6E, 0x63, 0x6C, 0x75, 0x64, 0x65 } },
            { ".cpp", new byte[] { 0x23, 0x69, 0x6E, 0x63, 0x6C, 0x75, 0x64, 0x65 } },
            { ".h", new byte[] { 0x23, 0x69, 0x6E, 0x63, 0x6C, 0x75, 0x64, 0x65 } },
            { ".py", new byte[] { 0x23, 0x21 } },
            { ".php", new byte[] { 0x3C, 0x3F, 0x70, 0x68, 0x70 } },
            { ".rb", new byte[] { 0x23, 0x21 } },
            { ".sh", new byte[] { 0x23, 0x21 } },
            { ".bat", new byte[] { 0x40 } },
            { ".ps1", new byte[] { 0x23, 0x21 } },
        };

        /// <summary>
        /// 根据文件路径检测文件类型
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>文件扩展名</returns>
        public static string DetectFileType(string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return DetectFileType(stream);
        }

        /// <summary>
        /// 根据流检测文件类型
        /// </summary>
        /// <param name="stream">流</param>
        /// <returns>文件扩展名</returns>
        public static string DetectFileType(Stream stream)
        {
            var buffer = new byte[16];
            var bytesRead = stream.Read(buffer, 0, buffer.Length);
            stream.Position = 0; // 重置流位置

            foreach (var entry in _fileSignatures)
            {
                var extension = entry.Key;
                var signature = entry.Value;

                if (bytesRead >= signature.Length)
                {
                    bool match = true;
                    for (int i = 0; i < signature.Length; i++)
                    {
                        if (buffer[i] != signature[i])
                        {
                            match = false;
                            break;
                        }
                    }
                    if (match)
                    {
                        return extension;
                    }
                }
            }

            return ".unknown";
        }

        /// <summary>
        /// 根据字节数组检测文件类型
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns>文件扩展名</returns>
        public static string DetectFileType(byte[] bytes)
        {
            using var stream = new MemoryStream(bytes);
            return DetectFileType(stream);
        }

        /// <summary>
        /// 检查文件是否为指定类型
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="extension">文件扩展名</param>
        /// <returns>是否为指定类型</returns>
        public static bool IsFileType(string filePath, string extension)
        {
            return DetectFileType(filePath).Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 检查流是否为指定类型
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="extension">文件扩展名</param>
        /// <returns>是否为指定类型</returns>
        public static bool IsFileType(Stream stream, string extension)
        {
            return DetectFileType(stream).Equals(extension, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 检查字节数组是否为指定类型
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="extension">文件扩展名</param>
        /// <returns>是否为指定类型</returns>
        public static bool IsFileType(byte[] bytes, string extension)
        {
            return DetectFileType(bytes).Equals(extension, StringComparison.OrdinalIgnoreCase);
        }
    }
}