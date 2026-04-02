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

using System.Text;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 读取带BOM头的流内容的Reader，如果非bom的流或无法识别的编码，则默认UTF-8<br>
    /// BOM定义：http://www.unicode.org/unicode/faq/utf_bom.html
    /// <ul>
    /// <li>00 00 FE FF = UTF-32, big-endian</li>
    /// <li>FF FE 00 00 = UTF-32, little-endian</li>
    /// <li>EF BB BF = UTF-8</li>
    /// <li>FE FF = UTF-16, big-endian</li>
    /// <li>FF FE = UTF-16, little-endian</li>
    /// </ul>
    /// </summary>
    public class BomReader : TextReader
    {
        private readonly StreamReader _reader;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="inputStream">流</param>
        public BomReader(Stream inputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream", "InputStream must be not null!");
            }

            var bin = inputStream is BOMInputStream ? (BOMInputStream)inputStream : new BOMInputStream(inputStream);
            var charset = bin.GetCharset();
            _reader = new StreamReader(bin, Encoding.GetEncoding(charset));
        }

        public override int Read(char[] buffer, int index, int count)
        {
            return _reader.Read(buffer, index, count);
        }

        public override int Read()
        {
            return _reader.Read();
        }

        public override int Peek()
        {
            return _reader.Peek();
        }

        public override void Close()
        {
            _reader.Close();
            base.Close();
        }
    }
}