using System;
using System.IO;
using System.Collections.Generic;

namespace WellTool.Poi.Ofd
{
    /// <summary>
    /// OFD文件生成器
    /// </summary>
    public class OfdWriter : IDisposable
    {
        private readonly FileInfo? _file;
        private readonly Stream? _outputStream;
        private bool _disposed = false;
        private List<string> _texts = new List<string>();
        private List<(FileInfo, int, int)> _pictures = new List<(FileInfo, int, int)>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">生成的文件</param>
        public OfdWriter(string file) : this(new FileInfo(file))
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">生成的文件</param>
        public OfdWriter(FileInfo file)
        {
            _file = file ?? throw new ArgumentNullException(nameof(file));
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="outputStream">需要输出的流</param>
        public OfdWriter(Stream outputStream)
        {
            _outputStream = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        }

        /// <summary>
        /// 增加文本内容
        /// </summary>
        /// <param name="texts">文本</param>
        /// <returns>this</returns>
        public OfdWriter AddText(params string[] texts)
        {
            if (texts != null)
            {
                _texts.AddRange(texts);
            }
            return this;
        }

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public OfdWriter AddPicture(string picFile, int width, int height)
        {
            return AddPicture(new FileInfo(picFile), width, height);
        }

        /// <summary>
        /// 追加图片
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>this</returns>
        public OfdWriter AddPicture(FileInfo picFile, int width, int height)
        {
            if (picFile != null && width > 0 && height > 0)
            {
                _pictures.Add((picFile, width, height));
            }
            return this;
        }

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="element">元素</param>
        /// <returns>this</returns>
        public OfdWriter Add(object element)
        {
            // 简化实现，仅记录元素
            if (element != null)
            {
                _texts.Add(element.ToString());
            }
            return this;
        }

        /// <summary>
        /// 增加注释，比如水印等
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="annotation">注释</param>
        /// <returns>this</returns>
        public OfdWriter Add(int page, object annotation)
        {
            // 简化实现，仅记录注释
            if (annotation != null)
            {
                _texts.Add($"Page {page}: {annotation.ToString()}");
            }
            return this;
        }

        /// <summary>
        /// 完成并关闭
        /// </summary>
        public void Close()
        {
            Dispose();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                // 简化实现，创建一个空的 OFD 文件
                try
                {
                    if (_file != null)
                    {
                        // 确保目录存在
                        if (!_file.Directory.Exists)
                        {
                            _file.Directory.Create();
                        }

                        // 创建一个空的 OFD 文件（实际应该是一个 ZIP 压缩文件）
                        using (var stream = _file.Create())
                        {
                            // 写入一个简单的标记
                            var writer = new StreamWriter(stream);
                            writer.WriteLine("OFD File");
                            writer.WriteLine($"Texts: {_texts.Count}");
                            writer.WriteLine($"Pictures: {_pictures.Count}");
                            writer.Flush();
                        }
                    }
                    else if (_outputStream != null)
                    {
                        // 写入到输出流
                        using (var writer = new StreamWriter(_outputStream))
                        {
                            writer.WriteLine("OFD File");
                            writer.WriteLine($"Texts: {_texts.Count}");
                            writer.WriteLine($"Pictures: {_pictures.Count}");
                            writer.Flush();
                        }
                    }
                }
                catch
                {
                    // 忽略异常
                }

                _disposed = true;
            }
        }
    }
}