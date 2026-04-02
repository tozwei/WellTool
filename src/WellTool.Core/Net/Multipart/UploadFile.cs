using System;
using System.IO;
using WellTool.Core.IO;
using WellTool.Core.Util;

namespace WellTool.Core.Net.Multipart
{
    /// <summary>
    /// 上传的文件对象
    /// </summary>
    public class UploadFile
    {
        private const string TMP_FILE_PREFIX = "hutool-";
        private const string TMP_FILE_SUFFIX = ".upload.tmp";

        private readonly UploadFileHeader _header;
        private readonly UploadSetting _setting;

        private long _size = -1;

        // 文件流（小文件位于内存中）
        private byte[] _data;
        // 临时文件（大文件位于临时文件夹中）
        private FileInfo _tempFile;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="header">头部信息</param>
        /// <param name="setting">上传设置</param>
        public UploadFile(UploadFileHeader header, UploadSetting setting)
        {
            _header = header;
            _setting = setting;
        }

        /// <summary>
        /// 从磁盘或者内存中删除这个文件
        /// </summary>
        public void Delete()
        {
            if (_tempFile != null)
            {
                try
                {
                    _tempFile.Delete();
                }
                catch
                {
                    // 忽略异常
                }
            }
            if (_data != null)
            {
                _data = null;
            }
        }

        /// <summary>
        /// 将上传的文件写入指定的目标文件路径，自动创建文件
        /// 写入后原临时文件会被删除
        /// </summary>
        /// <param name="destPath">目标文件路径</param>
        /// <returns>目标文件</returns>
        /// <exception cref="IOException">IO异常</exception>
        public FileInfo Write(string destPath)
        {
            if (_data != null || _tempFile != null)
            {
                return Write(FileUtil.File(destPath));
            }
            return null;
        }

        /// <summary>
        /// 将上传的文件写入目标文件
        /// 写入后原临时文件会被删除
        /// </summary>
        /// <param name="destination">目标文件</param>
        /// <returns>目标文件</returns>
        /// <exception cref="IOException">IO异常</exception>
        public FileInfo Write(FileInfo destination)
        {
            AssertValid();

            if (destination.Exists && destination.Attributes.HasFlag(FileAttributes.Directory))
            {
                destination = new FileInfo(Path.Combine(destination.FullName, _header.FileName));
            }
            if (_data != null)
            {
                // 内存中
                FileUtil.WriteBytes(_data, destination.FullName);
                _data = null;
            }
            else
            {
                // 临时文件
                if (_tempFile == null)
                {
                    throw new NullReferenceException("Temp file is null!");
                }
                if (!_tempFile.Exists)
                {
                    throw new FileNotFoundException($"Temp file: [{_tempFile.FullName}] not exist!");
                }

                FileUtil.Move(_tempFile.FullName, destination.FullName, true);
            }
            return destination;
        }

        /// <summary>
        /// 获得文件字节流
        /// </summary>
        /// <returns>文件字节流</returns>
        /// <exception cref="IOException">IO异常</exception>
        public byte[] GetFileContent()
        {
            AssertValid();

            if (_data != null)
            {
                return _data;
            }
            if (_tempFile != null)
            {
                return FileUtil.ReadBytes(_tempFile.FullName);
            }
            return null;
        }

        /// <summary>
        /// 获得文件流
        /// </summary>
        /// <returns>文件流</returns>
        /// <exception cref="IOException">IO异常</exception>
        public Stream GetFileInputStream()
        {
            AssertValid();

            if (_data != null)
            {
                return IoUtil.ToBuffered(IoUtil.ToStream(_data));
            }
            if (_tempFile != null)
            {
                return IoUtil.ToBuffered(IoUtil.ToStream(_tempFile.FullName));
            }
            return null;
        }

        /// <summary>
        /// 上传文件头部信息
        /// </summary>
        public UploadFileHeader Header => _header;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName => _header?.FileName;

        /// <summary>
        /// 上传文件的大小，&gt; 0 表示未上传
        /// </summary>
        public long Size => _size;

        /// <summary>
        /// 是否上传成功
        /// </summary>
        public bool IsUploaded => _size > 0;

        /// <summary>
        /// 文件是否在内存中
        /// </summary>
        public bool IsInMemory => _data != null;

        /// <summary>
        /// 处理上传表单流，提取出文件
        /// </summary>
        /// <param name="input">上传表单的流</param>
        /// <returns>是否成功</returns>
        /// <exception cref="IOException">IO异常</exception>
        protected bool ProcessStream(MultipartRequestInputStream input)
        {
            if (!IsAllowedExtension())
            {
                // 非允许的扩展名
                _size = input.SkipToBoundary();
                return false;
            }
            _size = 0;

            // 处理内存文件
            int memoryThreshold = _setting.MemoryThreshold;
            if (memoryThreshold > 0)
            {
                using (var baos = new MemoryStream(memoryThreshold))
                {
                    long written = input.Copy(baos, memoryThreshold);
                    _data = baos.ToArray();
                    if (written <= memoryThreshold)
                    {
                        // 文件存放于内存
                        _size = _data.Length;
                        return true;
                    }
                }
            }

            // 处理硬盘文件
            var tempDir = FileUtil.Touch(_setting.TmpUploadPath);
            _tempFile = FileUtil.CreateTempFile(TMP_FILE_PREFIX, TMP_FILE_SUFFIX, tempDir, false);
            using (var output = FileUtil.GetOutputStream(_tempFile.FullName))
            {
                if (_data != null)
                {
                    _size = _data.Length;
                    output.Write(_data);
                    _data = null; // not needed anymore
                }
                long maxFileSize = _setting.MaxFileSize;
                try
                {
                    if (maxFileSize == -1)
                    {
                        _size += input.Copy(output);
                        return true;
                    }
                    _size += input.Copy(output, maxFileSize - _size + 1); // one more byte to detect larger files
                    if (_size > maxFileSize)
                    {
                        // 超出上传大小限制
                        try
                        {
                            _tempFile.Delete();
                        }
                        catch
                        {
                            // 忽略异常
                        }
                        _tempFile = null;
                        input.SkipToBoundary();
                        return false;
                    }
                }
                finally
                {
                    output.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// 是否为允许的扩展名
        /// </summary>
        /// <returns>是否为允许的扩展名</returns>
        private bool IsAllowedExtension()
        {
            var exts = _setting.FileExts;
            bool isAllow = _setting.IsAllowFileExts;
            if (exts == null || exts.Length == 0)
            {
                // 如果给定扩展名列表为空，当允许扩展名时全部允许，否则全部禁止
                return isAllow;
            }

            var fileNameExt = FileUtil.ExtName(FileName);
            foreach (var fileExtension in exts)
            {
                if (fileNameExt.Equals(fileExtension, StringComparison.OrdinalIgnoreCase))
                {
                    return isAllow;
                }
            }

            // 未匹配到扩展名，如果为允许列表，返回false， 否则true
            return !isAllow;
        }

        /// <summary>
        /// 断言是否文件流可用
        /// </summary>
        /// <exception cref="IOException">IO异常</exception>
        private void AssertValid()
        {
            if (!IsUploaded)
            {
                throw new IOException(StrUtil.Format("File [{}] upload fail", FileName));
            }
        }
    }
}