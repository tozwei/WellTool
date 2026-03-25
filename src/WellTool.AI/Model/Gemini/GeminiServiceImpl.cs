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

namespace WellTool.AI.Model.Gemini
{
    /// <summary>
    /// Gemini服务实现类
    /// </summary>
    public class GeminiServiceImpl : BaseAIService, GeminiService
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="config">AI配置</param>
        public GeminiServiceImpl(AIConfig config) : base(config) { }

        /// <summary>
        /// 对话
        /// </summary>
        /// <param name="prompt">user题词</param>
        /// <returns>AI回答</returns>
        public string Chat(string prompt)
        {
            var messages = new List<Message>
            {
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
        /// 生成内容
        /// </summary>
        /// <param name="prompt">提示词</param>
        /// <returns>生成的内容</returns>
        public string Generate(string prompt)
        {
            return GenerateAsync(prompt).GetAwaiter().GetResult();
        }

        /// <summary>
        /// 生成内容-SSE流式输出
        /// </summary>
        /// <param name="prompt">提示词</param>
        /// <param name="callback">流式数据回调函数</param>
        public void Generate(string prompt, System.Action<string> callback)
        {
            GenerateStreamAsync(prompt, callback).GetAwaiter().GetResult();
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
        /// 异步对话
        /// </summary>
        /// <param name="messages">消息列表</param>
        /// <returns>AI回答</returns>
        private async Task<string> ChatAsync(List<Message> messages)
        {
            var requestBody = new
            {
                contents = messages.Select(m => new { role = m.Role, parts = new[] { new { text = m.Content } } }).ToList(),
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await SendPostAsync($"/models/{config.GetModel()}:generateContent?key={config.GetApiKey()}", json);
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
                contents = messages.Select(m => new { role = m.Role, parts = new[] { new { text = m.Content } } }).ToList(),
                temperature = 0.7,
                stream = true
            };

            var json = JsonSerializer.Serialize(requestBody);
            await SendPostStreamAsync($"/models/{config.GetModel()}:streamGenerateContent?key={config.GetApiKey()}", json, callback);
        }

        /// <summary>
        /// 异步生成内容
        /// </summary>
        /// <param name="prompt">提示词</param>
        /// <returns>生成的内容</returns>
        private async Task<string> GenerateAsync(string prompt)
        {
            var requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);
            var response = await SendPostAsync($"/models/{config.GetModel()}:generateContent?key={config.GetApiKey()}", json);
            return response;
        }

        /// <summary>
        /// 异步生成内容-SSE流式输出
        /// </summary>
        /// <param name="prompt">提示词</param>
        /// <param name="callback">回调函数</param>
        private async Task GenerateStreamAsync(string prompt, System.Action<string> callback)
        {
            var requestBody = new
            {
                contents = new[] { new { parts = new[] { new { text = prompt } } } },
                temperature = 0.7,
                stream = true
            };

            var json = JsonSerializer.Serialize(requestBody);
            await SendPostStreamAsync($"/models/{config.GetModel()}:streamGenerateContent?key={config.GetApiKey()}", json, callback);
        }

        /// <summary>
        /// 异步获取模型列表
        /// </summary>
        /// <returns>model列表</returns>
        private async Task<string> ModelsAsync()
        {
            return await SendGetAsync($"/models?key={config.GetApiKey()}");
        }
    }
}
