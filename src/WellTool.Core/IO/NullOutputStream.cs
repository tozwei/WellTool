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
    /// 此OutputStream写出数据到<b>/dev/null</b>，即忽略所有数据
    /// </summary>
    public class NullOutputStream : Stream
    {
        /// <summary>
        /// 单例
        /// </summary>
        public static readonly NullOutputStream Null = new NullOutputStream();

        /// <summary>
        /// 什么也不做，写出到 /dev/null
        /// </summary>
        public override void Write(byte[] buffer, int offset, int count)
        {
            // to /dev/null
        }

        /// <summary>
        /// 什么也不做，写出到 /dev/null
        /// </summary>
        public override void WriteByte(byte value)
        {
            // to /dev/null
        }

        /// <summary>
        /// 什么也不做，写出到 /dev/null
        /// </summary>
        public override void Flush()
        {
            // to /dev/null
        }

        /// <summary>
        /// 读取操作不支持
        /// </summary>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 读取操作不支持
        /// </summary>
        public override int ReadByte()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// 始终返回0
        /// </summary>
        public override long Length => 0;

        /// <summary>
        /// 始终返回0
        /// </summary>
        public override long Position
        {
            get => 0;
            set { }
        }

        /// <summary>
        /// 始终返回False
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// 始终返回True
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// 始终返回True
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Seek操作不支持
        /// </summary>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// SetLength操作不支持
        /// </summary>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }
    }
}
