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

namespace WellTool.Core.Threading
{
    /// <summary>
    /// 当任务队列过长时处于阻塞状态，直到添加到队列中
    /// 如果阻塞过程中被中断，就会抛出{@link ThreadInterruptedException}异常<br>
    /// 有时候在线程池内访问第三方接口，只希望固定并发数去访问，并且不希望丢弃任务时使用此策略，队列满的时候会处于阻塞状态(例如刷库的场景)
    /// </summary>
    public class BlockPolicy : IRejectedExecutionHandler
    {
        /// <summary>
        /// 线程池关闭时，为避免任务丢失，留下处理方法
        /// 如果需要由调用方来运行，可以{@code new BlockPolicy(action => action())}
        /// </summary>
        private readonly Action<Action> _handlerWhenShutdown;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="handlerWhenShutdown">线程池关闭后的执行策略</param>
        public BlockPolicy(Action<Action> handlerWhenShutdown)
        {
            _handlerWhenShutdown = handlerWhenShutdown;
        }

        /// <summary>
        /// 构造
        /// </summary>
        public BlockPolicy() : this(null)
        {
        }

        /// <summary>
        /// 当线程池无法接受任务时的处理策略
        /// </summary>
        /// <param name="action">被拒绝的任务</param>
        /// <param name="taskScheduler">线程池调度器</param>
        public void RejectedExecution(Action action, TaskScheduler taskScheduler)
        {
            // 在线程池中，我们使用 TaskScheduler 来调度任务
            // 当任务被拒绝时，我们可以尝试将其添加到队列中，或者使用其他策略
            try
            {
                // 尝试将任务添加到队列中
                Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            }
            catch (Exception ex)
            {
                // 如果线程池已关闭，并且设置了关闭时的处理策略，则执行该策略
                if (_handlerWhenShutdown != null)
                {
                    _handlerWhenShutdown(action);
                }
                else
                {
                    throw new RejectedExecutionException("Task rejected from task scheduler", ex);
                }
            }
        }
    }

    /// <summary>
    /// 拒绝执行处理器接口
    /// </summary>
    public interface IRejectedExecutionHandler
    {
        /// <summary>
        /// 当线程池无法接受任务时的处理策略
        /// </summary>
        /// <param name="action">被拒绝的任务</param>
        /// <param name="taskScheduler">线程池调度器</param>
        void RejectedExecution(Action action, TaskScheduler taskScheduler);
    }

    /// <summary>
    /// 拒绝执行异常
    /// </summary>
    public class RejectedExecutionException : Exception
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        public RejectedExecutionException(string message) : base(message)
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        public RejectedExecutionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}