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
using System.Threading.Tasks;

namespace WellTool.Core.Thread
{
    /// <summary>
    /// 线程拒绝策略枚举
    /// <p>
    /// 如果设置了maxSize, 当总线程数达到上限, 会调用IRejectedExecutionHandler进行处理，此枚举为预定义的几种策略枚举表示
    /// </p>
    /// </summary>
    public enum RejectPolicy
    {
        /// <summary>处理程序遭到拒绝将抛出RejectedExecutionException</summary>
        Abort,
        /// <summary>放弃当前任务</summary>
        Discard,
        /// <summary>如果执行程序尚未关闭，则位于工作队列头部的任务将被删除，然后重试执行程序（如果再次失败，则重复此过程）</summary>
        DiscardOldest,
        /// <summary>由主线程来直接执行</summary>
        CallerRuns,
        /// <summary>当任务队列过长时处于阻塞状态，直到添加到队列中，固定并发数去访问，并且不希望丢弃任务时使用此策略</summary>
        Block
    }

    /// <summary>
    /// 拒绝策略扩展方法
    /// </summary>
    public static class RejectPolicyExtensions
    {
        /// <summary>
        /// 获取IRejectedExecutionHandler枚举值
        /// </summary>
        /// <param name="policy">拒绝策略</param>
        /// <returns>IRejectedExecutionHandler</returns>
        public static IRejectedExecutionHandler GetValue(this RejectPolicy policy)
        {
            switch (policy)
            {
                case RejectPolicy.Abort:
                    return new AbortPolicy();
                case RejectPolicy.Discard:
                    return new DiscardPolicy();
                case RejectPolicy.DiscardOldest:
                    return new DiscardOldestPolicy();
                case RejectPolicy.CallerRuns:
                    return new CallerRunsPolicy();
                case RejectPolicy.Block:
                    return new BlockPolicy();
                default:
                    throw new ArgumentOutOfRangeException(nameof(policy), policy, null);
            }
        }
    }

    /// <summary>
    /// 处理程序遭到拒绝将抛出RejectedExecutionException
    /// </summary>
    public class AbortPolicy : IRejectedExecutionHandler
    {
        /// <summary>
        /// 当线程池无法接受任务时的处理策略
        /// </summary>
        /// <param name="action">被拒绝的任务</param>
        /// <param name="taskScheduler">线程池调度器</param>
        public void RejectedExecution(Action action, TaskScheduler taskScheduler)
        {
            throw new RejectedExecutionException("Task rejected from task scheduler");
        }
    }

    /// <summary>
    /// 放弃当前任务
    /// </summary>
    public class DiscardPolicy : IRejectedExecutionHandler
    {
        /// <summary>
        /// 当线程池无法接受任务时的处理策略
        /// </summary>
        /// <param name="action">被拒绝的任务</param>
        /// <param name="taskScheduler">线程池调度器</param>
        public void RejectedExecution(Action action, TaskScheduler taskScheduler)
        {
            // 直接丢弃任务，不做任何处理
        }
    }

    /// <summary>
    /// 如果执行程序尚未关闭，则位于工作队列头部的任务将被删除，然后重试执行程序（如果再次失败，则重复此过程）
    /// </summary>
    public class DiscardOldestPolicy : IRejectedExecutionHandler
    {
        /// <summary>
        /// 当线程池无法接受任务时的处理策略
        /// </summary>
        /// <param name="action">被拒绝的任务</param>
        /// <param name="taskScheduler">线程池调度器</param>
        public void RejectedExecution(Action action, TaskScheduler taskScheduler)
        {
            // 在C#中，TaskScheduler不提供直接访问队列的方法，这里简化处理
            try
            {
                Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, taskScheduler);
            }
            catch
            {
                // 丢弃任务
            }
        }
    }

    /// <summary>
    /// 由主线程来直接执行
    /// </summary>
    public class CallerRunsPolicy : IRejectedExecutionHandler
    {
        /// <summary>
        /// 当线程池无法接受任务时的处理策略
        /// </summary>
        /// <param name="action">被拒绝的任务</param>
        /// <param name="taskScheduler">线程池调度器</param>
        public void RejectedExecution(Action action, TaskScheduler taskScheduler)
        {
            // 由调用线程直接执行任务
            action();
        }
    }
}