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

namespace WellTool.Cron
{
    /// <summary>
    /// 定时任务定时器
    /// </summary>
    public class CronTimer
    {
        /// <summary>
        /// 调度器
        /// </summary>
        private readonly Scheduler scheduler;

        /// <summary>
        /// 线程
        /// </summary>
        private Thread thread;

        /// <summary>
        /// 是否停止
        /// </summary>
        private volatile bool isStop;

        /// <summary>
        /// 是否为守护线程
        /// </summary>
        private bool isDaemon;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="scheduler">调度器</param>
        public CronTimer(Scheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        /// <summary>
        /// 设置是否为守护线程
        /// </summary>
        /// <param name="isDaemon">是否为守护线程</param>
        public void SetDaemon(bool isDaemon)
        {
            this.isDaemon = isDaemon;
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            isStop = false;
            thread = new Thread(Run) { IsBackground = isDaemon, Name = "CronTimer" };
            thread.Start();
        }

        /// <summary>
        /// 运行
        /// </summary>
        private void Run()
        {
            while (!isStop)
            {
                // 启动任务
                new TaskLauncher(scheduler).Launch();

                // 休眠
                int sleepTime = scheduler.IsMatchSecond() ? 1000 : 60000;
                Thread.Sleep(sleepTime);
            }
        }

        /// <summary>
        /// 停止定时器
        /// </summary>
        public void StopTimer()
        {
            isStop = true;
            thread?.Join(1000);
        }
    }
}