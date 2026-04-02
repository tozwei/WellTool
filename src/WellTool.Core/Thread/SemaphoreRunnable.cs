using System;
using System.Threading;

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 带有信号量控制的{@link Action} 接口抽象实现
    /// </summary>
    /// <remarks>
    /// 通过设置信号量，可以限制可以访问某些资源（物理或逻辑的）线程数目。<br>
    /// 例如：设置信号量为2，表示最多有两个线程可以同时执行方法逻辑，其余线程等待，直到此线程逻辑执行完毕
    /// </remarks>
    public class SemaphoreRunnable : Runnable
    {
        /// <summary>
        /// 实际执行的逻辑
        /// </summary>
        private readonly Action _action;
        /// <summary>
        /// 信号量
        /// </summary>
        private readonly SemaphoreSlim _semaphore;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="action">实际执行的线程逻辑</param>
        /// <param name="semaphore">信号量，多个线程必须共享同一信号量</param>
        public SemaphoreRunnable(Action action, SemaphoreSlim semaphore)
        {
            _action = action;
            _semaphore = semaphore;
        }

        /// <summary>
        /// 获得信号量
        /// </summary>
        /// <returns><see cref="SemaphoreSlim"/></returns>
        public SemaphoreSlim GetSemaphore()
        {
            return _semaphore;
        }

        /// <summary>
        /// 执行线程逻辑
        /// </summary>
        public override void Run()
        {
            if (_semaphore != null)
            {
                try
                {
                    _semaphore.Wait();
                    try
                    {
                        _action();
                    }
                    finally
                    {
                        _semaphore.Release();
                    }
                }
                catch (System.Threading.ThreadInterruptedException ex)
                {
                    Thread.CurrentThread.Interrupt();
                }
            }
        }
    }
}