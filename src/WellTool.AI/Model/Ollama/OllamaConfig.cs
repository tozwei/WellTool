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
    /// Ollama配置类，初始化API接口地址，设置默认的模型
    /// </summary>
    public class OllamaConfig : BaseConfig
    {
        private const string API_URL = "http://localhost:11434";
        private const string DEFAULT_MODEL = "qwen3:32b";

        /// <summary>
        /// 构造
        /// </summary>
        public OllamaConfig()
        {
            SetApiUrl(API_URL);
            SetModel(DEFAULT_MODEL);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="apiUrl">API地址</param>
        public OllamaConfig(string apiUrl)
        {
            SetApiUrl(apiUrl);
            SetModel(DEFAULT_MODEL);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="apiUrl">API地址</param>
        /// <param name="model">模型名称</param>
        public OllamaConfig(string apiUrl, string model)
        {
            SetApiUrl(apiUrl);
            SetModel(model);
        }
    }
}
