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

namespace WellTool.AI.Model.Hutool
{
    /// <summary>
    /// Hutool配置类，初始化API接口地址，设置默认的模型
    /// </summary>
    public class HutoolConfig : BaseConfig
    {
        private const string API_URL = "https://api.hutool.cn/ai/api";
        private const string DEFAULT_MODEL = "hutool";

        /// <summary>
        /// 构造
        /// </summary>
        public HutoolConfig()
        {
            SetApiUrl(API_URL);
            SetModel(DEFAULT_MODEL);
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="apiKey">API密钥</param>
        public HutoolConfig(string apiKey)
        {
            SetApiUrl(API_URL);
            SetModel(DEFAULT_MODEL);
            SetApiKey(apiKey);
        }
    }
}
