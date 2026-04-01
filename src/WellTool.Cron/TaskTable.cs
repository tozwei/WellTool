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
using WellTool.Cron.Pattern;
using WellTool.Cron.Task;

namespace WellTool.Cron
{
    /// <summary>
    /// 定时任务表
    /// </summary>
    public class TaskTable
    {
        /// <summary>
        /// 任务映射
        /// </summary>
        private readonly Dictionary<string, CronTask> tasks = new Dictionary<string, CronTask>();

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">Cron表达式</param>
        /// <param name="task">任务</param>
        /// <returns>是否添加成功</returns>
        public bool Add(string id, CronPattern pattern, Task.Task task)
        {
            if (tasks.ContainsKey(id))
            {
                throw new CronException("Task with id '{}' already exists", id);
            }

            tasks[id] = new CronTask(id, pattern, task);
            return true;
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否移除成功</returns>
        public bool Remove(string id)
        {
            return tasks.Remove(id);
        }

        /// <summary>
        /// 更新任务的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="pattern">新的Cron表达式</param>
        public void UpdatePattern(string id, CronPattern pattern)
        {
            if (tasks.TryGetValue(id, out var cronTask))
            {
                cronTask.Pattern = pattern;
            }
            else
            {
                throw new CronException("Task with id '{}' not found", id);
            }
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>任务</returns>
        public Task.Task GetTask(string id)
        {
            if (tasks.TryGetValue(id, out var cronTask))
            {
                return cronTask.Task;
            }
            return null;
        }

        /// <summary>
        /// 获取任务的Cron表达式
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>Cron表达式</returns>
        public CronPattern GetPattern(string id)
        {
            if (tasks.TryGetValue(id, out var cronTask))
            {
                return cronTask.Pattern;
            }
            return null;
        }

        /// <summary>
        /// 获取所有任务
        /// </summary>
        /// <returns>任务列表</returns>
        public List<CronTask> GetAllTasks()
        {
            return new List<CronTask>(tasks.Values);
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns>是否为空</returns>
        public bool IsEmpty()
        {
            return tasks.Count == 0;
        }

        /// <summary>
        /// 获取任务数量
        /// </summary>
        /// <returns>任务数量</returns>
        public int Size()
        {
            return tasks.Count;
        }

        /// <summary>
        /// 获取任务数量（与Size方法相同，为了兼容测试代码）
        /// </summary>
        /// <returns>任务数量</returns>
        public int Count
        {
            get { return tasks.Count; }
        }

        /// <summary>
        /// 获取任务（与GetTask方法相同，为了兼容测试代码）
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>任务</returns>
        public Task.Task Get(string id)
        {
            return GetTask(id);
        }

        /// <summary>
        /// 检查任务是否存在
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <returns>是否存在</returns>
        public bool Contains(string id)
        {
            return tasks.ContainsKey(id);
        }

        /// <summary>
        /// 清空任务表
        /// </summary>
        public void Clear()
        {
            tasks.Clear();
        }

        /// <summary>
        /// 获取所有任务（与GetAllTasks方法相同，为了兼容测试代码）
        /// </summary>
        /// <returns>任务列表</returns>
        public List<CronTask> GetAll()
        {
            return GetAllTasks();
        }
    }
}