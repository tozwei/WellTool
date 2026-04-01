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

namespace WellTool.Cron.TimingWheel
{
    /// <summary>
    /// 时间轮
    /// </summary>
    public class TimingWheel
    {
        /// <summary>
        /// 时间轮的槽位数
        /// </summary>
        private readonly int wheelSize;

        /// <summary>
        /// 每个槽位的时间跨度（毫秒）
        /// </summary>
        private readonly long tickMs;

        /// <summary>
        /// 时间轮的总时间跨度（毫秒）
        /// </summary>
        private readonly long interval;

        /// <summary>
        /// 当前时间轮的当前时间（毫秒时间戳）
        /// </summary>
        private long currentTime;

        /// <summary>
        /// 时间轮的槽位
        /// </summary>
        private readonly TimerTaskList[] buckets;

        /// <summary>
        /// 上层时间轮
        /// </summary>
        private TimingWheel overflowWheel;

        /// <summary>
        /// 任务映射，用于快速查找和取消任务
        /// </summary>
        private readonly Dictionary<string, TimerTask> taskMap = new Dictionary<string, TimerTask>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tickMs">每个槽位的时间跨度（毫秒）</param>
        /// <param name="wheelSize">时间轮的槽位数</param>
        /// <param name="startMs">起始时间（毫秒时间戳）</param>
        public TimingWheel(long tickMs, int wheelSize, long startMs)
        {
            this.tickMs = tickMs;
            this.wheelSize = wheelSize;
            this.interval = tickMs * wheelSize;
            this.currentTime = startMs - (startMs % tickMs); // 向下取整到最近的tickMs
            this.buckets = new TimerTaskList[wheelSize];

            for (int i = 0; i < wheelSize; i++)
            {
                buckets[i] = new TimerTaskList(currentTime + i * tickMs);
            }
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task">任务</param>
        /// <returns>是否添加成功</returns>
        public bool AddTask(TimerTask task)
        {
            long expiration = task.ExecutionTime;

            // 如果任务已过期，直接执行
            if (expiration < currentTime + tickMs)
            {
                return false;
            }

            // 如果任务在当前时间轮的范围内
            if (expiration < currentTime + interval)
            {
                // 计算任务应该放在哪个槽位
                long virtualId = expiration / tickMs;
                int index = (int)(virtualId % wheelSize);
                var bucket = buckets[index];
                bucket.AddTask(task);
                taskMap[task.Id] = task;

                // 更新槽位的过期时间
                if (bucket.Expiration != virtualId * tickMs)
                {
                    bucket.Expiration = virtualId * tickMs;
                }

                return true;
            }
            else
            {
                // 任务超出当前时间轮的范围，添加到上层时间轮
                if (overflowWheel == null)
                {
                    overflowWheel = new TimingWheel(interval, wheelSize, currentTime);
                }
                return overflowWheel.AddTask(task);
            }
        }

        /// <summary>
        /// 移除任务
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <returns>是否移除成功</returns>
        public bool RemoveTask(string taskId)
        {
            if (taskMap.TryGetValue(taskId, out var task))
            {
                // 从任务映射中移除
                taskMap.Remove(taskId);

                // 从对应的槽位中移除
                long virtualId = task.ExecutionTime / tickMs;
                int index = (int)(virtualId % wheelSize);
                var bucket = buckets[index];
                return bucket.RemoveTask(task);
            }

            // 如果当前时间轮中没有，尝试从上层时间轮中移除
            if (overflowWheel != null)
            {
                return overflowWheel.RemoveTask(taskId);
            }

            return false;
        }

        /// <summary>
        /// 推进时间轮
        /// </summary>
        /// <param name="timeMs">当前时间（毫秒时间戳）</param>
        public void AdvanceClock(long timeMs)
        {
            if (timeMs < currentTime + tickMs)
            {
                return;
            }

            // 计算需要推进的槽位数
            long oldTime = currentTime;
            currentTime = timeMs - (timeMs % tickMs);

            // 推进上层时间轮
            if (overflowWheel != null)
            {
                overflowWheel.AdvanceClock(currentTime);
            }

            // 处理所有过期的槽位
            long oldVirtualId = oldTime / tickMs;
            long newVirtualId = currentTime / tickMs;
            int steps = (int)(newVirtualId - oldVirtualId);

            for (int i = 0; i < steps; i++)
            {
                int index = (int)((oldVirtualId + i) % wheelSize);
                var bucket = buckets[index];
                bucket.Expiration = (oldVirtualId + i + 1) * tickMs;

                // 执行槽位中的所有任务
                var tasks = bucket.GetTasks();
                foreach (var task in tasks)
                {
                    task.Execute();
                    taskMap.Remove(task.Id);
                }
                bucket.Clear();
            }
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>当前时间（毫秒时间戳）</returns>
        public long GetCurrentTime()
        {
            return currentTime;
        }
    }
}