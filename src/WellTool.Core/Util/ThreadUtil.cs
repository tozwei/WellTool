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
using System.Threading.Tasks;

namespace WellTool.Core.Util
{
    /// <summary>
    /// 线程工具类
    /// </summary>
    public static class ThreadUtil
    {
        /// <summary>
        /// 创建一个新的线程池执行器
        /// </summary>
        /// <param name="corePoolSize">核心线程数</param>
        /// <returns>线程池执行器</returns>
        public static TaskFactory NewExecutor(int corePoolSize)
        {
            // 设置线程池最小线程数
            ThreadPool.SetMinThreads(corePoolSize, corePoolSize);
            return new TaskFactory();
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="action">要执行的操作</param>
        public static void Execute(Action action)
        {
            Task.Run(action);
        }

        /// <summary>
        /// 安全睡眠
        /// </summary>
        /// <param name="millis">睡眠毫秒数</param>
        public static void SafeSleep(long millis)
        {
            Thread.Sleep((int)millis);
        }

        /// <summary>
        /// 获取当前线程
        /// </summary>
        /// <returns>当前线程</returns>
        public static Thread CurrentThread()
        {
            return Thread.CurrentThread;
        }

        /// <summary>
        /// 获取当前线程ID
        /// </summary>
        /// <returns>当前线程ID</returns>
        public static long CurrentThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// 中断当前线程
        /// </summary>
        public static void Interrupt()
        {
            Thread.CurrentThread.Interrupt();
        }

        /// <summary>
        /// 检查线程是否被中断
        /// </summary>
        /// <returns>如果线程被中断则返回 true，否则返回 false</returns>
        public static bool IsInterrupted()
        {
            return (Thread.CurrentThread.ThreadState & ThreadState.Aborted) != 0;
        }

        /// <summary>
        /// 使当前线程让出CPU时间片
        /// </summary>
        public static void Yield()
        {
            Thread.Yield();
        }

        /// <summary>
        /// 使当前线程等待指定的时间
        /// </summary>
        /// <param name="millis">等待毫秒数</param>
        public static void Sleep(long millis)
        {
            Thread.Sleep((int)millis);
        }
    }

    /// <summary>
    /// 异步工具类
    /// </summary>
    public static class AsyncUtil
    {
        /// <summary>
        /// 执行多个任务
        /// </summary>
        /// <param name="actions">要执行的操作数组</param>
        public static void Execute(params Action[] actions)
        {
            if (actions == null || actions.Length == 0)
            {
                return;
            }

            var tasks = new Task[actions.Length];
            for (int i = 0; i < actions.Length; i++)
            {
                tasks[i] = Task.Run(actions[i]);
            }

            Task.WaitAll(tasks);
        }
    }

    /// <summary>
    /// 并发测试器
    /// </summary>
    public class ConcurrencyTester
    {
        private readonly int _threadCount;
        private readonly int _taskCount;
        private Action _task;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threadCount">线程数</param>
        /// <param name="taskCount">每个线程的任务数</param>
        public ConcurrencyTester(int threadCount, int taskCount)
        {
            _threadCount = threadCount;
            _taskCount = taskCount;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task">要执行的任务</param>
        public void AddTask(Action task)
        {
            _task = task;
        }

        /// <summary>
        /// 执行测试
        /// </summary>
        public void Execute()
        {
            if (_task == null)
            {
                throw new InvalidOperationException("Task not set");
            }

            var tasks = new Task[_threadCount];
            for (int i = 0; i < _threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    for (int j = 0; j < _taskCount; j++)
                    {
                        _task();
                    }
                });
            }

            Task.WaitAll(tasks);
        }
    }

    /// <summary>
    /// 同步完成器
    /// </summary>
    public class SyncFinisher
    {
        private readonly int _count;
        private int _currentCount;
        private Action _listener;
        private readonly object _lock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="count">需要完成的任务数</param>
        public SyncFinisher(int count)
        {
            _count = count;
            _currentCount = 0;
        }

        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="listener">完成时的回调</param>
        public void AddListener(Action listener)
        {
            _listener = listener;
        }

        /// <summary>
        /// 计数减一
        /// </summary>
        public void CountDown()
        {
            lock (_lock)
            {
                _currentCount++;
                if (_currentCount >= _count)
                {
                    _listener?.Invoke();
                }
            }
        }
    }
}
