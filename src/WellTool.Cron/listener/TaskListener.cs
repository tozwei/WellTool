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

namespace WellTool.Cron.Listener
{
    /// <summary>
    /// 任务监听器接口
    /// </summary>
    public interface TaskListener
    {
        /// <summary>
        /// 任务开始时调用
        /// </summary>
        /// <param name="taskId">任务ID</param>
        void OnTaskStart(string taskId);

        /// <summary>
        /// 任务成功完成时调用
        /// </summary>
        /// <param name="taskId">任务ID</param>
        void OnTaskSuccess(string taskId);

        /// <summary>
        /// 任务失败时调用
        /// </summary>
        /// <param name="taskId">任务ID</param>
        /// <param name="exception">异常信息</param>
        void OnTaskFailure(string taskId, Exception exception);
    }
}