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
using WellTool.Cron.Listener;

namespace WellTool.Cron
{
    /// <summary>
    /// 任务启动器
    /// </summary>
    public class TaskLauncher
    {
        /// <summary>
        /// 调度器
        /// </summary>
        private readonly Scheduler scheduler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scheduler">调度器</param>
        public TaskLauncher(Scheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        /// <summary>
        /// 启动任务
        /// </summary>
        public void Launch()
        {
            var now = DateTime.Now;
            var tasks = scheduler.GetTaskTable().GetAllTasks();

            foreach (var task in tasks)
            {
                if (task.Pattern.Match(now))
                {
                    // 执行任务
                    var executor = new TaskExecutor(scheduler, task);
                    executor.Run();
                }
            }
        }
    }
}