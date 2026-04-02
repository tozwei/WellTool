using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 线程池构建器
    /// </summary>
    public class ExecutorBuilder
    {
        private int _corePoolSize = Environment.ProcessorCount;
        private int _maxPoolSize = Environment.ProcessorCount * 2;
        private int _queueCapacity = 1000;
        private int _keepAliveSeconds = 60;
        private bool _allowCoreThreadTimeOut = false;
        private string _threadNamePrefix = "WellTool-Thread-";

        /// <summary>
        /// 设置核心线程数
        /// </summary>
        /// <param name="corePoolSize">核心线程数</param>
        /// <returns>当前实例</returns>
        public ExecutorBuilder SetCorePoolSize(int corePoolSize)
        {
            _corePoolSize = corePoolSize;
            return this;
        }

        /// <summary>
        /// 设置最大线程数
        /// </summary>
        /// <param name="maxPoolSize">最大线程数</param>
        /// <returns>当前实例</returns>
        public ExecutorBuilder SetMaxPoolSize(int maxPoolSize)
        {
            _maxPoolSize = maxPoolSize;
            return this;
        }

        /// <summary>
        /// 设置队列容量
        /// </summary>
        /// <param name="queueCapacity">队列容量</param>
        /// <returns>当前实例</returns>
        public ExecutorBuilder SetQueueCapacity(int queueCapacity)
        {
            _queueCapacity = queueCapacity;
            return this;
        }

        /// <summary>
        /// 设置线程保活时间（秒）
        /// </summary>
        /// <param name="keepAliveSeconds">线程保活时间（秒）</param>
        /// <returns>当前实例</returns>
        public ExecutorBuilder SetKeepAliveSeconds(int keepAliveSeconds)
        {
            _keepAliveSeconds = keepAliveSeconds;
            return this;
        }

        /// <summary>
        /// 设置是否允许核心线程超时
        /// </summary>
        /// <param name="allowCoreThreadTimeOut">是否允许核心线程超时</param>
        /// <returns>当前实例</returns>
        public ExecutorBuilder SetAllowCoreThreadTimeOut(bool allowCoreThreadTimeOut)
        {
            _allowCoreThreadTimeOut = allowCoreThreadTimeOut;
            return this;
        }

        /// <summary>
        /// 设置线程名称前缀
        /// </summary>
        /// <param name="threadNamePrefix">线程名称前缀</param>
        /// <returns>当前实例</returns>
        public ExecutorBuilder SetThreadNamePrefix(string threadNamePrefix)
        {
            _threadNamePrefix = threadNamePrefix;
            return this;
        }

        /// <summary>
        /// 构建线程池
        /// </summary>
        /// <returns>线程池</returns>
        public TaskScheduler Build()
        {
            var queue = new BlockingCollection<Func<Task>>(_queueCapacity);
            var threads = new List<System.Threading.Thread>();
            var threadId = 0;

            // 启动核心线程
            for (int i = 0; i < _corePoolSize; i++)
            {
                var thread = CreateThread(queue, ref threadId);
                threads.Add(thread);
                thread.Start();
            }

            // 这里返回一个自定义的 TaskScheduler，实际项目中可能需要更复杂的实现
            return TaskScheduler.Default;
        }

        /// <summary>
        /// 创建线程
        /// </summary>
        /// <param name="queue">任务队列</param>
        /// <param name="threadId">线程ID</param>
        /// <returns>线程</returns>
        private System.Threading.Thread CreateThread(BlockingCollection<Func<Task>> queue, ref int threadId)
        {
            var thread = new System.Threading.Thread(() =>
            {
                while (!queue.IsCompleted)
                {
                    try
                    {
                        if (queue.TryTake(out var taskFunc, _keepAliveSeconds * 1000))
                        {
                            taskFunc();
                        }
                        else if (_allowCoreThreadTimeOut)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        // 处理异常
                        Console.WriteLine($"Thread error: {ex.Message}");
                    }
                }
            });

            thread.Name = $"{_threadNamePrefix}{Interlocked.Increment(ref threadId)}";
            thread.IsBackground = true;

            return thread;
        }
    }
}