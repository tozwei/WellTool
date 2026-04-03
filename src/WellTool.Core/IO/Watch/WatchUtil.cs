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
    /// 监听工具类，主要负责文件监听器的快捷创建
    /// </summary>
    public static class WatchUtil
    {
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
        /// 创建并初始化监听
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="events">监听的事件列表</param>
        /// <returns>监听对象</returns>
        public static WatchMonitor Create(FileInfo file, WatcherEventTypes events = WatcherEventTypes.All)
        {
            return Create(file.FullName, 0, events);
        }

        /// <summary>
        /// 创建并初始化监听
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="events">监听的事件列表</param>
        /// <returns>监听对象</returns>
        public static WatchMonitor Create(FileInfo file, int maxDepth, WatcherEventTypes events = WatcherEventTypes.All)
        {
            return Create(file.FullName, maxDepth, events);
        }

        /// <summary>
        /// 创建并初始化监听，监听所有事件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateAll(string path, Watcher watcher)
        {
            return CreateAll(path, 0, watcher);
        }

        /// <summary>
        /// 创建并初始化监听，监听所有事件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateAll(string path, int maxDepth, Watcher watcher)
        {
            var watchMonitor = Create(path, maxDepth, WatcherEventTypes.All);
            watchMonitor.SetWatcher(watcher);
            return watchMonitor;
        }

        /// <summary>
        /// 创建并初始化监听，监听所有事件
        /// </summary>
        /// <param name="file">被监听文件</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateAll(FileInfo file, Watcher watcher)
        {
            return CreateAll(file.FullName, 0, watcher);
        }

        /// <summary>
        /// 创建并初始化监听，监听所有事件
        /// </summary>
        /// <param name="file">被监听文件</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateAll(FileInfo file, int maxDepth, Watcher watcher)
        {
            return CreateAll(file.FullName, maxDepth, watcher);
        }

        /// <summary>
        /// 创建并初始化监听，监听修改事件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateModify(string path, Watcher watcher)
        {
            return CreateModify(path, 0, watcher);
        }

        /// <summary>
        /// 创建并初始化监听，监听修改事件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateModify(string path, int maxDepth, Watcher watcher)
        {
            var watchMonitor = Create(path, maxDepth, WatcherEventTypes.Modified);
            watchMonitor.SetWatcher(watcher);
            return watchMonitor;
        }

        /// <summary>
        /// 创建并初始化监听，监听修改事件
        /// </summary>
        /// <param name="file">被监听文件</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateModify(FileInfo file, Watcher watcher)
        {
            return CreateModify(file.FullName, 0, watcher);
        }

        /// <summary>
        /// 创建并初始化监听，监听修改事件
        /// </summary>
        /// <param name="file">被监听文件</param>
        /// <param name="maxDepth">递归目录的最大深度，当小于2时不递归下层目录</param>
        /// <param name="watcher">Watcher</param>
        /// <returns>WatchMonitor</returns>
        public static WatchMonitor CreateModify(FileInfo file, int maxDepth, Watcher watcher)
        {
            return CreateModify(file.FullName, maxDepth, watcher);
        }
    }

    /// <summary>
    /// 监听事件类型
    /// </summary>
    [Flags]
    public enum WatcherEventTypes
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 创建事件
        /// </summary>
        Created = 1,
        /// <summary>
        /// 删除事件
        /// </summary>
        Deleted = 2,
        /// <summary>
        /// 修改事件
        /// </summary>
        Modified = 4,
        /// <summary>
        /// 全部事件
        /// </summary>
        All = Created | Deleted | Modified
    }
}
