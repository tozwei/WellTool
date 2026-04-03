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

namespace WellTool.Core.IO.Copy
{
    /// <summary>
    /// IO拷贝抽象，可自定义包括缓存、进度条等信息<br>
    /// 此对象非线程安全
    /// </summary>
    /// <typeparam name="S">拷贝源类型，如Stream、TextReader等</typeparam>
    /// <typeparam name="T">拷贝目标类型，如Stream、TextWriter等</typeparam>
    public abstract class IoCopier<S, T>
    {
        protected readonly int BufferSize;
        /// <summary>
        /// 拷贝总数
        /// </summary>
        protected readonly long Count;

        /// <summary>
        /// 进度条
        /// </summary>
        protected StreamProgress Progress;

        /// <summary>
        /// 是否每次写出一个buffer内容就执行flush
        /// </summary>
        protected bool FlushEveryBuffer;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="bufferSize">缓存大小，&lt; 0 表示默认{@link IoUtil#DefaultBufferSize}</param>
        /// <param name="count">拷贝总数，-1表示无限制</param>
        /// <param name="progress">进度条</param>
        public IoCopier(int bufferSize, long count, StreamProgress progress)
        {
            BufferSize = bufferSize > 0 ? bufferSize : WellTool.Core.Util.IOUtil.DEFAULT_BUFFER_SIZE;
            Count = count <= 0 ? long.MaxValue : count;
            Progress = progress;
        }

        /// <summary>
        /// 执行拷贝
        /// </summary>
        /// <param name="source">拷贝源，如Stream、TextReader等</param>
        /// <param name="target">拷贝目标，如Stream、TextWriter等</param>
        /// <returns>拷贝的实际长度</returns>
        public abstract long Copy(S source, T target);

        /// <summary>
        /// 缓存大小，取默认缓存和目标长度最小值
        /// </summary>
        /// <param name="count">目标长度</param>
        /// <returns>缓存大小</returns>
        protected int BufferSizeValue(long count)
        {
            return (int)System.Math.Min(BufferSize, count);
        }

        /// <summary>
        /// 设置是否每次写出一个buffer内容就执行flush
        /// </summary>
        /// <param name="flushEveryBuffer">是否每次写出一个buffer内容就执行flush</param>
        /// <returns>this</returns>
        public IoCopier<S, T> SetFlushEveryBuffer(bool flushEveryBuffer)
        {
            FlushEveryBuffer = flushEveryBuffer;
            return this;
        }
    }
}