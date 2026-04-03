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
using WellTool.Cron.Task;

namespace WellTool.Cron
{
    /// <summary>
    /// 任务执行器
    /// 执行具体的任务，执行完毕销毁
    /// 任务执行器唯一关联一个任务，负责管理任务的运行的生命周期
    /// </summary>
    public class TaskExecutor
    {
        /// <summary>
        /// 调度器
        /// </summary>
        private readonly Scheduler scheduler;

        /// <summary>
        /// 任务
        /// </summary>
        private readonly CronTask task;

        /// <summary>
        /// 获得原始任务对象
        /// </summary>
        /// <returns>任务对象</returns>
        public WellTool.Cron.Task.Task GetTask()
        {
            return task.Task;
        }

        /// <summary>
        /// 获得原始 CronTask 对象
        /// </summary>
        /// <returns>CronTask 对象</returns>
        public CronTask GetCronTask()
        {
            return task;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="scheduler">调度器</param>
        /// <param name="task">被执行的任务</param>
        public TaskExecutor(Scheduler scheduler, CronTask task)
        {
            this.scheduler = scheduler;
            this.task = task;
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Run()
        {
            try
            {
                scheduler.ListenerManager.NotifyTaskStart(task.Id);
                task.Execute();
                scheduler.ListenerManager.NotifyTaskSuccess(task.Id);
            }
            catch (System.Exception ex)
            {
                scheduler.ListenerManager.NotifyTaskFailure(task.Id, ex);
            }
            finally
            {
                scheduler.TaskExecutorManager.NotifyExecutorCompleted(this);
            }
        }
    }
}

