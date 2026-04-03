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

using System;
using System.IO;

namespace WellTool.Core.IO
{
    /// <summary>
    /// 限制读取最大长度的流实现
    /// </summary>
    public class LimitedInputStream : Stream
    {
        private readonly Stream _source;
        private readonly long _maxSize;
        private long _currentPos;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="source">源流</param>
        /// <param name="maxSize">限制最大读取量，单位byte</param>
        public LimitedInputStream(Stream source, long maxSize)
        {
            this._source = source;
            this._maxSize = maxSize;
        }

        /// <summary>
        /// 获取底层流
        /// </summary>
        public Stream Source => _source;

        /// <summary>
        /// 当前读取位置
        /// </summary>
        public long CurrentPos => _currentPos;

        public override bool CanRead => _source.CanRead;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => _source.Length;

        public override long Position
        {
            get => _source.Position;
            set => _source.Position = value;
        }

        public override void Flush()
        {
            _source.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int readCount = _source.Read(buffer, offset, count);
            if (readCount > 0)
            {
                _currentPos += readCount;
                CheckPos();
            }
            return readCount;
        }

        public override int ReadByte()
        {
            int b = _source.ReadByte();
            if (b != -1)
            {
                _currentPos++;
                CheckPos();
            }
            return b;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            _source.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        private void CheckPos()
        {
            if (_currentPos > _maxSize)
            {
                throw new InvalidOperationException("Read limit exceeded");
            }
        }
    }
}
