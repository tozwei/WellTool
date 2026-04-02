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
    /// 借助{@link StringBuilder} 提供快读的字符串写出，相比.NET的StringWriter非线程安全，速度更快。
    /// </summary>
    public class FastStringWriter : TextWriter
    {
        private readonly StringBuilder _builder;

        /// <summary>
        /// 构造
        /// </summary>
        public FastStringWriter() : this(16)
        { }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="initialSize">初始容量</param>
        public FastStringWriter(int initialSize)
        {
            if (initialSize < 0)
            {
                initialSize = 16;
            }
            _builder = new StringBuilder(initialSize);
        }

        public override void Write(int value)
        {
            _builder.Append((char)value);
        }

        public override void Write(string value)
        {
            _builder.Append(value);
        }

        public override void Write(string value, int index, int count)
        {
            _builder.Append(value, index, count);
        }

        public override void Write(char[] buffer)
        {
            _builder.Append(buffer);
        }

        public override void Write(char[] buffer, int index, int count)
        {
            if ((index < 0) || (index > buffer.Length) || (count < 0) ||
                ((index + count) > buffer.Length) || ((index + count) < 0))
            {
                throw new IndexOutOfRangeException();
            }
            else if (count == 0)
            {
                return;
            }
            _builder.Append(buffer, index, count);
        }

        public override void Flush()
        {
            // Nothing to be flushed
        }

        public override void Close()
        {
            // Nothing to be closed
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}