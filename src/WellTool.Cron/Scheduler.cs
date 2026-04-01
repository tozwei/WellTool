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
using System.Threading;
using System.Threading.Tasks;
using WellTool.Cron.Listener;
using WellTool.Cron.Pattern;
using WellTool.Cron.Task;

namespace WellTool.Cron
{
    /// <summary>
    /// 任务调度器
    /// </summary>
    public class Scheduler
    {
        /// <summary>
        /// 锁
        /// </summary>
        private readonly object syncRoot = new object();

        /// <summary>
        /// 定时任务配置
        /// </summary>
        protected CronConfig config = new CronConfig();

        /// <summary>
        /// 是否已经启动
        /// </summary>
        private bool started = false;

        /// <summary>
        /// 是否为守护线程
        /// </summary>
        protected bool daemon;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="daemon">是否为守护线程</param>
        public Scheduler(bool daemon)
        {
            this.daemon = daemon;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Scheduler()
        {
        }

        /// <summary>
        /// 定时器
        /// </summary>
        private CronTimer timer;

        /// <summary>
        /// 定时任务表
        /// </summary>
        protected TaskTable taskTable = new TaskTable();

        /// <summary>
        /// 启动器管理器
        /// </summary>
        protected TaskLauncherManager taskLauncherManager;

        /// <summary>
        /// 监听器管理器列表
        /// </summary>
        protected TaskListenerManager listenerManager = new TaskListenerManager();

        /// <summary>
        /// 任务执行器管理器
        /// </summary>
        protected TaskExecutorManager taskExecutorManager;

        /// <summary>
        /// 线程池
        /// </summary>
        protected TaskFactory taskFactory;

        /// <summary>
        /// 监听器管理器属性访问器
        /// </summary>
        public TaskListenerManager ListenerManager => listenerManager;

        /// <summary>
        /// 任务执行器管理器属性访问器
        /// </summary>
        public TaskExecutorManager TaskExecutorManager => taskExecutorManager;

        // --------------------------------------------------------- Getters and Setters start
        /// <summary>
        /// 设置时区
        /// </summary>
        /// <param name="timeZone">时区</param>
        /// <returns>当前调度器</returns>
        public Scheduler SetTimeZone(TimeZoneInfo timeZone)
        {
            config.SetTimeZone(timeZone);
            return this;
        }

        /// <summary>
        /// 获得时区
        /// </summary>
        /// <returns>时区</returns>
        public TimeZoneInfo GetTimeZone()
        {
            return config.TimeZone;
        }

        /// <summary>
        /// 设置是否为守护线程
        /// </summary>
        /// <param name="daemon">是否为守护线程</param>
        /// <returns>当前调度器</returns>
        public Scheduler SetDaemon(bool daemon)
        {
            lock (syncRoot)
            {
                CheckStarted();
                this.daemon = daemon;
            }
            return this;
        }

        /// <summary>
        /// 是否为守护线程
        /// </summary>
        /// <returns>是否为守护线程</returns>
        public bool IsDaemon()
        {
            return daemon;
        }

        /// <summary>
        /// 是否支持秒匹配
        /// </summary>
        /// <returns>是否支持秒匹配</returns>
        public bool IsMatchSecond()
        {
            return config.IsMatchSecond;
        }

        /// <summary>
        /// 设置是否支持秒匹配
        /// </summary>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>当前调度器</returns>
        public Scheduler SetMatchSecond(bool matchSecond)
        {
            config.SetMatchSecond(matchSecond);
            return this;
        }

        /// <summary>
        /// 增加监听器
        /// </summary>
        /// <param name="listener">监听器</param>
        /// <returns>当前调度器</returns>
        public Scheduler AddListener(TaskListener listener)
        {
            listenerManager.AddListener(listener);
            return this;
        }

        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="listener">监听器</param>
        /// <returns>当前调度器</returns>
        public Scheduler RemoveListener(TaskListener listener)
        {
            listenerManager.RemoveListener(listener);
            return this;
        }

        /// <summary>
        /// 获取监听器管理器
        /// </summary>
        /// <returns>监听器管理器</returns>
        public TaskListenerManager GetListenerManager()
        {
            return listenerManager;
        }

        /// <summary>
        /// 获取任务执行器管理器
        /// </summary>
        /// <returns>任务执行器管理器</returns>
        public TaskExecutorManager GetTaskExecutorManager()
        {
            return taskExecutorManager;
        }

        /// <summary>
        /// 获取任务表
        /// </summary>
        /// <returns>任务表</returns>
        public TaskTable GetTaskTable()
        {
            return taskTable;
        }
        // --------------------------------------------------------- Getters and Setters end

        // -------------------------------------------------------------------- schedule start
        /// <summary>
        /// 新增Task，使用随机ID
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">任务</param>
        /// <returns>任务ID</returns>
        public string Schedule(string pattern, Task.Task task)
        {
            string id = Guid.NewGuid().ToString();
            Schedule(id, pattern, task);
            return id;
        }

        /// <summary>
        /// 新增Task，使用指定ID
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">任务</param>
        /// <returns>当前调度器</returns>
        public Scheduler Schedule(string id, string pattern, Task.Task task)
        {
            return Schedule(id, new CronPattern(pattern, config.IsMatchSecond), task);
        }

        /// <summary>
        /// 新增Task，使用指定ID
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">任务</param>
        /// <returns>当前调度器</returns>
        public Scheduler Schedule(string id, CronPattern pattern, Task.Task task)
        {
            lock (syncRoot)
            {
                taskTable.Add(id, pattern, task);
            }
            return this;
        }

        /// <summary>
        /// 新增Task，使用随机ID
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="action">任务委托</param>
        /// <returns>任务ID</returns>
        public string Schedule(string pattern, Action action)
        {
            return Schedule(pattern, new RunnableTask(action));
        }

        /// <summary>
        /// 新增Task，使用指定ID
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="action">任务委托</param>
        /// <returns>当前调度器</returns>
        public Scheduler Schedule(string id, string pattern, Action action)
        {
            return Schedule(id, pattern, new RunnableTask(action));
        }

        /// <summary>
        /// 移除Task
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>当前调度器</returns>
        public Scheduler Deschedule(string id)
        {
            DescheduleWithStatus(id);
            return this;
        }

        /// <summary>
        /// 移除Task，并返回是否移除成功
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否移除成功</returns>
        public bool DescheduleWithStatus(string id)
        {
            lock (syncRoot)
            {
                return taskTable.Remove(id);
            }
        }

        /// <summary>
        /// 更新Task的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">新的Cron表达式</param>
        /// <returns>当前调度器</returns>
        public Scheduler UpdatePattern(string id, string pattern)
        {
            return UpdatePattern(id, new CronPattern(pattern, config.IsMatchSecond));
        }

        /// <summary>
        /// 更新Task的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">新的Cron表达式</param>
        /// <returns>当前调度器</returns>
        public Scheduler UpdatePattern(string id, CronPattern pattern)
        {
            lock (syncRoot)
            {
                taskTable.UpdatePattern(id, pattern);
            }
            return this;
        }

        /// <summary>
        /// 获取指定ID的Task
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>任务</returns>
        public Task.Task GetTask(string id)
        {
            return taskTable.GetTask(id);
        }

        /// <summary>
        /// 获取指定ID的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>Cron表达式</returns>
        public CronPattern GetPattern(string id)
        {
            return taskTable.GetPattern(id);
        }

        /// <summary>
        /// 是否无任务
        /// </summary>
        /// <returns>是否无任务</returns>
        public bool IsEmpty()
        {
            return taskTable.IsEmpty();
        }

        /// <summary>
        /// 当前任务数
        /// </summary>
        /// <returns>当前任务数</returns>
        public int Size()
        {
            return taskTable.Size();
        }

        /// <summary>
        /// 清空任务表
        /// </summary>
        /// <returns>当前调度器</returns>
        public Scheduler Clear()
        {
            lock (syncRoot)
            {
                taskTable = new TaskTable();
            }
            return this;
        }

        /// <summary>
        /// 移除Task（与Deschedule方法相同，为了兼容测试代码）
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否移除成功</returns>
        public bool Unschedule(string id)
        {
            return DescheduleWithStatus(id);
        }

        /// <summary>
        /// 获取任务数量（与Size方法相同，为了兼容测试代码）
        /// </summary>
        /// <returns>任务数量</returns>
        public int GetTaskCount()
        {
            return Size();
        }
        // -------------------------------------------------------------------- schedule end

        /// <summary>
        /// 是否已经启动
        /// </summary>
        /// <returns>是否已经启动</returns>
        public bool IsStarted()
        {
            return started;
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="daemon">是否为守护线程</param>
        /// <returns>当前调度器</returns>
        public Scheduler Start(bool daemon)
        {
            this.daemon = daemon;
            return Start();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <returns>当前调度器</returns>
        public Scheduler Start()
        {
            lock (syncRoot)
            {
                CheckStarted();

                // 初始化线程池
                taskFactory = new TaskFactory();

                // 初始化任务执行器管理器
                taskExecutorManager = new TaskExecutorManager(this);

                // 启动任务启动器管理器
                taskLauncherManager = new TaskLauncherManager(this);
                taskLauncherManager.Start();

                // 启动定时器
                timer = new CronTimer(this);
                timer.SetDaemon(daemon);
                timer.Start();

                started = true;
            }
            return this;
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="clearTasks">是否清除所有任务</param>
        /// <returns>当前调度器</returns>
        public Scheduler Stop(bool clearTasks = false)
        {
            lock (syncRoot)
            {
                if (!started)
                {
                    throw new CronException("Scheduler not started");
                }

                // 停止定时器
                timer.StopTimer();
                timer = null;

                // 停止任务启动器管理器
                taskLauncherManager.Stop();
                taskLauncherManager = null;

                // 可选是否清空任务表
                if (clearTasks)
                {
                    Clear();
                }

                started = false;
            }
            return this;
        }

        /// <summary>
        /// 检查是否已经启动
        /// </summary>
        private void CheckStarted()
        {
            if (started)
            {
                throw new CronException("Scheduler already started");
            }
        }
    }
}
