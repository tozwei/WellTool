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

namespace WellTool.Core.Threading
{
    /// <summary>
    /// ThreadFactory创建器
    /// </summary>
    public class ThreadFactoryBuilder
    {
        /// <summary>
        /// 用于线程创建的线程工厂
        /// </summary>
        private Func<Action, Thread> _backingThreadFactory;

        /// <summary>
        /// 线程名的前缀
        /// </summary>
        private string _namePrefix;

        /// <summary>
        /// 是否守护线程，默认false
        /// </summary>
        private bool? _daemon;

        /// <summary>
        /// 线程优先级
        /// </summary>
        private int? _priority;

        /// <summary>
        /// 未捕获异常处理器
        /// </summary>
        private UnhandledExceptionEventHandler _uncaughtExceptionHandler;

        /// <summary>
        /// 创建ThreadFactoryBuilder
        /// </summary>
        /// <returns>ThreadFactoryBuilder</returns>
        public static ThreadFactoryBuilder Create()
        {
            return new ThreadFactoryBuilder();
        }

        /// <summary>
        /// 设置用于创建基础线程的线程工厂
        /// </summary>
        /// <param name="backingThreadFactory">用于创建基础线程的线程工厂</param>
        /// <returns>this</returns>
        public ThreadFactoryBuilder SetThreadFactory(Func<Action, Thread> backingThreadFactory)
        {
            _backingThreadFactory = backingThreadFactory;
            return this;
        }

        /// <summary>
        /// 设置线程名前缀，例如设置前缀为hutool-thread-，则线程名为hutool-thread-1之类。
        /// </summary>
        /// <param name="namePrefix">线程名前缀</param>
        /// <returns>this</returns>
        public ThreadFactoryBuilder SetNamePrefix(string namePrefix)
        {
            _namePrefix = namePrefix;
            return this;
        }

        /// <summary>
        /// 设置是否守护线程
        /// </summary>
        /// <param name="daemon">是否守护线程</param>
        /// <returns>this</returns>
        public ThreadFactoryBuilder SetDaemon(bool daemon)
        {
            _daemon = daemon;
            return this;
        }

        /// <summary>
        /// 设置线程优先级
        /// </summary>
        /// <param name="priority">优先级</param>
        /// <returns>this</returns>
        public ThreadFactoryBuilder SetPriority(int priority)
        {
            if (priority < Thread.MinPriority)
            {
                throw new ArgumentException($"Thread priority ({priority}) must be >= {Thread.MinPriority}");
            }
            if (priority > Thread.MaxPriority)
            {
                throw new ArgumentException($"Thread priority ({priority}) must be <= {Thread.MaxPriority}");
            }
            _priority = priority;
            return this;
        }

        /// <summary>
        /// 设置未捕获异常的处理方式
        /// </summary>
        /// <param name="uncaughtExceptionHandler">未捕获异常处理器</param>
        /// <returns>this</returns>
        public ThreadFactoryBuilder SetUncaughtExceptionHandler(UnhandledExceptionEventHandler uncaughtExceptionHandler)
        {
            _uncaughtExceptionHandler = uncaughtExceptionHandler;
            return this;
        }

        /// <summary>
        /// 构建Func&lt;Action, Thread&gt;
        /// </summary>
        /// <returns>Func&lt;Action, Thread&gt;</returns>
        public Func<Action, Thread> Build()
        {
            return Build(this);
        }

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="builder">ThreadFactoryBuilder</param>
        /// <returns>Func&lt;Action, Thread&gt;</returns>
        private static Func<Action, Thread> Build(ThreadFactoryBuilder builder)
        {
            var backingThreadFactory = builder._backingThreadFactory ?? DefaultThreadFactory;
            var namePrefix = builder._namePrefix;
            var daemon = builder._daemon;
            var priority = builder._priority;
            var handler = builder._uncaughtExceptionHandler;
            var count = namePrefix != null ? new System.Threading.Interlocked() : null;
            var counter = 0;

            return action => {
                var thread = backingThreadFactory(action);
                if (namePrefix != null)
                {
                    thread.Name = namePrefix + System.Threading.Interlocked.Increment(ref counter);
                }
                if (daemon.HasValue)
                {
                    thread.IsBackground = daemon.Value;
                }
                if (priority.HasValue)
                {
                    thread.Priority = (ThreadPriority)priority.Value;
                }
                if (handler != null)
                {
                    AppDomain.CurrentDomain.UnhandledException += handler;
                }
                return thread;
            };
        }

        /// <summary>
        /// 默认线程工厂
        /// </summary>
        private static Func<Action, Thread> DefaultThreadFactory => action => new Thread(new ThreadStart(action));
    }
}