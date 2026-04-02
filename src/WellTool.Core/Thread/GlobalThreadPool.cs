using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 全局线程池
    /// </summary>
    public class GlobalThreadPool
    {
        private static readonly Lazy<GlobalThreadPool> _instance = new Lazy<GlobalThreadPool>(() => new GlobalThreadPool());
        private readonly BlockingCollection<Func<Task>> _taskQueue = new BlockingCollection<Func<Task>>();
        private readonly List<System.Threading.Thread> _threads = new List<System.Threading.Thread>();
        private bool _isRunning = true;

        /// <summary>
        /// 单例实例
        /// </summary>
        public static GlobalThreadPool Instance => _instance.Value;

        /// <summary>
        /// 构造函数
        /// </summary>
        private GlobalThreadPool()
        {
            // 初始化线程池
            var threadCount = Environment.ProcessorCount;
            for (int i = 0; i < threadCount; i++)
            {
                var thread = new System.Threading.Thread(ProcessTasks)
                {
                    Name = $"GlobalThreadPool-Thread-{i}",
                    IsBackground = true
                };
                _threads.Add(thread);
                thread.Start();
            }
        }

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="action">任务操作</param>
        /// <returns>任务</returns>
        public Task Submit(Action action)
        {
            var tcs = new TaskCompletionSource<object>();
            _taskQueue.Add(async () =>
            {
                try
                {
                    action();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">任务操作</param>
        /// <returns>任务</returns>
        public Task<T> Submit<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            _taskQueue.Add(async () =>
            {
                try
                {
                    var result = func();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        /// <summary>
        /// 提交异步任务
        /// </summary>
        /// <param name="func">异步任务操作</param>
        /// <returns>任务</returns>
        public Task Submit(Func<Task> func)
        {
            var tcs = new TaskCompletionSource<object>();
            _taskQueue.Add(async () =>
            {
                try
                {
                    await func();
                    tcs.SetResult(null);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        /// <summary>
        /// 提交异步任务
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">异步任务操作</param>
        /// <returns>任务</returns>
        public Task<T> Submit<T>(Func<Task<T>> func)
        {
            var tcs = new TaskCompletionSource<T>();
            _taskQueue.Add(async () =>
            {
                try
                {
                    var result = await func();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        /// <summary>
        /// 处理任务
        /// </summary>
        private void ProcessTasks()
        {
            while (_isRunning)
            {
                try
                {
                    if (_taskQueue.TryTake(out var taskFunc, 1000))
                    {
                        taskFunc();
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常
                    Console.WriteLine($"GlobalThreadPool error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 关闭线程池
        /// </summary>
        public void Shutdown()
        {
            _isRunning = false;
            _taskQueue.CompleteAdding();
            foreach (var thread in _threads)
            {
                thread.Join(1000);
            }
        }

        /// <summary>
        /// 获取任务队列大小
        /// </summary>
        public int QueueSize => _taskQueue.Count;

        /// <summary>
        /// 获取线程数量
        /// </summary>
        public int ThreadCount => _threads.Count;
    }
}