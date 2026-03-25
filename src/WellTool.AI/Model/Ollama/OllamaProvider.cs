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

namespace WellTool.AI.Model.Ollama
{
    /// <summary>
    /// Ollama服务提供者
    /// </summary>
    public class OllamaProvider : AIServiceProvider
    {
        /// <summary>
        /// 获取服务名称
        /// </summary>
        /// <returns>服务名称</returns>
        public string GetServiceName()
        {
            return "Ollama";
        }

        /// <summary>
        /// 创建AI服务实例
        /// </summary>
        /// <param name="config">AI配置</param>
        /// <returns>AI服务实例</returns>
        public AIService Create(AIConfig config)
        {
            return new OllamaServiceImpl(config);
        }
    }
}
