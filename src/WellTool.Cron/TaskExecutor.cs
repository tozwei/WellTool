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
using WellTool.Cron.Listener;
using WellTool.Cron.Task;

namespace WellTool.Cron
{
    /// <summary>
    /// 任务执行器
    /// </summary>
    public class TaskExecutor
    {
        /// <summary>
        /// 任务
        /// </summary>
        private readonly CronTask task;

        /// <summary>
        /// 监听器管理器
        /// </summary>
        private readonly TaskListenerManager listenerManager;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="task">任务</param>
        /// <param name="listenerManager">监听器管理器</param>
        public TaskExecutor(CronTask task, TaskListenerManager listenerManager)
        {
            this.task = task;
            this.listenerManager = listenerManager;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Execute()
        {
            listenerManager.NotifyTaskStart(task.Id);

            try
            {
                task.Execute();
                listenerManager.NotifyTaskSuccess(task.Id);
            }
            catch (Exception ex)
            {
                listenerManager.NotifyTaskFailure(task.Id, ex);
            }
        }

        /// <summary>
        /// 异步执行任务
        /// </summary>
        public void ExecuteAsync()
        {
            ThreadPool.QueueUserWorkItem(_ => Execute());
        }
    }
}