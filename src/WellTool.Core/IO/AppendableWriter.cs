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
    /// 同时继承{@link TextWriter}和实现{@link IAppendable}的聚合类，用于适配两种接口操作
    /// 实现来自：jodd
    /// </summary>
    public class AppendableWriter : TextWriter
    {
        private readonly StringBuilder _appendable;
        private bool _closed;

        public AppendableWriter(StringBuilder appendable)
        {
            _appendable = appendable;
            _closed = false;
        }

        public override void Write(char[] buffer, int index, int count)
        {
            CheckNotClosed();
            _appendable.Append(buffer, index, count);
        }

        public override void Write(int value)
        {
            CheckNotClosed();
            _appendable.Append((char)value);
        }

        public override void Write(string value, int index, int count)
        {
            CheckNotClosed();
            _appendable.Append(value, index, count);
        }

        public override void Write(string value)
        {
            CheckNotClosed();
            _appendable.Append(value);
        }

        public override void Write(char[] buffer)
        {
            CheckNotClosed();
            _appendable.Append(buffer);
        }

        public override void Flush()
        {
            CheckNotClosed();
            // StringBuilder 不需要显式刷新
        }

        /// <summary>
        /// 检查Writer是否已经被关闭
        /// </summary>
        /// <exception cref="IOException">IO异常</exception>
        private void CheckNotClosed()
        {
            if (_closed)
            {
                throw new IOException($"Writer is closed!{this}");
            }
        }

        public override void Close()
        {
            if (!_closed)
            {
                Flush();
                _closed = true;
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }
}