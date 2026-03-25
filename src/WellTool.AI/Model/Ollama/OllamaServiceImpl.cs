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

namespace WellTool.AI.Model.Ollama
{
    /// <summary>
    /// Ollama服务实现类
    /// </summary>
    public class OllamaServiceImpl : BaseAIService, OllamaService
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">AI配置</param>
        public OllamaServiceImpl(AIConfig config) : base(config) { }

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
        /// 拉取模型
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns>拉取结果</returns>
        public string PullModel(string model)
        {
            return PullModelAsync(model).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns>删除结果</returns>
        public string DeleteModel(string model)
        {
            return DeleteModelAsync(model).GetAwaiter().GetResult();
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
            var response = await SendPostAsync("/api/chat", json);
            return response;
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
            await SendPostStreamAsync("/api/chat", json, callback);
        }

        /// <summary>
        /// 异步获取模型列表
        /// </summary>
        /// <returns>model列表</returns>
        private async Task<string> ModelsAsync()
        {
            return await SendGetAsync("/api/tags");
        }

        /// <summary>
        /// 异步拉取模型
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns>拉取结果</returns>
        private async Task<string> PullModelAsync(string model)
        {
            var requestBody = new
            {
                name = model
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await SendPostAsync("/api/pull", json);
            return response;
        }

        /// <summary>
        /// 异步删除模型
        /// </summary>
        /// <param name="model">模型名称</param>
        /// <returns>删除结果</returns>
        private async Task<string> DeleteModelAsync(string model)
        {
            var requestBody = new
            {
                name = model
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await SendPostAsync("/api/delete", json);
            return response;
        }
    }
}
