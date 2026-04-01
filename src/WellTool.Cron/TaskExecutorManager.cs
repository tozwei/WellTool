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
using WellTool.Cron.Task;

namespace WellTool.Cron
{
    /// <summary>
    /// 任务执行管理器
    /// 负责管理任务的启动、停止等
    /// </summary>
    /// <remarks>
    /// 此类用于管理正在运行的任务情况，任务启动后加入任务列表，任务结束移除
    /// </remarks>
    public class TaskExecutorManager
    {
        /// <summary>
        /// 调度器
        /// </summary>
        protected Scheduler scheduler;

        /// <summary>
        /// 执行器列表
        /// </summary>
        private readonly List<TaskExecutor> executors = new List<TaskExecutor>();

        /// <summary>
        /// 使用指定的调度器初始化 TaskExecutorManager 的新实例
        /// </summary>
        /// <param name="scheduler">调度器</param>
        public TaskExecutorManager(Scheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        /// <summary>
        /// 获取所有正在执行的任务调度执行器
        /// </summary>
        /// <returns>任务执行器列表</returns>
        public IReadOnlyList<TaskExecutor> GetExecutors()
        {
            lock (executors)
            {
                return executors.AsReadOnly();
            }
        }

        /// <summary>
        /// 启动执行器 TaskExecutor，即启动任务
        /// </summary>
        /// <param name="task">任务</param>
        /// <returns>任务执行器</returns>
        public TaskExecutor SpawnExecutor(CronTask task)
        {
            var executor = new TaskExecutor(scheduler, task);
            lock (executors)
            {
                executors.Add(executor);
            }

            // 使用线程池执行任务
            System.Threading.ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    executor.Run();
                }
                finally
                {
                    // 任务执行完毕后通知管理器
                    NotifyExecutorCompleted(executor);
                }
            });

            return executor;
        }

        /// <summary>
        /// 执行器执行完毕调用此方法，将执行器从执行器列表移除
        /// </summary>
        /// <remarks>
        /// 此方法由 TaskExecutor 对象调用，用于通知管理器自身已完成执行
        /// </remarks>
        /// <param name="executor">执行器</param>
        /// <returns>this</returns>
        public TaskExecutorManager NotifyExecutorCompleted(TaskExecutor executor)
        {
            lock (executors)
            {
                executors.Remove(executor);
            }
            return this;
        }

        /// <summary>
        /// 获取当前正在执行的任务数量
        /// </summary>
        /// <returns>正在执行的任务数量</returns>
        public int GetActiveExecutorCount()
        {
            lock (executors)
            {
                return executors.Count;
            }
        }

        /// <summary>
        /// 检查是否包含指定的执行器
        /// </summary>
        /// <param name="executor">执行器</param>
        /// <returns>如果包含返回 true，否则返回 false</returns>
        public bool ContainsExecutor(TaskExecutor executor)
        {
            lock (executors)
            {
                return executors.Contains(executor);
            }
        }
    }
}
