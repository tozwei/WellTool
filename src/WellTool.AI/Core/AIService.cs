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

namespace WellTool.AI.Core
{
    /// <summary>
    /// 模型公共的API功能，特有的功能在model.xx.XXService下定义
    /// </summary>
    public interface AIService
    {
        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="prompt">user题词</param>
        /// <returns>AI回答</returns>
        string Chat(string prompt);

        /// <summary>
        /// 对话-SSE流式输出
        /// </summary>
        /// <param name="prompt">user题词</param>
        /// <param name="callback">流式数据回调函数</param>
        void Chat(string prompt, System.Action<string> callback);

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="messages">由目前为止的对话组成的消息列表，可以设置role，content。详细参考官方文档</param>
        /// <returns>AI回答</returns>
        string Chat(List<Message> messages);

        /// <summary>
        /// 对话-SSE流式输出
        /// </summary>
        /// <param name="messages">由目前为止的对话组成的消息列表，可以设置role，content。详细参考官方文档</param>
        /// <param name="callback">流式数据回调函数</param>
        void Chat(List<Message> messages, System.Action<string> callback);
    }

    /// <summary>
    /// AIService扩展方法
    /// </summary>
    public static class AIServiceExtensions
    {
        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="service">AI服务</param>
        /// <param name="prompt">user题词</param>
        /// <returns>AI回答</returns>
        public static string Chat(this AIService service, string prompt)
        {
            var messages = new List<Message>
            {
                new Message("system", "You are a helpful assistant"),
                new Message("user", prompt)
            };
            return service.Chat(messages);
        }

        /// <summary>
        /// 对话-SSE流式输出
        /// </summary>
        /// <param name="service">AI服务</param>
        /// <param name="prompt">user题词</param>
        /// <param name="callback">流式数据回调函数</param>
        public static void Chat(this AIService service, string prompt, System.Action<string> callback)
        {
            var messages = new List<Message>
            {
                new Message("system", "You are a helpful assistant"),
                new Message("user", prompt)
            };
            service.Chat(messages, callback);
        }
    }
}
