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
    /// 基于byte[]的资源获取器<br>
    /// 注意：此对象中GetUri方法始终返回null
    /// </summary>
    public class BytesResource : IResource
    {
        private readonly byte[] _bytes;
        private readonly string _name;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bytes">字节数组</param>
        public BytesResource(byte[] bytes) : this(bytes, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="name">资源名称</param>
        public BytesResource(byte[] bytes, string name)
        {
            _bytes = bytes;
            _name = name;
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
            return null;
        }

        /// <summary>
        /// 获得 {@link Stream}
        /// </summary>
        /// <returns>{@link Stream}</returns>
        public Stream GetStream()
        {
            return new MemoryStream(_bytes);
        }

        /// <summary>
        /// 检查资源是否变更<br>
        /// 一般用于文件类资源，检查文件是否被修改过。
        /// </summary>
        /// <returns>是否变更</returns>
        public bool IsModified()
        {
            return false;
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
            return new StreamReader(new MemoryStream(_bytes), encoding);
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
            return encoding.GetString(_bytes);
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
            return _bytes;
        }
    }
}