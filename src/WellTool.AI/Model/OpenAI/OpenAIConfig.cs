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

namespace WellTool.AI.Model.OpenAI
{
    /// <summary>
    /// OpenAI配置类
    /// </summary>
    public class OpenAIConfig : BaseConfig
    {
        /// <summary>
        /// 构造
        /// </summary>
        public OpenAIConfig()
        {
            SetApiUrl("https://api.openai.com/v1");
            SetModel("gpt-3.5-turbo");
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="apiKey">API密钥</param>
        public OpenAIConfig(string apiKey)
        {
            SetApiUrl("https://api.openai.com/v1");
            SetModel("gpt-3.5-turbo");
            SetApiKey(apiKey);
        }

        /// <summary>
        /// 获取模型名称
        /// </summary>
        /// <returns>模型名称</returns>
        public override string GetModelName()
        {
            return "OpenAI";
        }
    }
}
