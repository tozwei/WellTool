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

namespace WellTool.Cron.TimingWheel
{
    /// <summary>
    /// 时间轮中的定时任务
    /// </summary>
    public class TimerTask
    {
        /// <summary>
        /// 任务ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 任务执行时间（毫秒时间戳）
        /// </summary>
        public long ExecutionTime { get; set; }

        /// <summary>
        /// 任务回调
        /// </summary>
        public Action<string> Callback { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">任务ID</param>
        /// <param name="executionTime">任务执行时间（毫秒时间戳）</param>
        /// <param name="callback">任务回调</param>
        public TimerTask(string id, long executionTime, Action<string> callback)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ExecutionTime = executionTime;
            Callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        public void Execute()
        {
            Callback(Id);
        }
    }
}