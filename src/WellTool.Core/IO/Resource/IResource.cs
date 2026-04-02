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
    /// 资源接口定义<br>
    /// <p>资源是数据表示的统称，我们可以将任意的数据封装为一个资源，然后读取其内容。</p>
    /// <p>资源可以是文件、URL、ClassPath中的文件亦或者jar(zip)包中的文件。</p>
    /// <p>
    ///     提供资源接口的意义在于，我们可以使用一个方法接收任意类型的数据，从而处理数据，
    ///     无需专门针对File、InputStream等写多个重载方法，同时也为更好的扩展提供了可能。
    /// </p>
    /// <p>使用非常简单，假设我们需要从classpath中读取一个xml，我们不用关心这个文件在目录中还是在jar中：</p>
    /// <pre>
    ///     IResource resource = new ClassPathResource("test.xml");
    ///     string xmlStr = resource.ReadUtf8Str();
    /// </pre>
    /// <p>同样，我们可以自己实现IResource接口，按照业务需要从任意位置读取数据，比如从数据库中。</p>
    /// </summary>
    public interface IResource
    {
        /// <summary>
        /// 获取资源名，例如文件资源的资源名为文件名
        /// </summary>
        /// <returns>资源名</returns>
        string GetName();

        /// <summary>
        /// 获得解析后的{@link System.Uri}，无对应Uri的返回{@code null}
        /// </summary>
        /// <returns>解析后的{@link System.Uri}</returns>
        Uri GetUri();

        /// <summary>
        /// 获得 {@link Stream}
        /// </summary>
        /// <returns>{@link Stream}</returns>
        Stream GetStream();

        /// <summary>
        /// 检查资源是否变更<br>
        /// 一般用于文件类资源，检查文件是否被修改过。
        /// </summary>
        /// <returns>是否变更</returns>
        bool IsModified();

        /// <summary>
        /// 将资源内容写出到流，不关闭输出流，但是关闭资源流
        /// </summary>
        /// <param name="output">输出流</param>
        /// <exception cref="IORuntimeException">IO异常</exception>
        void WriteTo(Stream output);

        /// <summary>
        /// 获得StreamReader
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>{@link StreamReader}</returns>
        StreamReader GetReader(Encoding encoding);

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装{@link IOException}</exception>
        string ReadStr(Encoding encoding);

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装IOException</exception>
        string ReadUtf8Str();

        /// <summary>
        /// 读取资源内容，读取完毕后会关闭流<br>
        /// 关闭流并不影响下一次读取
        /// </summary>
        /// <returns>读取资源内容</returns>
        /// <exception cref="IORuntimeException">包装IOException</exception>
        byte[] ReadBytes();
    }
}