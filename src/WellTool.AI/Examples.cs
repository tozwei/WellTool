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

using WellTool.AI.Core;

namespace WellTool.AI
{
    /// <summary>
    /// AI框架使用示例
    /// </summary>
    public class Examples
    {
        /// <summary>
        /// 示例：使用OpenAI服务
        /// </summary>
        public static void UseOpenAI()
        {
            // 创建配置
            var config = AIUtil.CreateConfigBuilder("OpenAI")
                .SetApiKey("your-api-key")
                .SetModel("gpt-3.5-turbo")
                .Build();

            // 获取服务
            var service = AIUtil.GetService(config);

            // 简单对话
            var response = service.Chat("Hello, how are you?");
            Console.WriteLine("AI Response: " + response);

            // 流式对话
            Console.WriteLine("Streaming response:");
            service.Chat("Tell me a short story", (chunk) =>
            {
                Console.Write(chunk);
            });
        }
    }
}
