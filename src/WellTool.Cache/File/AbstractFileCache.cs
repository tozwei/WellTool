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

namespace WellTool.Cache.File
{
    /// <summary>
    /// 文件缓存抽象类，以解决频繁读取文件引起的性能问题
    /// </summary>
    public abstract class AbstractFileCache
    {
        private static readonly long SerialVersionUid = 1L;

        /// <summary>
        /// 容量
        /// </summary>
        protected int _capacity;

        /// <summary>
        /// 缓存的最大文件大小，文件大于此大小时将不被缓存
        /// </summary>
        protected int _maxFileSize;

        /// <summary>
        /// 默认超时时间，0表示无默认超时
        /// </summary>
        protected long _timeout;

        /// <summary>
        /// 已使用缓存空间
        /// </summary>
        protected int _usedSize;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">缓存容量</param>
        /// <param name="maxFileSize">文件最大大小</param>
        /// <param name="timeout">默认超时时间，0表示无默认超时</param>
        public AbstractFileCache(int capacity, int maxFileSize, long timeout)
        {
            _capacity = capacity;
            _maxFileSize = maxFileSize;
            _timeout = timeout;
        }

        /// <summary>
        /// 获取缓存容量
        /// </summary>
        /// <returns>缓存容量（byte数）</returns>
        public int Capacity()
        {
            return _capacity;
        }

        /// <summary>
        /// 获取已使用空间大小
        /// </summary>
        /// <returns>已使用空间大小（byte数）</returns>
        public int GetUsedSize()
        {
            return _usedSize;
        }

        /// <summary>
        /// 获取允许被缓存文件的最大byte数
        /// </summary>
        /// <returns>最大文件大小</returns>
        public int MaxFileSize()
        {
            return _maxFileSize;
        }

        /// <summary>
        /// 获取超时时间
        /// </summary>
        /// <returns>超时时间（毫秒）</returns>
        public long Timeout()
        {
            return _timeout;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 获得缓存过的文件bytes
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>缓存过的文件bytes</returns>
        public abstract byte[] GetFileBytes(string path);

        /// <summary>
        /// 获得缓存过的文件bytes
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>缓存过的文件bytes</returns>
        public abstract byte[] GetFileBytes(FileInfo file);

        /// <summary>
        /// 获取缓存的文件数
        /// </summary>
        /// <returns>缓存的文件数</returns>
        public abstract int GetCachedFilesCount();
    }
}
