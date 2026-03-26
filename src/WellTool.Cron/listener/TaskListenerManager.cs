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

using System.Collections.Generic;

namespace WellTool.Cron.Listener
{
    /// <summary>
    /// 任务监听器管理器
    /// </summary>
    public class TaskListenerManager
    {
        /// <summary>
        /// 监听器列表
        /// </summary>
        private readonly List<TaskListener> listeners = new List<TaskListener>();

        /// <summary>
        /// 添加监听器
        /// </summary>
        /// <param name="listener">监听器</param>
        public void AddListener(TaskListener listener)
        {
            if (listener != null && !listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        /// <summary>
        /// 移除监听器
        /// </summary>
        /// <param name="listener">监听器</param>
        public void RemoveListener(TaskListener listener)
        {
            listeners.Remove(listener);
        }

        /// <summary>
        /// 通知所有监听器任务开始
        /// </summary>
        /// <param name="taskId">任务ID</param>
        public void NotifyTaskStart(string taskId)
        {
            foreach (var listener in listeners)
            {
                try
                {
                    listener.OnTaskStart(taskId);
                }
                catch { /* 忽略监听器异常 */ }
            }
        }

        /// <summary>
        /// 通知所有监听器任务成功
        /// </summary>
        /// <param name="taskId">任务ID</param>
        public void NotifyTaskSuccess(string taskId)
        {
            foreach (var listener in listeners)
            {
                try
                {
                    listener.OnTaskSuccess(taskId);
                }
                catch { /* 忽略监听器异常 */ }
            }
        }

        /// <summary>
        /// 通知所有监听器任务失败
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="exception">异常信息</param>
        public void NotifyTaskFailure(string taskId, System.Exception exception)
        {
            foreach (var listener in listeners)
            {
                try
                {
                    listener.OnTaskFailure(taskId, exception);
                }
                catch { /* 忽略监听器异常 */ }
            }
        }
    }
}