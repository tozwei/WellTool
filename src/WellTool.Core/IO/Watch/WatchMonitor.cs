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
using System.Threading;

namespace WellTool.Core.IO.Watch
{
    /// <summary>
    /// 路径监听器
    /// 
    /// 监听器可监听目录或文件
    /// 如果监听的Path不存在，则递归创建空目录然后监听此空目录
    /// 递归监听目录时，并不会监听新创建的目录
    /// </summary>
    public class WatchMonitor : IDisposable
    {
        private readonly FileSystemWatcher? _fileSystemWatcher;
        private readonly FileSystemWatcher? _directoryWatcher;
        private Watcher? _watcher;
        private readonly int _maxDepth;
        private readonly string? _filePath;
        private bool _isClosed;

        /// <summary>
        /// 事件丢失
        /// </summary>
        public static readonly WatcherEventTypes OVERFLOW = 0;
        /// <summary>
        /// 修改事件
        /// </summary>
        public static readonly WatcherEventTypes ENTRY_MODIFY = WatcherEventTypes.Modified;
        /// <summary>
        /// 创建事件
        /// </summary>
        public static readonly WatcherEventTypes ENTRY_CREATE = WatcherEventTypes.Created;
        /// <summary>
        /// 删除事件
        /// </summary>
        public static readonly WatcherEventTypes ENTRY_DELETE = WatcherEventTypes.Deleted;
        /// <summary>
        /// 全部事件
        /// </summary>
        public static readonly WatcherEventTypes EVENTS_ALL = WatcherEventTypes.All;

        /// <summary>
        /// 创建并初始化监听
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="events">监听的事件列表</param>
        /// <returns>监听对象</returns>
        public static WatchMonitor Create(string path, WatcherEventTypes events = WatcherEventTypes.All)
        {
            return Create(path, 0, events);
        }

        /// <summary>
        /// 创建并初始化监听
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="events">监听的事件列表</param>
        /// <returns>监听对象</returns>
        public static WatchMonitor Create(string path, int maxDepth, WatcherEventTypes events = WatcherEventTypes.All)
        {
            return new WatchMonitor(path, maxDepth, events);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="events">监听的事件列表</param>
        public WatchMonitor(string path, WatcherEventTypes events = WatcherEventTypes.All)
            : this(path, 0, events)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="events">监听的事件列表</param>
        public WatchMonitor(string path, int maxDepth, WatcherEventTypes events = WatcherEventTypes.All)
        {
            this._maxDepth = maxDepth;
            this._filePath = null;

            // 确保目录存在
            string directoryPath = path;
            if (System.IO.File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                this._filePath = fileInfo.FullName;
                directoryPath = fileInfo.DirectoryName ?? path;
            }
            else if (Directory.Exists(path))
            {
                directoryPath = path;
            }
            else
            {
                // 路径不存在，创建目录
                Directory.CreateDirectory(path);
            }

            // 创建文件监听器
            _fileSystemWatcher = new FileSystemWatcher(directoryPath)
            {
                NotifyFilter = GetNotifyFilters(events),
                IncludeSubdirectories = maxDepth > 1,
                EnableRaisingEvents = false
            };

            if (!string.IsNullOrEmpty(_filePath))
            {
                _fileSystemWatcher.Filter = Path.GetFileName(_filePath);
            }

            _fileSystemWatcher.Created += OnCreated;
            _fileSystemWatcher.Changed += OnChanged;
            _fileSystemWatcher.Deleted += OnDeleted;
            _fileSystemWatcher.Error += OnError;
        }

        private NotifyFilters GetNotifyFilters(WatcherEventTypes events)
        {
            var filters = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite;
            return filters;
        }

        /// <summary>
        /// 设置监听
        /// </summary>
        /// <param name="watcher">监听</param>
        /// <returns>this</returns>
        public WatchMonitor SetWatcher(Watcher watcher)
        {
            this._watcher = watcher;
            return this;
        }

        /// <summary>
        /// 开始监听事件，阻塞当前进程
        /// </summary>
        public void Watch()
        {
            Watch(this._watcher);
        }

        /// <summary>
        /// 开始监听事件，阻塞当前进程
        /// </summary>
        /// <param name="watcher">监听</param>
        public void Watch(Watcher watcher)
        {
            if (_isClosed)
            {
                throw new WatchException("Watch Monitor is closed !");
            }

            if (watcher == null)
            {
                watcher = this._watcher ?? new SimpleWatcher();
            }

            // 启动监听
            if (_fileSystemWatcher != null)
            {
                _fileSystemWatcher.EnableRaisingEvents = true;
            }

            // 阻塞直到关闭
            var resetEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                Stop();
                resetEvent.Set();
            };

            // 等待关闭信号
            resetEvent.WaitOne();
        }

        /// <summary>
        /// 当监听目录时，监听目录的最大深度
        /// </summary>
        /// <param name="maxDepth">最大深度，当设置值为1（或小于1）时，表示不递归监听子目录</param>
        /// <returns>this</returns>
        public WatchMonitor SetMaxDepth(int maxDepth)
        {
            if (_fileSystemWatcher != null)
            {
                _fileSystemWatcher.IncludeSubdirectories = maxDepth > 1;
            }
            return this;
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            _isClosed = true;
            if (_fileSystemWatcher != null)
            {
                _fileSystemWatcher.EnableRaisingEvents = false;
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _watcher?.OnCreate(e, Path.GetDirectoryName(e.FullPath) ?? string.Empty);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            _watcher?.OnModify(e, Path.GetDirectoryName(e.FullPath) ?? string.Empty);
        }

        private void OnDeleted(object sender, FileSystemEventArgs e)
        {
            _watcher?.OnDelete(e, Path.GetDirectoryName(e.FullPath) ?? string.Empty);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            _watcher?.OnError(e, string.Empty);
        }

        public void Dispose()
        {
            Stop();
            _fileSystemWatcher?.Dispose();
            _directoryWatcher?.Dispose();
        }
    }
}
