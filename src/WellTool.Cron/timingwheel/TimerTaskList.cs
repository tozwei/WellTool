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

namespace WellTool.Cron.TimingWheel
{
    /// <summary>
    /// 时间轮中的任务列表
    /// </summary>
    public class TimerTaskList
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        private readonly LinkedList<TimerTask> tasks = new LinkedList<TimerTask>();

        /// <summary>
        /// 过期时间（毫秒时间戳）
        /// </summary>
        public long Expiration { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expiration">过期时间（毫秒时间戳）</param>
        public TimerTaskList(long expiration)
        {
            Expiration = expiration;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task">任务</param>
        public void AddTask(TimerTask task)
        {
            tasks.AddLast(task);
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="task">任务</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveTask(TimerTask task)
        {
            return tasks.Remove(task);
        }

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns>任务列表</returns>
        public LinkedList<TimerTask> GetTasks()
        {
            return tasks;
        }

        /// <summary>
        /// 清空任务
        /// </summary>
        public void Clear()
        {
            tasks.Clear();
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns>是否为空</returns>
        public bool IsEmpty()
        {
            return tasks.Count == 0;
        }
    }
}