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
using System.Text;

namespace WellTool.Core.IO.Resource
{
    /// <summary>
    /// 文件资源访问对象，支持文件路径访问
    /// </summary>
    public class FileResource : IResource
    {
        private readonly FileInfo _file;
        private readonly long _lastModified;
        private readonly string _name;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">文件绝对路径或相对路径</param>
        public FileResource(string path)
        {
            _file = new FileInfo(path);
            _lastModified = _file.LastWriteTime.Ticks;
            _name = _file.Name;
        }

        /// <summary>
        /// 构造，文件名使用文件本身的名字，带扩展名
        /// </summary>
        /// <param name="file">文件</param>
        public FileResource(FileInfo file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file", "File must be not null !");
            }
            _file = file;
            _lastModified = _file.LastWriteTime.Ticks;
            _name = _file.Name;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="fileName">文件名，带扩展名，如果为null获取文件本身的文件名</param>
        public FileResource(FileInfo file, string fileName)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file", "File must be not null !");
            }
            _file = file;
            _lastModified = _file.LastWriteTime.Ticks;
            _name = string.IsNullOrEmpty(fileName) ? file.Name : fileName;
        }

        /// <summary>
        /// 获取资源名，例如文件资源的资源名为文件名
        /// </summary>
        /// <returns>资源名</returns>
        public string GetName()
        {
            return _name;
        }

        /// <summary>
        /// 获得解析后的{@link System.Uri}，无对应Uri的返回{@code null}
        /// </summary>
        /// <returns>解析后的{@link System.Uri}</returns>
        public Uri GetUri()
        {
            return new Uri(_file.FullName);
        }

        /// <summary>
        /// 获得 {@link Stream}
        /// </summary>
        /// <returns>{@link Stream}</returns>
        public Stream GetStream()
        {
            if (!_file.Exists)
            {
                throw new NoResourceException($"File [{_file.FullName}] not exist!");
            }
            return _file.OpenRead();
        }

        /// <summary>
        /// 检查资源是否变更<br>
        /// 一般用于文件类资源，检查文件是否被修改过。
        /// </summary>
        /// <returns>是否变更</returns>
        public bool IsModified()
        {
            return _lastModified != _file.LastWriteTime.Ticks;
        }

        /// <summary>
        /// 将资源内容写出到流，不关闭输出流，但是关闭资源流
        /// </summary>
        /// <param name="output">输出流</param>
        /// <exception cref="IORuntimeException">IO异常</exception>
        public void WriteTo(Stream output)
        {
            using (var input = GetStream())
            {
                input.CopyTo(output);
            }
        }

        /// <summary>
        /// 获得StreamReader
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>{@link StreamReader}</returns>
        public StreamReader GetReader(Encoding encoding)
        {
            return new StreamReader(GetStream(), encoding);
        }

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装{@link IOException}</exception>
        public string ReadStr(Encoding encoding)
        {
            using (var reader = GetReader(encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装IOException</exception>
        public string ReadUtf8Str()
        {
            return ReadStr(Encoding.UTF8);
        }

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装IOException</exception>
        public byte[] ReadBytes()
        {
            using (var stream = GetStream())
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <returns>文件</returns>
        public FileInfo GetFile()
        {
            return _file;
        }

        /// <summary>
        /// 返回路径
        /// </summary>
        /// <returns>返回文件路径</returns>
        public override string ToString()
        {
            return _file.FullName;
        }
    }
}