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
using System.Threading;

namespace WellTool.Core.Thread
{
    /// <summary>
    /// 线程创建工厂类，此工厂可选配置：
    /// <pre>
    /// 1. 自定义线程命名前缀
    /// 2. 自定义是否守护线程
    /// </pre>
    /// </summary>
    public class NamedThreadFactory
    {
        /// <summary>
        /// 命名前缀
        /// </summary>
        private readonly string _prefix;
        /// <summary>
        /// 线程组
        /// </summary>
        private readonly ThreadGroup _group;
        /// <summary>
        /// 线程组
        /// </summary>
        private int _threadNumber = 1;
        /// <summary>
        /// 是否守护线程
        /// </summary>
        private readonly bool _isDaemon;
        /// <summary>
        /// 无法捕获的异常统一处理
        /// </summary>
        private readonly UnhandledExceptionEventHandler _handler;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="prefix">线程名前缀</param>
        /// <param name="isDaemon">是否守护线程</param>
        public NamedThreadFactory(string prefix, bool isDaemon)
            : this(prefix, null, isDaemon)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="prefix">线程名前缀</param>
        /// <param name="threadGroup">线程组，可以为null</param>
        /// <param name="isDaemon">是否守护线程</param>
        public NamedThreadFactory(string prefix, ThreadGroup threadGroup, bool isDaemon)
            : this(prefix, threadGroup, isDaemon, null)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="prefix">线程名前缀</param>
        /// <param name="threadGroup">线程组，可以为null</param>
        /// <param name="isDaemon">是否守护线程</param>
        /// <param name="handler">未捕获异常处理</param>
        public NamedThreadFactory(string prefix, ThreadGroup threadGroup, bool isDaemon, UnhandledExceptionEventHandler handler)
        {
            _prefix = string.IsNullOrWhiteSpace(prefix) ? "WellTool" : prefix;
            _group = threadGroup ?? CurrentThreadGroup();
            _isDaemon = isDaemon;
            _handler = handler;
        }

        /// <summary>
        /// 创建新线程
        /// </summary>
        /// <param name="action">线程执行的操作</param>
        /// <returns>新线程</returns>
        public Thread NewThread(Action action)
        {
            var threadName = $"{_prefix}{Interlocked.Increment(ref _threadNumber)}";
            var thread = new Thread(new ThreadStart(action), 0, threadName);

            // 守护线程
            if (thread.IsBackground != _isDaemon)
            {
                thread.IsBackground = _isDaemon;
            }

            // 异常处理
            if (_handler != null)
            {
                AppDomain.CurrentDomain.UnhandledException += _handler;
            }

            // 优先级
            if (thread.Priority != ThreadPriority.Normal)
            {
                thread.Priority = ThreadPriority.Normal;
            }

            return thread;
        }

        /// <summary>
        /// 获取当前线程组
        /// </summary>
        /// <returns>当前线程组</returns>
        private static ThreadGroup CurrentThreadGroup()
        {
            // 在C#中，线程组的概念不那么重要，这里返回null
            return null;
        }
    }
}