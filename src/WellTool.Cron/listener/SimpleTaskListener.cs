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

namespace WellTool.Cron.Listener
{
    /// <summary>
    /// 简单任务监听器
    /// </summary>
    public class SimpleTaskListener : TaskListener
    {
        /// <summary>
        /// 任务开始回调
        /// </summary>
        private readonly Action<string> onTaskStart;

        /// <summary>
        /// 任务成功回调
        /// </summary>
        private readonly Action<string> onTaskSuccess;

        /// <summary>
        /// 任务失败回调
        /// </summary>
        private readonly Action<string, Exception> onTaskFailure;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onTaskStart">任务开始回调</param>
        /// <param name="onTaskSuccess">任务成功回调</param>
        /// <param name="onTaskFailure">任务失败回调</param>
        public SimpleTaskListener(Action<string> onTaskStart = null, Action<string> onTaskSuccess = null, Action<string, Exception> onTaskFailure = null)
        {
            this.onTaskStart = onTaskStart;
            this.onTaskSuccess = onTaskSuccess;
            this.onTaskFailure = onTaskFailure;
        }

        /// <summary>
        /// 任务开始时调用
        /// </summary>
        /// <param name="taskId">任务ID</param>
        public void OnTaskStart(string taskId)
        {
            onTaskStart?.Invoke(taskId);
        }

        /// <summary>
        /// 任务成功完成时调用
        /// </summary>
        /// <param name="taskId">任务ID</param>
        public void OnTaskSuccess(string taskId)
        {
            onTaskSuccess?.Invoke(taskId);
        }

        /// <summary>
        /// 任务失败时调用
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="exception">异常信息</param>
        public void OnTaskFailure(string taskId, Exception exception)
        {
            onTaskFailure?.Invoke(taskId, exception);
        }
    }
}