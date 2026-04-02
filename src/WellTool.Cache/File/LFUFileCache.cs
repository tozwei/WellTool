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
using System.Collections.Generic;
using System.IO;

namespace WellTool.Cache.File
{
    /// <summary>
    /// 使用LFU缓存文件，以解决频繁读取文件引起的性能问题
    /// </summary>
    public class LFUFileCache : AbstractFileCache
    {
        private static readonly long SerialVersionUid = 1L;

        /// <summary>
        /// 缓存实现
        /// </summary>
        private readonly LFUCacheInner _cache;

        /// <summary>
        /// 构造
        /// 最大文件大小为缓存容量的一半
        /// 默认无超时
        /// </summary>
        /// <param name="capacity">缓存容量</param>
        public LFUFileCache(int capacity)
            : this(capacity, capacity / 2, 0)
        {
        }

        /// <summary>
        /// 构造
        /// 默认无超时
        /// </summary>
        /// <param name="capacity">缓存容量</param>
        /// <param name="maxFileSize">最大文件大小</param>
        public LFUFileCache(int capacity, int maxFileSize)
            : this(capacity, maxFileSize, 0)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="capacity">缓存容量</param>
        /// <param name="maxFileSize">文件最大大小</param>
        /// <param name="timeout">默认超时时间，0表示无默认超时</param>
        public LFUFileCache(int capacity, int maxFileSize, long timeout)
            : base(capacity, maxFileSize, timeout)
        {
            _cache = new LFUCacheInner(capacity, timeout, this);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public override void Clear()
        {
            _cache.Clear();
            _usedSize = 0;
        }

        /// <summary>
        /// 获得缓存过的文件bytes
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>缓存过的文件bytes</returns>
        public override byte[] GetFileBytes(string path)
        {
            return GetFileBytes(new FileInfo(path));
        }

        /// <summary>
        /// 获得缓存过的文件bytes
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>缓存过的文件bytes</returns>
        public override byte[] GetFileBytes(FileInfo file)
        {
            if (!file.Exists)
            {
                return null;
            }

            byte[] bytes = _cache.Get(file);
            if (bytes != null)
            {
                return bytes;
            }

            // 读取文件
            bytes = ReadBytes(file);

            if (_maxFileSize > 0 && file.Length > _maxFileSize)
            {
                // 大于缓存空间，不缓存，直接返回
                return bytes;
            }

            _usedSize += bytes.Length;

            // 文件放入缓存
            _cache.Put(file, bytes);

            return bytes;
        }

        /// <summary>
        /// 获取缓存的文件数
        /// </summary>
        /// <returns>缓存的文件数</returns>
        public override int GetCachedFilesCount()
        {
            return _cache.Size();
        }

        /// <summary>
        /// 读取文件字节
        /// </summary>
        private static byte[] ReadBytes(FileInfo file)
        {
            using (var fs = file.OpenRead())
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 内部LFU缓存实现
        /// </summary>
        private class LFUCacheInner : LFUCache<FileInfo, byte[]>
        {
            private readonly LFUFileCache _outer;

            public LFUCacheInner(int capacity, long timeout, LFUFileCache outer)
                : base(capacity, timeout)
            {
                _outer = outer;
            }

            public override bool IsFull()
            {
                return _outer._usedSize > _outer._capacity;
            }

            protected override void OnRemove(FileInfo key, byte[] cachedObject)
            {
                base.OnRemove(key, cachedObject);
                _outer._usedSize -= cachedObject.Length;
            }
        }
    }
}
