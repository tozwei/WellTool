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
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Cache
{
    /// <summary>
    /// 全局缓存清理定时器池，用于在需要过期支持的缓存对象中执行超时任务
    /// 这是一个单例模式的实现
    /// </summary>
    public sealed class GlobalPruneTimer
    {
        /// <summary>
        /// 单例对象
        /// </summary>
        public static readonly GlobalPruneTimer Instance = new GlobalPruneTimer();

        /// <summary>
        /// 缓存任务计数
        /// </summary>
        private int _cacheTaskNumber = 1;

        /// <summary>
        /// 定时器
        /// </summary>
        private ScheduledExecutorService _pruneTimer;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private GlobalPruneTimer()
        {
            Create();
        }

        /// <summary>
        /// 启动定时任务
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="delay">周期（毫秒）</param>
        /// <returns>可取消的任务句柄</returns>
        public CancellationToken Schedule(Action task, long delay)
        {
            var cts = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!cts.Token.IsCancellationRequested)
                {
                    await Task.Delay((int)delay, cts.Token);
                    if (!cts.Token.IsCancellationRequested)
                    {
                        task();
                    }
                }
            }, cts.Token);
            return cts.Token;
        }

        /// <summary>
        /// 创建定时器
        /// </summary>
        public void Create()
        {
            if (_pruneTimer != null)
            {
                ShutdownNow();
            }
            _pruneTimer = new ScheduledExecutorService(1, $"Prune-Timer-{_cacheTaskNumber++}");
        }

        /// <summary>
        /// 销毁全局定时器
        /// </summary>
        public void Shutdown()
        {
            _pruneTimer?.Shutdown();
        }

        /// <summary>
        /// 销毁全局定时器并返回未执行的任务
        /// </summary>
        /// <returns>被中断的任务列表</returns>
        public List<Action> ShutdownNow()
        {
            if (_pruneTimer != null)
            {
                return _pruneTimer.ShutdownNow();
            }
            return null;
        }
    }

    /// <summary>
    /// 简单的定时执行服务
    /// </summary>
    public class ScheduledExecutorService : IDisposable
    {
        private readonly ThreadPoolExecutor _executor;
        private readonly List<CancellationTokenSource> _tasks = new List<CancellationTokenSource>();
        private readonly object _lock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="corePoolSize">核心线程数</param>
        /// <param name="threadNamePrefix">线程名称前缀</param>
        public ScheduledExecutorService(int corePoolSize, string threadNamePrefix)
        {
            _executor = new ThreadPoolExecutor(corePoolSize, threadNamePrefix);
        }

        /// <summary>
        /// 定期执行任务
        /// </summary>
        /// <param name="action">要执行的任务</param>
        /// <param name="initialDelay">初始延迟（毫秒）</param>
        /// <param name="period">执行周期（毫秒）</param>
        public CancellationToken ScheduleAtFixedRate(Action action, long initialDelay, long period)
        {
            var cts = new CancellationTokenSource();
            lock (_lock)
            {
                _tasks.Add(cts);
            }

            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay((int)initialDelay, cts.Token);
                    while (!cts.Token.IsCancellationRequested)
                    {
                        action();
                        await Task.Delay((int)period, cts.Token);
                    }
                }
                catch (TaskCanceledException)
                {
                    // 正常取消
                }
            }, cts.Token);

            return cts.Token;
        }

        /// <summary>
        /// 关闭执行器
        /// </summary>
        public void Shutdown()
        {
            lock (_lock)
            {
                foreach (var cts in _tasks)
                {
                    cts.Cancel();
                }
                _tasks.Clear();
            }
            _executor.Shutdown();
        }

        /// <summary>
        /// 立即关闭并返回未执行的任务
        /// </summary>
        /// <returns>未执行的任务列表</returns>
        public List<Action> ShutdownNow()
        {
            var cancelledTasks = new List<Action>();
            lock (_lock)
            {
                foreach (var cts in _tasks)
                {
                    cts.Cancel();
                    cancelledTasks.Add(() => { }); // 返回空操作作为占位
                }
                _tasks.Clear();
            }
            _executor.Shutdown();
            return cancelledTasks;
        }

        public void Dispose()
        {
            Shutdown();
        }
    }

    /// <summary>
    /// 简单的线程池执行器
    /// </summary>
    public class ThreadPoolExecutor
    {
        private readonly string _threadNamePrefix;
        private readonly int _corePoolSize;
        private readonly List<Thread> _threads = new List<Thread>();
        private readonly object _lock = new object();
        private bool _isShutdown = false;

        public ThreadPoolExecutor(int corePoolSize, string threadNamePrefix)
        {
            _corePoolSize = corePoolSize;
            _threadNamePrefix = threadNamePrefix;
        }

        public void Execute(Action action)
        {
            if (_isShutdown) return;

            Thread thread;
            lock (_lock)
            {
                if (_threads.Count < _corePoolSize)
                {
                    thread = new Thread(() => action());
                    thread.Name = $"{_threadNamePrefix}-{_threads.Count}";
                    thread.IsBackground = true;
                    thread.Start();
                    _threads.Add(thread);
                }
                else
                {
                    // 使用系统线程池
                    thread = new Thread(() => action());
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
        }

        public void Shutdown()
        {
            lock (_lock)
            {
                _isShutdown = true;
                foreach (var thread in _threads)
                {
                    // 只标记，不强制中断
                }
            }
        }
    }
}
