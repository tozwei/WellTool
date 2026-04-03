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

namespace WellTool.Core.IO.Copy
{
    /// <summary>
    /// {@link TextReader} 向 {@link TextWriter} 拷贝
    /// </summary>
    public class ReaderWriterCopier : IoCopier<TextReader, TextWriter>
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ReaderWriterCopier() : this(WellTool.Core.Util.IOUtil.DEFAULT_BUFFER_SIZE)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bufferSize">缓存大小</param>
        public ReaderWriterCopier(int bufferSize) : this(bufferSize, -1)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bufferSize">缓存大小</param>
        /// <param name="count">拷贝总数</param>
        public ReaderWriterCopier(int bufferSize, long count) : this(bufferSize, count, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bufferSize">缓存大小</param>
        /// <param name="count">拷贝总数</param>
        /// <param name="progress">进度条</param>
        public ReaderWriterCopier(int bufferSize, long count, StreamProgress progress) : base(bufferSize, count, progress)
        {
        }

        public override long Copy(TextReader source, TextWriter target)
        {
            if (source == null)
            {
                throw new System.ArgumentNullException("source", "InputStream is null !");
            }
            if (target == null)
            {
                throw new System.ArgumentNullException("target", "OutputStream is null !");
            }

            var progress = this.Progress;
            if (progress != null)
            {
                progress.Start();
            }
            long size;
            try
            {
                size = DoCopy(source, target, new char[BufferSizeValue(Count)], progress);
                target.Flush();
            }
            catch (System.Exception e)
            {
                throw new IORuntimeException(e);
            }

            if (progress != null)
            {
                progress.Finish();
            }
            return size;
        }

        /// <summary>
        /// 执行拷贝，如果限制最大长度，则按照最大长度读取，否则一直读取直到遇到-1
        /// </summary>
        /// <param name="source">{@link TextReader}</param>
        /// <param name="target">{@link TextWriter}</param>
        /// <param name="buffer">缓存</param>
        /// <param name="progress">进度条</param>
        /// <returns>拷贝总长度</returns>
        private long DoCopy(TextReader source, TextWriter target, char[] buffer, StreamProgress progress)
        {
            long numToRead = Count > 0 ? Count : long.MaxValue;
            long total = 0;

            int read;
            while (numToRead > 0)
            {
                read = source.Read(buffer, 0, BufferSizeValue(numToRead));
                if (read < 0)
                {
                    // 提前读取到末尾
                    break;
                }
                target.Write(buffer, 0, read);
                if (FlushEveryBuffer)
                {
                    target.Flush();
                }

                numToRead -= read;
                total += read;
                if (progress != null)
                {
                    progress.Progress(Count, total);
                }
            }

            return total;
        }
    }
}