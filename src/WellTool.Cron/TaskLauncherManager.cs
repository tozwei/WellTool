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

using System.Threading;

namespace WellTool.Cron
{
    /// <summary>
    /// 任务启动器管理器
    /// </summary>
    public class TaskLauncherManager
    {
        /// <summary>
        /// 调度器
        /// </summary>
        private readonly Scheduler scheduler;

        /// <summary>
        /// 定时器
        /// </summary>
        private Timer timer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scheduler">调度器</param>
        public TaskLauncherManager(Scheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            // 每分钟执行一次任务启动器
            int interval = scheduler.IsMatchSecond() ? 1000 : 60000;
            timer = new Timer(_ => new TaskLauncher(scheduler).Launch(), null, 0, interval);
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            timer?.Dispose();
        }
    }
}