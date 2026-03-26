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

using WellTool.Cron.Pattern;

namespace WellTool.Cron.Task
{
    /// <summary>
    /// 定时任务
    /// </summary>
    public class CronTask
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public CronPattern Pattern { get; set; }

        /// <summary>
        /// 实际任务
        /// </summary>
        public Task Task { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">实际任务</param>
        public CronTask(string id, CronPattern pattern, Task task)
        {
            Id = id ?? throw new CronException("Task id cannot be null");
            Pattern = pattern ?? throw new CronException("Cron pattern cannot be null");
            Task = task ?? throw new CronException("Task cannot be null");
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Execute()
        {
            Task.Execute();
        }
    }
}