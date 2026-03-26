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
using WellTool.Cron.Pattern;
using WellTool.Cron.Task;
using WellTool.Cron.Listener;

namespace WellTool.Cron
{
    /// <summary>
    /// 定时任务工具类
    /// </summary>
    public class CronUtil
    {
        /// <summary>
        /// 默认调度器
        /// </summary>
        private static Scheduler defaultScheduler = new Scheduler();

        /// <summary>
        /// 获取默认调度器
        /// </summary>
        /// <returns>默认调度器</returns>
        public static Scheduler GetDefaultScheduler()
        {
            return defaultScheduler;
        }

        /// <summary>
        /// 设置默认调度器
        /// </summary>
        /// <param name="scheduler">调度器</param>
        public static void SetDefaultScheduler(Scheduler scheduler)
        {
            defaultScheduler = scheduler;
        }

        /// <summary>
        /// 新增Task，使用随机ID
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">任务</param>
        /// <returns>任务ID</returns>
        public static string Schedule(string pattern, Task.Task task)
        {
            return defaultScheduler.Schedule(pattern, task);
        }

        /// <summary>
        /// 新增Task，使用指定ID
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">任务</param>
        /// <returns>当前工具类</returns>
        public static CronUtil Schedule(string id, string pattern, Task.Task task)
        {
            defaultScheduler.Schedule(id, pattern, task);
            return new CronUtil();
        }

        /// <summary>
        /// 新增Task，使用随机ID
        /// </summary>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="action">任务委托</param>
        /// <returns>任务ID</returns>
        public static string Schedule(string pattern, Action action)
        {
            return defaultScheduler.Schedule(pattern, action);
        }

        /// <summary>
        /// 新增Task，使用指定ID
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="action">任务委托</param>
        /// <returns>当前工具类</returns>
        public static CronUtil Schedule(string id, string pattern, Action action)
        {
            defaultScheduler.Schedule(id, pattern, action);
            return new CronUtil();
        }

        /// <summary>
        /// 移除Task
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>当前工具类</returns>
        public static CronUtil Deschedule(string id)
        {
            defaultScheduler.Deschedule(id);
            return new CronUtil();
        }

        /// <summary>
        /// 移除Task，并返回是否移除成功
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否移除成功</returns>
        public static bool DescheduleWithStatus(string id)
        {
            return defaultScheduler.DescheduleWithStatus(id);
        }

        /// <summary>
        /// 更新Task的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">新的Cron表达式</param>
        /// <returns>当前工具类</returns>
        public static CronUtil UpdatePattern(string id, string pattern)
        {
            defaultScheduler.UpdatePattern(id, pattern);
            return new CronUtil();
        }

        /// <summary>
        /// 获取指定ID的Task
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>任务</returns>
        public static Task.Task GetTask(string id)
        {
            return defaultScheduler.GetTask(id);
        }

        /// <summary>
        /// 获取指定ID的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>Cron表达式</returns>
        public static CronPattern GetPattern(string id)
        {
            return defaultScheduler.GetPattern(id);
        }

        /// <summary>
        /// 是否无任务
        /// </summary>
        /// <returns>是否无任务</returns>
        public static bool IsEmpty()
        {
            return defaultScheduler.IsEmpty();
        }

        /// <summary>
        /// 当前任务数
        /// </summary>
        /// <returns>当前任务数</returns>
        public static int Size()
        {
            return defaultScheduler.Size();
        }

        /// <summary>
        /// 清空任务表
        /// </summary>
        /// <returns>当前工具类</returns>
        public static CronUtil Clear()
        {
            defaultScheduler.Clear();
            return new CronUtil();
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="daemon">是否为守护线程</param>
        /// <returns>当前工具类</returns>
        public static CronUtil Start(bool daemon = false)
        {
            defaultScheduler.Start(daemon);
            return new CronUtil();
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="clearTasks">是否清除所有任务</param>
        /// <returns>当前工具类</returns>
        public static CronUtil Stop(bool clearTasks = false)
        {
            defaultScheduler.Stop(clearTasks);
            return new CronUtil();
        }

        /// <summary>
        /// 是否已经启动
        /// </summary>
        /// <returns>是否已经启动</returns>
        public static bool IsStarted()
        {
            return defaultScheduler.IsStarted();
        }

        /// <summary>
        /// 设置时区
        /// </summary>
        /// <param name="timeZone">时区</param>
        /// <returns>当前工具类</returns>
        public static CronUtil SetTimeZone(TimeZoneInfo timeZone)
        {
            defaultScheduler.SetTimeZone(timeZone);
            return new CronUtil();
        }

        /// <summary>
        /// 获得时区
        /// </summary>
        /// <returns>时区</returns>
        public static TimeZoneInfo GetTimeZone()
        {
            return defaultScheduler.GetTimeZone();
        }

        /// <summary>
        /// 设置是否为守护线程
        /// </summary>
        /// <param name="daemon">是否为守护线程</param>
        /// <returns>当前工具类</returns>
        public static CronUtil SetDaemon(bool daemon)
        {
            defaultScheduler.SetDaemon(daemon);
            return new CronUtil();
        }

        /// <summary>
        /// 是否为守护线程
        /// </summary>
        /// <returns>是否为守护线程</returns>
        public static bool IsDaemon()
        {
            return defaultScheduler.IsDaemon();
        }

        /// <summary>
        /// 是否支持秒匹配
        /// </summary>
        /// <returns>是否支持秒匹配</returns>
        public static bool IsMatchSecond()
        {
            return defaultScheduler.IsMatchSecond();
        }

        /// <summary>
        /// 设置是否支持秒匹配
        /// </summary>
        /// <param name="matchSecond">是否支持秒匹配</param>
        /// <returns>当前工具类</returns>
        public static CronUtil SetMatchSecond(bool matchSecond)
        {
            defaultScheduler.SetMatchSecond(matchSecond);
            return new CronUtil();
        }

        /// <summary>
        /// 增加监听器
        /// </summary>
        /// <param name="listener">监听器</param>
        /// <returns>当前工具类</returns>
        public static CronUtil AddListener(TaskListener listener)
        {
            defaultScheduler.AddListener(listener);
            return new CronUtil();
        }

        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="listener">监听器</param>
        /// <returns>当前工具类</returns>
        public static CronUtil RemoveListener(TaskListener listener)
        {
            defaultScheduler.RemoveListener(listener);
            return new CronUtil();
        }
    }
}
