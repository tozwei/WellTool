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
using System.Text.Json;
using System.Threading.Tasks;
using WellTool.AI.Core;

namespace WellTool.AI.Model.OpenAI
{
    /// <summary>
    /// OpenAI服务类
    /// </summary>
    public class OpenAIService : BaseAIService, AIService
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">AI配置</param>
        public OpenAIService(AIConfig config) : base(config) { }

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="prompt">user题词</param>
        /// <returns>AI回答</returns>
        public string Chat(string prompt)
        {
            var messages = new List<Message>
            {
                new Message("system", "You are a helpful assistant"),
                new Message("user", prompt)
            };
            return Chat(messages);
        }

        /// <summary>
        /// 对话-SSE流式输出
        /// </summary>
        /// <param name="prompt">user题词</param>
        /// <param name="callback">流式数据回调函数</param>
        public void Chat(string prompt, System.Action<string> callback)
        {
            var messages = new List<Message>
            {
                new Message("system", "You are a helpful assistant"),
                new Message("user", prompt)
            };
            Chat(messages, callback);
        }

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="messages">消息列表</param>
        /// <returns>AI回答</returns>
        public string Chat(List<Message> messages)
        {
            return ChatAsync(messages).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 对话-SSE流式输出
        /// </summary>
        /// <param name="messages">消息列表</param>
        /// <param name="callback">回调函数</param>
        public void Chat(List<Message> messages, System.Action<string> callback)
        {
            ChatStreamAsync(messages, callback).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 列出所有模型列表
        /// </summary>
        /// <returns>model列表</returns>
        public string Models()
        {
            return ModelsAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 查询余额
        /// </summary>
        /// <returns>余额</returns>
        public string Balance()
        {
            return BalanceAsync().GetAwaiter().GetResult();
        }

        /// <summary>
        /// 异步对话
        /// </summary>
        /// <param name="messages">消息列表</param>
        /// <returns>AI回答</returns>
        private async Task<string> ChatAsync(List<Message> messages)
        {
            var requestBody = new
            {
                model = config.GetModel(),
                messages = messages.Select(m => new { role = m.Role, content = m.Content }).ToList(),
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await SendPostAsync("/chat/completions", json);
            
            var result = JsonSerializer.Deserialize<JsonElement>(response);
            return result.GetProperty("choices").EnumerateArray().First().GetProperty("message").GetProperty("content").GetString() ?? string.Empty;
        }

        /// <summary>
        /// 异步对话-SSE流式输出
        /// </summary>
        /// <param name="messages">消息列表</param>
        /// <param name="callback">回调函数</param>
        private async Task ChatStreamAsync(List<Message> messages, System.Action<string> callback)
        {
            var requestBody = new
            {
                model = config.GetModel(),
                messages = messages.Select(m => new { role = m.Role, content = m.Content }).ToList(),
                temperature = 0.7,
                stream = true
            };

            var json = JsonSerializer.Serialize(requestBody);
            await SendPostStreamAsync("/chat/completions", json, callback);
        }

        /// <summary>
        /// 异步获取模型列表
        /// </summary>
        /// <returns>model列表</returns>
        private async Task<string> ModelsAsync()
        {
            return await SendGetAsync("/models");
        }

        /// <summary>
        /// 异步查询余额
        /// </summary>
        /// <returns>余额</returns>
        private async Task<string> BalanceAsync()
        {
            return await SendGetAsync("/usage");
        }
    }
}
